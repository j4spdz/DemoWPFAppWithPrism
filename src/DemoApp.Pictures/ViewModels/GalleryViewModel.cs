using AsyncAwaitBestPractices;
using AsyncAwaitBestPractices.MVVM;
using DemoApp.Gallery.Services;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace DemoApp.Gallery.ViewModels
{
    /// <summary>
    /// - Retrieve pictures from API or import from local files
    /// - Download picture
    /// </summary>
    public class GalleryViewModel : BindableBase, INavigationAware
    {
        private readonly GalleryService _galleryService;

        public GalleryViewModel(IContainerProvider provider)
        {
            _galleryService = provider.Resolve<GalleryService>();
        }

        private string _imageUrl = "https://images.pexels.com/photos/842711/pexels-photo-842711.jpeg?cs=srgb&dl=pexels-christian-heitz-842711.jpg&fm=jpg";
        public string ImageUrl
        {
            get => _imageUrl;
            set => SetProperty(ref _imageUrl, value);
        }

        private string _progressText = "Loading...";
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

        public ImageSource _displayImage;
        public ImageSource DisplayImage
        {
            get => _displayImage;
            set => SetProperty(ref _displayImage, value);
        }

        private bool _loadingImage;
        public bool LoadingImage
        {
            get => _loadingImage;
            set => SetProperty(ref _loadingImage, value);
        }

        private CancellationTokenSource Cts { get; set; }

        private ICommand _submitCommand;
        public ICommand SubmitCommand => _submitCommand ??
            (_submitCommand = new AsyncCommand(GetImage));

        private async Task GetImage()
        {
            LoadingImage = true;

            var progress = new Progress<float>(value =>
            {
                ProgressValue = value * 100;
                ProgressText = $"{value * 100}%";
            });
            DisplayImage = await _galleryService.GetImageFromURL(ImageUrl, progress, Cts.Token);

            ProgressText = "Completed";
            LoadingImage = false;
        }

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
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        #endregion INavigationAware
    }
}
