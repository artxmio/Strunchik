using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;
namespace Strunchik.ViewModel.Validation;

public class EmailValidationRule : ValidationRule
{
    private static readonly Regex EmailRegex = new Regex("^[^\\s@]+@[^\\s@]+\\.[^\\s@]+$", RegexOptions.Compiled);

    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        var email = value as string;
        if (string.IsNullOrWhiteSpace(email))
        {
            return new ValidationResult(false, "Введите пароль.");
        }
        if (!EmailRegex.IsMatch(email))
        {
            return new ValidationResult(false, "Неправильный формат почты.");
        }
        return ValidationResult.ValidResult;
    }
}