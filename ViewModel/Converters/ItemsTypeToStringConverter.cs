using Strunchik.Model.Item;
using System.Globalization;
using System.Windows.Data;

namespace Strunchik.ViewModel.Converters
{
    public class ItemsTypeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ItemsType type)
            {
                return type.ToMyString();
            }
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
