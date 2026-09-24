using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CS311_CS3A_2026_Flores.Services;
using CS311_CS3A_2026_Flores.Validators;
using CS311_CS3A_2026_Flores.Views;


namespace CS311_CS3A_2026_Flores.ViewModels;

public partial class UpdateEquipmentViewModel : ObservableValidator
{
  public event Action? OnClose;
  private IAuthService authService;
  private IEquipmentService equipmentService;
  private ILoggingService loggingService;

  private Equipment target;

  [ObservableProperty] private string _assetnumber;

  [CustomValidation(typeof(UpdateEquipmentViewModel), nameof(ValidateUniqueSerialnumber))]
  [ObservableProperty] private string _serialnumber;
  [ObservableProperty] private string _type;
  [ObservableProperty] private string _manufacturer;
  [ValidYear][ObservableProperty] private string _yearmodel;
  [ObservableProperty] private string _description;
  [ObservableProperty] private string _branch;
  [ObservableProperty] private string _department;
  [ObservableProperty] private string _status;

  public UpdateEquipmentViewModel(
    Equipment target,
    IAuthService authService,
    IEquipmentService equipmentService,
    ILoggingService loggingService
  )
  {
    this.authService = authService;
    this.loggingService = loggingService;
    this.equipmentService = equipmentService;
    this.target = target;
    Assetnumber = target.Assetnumber;
    Serialnumber = target.Serialnumber;
    Type = target.Type;
    Manufacturer = target.Manufacturer;
    Yearmodel = target.Yearmodel;
    Description = target.Description;
    Branch = target.Branch;
    Department = target.Department;
    Status = target.Status;
  }

  [RelayCommand]
  private async Task Save()
  {
    ValidateAllProperties();
    if (HasErrors) return;


    var result = await Dialog.Show("Are you sure you want to edit this equipment?",
                Dialog.Buttons.YesNo);

    if (result == Dialog.DialogResult.Yes)
    {
      var updated = target with
      {
        Serialnumber = Serialnumber,
        Type = Type,
        Manufacturer = Manufacturer,
        Yearmodel = Yearmodel,
        Description = Description,
        Branch = Branch,
        Department = Department,
        Status = Status
      };

      if (equipmentService.UpdateEquipment(updated))
      {
        loggingService.LogAction(
          Log.WithCurrentTimeStamp("Update Equipment", "Equipment Management",
                (await authService.GetUser()).Username, updated.Assetnumber
        ));
        await Dialog.Show("Equipment Updated.", Dialog.Buttons.Ok);
        Close();
      }
      else
      {
        await Dialog.Show("Error in updating Equipment.", Dialog.Buttons.Ok);
      }

    }

  }

  [RelayCommand]
  private void Close()
  {
    OnClose?.Invoke();
  }

  public static ValidationResult? ValidateUniqueSerialnumber(
    string serial,
    ValidationContext context
    )
  {
    var viewModel = (UpdateEquipmentViewModel)context.ObjectInstance;
    if (string.IsNullOrWhiteSpace(serial))
      return ValidationResult.Success;

    var existing = viewModel.equipmentService.GetEquipmentBySerialnumber(serial);
    if (existing is null)
      return ValidationResult.Success;

    if (viewModel.target != null &&
          existing.Assetnumber == viewModel.target.Assetnumber)
      return ValidationResult.Success;

    return new ValidationResult($"'{serial}' is already in use");

  }
}
