using System;
using System.Windows.Data;
namespace CodeCamp.RIA.UI
{
    public class EnumBoolConverter : IValueConverter
    {
        #region Methods
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || parameter == null)
                return value;

            return value.ToString() == parameter.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || parameter == null)
                return value;

            return Enum.Parse(targetType, (String)parameter, true);
        }
        #endregion Methods
    }
}