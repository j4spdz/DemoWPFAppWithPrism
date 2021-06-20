using DemoApp.Gallery.Services;
using DemoApp.Shared.Models;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace DemoApp.Gallery
{
    public class GalleryModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion(RegionConstants.MainContentRegion, typeof(Views.GalleryView));
        }

        // register with the container that SomeService implements ISomeService
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            if (!containerRegistry.IsRegistered<GalleryService>())
            {
                containerRegistry.Register<GalleryService>();
            }
        }
    }
}
