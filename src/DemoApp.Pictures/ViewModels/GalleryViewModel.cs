using AsyncAwaitBestPractices.MVVM;
using DemoApp.Gallery.Services;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DemoApp.Gallery.ViewModels
{
    /// <summary>
    /// - Retrieve pictures from API or import from local files
    /// - Download picture
    /// - Show pictures in a gallery
    /// </summary>
    public class GalleryViewModel : BindableBase, INavigationAware
    {
        private readonly GalleryService _galleryService;

        public GalleryViewModel(IContainerProvider provider)
        {
            _galleryService = provider.Resolve<GalleryService>();
            ImagesView = CollectionViewSource.GetDefaultView(Images);
            ImagesView.Filter = o =>
            {
                // This condition must be true for the row to be visible on the grid.
                //return ((RowViewModel)o).IsVisible == true;
                return true;
            };
            ImagesLiveView = (ICollectionViewLiveShaping)ImagesView;
            if(ImagesLiveView.CanChangeLiveFiltering)
            {
                ImagesLiveView.IsLiveFiltering = true;
            }

            // For completeness. Changing these properties fires a change notification (although
            // we bypass this and manually call a bulk update using Refresh() for speed).
            //ImagesLiveView.LiveFilteringProperties.Add("IsVisible");
        }

        private ObservableCollection<ImageSource> Images { get; set; } = new ObservableCollection<ImageSource>();
        private ICollectionView ImagesView { get; set; }
        public ICollectionViewLiveShaping ImagesLiveView { get; set; }

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

        private bool _loadingImage;
        public bool LoadingImage
        {
            get => _loadingImage;
            set => SetProperty(ref _loadingImage, value);
        }

        private CancellationTokenSource Cts { get; set; }

        private ICommand _importImageFromFileCommand;
        public ICommand ImportImageFromFileCommand => _importImageFromFileCommand ??
            (_importImageFromFileCommand = new DelegateCommand(ImportImageFromFile));

        private void ImportImageFromFile()
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;...";
            if (openFileDialog.ShowDialog() == true)
            {
                Uri fileUri = new Uri(openFileDialog.FileName);
                var newImage = new BitmapImage(fileUri);
                Images.Add(newImage);
            }
        }

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

            var newImage = await _galleryService.GetImageFromURL(ImageUrl, progress, Cts.Token);
            Images.Add(newImage);
            ProgressText = "Completed";
            LoadingImage = false;
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
            Cts.Cancel();
            Cts.Dispose();
        }

        #endregion INavigationAware
    }
}
