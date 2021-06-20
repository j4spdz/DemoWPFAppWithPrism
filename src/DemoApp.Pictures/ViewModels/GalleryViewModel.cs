using AsyncAwaitBestPractices;
using DemoApp.Shared.Helper.Extensions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DemoApp.Gallery.ViewModels
{
    /// <summary>
    /// - Retrieve pictures from API or import from local files
    /// - Download picture
    /// </summary>
    public class GalleryViewModel : BindableBase, INavigationAware
    {
        private string _progressText = "Initializing...";
        public string ProgressText
        {
            get => _progressText;
            set => SetProperty(ref _progressText, value);
        }

        private float _progressValue;
        public float ProgressValue
        {
            get => _progressValue;
            set => SetProperty(ref _progressValue, value);
        }

        public bool HasImage => SampleImage != null;

        public ImageSource _sampleImage;
        public ImageSource SampleImage
        {
            get => _sampleImage;
            set
            {
                SetProperty(ref _sampleImage, value);
                RaisePropertyChanged(nameof(HasImage));
            }
        }

        private CancellationTokenSource Cts { get; set; }

        private ICommand _unloadViewCommand;
        public ICommand UnloadViewCommand => _unloadViewCommand ??
            (_unloadViewCommand = new DelegateCommand(UnloadView));

        private void UnloadView()
        {
            Cts.Cancel();
            Cts.Dispose();
        }

        #region INavigationAware

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            Cts = new CancellationTokenSource();
            LoadView(Cts.Token).SafeFireAndForget();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        #endregion INavigationAware

        private async Task LoadView(CancellationToken token)
        {
            var progress = new Progress<float>(value =>
            {
                ProgressValue = value*100;
                ProgressText = $"{value*100}%";
            });

            if(!HasImage)
            {
                await FetchImages(200, progress, token);

                if (!token.IsCancellationRequested)
                {
                    ProgressText = "Completed";
                }
            }
        }

        // Show a progress bar while retrieving images
        private async Task FetchImages(int count, IProgress<float> progress, CancellationToken token)
        {
            try
            {
                // Setting up the http client used to download the data
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromMinutes(5);
                    var downloadUrl = "https://images.pexels.com/photos/842711/pexels-photo-842711.jpeg?cs=srgb&dl=pexels-christian-heitz-842711.jpg&fm=jpg";

                    using (var ms = new MemoryStream())
                    {
                        await client.DownloadAsync(downloadUrl, ms, progress, token); 

                        //[BugFix] The image cannot be decoded. The image header might be corrupted
                        ms.Position = 0;

                        // Create a BitmapSource  
                        BitmapImage bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.CacheOption = BitmapCacheOption.OnLoad;
                        bitmap.StreamSource = ms;
                        bitmap.EndInit();

                        // Set Image.Source  
                        SampleImage = bitmap;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
