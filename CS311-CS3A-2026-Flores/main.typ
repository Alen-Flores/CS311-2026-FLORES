#align(center, text("Recitation-1", 27pt))
#grid(
  columns: (1fr, 1fr),
  align(left)[
    Marlon Alen I Flores\
    BSCS - 3A
  ],
  align(right)[
    CS311\
    #datetime.display(datetime.today())
  ],
)

#show raw: set text(font: "JetBrainsMono NF")
#show link: set text(fill: blue)

#import "@preview/codly:1.3.0": *
#import "@preview/codly-languages:0.1.1": *
#import "@preview/cetz:0.4.2"
#show: codly-init.with()
#codly(languages: codly-languages, stroke: 0.3pt + black)
#show raw.where(block: true, lang: "console"): it => local(
  header: text(fill: black, font: "New Computer Modern", [*Output*]),
  header-cell-args: (fill: luma(240)),
  number-format: none,
  zebra-fill: none,
  display-icon: false,
  display-name: false,
  breakable: false,
  fill: rgb("#202032"),
  {
    show regex("^\$.*"): text.with(fill: rgb("#a6e3a1"))
    text(fill: white, it)
  },
)


= Code
```csharp
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
```

= Screenshots

- Both inputs are empty and click subtract.
#image("./screenshots/scene-1")

- Input 1 is empty and input2 is a character/string and click subtract.
#image("./screenshots/scene-2")

- Input 1 is a character/string and input 2 is empty and click subtract.
#image("./screenshots/scene-3")

- Both inputs are character and string and click subtract.
#image("./screenshots/scene-4")

- Input 1 is empty and input 2 is a number and click subtract.
#image("./screenshots/scene-5")

- Input 1 is a number and input 2 is empty and click subtract.
#image("./screenshots/scene-6")

- Input 1 is a character/string and input 2 is a number and click subtract.
#image("./screenshots/scene-7")

- Input 1 is a number and input 2 is a character/string and click subtract.
#image("./screenshots/scene-8")

- Both inputs are number and click subtract.
#image("./screenshots/scene-9")

- Both inputs are number and click multiply.
#image("./screenshots/scene-10")

- Both inputs are number and click divide.
#image("./screenshots/scene-11")

#link("https://drive.google.com/file/d/1Ydo-H7juLxrJCKtv3INWAfdIFmhECI7l/view?usp=sharing")
