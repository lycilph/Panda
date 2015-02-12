using System.Collections.Generic;

namespace Panda.ApplicationCore.Menu.ViewModels
{
    public interface IMenu : IEnumerable<MenuItemBase>
    {
        IEnumerable<MenuItemBase> All { get; }

        void Add(params MenuItemBase[] items);
    }
}
