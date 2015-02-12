using System.Collections.Generic;
using System.ComponentModel.Composition;
using Caliburn.Micro;
using ReactiveUI;

namespace Panda.ApplicationCore.Menu.ViewModels
{
    [Export(typeof(IMenu))]
    public class MenuViewModel : ReactiveList<MenuItemBase>, IMenu
    {
        public IEnumerable<MenuItemBase> All
        {
            get { return this; }
        }

        public void Add(params MenuItemBase[] items)
        {
            items.Apply(Add);
        }
    }
}
