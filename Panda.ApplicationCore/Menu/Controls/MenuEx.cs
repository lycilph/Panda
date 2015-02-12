using System.Windows;

namespace Panda.ApplicationCore.Menu.Controls
{
    public class MenuEx : System.Windows.Controls.Menu
    {
        private object current_item;

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            current_item = item;
            return base.IsItemItsOwnContainerOverride(item);
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return MenuItemEx.GetContainer(this, current_item);
        }
    }
}
