using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;
namespace Strunchik.ViewModel.Validation;

public class PasswordValidationRule : ValidationRule
{
    private static readonly Regex PasswordRegex = new Regex("^[A-Za-z\\d@$!%*?&]{8,}$", RegexOptions.Compiled);

    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        var password = value as string;
        if (string.IsNullOrWhiteSpace(password))
        {
            return new ValidationResult(false, "Введите пароль.");
        }
        if (!PasswordRegex.IsMatch(password))
        {
            return new ValidationResult(false, "Неверный формат пароля.");
        }
        return ValidationResult.ValidResult;
    }
}