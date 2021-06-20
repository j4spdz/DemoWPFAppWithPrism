using DemoApp.Shared.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Windows.Input;

namespace DemoApp.Core.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;

        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        private ICommand _windowLoadedCommand;
        public ICommand WindowLoadedCommand => _windowLoadedCommand ??
            (_windowLoadedCommand = new DelegateCommand(WindowsLoaded));

        private void WindowsLoaded()
        {
            _regionManager.RequestNavigate(RegionConstants.MainContentRegion, NavigationConstants.HomeView);
        }

        private ICommand _navigateToPageCommand;
        public ICommand NavigateToPageCommand => _navigateToPageCommand ??
            (_navigateToPageCommand = new DelegateCommand<string>(NavigateToPage));

        private void NavigateToPage(string navigatePath)
        {
            _regionManager.RequestNavigate(RegionConstants.MainContentRegion, navigatePath);
        }
    }
}
