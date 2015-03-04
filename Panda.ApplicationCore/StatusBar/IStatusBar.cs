using System.Collections.Generic;
using Panda.ApplicationCore.StatusBar.ViewModels;

namespace Panda.ApplicationCore.StatusBar
{
    public interface IStatusBar
    {
        IEnumerable<StatusBarItemBase> All { get; }

        void Add(params StatusBarItemBase[] items);
    }
}