namespace CodeCamp.RIA.UI
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using CodeCamp.RIA.UI.Infrastructure;

    public class RadioButtonConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value.ToString() == parameter.ToString());
        }

        public object ConvertBack(object value, Type targetType,
               object parameter, CultureInfo culture)
        {
            return (bool)value ? Enum.Parse(typeof(PresentationLevel), parameter.ToString(), true) : null;
        }
    }
}
