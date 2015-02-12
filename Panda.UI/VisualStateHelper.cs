using System.Windows;

namespace Panda.UI
{
    public class VisualStateHelper
    {
        public static string GetState(DependencyObject obj)
        {
            return (string)obj.GetValue(StateProperty);
        }
        public static void SetState(DependencyObject obj, string value)
        {
            obj.SetValue(StateProperty, value);
        }
        public static readonly DependencyProperty StateProperty =
            DependencyProperty.RegisterAttached("State", typeof(string), typeof(VisualStateHelper), new PropertyMetadata(null, OnStateChanged));

        private static void OnStateChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            if (args.NewValue != null)
                VisualStateManager.GoToElementState((FrameworkElement)obj, (string)args.NewValue, true);
        }
    }
}
