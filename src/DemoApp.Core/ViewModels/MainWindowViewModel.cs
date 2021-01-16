using DemoApp.Shared.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Core.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager _manager;

        public MainWindowViewModel(IRegionManager manager)
        {
            _manager = manager;

            WindowLoadedCommand = new DelegateCommand(WindowsLoaded);
            NavigateToPageCommand = new DelegateCommand<object>(NavigateToPage);
        }

        public DelegateCommand WindowLoadedCommand { get; private set; }
        public DelegateCommand<object> NavigateToPageCommand { get; private set; }

        private void WindowsLoaded()
        {
            _manager.RequestNavigate(RegionConstants.MainContentRegion, NavigationConstants.HomeView);
        }

        private void NavigateToPage(object parameter)
        {
            var navigatePath = parameter as string;
            _manager.RequestNavigate(RegionConstants.MainContentRegion, navigatePath);
        }
    }
}
