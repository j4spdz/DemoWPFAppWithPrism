using DemoApp.Core.Views;
using DemoApp.Notes;
using DemoApp.Paint;
using Prism.Ioc;
using Prism.Modularity;
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
    }
}
