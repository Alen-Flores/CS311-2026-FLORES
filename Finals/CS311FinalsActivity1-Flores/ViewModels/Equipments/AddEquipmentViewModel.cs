using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CS311_CS3A_2026_Flores.Services;
using CS311_CS3A_2026_Flores.Validators;
using CS311_CS3A_2026_Flores.Views;

namespace CS311_CS3A_2026_Flores.ViewModels;

public partial class AddEquipmentViewModel : ObservableValidator
{
  public event Action? OnClose;

  private IAuthService authService;
  private IEquipmentService equipmentService;
  private ILoggingService loggingService;


  [Required]
  [ObservableProperty] private string? _assetnumber;

  [Required]
  [ObservableProperty] private string? _branch;

  [Required]
  [CustomValidation(typeof(AddEquipmentViewModel), nameof(ValidateUniqueSerialnumber))]
  [ObservableProperty] private string? _serialnumber;

  [Required]
  [ObservableProperty] private string? _type;

  [Required]
  [ObservableProperty] private string? _manufacturer;

  [Required]
  [ValidYear]

  [ObservableProperty] private string? _yearmodel;

  [ObservableProperty] private string? _description;

  [Required]
  [ObservableProperty] private string? _department;

  private string currentDatetime = $"{DateTime.Now:yyyyMMddHHmmss}";

  partial void OnBranchChanged(string? value)
  {
    Assetnumber = string.IsNullOrWhiteSpace(value)
      ? null
      : $"AU-{value}-{currentDatetime}";
  }



  public AddEquipmentViewModel(
    IAuthService authService,
    IEquipmentService equipmentService,
    ILoggingService loggingService
    )
  {
    this.authService = authService;
    this.equipmentService = equipmentService;
    this.loggingService = loggingService;
  }

  [RelayCommand]
  private void Close()
  {
    Branch = "";
    Serialnumber = "";
    Type = "";
    Manufacturer = "";
    Yearmodel = "";
    Description = "";
    Department = "";
    OnClose?.Invoke();
  }

  [RelayCommand]
  private async Task Save()
  {
    ValidateAllProperties();
    if (HasErrors) return;

    var result = await Dialog.Show("Are you sure you want to make this equipment?",
                Dialog.Buttons.YesNo);
    if (result == Dialog.DialogResult.Yes)
    {
      User user = await authService.GetUser();
      Equipment newEquipment = new Equipment(
        Assetnumber!, Serialnumber!, Type!, Manufacturer!,
        Yearmodel!, Description!, Branch!, Department!, "WORKING",
        user.Username, DateTime.Now.ToShortDateString()
      );

      if (equipmentService.AddEquipment(newEquipment))
      {
        loggingService.LogAction(
          Log.WithCurrentTimeStamp("Add Equipment", "Equipment Management", user.Username, newEquipment.Assetnumber
        ));

        await Dialog.Show("New Equipment Added.", Dialog.Buttons.Ok);
        Close();
      }
      else
      {
        await Dialog.Show("Error in adding new Equipment.", Dialog.Buttons.Ok);
      }
    }
  }

  public static ValidationResult? ValidateUniqueSerialnumber(
    string serial,
    ValidationContext context
    )
  {
    var viewModel = (AddEquipmentViewModel)context.ObjectInstance;
    if (string.IsNullOrWhiteSpace(serial))
      return ValidationResult.Success;

    if (viewModel.equipmentService.GetEquipmentBySerialnumber(serial)
          != null)
      return new ValidationResult($"'{serial}' is already in use");

    return ValidationResult.Success;
  }
}