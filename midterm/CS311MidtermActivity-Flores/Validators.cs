using System;
using System.ComponentModel.DataAnnotations;

namespace CS311_CS3A_2026_Flores.Validators;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public sealed class ValidYearAttribute : ValidationAttribute
{
    public ValidYearAttribute()
    {
        ErrorMessage = "Must be a number between 1000 to 9999.";
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null)
            return ValidationResult.Success;

        var text = value.ToString();

        if (string.IsNullOrWhiteSpace(text))
            return ValidationResult.Success;

        if (!int.TryParse(text, out int number))
            return new ValidationResult(ErrorMessage);

        if (number < 1000 || number > 9999)
            return new ValidationResult(ErrorMessage);

        return ValidationResult.Success;
    }
}