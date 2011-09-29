namespace CodeCamp.RIA.UI
{
    using System;
    using System.Net;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Ink;
    using System.Windows.Input;
    using System.Windows.Interactivity;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Windows.Shapes;

    public class UpdateOnTextChangedBehavior : Behavior<TextBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            this.AssociatedObject.TextChanged +=
                new TextChangedEventHandler(AssociatedObject_TextChanged);
        }

        void AssociatedObject_TextChanged(object sender, TextChangedEventArgs e)
        {
            BindingExpression binding =
                this.AssociatedObject.GetBindingExpression(TextBox.TextProperty);
            if (binding != null)
            {
                binding.UpdateSource();
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            this.AssociatedObject.TextChanged -=
                new TextChangedEventHandler(AssociatedObject_TextChanged);
        }
    }
}
