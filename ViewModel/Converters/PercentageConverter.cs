using System.Globalization;
using System.Windows.Data;

namespace Strunchik.ViewModel.Converters;

public class PercentageConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        double width = (double)value;
        double percentage = System.Convert.ToDouble(parameter, culture);
        return width * percentage;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}