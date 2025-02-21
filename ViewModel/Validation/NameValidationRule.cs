using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;
namespace Strunchik.ViewModel.Validation;

public class NameValidationRule : ValidationRule
{
    private static readonly Regex NameRegex = new Regex("^[A-Za-zА-Яа-яЁё\\s]+$", RegexOptions.Compiled);

    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        var name = value as string;
        if (string.IsNullOrWhiteSpace(name))
        {
            return new ValidationResult(false, "Введите имя."); 
        }
        if (!NameRegex.IsMatch(name))
        {
            return new ValidationResult(false, "Неправильный формат имени.");
        }
        return ValidationResult.ValidResult;
    }
}
