using System;
using System.Linq;
using System.Windows;

namespace Panda.UI
{
    public class FillPanel : AnimatedPanel
    {
        public static int GetIndex(DependencyObject obj)
        {
            return (int)obj.GetValue(IndexProperty);
        }
        public static void SetIndex(DependencyObject obj, int value)
        {
            obj.SetValue(IndexProperty, value);
        }
        public static readonly DependencyProperty IndexProperty =
            DependencyProperty.RegisterAttached("Index", typeof(int), typeof(FillPanel), new PropertyMetadata(-1));

        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }
        public static readonly DependencyProperty SelectedIndexProperty =
            DependencyProperty.Register("SelectedIndex", typeof(int), typeof(FillPanel), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure));

        protected override Size MeasureOverride(Size available_size)
        {
            var children = InternalChildren.Cast<UIElement>().ToList();
            var accumulated_height = 0.0;
            // Measure non-fill children
            foreach (var element in children.Where(c => GetIndex(c) == -1))
            {
                var constraint_size = new Size(available_size.Width, available_size.Height - accumulated_height);
                element.Measure(constraint_size);
                accumulated_height += element.DesiredSize.Height;
            }
            // Measure fill children
            foreach (var element in children.Where(c => GetIndex(c) > -1))
            {
                var constraint_size = new Size(available_size.Width, available_size.Height - accumulated_height);
                element.Measure(constraint_size);
                if (GetIndex(element) == SelectedIndex)
                    accumulated_height += element.DesiredSize.Height;
            }

            return new Size(available_size.Width, available_size.Height);
        }

        protected override Size ArrangeOverride(Size final_size)
        {
            var accumulated_height = InternalChildren.Cast<UIElement>().Sum(child => (GetIndex(child) == -1 ? child.DesiredSize.Height : 0));
            var fill_height = final_size.Height - accumulated_height;

            var current_y = 0.0;
            foreach (UIElement element in InternalChildren)
            {
                var width = Math.Max(final_size.Width, element.DesiredSize.Width);

                double height;
                if (GetIndex(element) == SelectedIndex)
                    height = fill_height;
                else if (GetIndex(element) > -1)
                    height = 0;
                else
                    height = element.DesiredSize.Height;

                AnimatedArrange(element, new Rect(0, current_y, width, height));
                current_y += height;
            }

            return base.ArrangeOverride(final_size);
        }
    }
}
