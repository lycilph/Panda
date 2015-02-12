using System;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using MahApps.Metro.Controls.Dialogs;

namespace Panda.ApplicationCore.Dialogs
{
    public partial class HostDialog
    {
        private readonly TaskCompletionSource<MessageDialogResult> tcs = new TaskCompletionSource<MessageDialogResult>();

        public Task<MessageDialogResult> Task
        {
            get { return tcs.Task; }
        }

        public IScreen ViewModel
        {
            get { return (IScreen)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(IScreen), typeof(HostDialog), new PropertyMetadata(null));

        public DialogButtons Buttons
        {
            get { return (DialogButtons)GetValue(ButtonsProperty); }
            set { SetValue(ButtonsProperty, value); }
        }
        public static readonly DependencyProperty ButtonsProperty =
            DependencyProperty.Register("Buttons", typeof(DialogButtons), typeof(HostDialog), new PropertyMetadata(DialogButtons.OkAndCancel, ButtonsChanged));

        private static void ButtonsChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var dialog = obj as HostDialog;
            SetButtons(dialog);
        }

        public HostDialog()
        {
            InitializeComponent();
            DataContext = this;
        }

        private static void SetButtons(HostDialog dialog)
        {
            switch (dialog.Buttons)
            {
                case DialogButtons.None:
                    dialog.OkButton.Visibility = Visibility.Collapsed;
                    dialog.CancelButton.Visibility = Visibility.Collapsed;
                    break;
                case DialogButtons.Ok:
                    dialog.OkButton.Visibility = Visibility.Visible;
                    dialog.CancelButton.Visibility = Visibility.Collapsed;
                    break;
                case DialogButtons.OkAndCancel:
                    dialog.OkButton.Visibility = Visibility.Visible;
                    dialog.CancelButton.Visibility = Visibility.Visible;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OkClick(object sender, RoutedEventArgs e)
        {
            tcs.SetResult(MessageDialogResult.Affirmative);
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            tcs.SetResult(MessageDialogResult.Negative);
        }
    }
}
