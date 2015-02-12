using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Caliburn.Micro;
using Panda.ApplicationCore.Shell;
using ReactiveUI;

namespace Panda.ApplicationCore.Menu.ViewModels
{
    public class MenuItem : MenuItemBase
    {
        private readonly System.Action action;
        private KeyGesture key_gesture;

        private string _Text;
        public string Text
        {
            get { return _Text; }
            set { this.RaiseAndSetIfChanged(ref _Text, value); }
        }

        public Image Icon { get; private set; }

        public string ActionText { get; private set; }
        
        public override string Name
        {
            get { return string.IsNullOrEmpty(Text) ? null : Text.Replace("_", string.Empty); }
        }

        public string InputGestureText
        {
            get { return key_gesture == null ? string.Empty : key_gesture.GetDisplayStringForCulture(CultureInfo.CurrentUICulture); }
        }

        private bool _CanExecute = true;
        public bool CanExecute
        {
            get { return _CanExecute; }
            set { this.RaiseAndSetIfChanged(ref _CanExecute, value); }
        }

        public MenuItem(string text)
        {
            _Text = text;
            ActionText = "Execute";
        }

        public MenuItem(string text, System.Action action) : this(text)
        {
            this.action = action;
        }

        public void Execute()
        {
            if (action != null)
                action();
        }

        public MenuItem WithGlobalShortcut(ModifierKeys modifier, Key key)
        {
            key_gesture = new KeyGesture(key, modifier);
            var input_manager = IoC.Get<IInputManager>();
            input_manager.SetShortcut(key_gesture, this);
            return this;
        }

        public MenuItem WithIcon(string uri)
        {
            Icon = new Image
            {
                Source = new BitmapImage(new Uri(uri, UriKind.Relative)),
                Width = 24,
                Height = 24
            };

            return this;
        }
    }
}
