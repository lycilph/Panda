using System.Collections;
using System.Collections.Generic;
using Caliburn.Micro;
using ReactiveUI;

namespace Panda.ApplicationCore.Menu.ViewModels
{
    public class MenuItemBase : ReactiveObject, IEnumerable<MenuItemBase>
    {
        public static MenuItemBase Separator
        {
            get { return new MenuItemSeparator(); }
        }

        public ReactiveList<MenuItemBase> Children { get; private set; }

        public virtual string Name
        {
            get { return "-"; }
        }

        public MenuItemBase()
        {
            Children = new ReactiveList<MenuItemBase>();
        }

        public void Add(params MenuItemBase[] items)
        {
            items.Apply(Children.Add);
        }

        public void Remove(MenuItemBase item)
        {
            Children.Remove(item);
        }

        public IEnumerator<MenuItemBase> GetEnumerator()
        {
            return Children.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
