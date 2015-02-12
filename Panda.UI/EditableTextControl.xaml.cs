using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Panda.UI
{
    public partial class EditableTextControl
    {
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(EditableTextControl), new FrameworkPropertyMetadata(string.Empty, OnTextChangedCallback) { BindsTwoWayByDefault = true });

        private string EditedText
        {
            get { return (string)GetValue(EditedTextProperty); }
            set { SetValue(EditedTextProperty, value); }
        }
        public static readonly DependencyProperty EditedTextProperty =
            DependencyProperty.Register("EditedText", typeof(string), typeof(EditableTextControl), new PropertyMetadata(string.Empty));

        public bool IsEditing
        {
            get { return (bool)GetValue(IsEditingProperty); }
            set { SetValue(IsEditingProperty, value); }
        }
        public static readonly DependencyProperty IsEditingProperty =
            DependencyProperty.Register("IsEditing", typeof(bool), typeof(EditableTextControl), new FrameworkPropertyMetadata(false) { BindsTwoWayByDefault = true });

        public EditableTextControl()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        protected override void OnMouseDoubleClick(MouseButtonEventArgs e)
        {
            IsEditing = true;
            e.Handled = true;
        }

        protected override void OnPreviewKeyUp(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.F2:
                    IsEditing = true;
                    e.Handled = true;
                    break;
                case Key.Enter:
                    IsEditing = false;
                    e.Handled = true;
                    Text = EditedText;
                    break;
                case Key.Escape:
                    IsEditing = false;
                    e.Handled = true;
                    EditedText = Text;
                    break;
            }
        }

        public static void OnTextChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var con = d as EditableTextControl;
            if (con != null)
                con.EditedText = con.Text;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var textbox = (TextBox)Template.FindName("editable_text", this);
            textbox.IsVisibleChanged += (s, arg) =>
            {
                Keyboard.Focus(textbox);
                textbox.CaretIndex = textbox.Text.Length;
            };
            textbox.LostFocus += (a, arg) =>
            {
                IsEditing = false;
                Text = EditedText;
            };
        }
    }
}
