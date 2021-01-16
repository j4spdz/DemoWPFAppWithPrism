using DemoApp.Shared.Models;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace DemoApp.Core
{
    public class CoreModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion(RegionConstants.MainContentRegion, typeof(Views.HomeView));
        }

        // register with the container that SomeService implements ISomeService
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //throw new NotImplementedException();
        }
    }
}
