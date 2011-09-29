namespace CodeCamp.RIA.UI
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class UpdateSourceTriggerHelper
    {
        public static readonly DependencyProperty UpdateSourceTriggerProperty =
            DependencyProperty.RegisterAttached("UpdateSourceTrigger", typeof(bool), typeof(UpdateSourceTriggerHelper),
                                                new PropertyMetadata(OnUpdateSourceTriggerChanged));

        public static bool GetUpdateSourceTrigger(DependencyObject d)
        {
            return (bool)d.GetValue(UpdateSourceTriggerProperty);
        }


        public static void SetUpdateSourceTrigger(DependencyObject d, bool value)
        {
            d.SetValue(UpdateSourceTriggerProperty, value);
        }

        private static void OnUpdateSourceTriggerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBox textBox = d as TextBox;
            if ((bool)e.OldValue)
            {
                textBox.TextChanged -= (s, arg) =>
                {

                };
            }
            if ((bool)e.NewValue)
            {
                textBox.TextChanged += (s, arg) =>
                {
                    var c = findFocusableControl(textBox);
                    if (c != null)
                    {
                        c.Focus();
                    }
                    textBox.Focus();
                };
            }
        }

        private static Control findFocusableControl(Control control)
        {
            var ctl = VisualTreeHelper.GetParent(control);
            if ((ctl as Control) != null)
            {
                return ctl as Control;
            }
            else
            {
                int childrenCount = VisualTreeHelper.GetChildrenCount(ctl);
                for (int i = 0; i < childrenCount; i++)
                {
                    var c = VisualTreeHelper.GetChild(ctl, i) as Control;
                    if ((c != null) && (c != control))
                    {
                        return c;
                    }
                }
            }
            return null;
        }
    } 
}
