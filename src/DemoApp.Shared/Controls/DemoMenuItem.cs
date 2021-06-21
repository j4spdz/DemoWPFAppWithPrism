using MahApps.Metro.Controls;
using System.Windows;

namespace DemoApp.Shared.Controls
{
    public class DemoMenuItem : HamburgerMenuIconItem
    {
        public static readonly DependencyProperty NavigationPathProperty = DependencyProperty.Register(
            nameof(NavigationPath), typeof(string), typeof(DemoMenuItem), new PropertyMetadata(default(string)));

        public string NavigationPath
        {
            get => (string)GetValue(NavigationPathProperty);
            set => SetValue(NavigationPathProperty, value);
        }

        public bool IsNavigation => NavigationPath != null;
    }
}
