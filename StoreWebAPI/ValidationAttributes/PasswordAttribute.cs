using System.ComponentModel.DataAnnotations;

namespace StoreWebAPI.ValidationAttributes;

public class PasswordAttribute : ValidationAttribute
{
    private static char[] _uppercaseLetters = new[]
    {
        'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'Ñ', 'O', 'P', 'Q', 'R', 'S', 'T', 'U',
        'V', 'W', 'X', 'Y', 'Z'
    };

    private static char[] _lowercaseLetters = new[]
    {
        'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'ñ', 'o', 'p', 'q', 'r', 's', 't', 'u',
        'v', 'w', 'x', 'y', 'z'
    };

    private static char[] _numbers = new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

    private static char[] _symbols = new[]
    {
        '¡', '!', '"', '#', '$', '%', '&', '/', '|', '°', '\\', '(', ')', '-', '_', ',', '.', ';', ':', '\'', '`', '´',
        '¿', '?', '{', '}', '[', ']', '+', '*', '¨', '~', '^', '@'
    };

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var password = value as string;
        if (string.IsNullOrWhiteSpace(password)) return new ValidationResult("The password field cannot be empty.");

        if (password.Length < 8) return new ValidationResult("The password must be at least 8 characters long.");
        if (!_uppercaseLetters.Any(c => password.Contains(c)))
            return new ValidationResult("The password must contain at least one uppercase letter.");

        if (!_lowercaseLetters.Any(c => password.Contains(c)))
            return new ValidationResult("The password must contain at least one lowercase letter.");

        if (!_numbers.Any(c => password.Contains(c)))
            return new ValidationResult("The password must contain at least one number.");

        if (!_symbols.Any(c => password.Contains(c)))
            return new ValidationResult("The password must contain at least one symbol.");

        return ValidationResult.Success;
    }
}
