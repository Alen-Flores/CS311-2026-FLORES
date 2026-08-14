using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ViewModels;

public partial class MainViewModel : ObservableValidator
{

    [ObservableProperty]
    [Required(ErrorMessage = "Input 1 is required.")]
    [CustomValidation(typeof(MainViewModel), nameof(ValidateNumber))]
    private string? _input1 = "";

    [ObservableProperty]
    [Required(ErrorMessage = "Input 2 is required.")]
    [CustomValidation(typeof(MainViewModel), nameof(ValidateNumber))]
    private string? _input2 = "";

    [ObservableProperty]
    private string? _result;

    public static ValidationResult? ValidateNumber(string? value, ValidationContext context)
    {
        if (double.TryParse(value, out _))
            return ValidationResult.Success;

        return new ValidationResult("Must be a valid number.");
    }

    [RelayCommand]
    private void Add()
    {
        ValidateAllProperties();
        if (HasErrors)
        {
            Result = "Invalid input.";
            return;
        }

        if (double.TryParse(Input1, out double num1) && double.TryParse(Input2, out double num2))
        {
            Result = (num1 + num2).ToString("0.0");
        }

    }

    [RelayCommand]
    private void Subtract()
    {
        if (double.TryParse(Input1, out double num1) && double.TryParse(Input2, out double num2))
        {
            Result = (num1 - num2).ToString("0.0");
        }

    }

    [RelayCommand]
    private void Multiply()
    {
        if (double.TryParse(Input1, out double num1) && double.TryParse(Input2, out double num2))
        {
            Result = (num1 * num2).ToString("0.0");
        }
    }

    [RelayCommand]
    private void Divide()
    {

        if (double.TryParse(Input1, out double num1) && double.TryParse(Input2, out double num2))
        {
            if (num2 != 0)
            {
                Result = (num1 / num2).ToString("0.0");
            }
    }
}
