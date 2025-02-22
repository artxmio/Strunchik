using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Strunchik.ViewModel.Converters;

public class InverseBooleanConverter : MarkupExtension, IValueConverter
{
    private static InverseBooleanConverter _instance;

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return _instance ??= new InverseBooleanConverter();
    }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool boolValue)
        {
            return !boolValue;
        }
        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}