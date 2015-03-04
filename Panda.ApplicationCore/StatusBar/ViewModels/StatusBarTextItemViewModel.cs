using System.Windows;
using ReactiveUI;

namespace Panda.ApplicationCore.StatusBar.ViewModels
{
    public class StatusBarTextItemViewModel : StatusBarItemBase
    {
        private string _Message;
        public string Message
        {
            get { return _Message; }
            set { this.RaiseAndSetIfChanged(ref _Message, value); }
        }

        public StatusBarTextItemViewModel() : this(string.Empty, new GridLength(1, GridUnitType.Star)) { }
        public StatusBarTextItemViewModel(string message) : this(message, new GridLength(1, GridUnitType.Star)) { }
        public StatusBarTextItemViewModel(GridLength width) : this(string.Empty, width) { }
        public StatusBarTextItemViewModel(string message, GridLength width) : base(width)
        {
            Message = message;
        }
    }
}
