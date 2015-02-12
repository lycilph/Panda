using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Panda.UI.Extensions;

namespace Panda.UI
{
    public class AnimatedPanel : Panel
    {
        private readonly Dictionary<UIElement, Rect> starting_positions = new Dictionary<UIElement, Rect>();
        private readonly Dictionary<UIElement, Rect> target_positions = new Dictionary<UIElement, Rect>();
        private DateTime end_time = DateTime.Now;
        private bool is_animating;

        public TimeSpan Duration
        {
            get { return (TimeSpan)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }
        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.Register("Duration", typeof(TimeSpan), typeof(AnimatedPanel), new UIPropertyMetadata(TimeSpan.FromMilliseconds(500)));

        public IEasingFunction EasingFunction
        {
            get { return (IEasingFunction)GetValue(EasingFunctionProperty); }
            set { SetValue(EasingFunctionProperty, value); }
        }
        public static readonly DependencyProperty EasingFunctionProperty =
            DependencyProperty.Register("EasingFunction", typeof(IEasingFunction), typeof(AnimatedPanel), new PropertyMetadata(null));

        protected override Size ArrangeOverride(Size final_size)
        {
            AnimatedArrangeInternal();

            return base.ArrangeOverride(final_size);
        }

        private void AnimatedArrangeInternal()
        {
            var now = DateTime.Now;

            foreach (UIElement child in InternalChildren)
            {
                if (!target_positions.ContainsKey(child))
                    throw new InvalidOperationException("Must call AnimatedPanel.AnimatedArrange for all children");

                if (!starting_positions.ContainsKey(child))
                    starting_positions[child] = target_positions[child];
            }

            if (!is_animating)
            {
                var something_moved = InternalChildren.Cast<UIElement>().Any(child => IsDifferent(starting_positions[child], target_positions[child]));
                if (something_moved)
                {
                    end_time = now.AddMilliseconds(Duration.TotalMilliseconds);
                    is_animating = true;
                    CompositionTarget.Rendering += CompositionTarget_Rendering;
                }
            }

            var time_remaining = (end_time - now).TotalMilliseconds;
            var normalized_time = Math.Min(1.0, 1.0 - time_remaining/Duration.TotalMilliseconds);
            var fraction_complete = (EasingFunction == null ? normalized_time : EasingFunction.Ease(normalized_time));

            foreach (UIElement child in InternalChildren)
            {
                var pos = RectExtensions.Interpolate(starting_positions[child], target_positions[child], fraction_complete);
                child.Arrange(pos);
            }

            if (time_remaining < 0)
            {
                is_animating = false;
                CompositionTarget.Rendering -= CompositionTarget_Rendering;
                foreach (UIElement child in InternalChildren)
                {
                    starting_positions[child] = target_positions[child];
                }
            }

            Clean(starting_positions);
            Clean(target_positions);
        }

        private static bool IsDifferent(Rect r1, Rect r2)
        {
            return (r1.Left != r2.Left || r1.Top != r2.Top || r1.Width != r2.Width || r1.Height != r2.Height);
        }

        // Dictionary may reference children that have been removed
        private void Clean(Dictionary<UIElement, Rect> dictionary)
        {
            if (dictionary.Count == Children.Count) 
                return;

            var childen_to_remove = dictionary.Keys.Where(k => !InternalChildren.Contains(k));
            foreach (var element in childen_to_remove)
            {
                dictionary.Remove(element);
            }
        }

        public void AnimatedArrange(UIElement child, Rect final_size)
        {
            target_positions[child] = final_size;
        }

        void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            InvalidateArrange();
        }
    }
}
