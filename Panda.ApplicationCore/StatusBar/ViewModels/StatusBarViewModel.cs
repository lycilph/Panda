using System.Collections.Generic;
using System.ComponentModel.Composition;
using ReactiveUI;

namespace Panda.ApplicationCore.StatusBar.ViewModels
{
    [Export(typeof(IStatusBar))]
    public class StatusBarViewModel : ReactiveList<StatusBarItemBase>, IStatusBar
    {
        public IEnumerable<StatusBarItemBase> All
        {
            get { return this; }
        }

        public void Add(params StatusBarItemBase[] items)
        {
            foreach (var item in items)
            {
                item.Index = Count;
                base.Add(item);
            }
        }
    }
}
