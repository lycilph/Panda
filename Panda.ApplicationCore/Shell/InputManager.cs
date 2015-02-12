using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;
using Caliburn.Micro;

namespace Panda.ApplicationCore.Shell
{
    [Export(typeof(IInputManager))]
    public class InputManager : IInputManager
    {
        public void SetShortcut(DependencyObject view, InputGesture gesture, object handler)
        {
            var trigger = new InputBindingTrigger {InputBinding = new InputBinding(new RoutedCommand(), gesture)};

            Interaction.GetTriggers(view).Add(trigger);

            trigger.Actions.Add(new GestureTriggerAction(handler));
        }

        public void SetShortcut(InputGesture gesture, object handler)
        {
            if (Application.Current.MainWindow == null)
                Application.Current.Activated += (s, e) => SetShortcut(Application.Current.MainWindow, gesture, handler);
            else
                SetShortcut(Application.Current.MainWindow, gesture, handler);
        }

        private class GestureTriggerAction : TriggerAction<FrameworkElement>
        {
            private readonly object handler;

            public GestureTriggerAction(object handler)
            {
                this.handler = handler;
            }

            protected override void Invoke(object parameter)
            {
                Action.Invoke(handler, "Execute");
            }
        }
    }
}
