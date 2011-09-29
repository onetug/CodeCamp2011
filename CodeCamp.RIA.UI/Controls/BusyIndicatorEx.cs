namespace CodeCamp.RIA.UI.Controls
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    
    public class BusyIndicatorEx : BusyIndicator
    {
        private bool _isDelayed = false;

        protected override void OnIsBusyChanged(DependencyPropertyChangedEventArgs e)
        {
            this._isDelayed = (bool)e.NewValue && !this.DisplayAfter.Equals(TimeSpan.Zero);
            base.OnIsBusyChanged(e);
            this._isDelayed = false;
        }

        protected override void ChangeVisualState(bool useTransitions)
        {
            if (!this._isDelayed) base.ChangeVisualState(useTransitions);
        }
    }
}
