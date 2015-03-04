using System.Collections.Generic;
using Panda.ApplicationCore.Menu.ViewModels;

namespace Panda.ApplicationCore.Menu
{
    public interface IMenu : IEnumerable<MenuItemBase>
    {
        IEnumerable<MenuItemBase> All { get; }

        void Add(params MenuItemBase[] items);
    }
}
