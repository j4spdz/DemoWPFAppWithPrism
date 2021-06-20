using AsyncAwaitBestPractices;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DemoApp.Gallery.ViewModels
{
    /// <summary>
    /// - Retrieve pictures from API or import from local files
    /// - Download picture
    /// </summary>
    public class GalleryViewModel : BindableBase, INavigationAware
    {
        // Using asyncCommand inside constructor 
        public GalleryViewModel()
        {
        }

        private string _progressText = "Initializing...";
        public string ProgressText
        {
            get => _progressText;
            set => SetProperty(ref _progressText, value);
        }

        private int _progressValue;
        public int ProgressValue
        {
            get => _progressValue;
            set => SetProperty(ref _progressValue, value);
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

        // Show a progress bar while retrieving images
        private async Task LoadView(CancellationToken token)
        {
            var progress = new Progress<int>(value =>
            {
                ProgressValue = value;
                ProgressText = $"{value}%";
            });

            await FetchImages(200, progress, token);

            if (!token.IsCancellationRequested)
            {
                ProgressText = "Completed";
            }
        }

        private async Task FetchImages(int count, IProgress<int> progress, CancellationToken token)
        {
            for (int i = 0; i < count; i++)
            {
                if (token.IsCancellationRequested)
                {
                    Debug.WriteLine("Fetch Images stopped as cancellation has been requested");
                    break;
                }

                await Task.Delay(100);
                var percentComplete = i * 100 / count;
                progress.Report(percentComplete);
            }
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
    }
}
