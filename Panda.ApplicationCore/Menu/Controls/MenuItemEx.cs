using System.Windows;
using System.Windows.Controls;
using Panda.ApplicationCore.Menu.ViewModels;

namespace Panda.ApplicationCore.Menu.Controls
{
    public class MenuItemEx : System.Windows.Controls.MenuItem
    {
        private object current_item;

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            current_item = item;
            return base.IsItemItsOwnContainerOverride(item);
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return GetContainer(this, current_item);
        }

        internal static DependencyObject GetContainer(FrameworkElement framework_element, object item)
        {
            if (item is MenuItemSeparator)
                return new Separator { Style = (Style)framework_element.FindResource(SeparatorStyleKey) };

            const string styleKey = "MenuItem";
            return new MenuItemEx { Style = (Style)framework_element.FindResource(styleKey) };
        }
    }
}
