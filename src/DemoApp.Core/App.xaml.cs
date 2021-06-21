using DemoApp.Core.Views;
using DemoApp.Gallery;
using DemoApp.Notes;
using DemoApp.Paint;
using DemoApp.Shared.RegionAdapters;
using MahApps.Metro.Controls;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DemoApp.Core
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        //This function is used to register any app dependencies
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //throw new NotImplementedException();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            Type coreModuleType = typeof(CoreModule);
            moduleCatalog.AddModule(new ModuleInfo()
            {
                ModuleName = coreModuleType.Name,
                ModuleType = coreModuleType.AssemblyQualifiedName,
            });

            Type galleryModuleType = typeof(GalleryModule);
            moduleCatalog.AddModule(new ModuleInfo()
            {
                ModuleName = galleryModuleType.Name,
                ModuleType = galleryModuleType.AssemblyQualifiedName,
            });

            Type notesModuleType = typeof(NotesModule);
            moduleCatalog.AddModule(new ModuleInfo()
            {
                ModuleName = notesModuleType.Name,
                ModuleType = notesModuleType.AssemblyQualifiedName,
            });

            Type paintModuleType = typeof(PaintModule);
            moduleCatalog.AddModule(new ModuleInfo()
            {
                ModuleName = paintModuleType.Name,
                ModuleType = paintModuleType.AssemblyQualifiedName,
            });

            base.ConfigureModuleCatalog(moduleCatalog);
        }

        protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings)
        {
            base.ConfigureRegionAdapterMappings(regionAdapterMappings);
            regionAdapterMappings.RegisterMapping(typeof(HamburgerMenu), Container.Resolve<HamburgerMenuRegionAdapter>());
        }
    }
}
