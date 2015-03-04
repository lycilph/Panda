using System.Windows;
using ReactiveUI;

namespace Panda.ApplicationCore.StatusBar.ViewModels
{
    public class StatusBarProgressItemViewModel : StatusBarItemBase
    {
        private bool _IsIndeterminate;
        public bool IsIndeterminate
        {
            get { return _IsIndeterminate; }
            set { this.RaiseAndSetIfChanged(ref _IsIndeterminate, value); }
        }

        private double _Progress;
        public double Progress
        {
            get { return _Progress; }
            set { this.RaiseAndSetIfChanged(ref _Progress, value); }
        }

        private bool _IsActive;
        public bool IsActive
        {
            get { return _IsActive; }
            set { this.RaiseAndSetIfChanged(ref _IsActive, value); }
        }

        public StatusBarProgressItemViewModel() : this(false, new GridLength(1, GridUnitType.Star)) { }
        public StatusBarProgressItemViewModel(bool is_indeterminate) : this(is_indeterminate, new GridLength(1, GridUnitType.Star)) { }
        public StatusBarProgressItemViewModel(GridLength width) : this(false, width) { }
        public StatusBarProgressItemViewModel(bool is_indeterminate, GridLength width) : base(width)
        {
            IsIndeterminate = is_indeterminate;
        }
    }
}
