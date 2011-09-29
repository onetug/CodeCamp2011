namespace CodeCamp.RIA.UI.Infrastructure
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    public class DateTimeConverter : IValueConverter
    {

        #region IValueConverter Members

        //Called when binding from an object property to a control property
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || (DateTime)value == DateTime.MinValue) return null;
            DateTime dt = (DateTime)value;
            return dt.ToString((string)parameter, culture);
        }

        //Called with two-way data binding as value is pulled out of control and put back into the property
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string val = (string)value;
            DateTime outDate;
            if (DateTime.TryParse(val, culture, DateTimeStyles.None, out outDate))
            {
                return outDate;
            }
            return DependencyProperty.UnsetValue;

        }

        #endregion
    }
}
