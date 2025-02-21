using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;
namespace Strunchik.ViewModel.Validation.PasswordValidationRule;

public class PasswordValidationRule : ValidationRule
{
    private static readonly Regex PasswordRegex = new Regex("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$", RegexOptions.Compiled);

    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        var password = value as string;
        if (string.IsNullOrWhiteSpace(password))
        {
            return new ValidationResult(false, "Введите пароль.");
        }
        if (!PasswordRegex.IsMatch(password))
        {
            return new ValidationResult(false, "Пароль должен быть длиной не менее 8 символов, содержать одну заглавную букву, одну строчную букву, одну цифру и один специальный символ.");
        }
        return ValidationResult.ValidResult;
    }
}