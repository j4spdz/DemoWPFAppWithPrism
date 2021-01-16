using DemoApp.Shared.Models;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Notes
{
    public class NotesModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion(RegionConstants.MainContentRegion, typeof(Views.NotesView));
        }

        // register with the container that SomeService implements ISomeService
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //throw new NotImplementedException();
        }
    }
}
