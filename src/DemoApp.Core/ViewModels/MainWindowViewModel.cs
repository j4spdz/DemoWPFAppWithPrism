using DemoApp.Shared.Controls;
using DemoApp.Shared.Models;
using MahApps.Metro.Controls;
using MahApps.Metro.IconPacks;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DemoApp.Core.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private static readonly ObservableCollection<DemoMenuItem> _appMenu = new ObservableCollection<DemoMenuItem>();

        private readonly IRegionManager _regionManager;

        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            BuildAppMenu();
        }

        public ObservableCollection<DemoMenuItem> AppMenu => _appMenu;

        private ICommand _navigateToPageCommand;
        public ICommand NavigateToPageCommand => _navigateToPageCommand ??
            (_navigateToPageCommand = new DelegateCommand<HamburgerMenuItemInvokedEventArgs>(NavigateToPage));

        private void NavigateToPage(HamburgerMenuItemInvokedEventArgs e)
        {
            if (!e.IsItemOptions && e.InvokedItem is DemoMenuItem menuItem)
            {
                _regionManager.RequestNavigate(RegionConstants.MainContentRegion, menuItem.NavigationPath);
            }
        }

        private void BuildAppMenu()
        {
            AppMenu.Add(new DemoMenuItem
            {
                Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.HomeSolid },
                Label = "Home",
                NavigationPath = NavigationConstants.HomeView
            });
            AppMenu.Add(new DemoMenuItem
            {
                Icon = new PackIconBoxIcons() { Kind = PackIconBoxIconsKind.RegularPhotoAlbum },
                Label = "Gallery",
                NavigationPath = NavigationConstants.GalleryView
            });
            AppMenu.Add(new DemoMenuItem
            {
                Icon = new PackIconBoxIcons() { Kind = PackIconBoxIconsKind.RegularNotepad },
                Label = "Notes",
                NavigationPath = NavigationConstants.NotesView
            });
            AppMenu.Add(new DemoMenuItem
            {
                Icon = new PackIconBoxIcons() { Kind = PackIconBoxIconsKind.RegularPaint },
                Label = "Paint",
                NavigationPath = NavigationConstants.PaintView
            });
        }
    }
}
