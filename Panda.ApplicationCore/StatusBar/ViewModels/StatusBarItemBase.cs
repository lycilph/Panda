using System.Windows;
using ReactiveUI;

namespace Panda.ApplicationCore.StatusBar.ViewModels
{
    public class StatusBarItemBase : ReactiveObject
    {
        private int _Index;
        public int Index
        {
            get { return _Index; }
            set { this.RaiseAndSetIfChanged(ref _Index, value); }
        }

        private GridLength _Width;
        public GridLength Width
        {
            get { return _Width; }
            set { this.RaiseAndSetIfChanged(ref _Width, value); }
        }

        public StatusBarItemBase(GridLength width)
        {
            Width = width;
        }
    }
}
