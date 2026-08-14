using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CS311_CS3A_2026_Flores.Services;
using CS311_CS3A_2026_Flores.Views;

namespace CS311_CS3A_2026_Flores.ViewModels;

public partial class AddAccountViewModel : ObservableValidator
{
  private readonly User user;
  private IDatabaseService dbService;

  public Action? OnClose;

  [ObservableProperty]
  [CustomValidation(typeof(AddAccountViewModel), nameof(ValidateUniqueUsername))]
  [Required]
  private string _username = "";

  [ObservableProperty]

  [Required]
  private string _password = "";

  [ObservableProperty]
  public ObservableCollection<string> _usertypes = new()
  {
    "Administrator",
    "Technical",
    "User",
  };

  [ObservableProperty]
  [Required(ErrorMessage = "Select a usertype")]
  private string? _selectedType;

  public AddAccountViewModel(User user, IDatabaseService dbService)
  {
    this.user = user;
    this.dbService = dbService;
  }

  public static ValidationResult? ValidateUniqueUsername(
    string username,
    ValidationContext context
    )
  {
    var viewModel = (AddAccountViewModel)context.ObjectInstance;
    if (string.IsNullOrWhiteSpace(username))
      return ValidationResult.Success;

    if (viewModel.dbService.GetUserByUsername(username) != null)
      return new ValidationResult($"'{username}' is already in use");

    return ValidationResult.Success;
  }

  [RelayCommand]
  private async Task Clear()
  {
    Username = "";
    Password = "";
    SelectedType = null;
    ClearErrors();
  }

  [RelayCommand]
  private async Task Save()
  {
    ValidateAllProperties();
    if (HasErrors) return;

    var result = await Dialog.Show("Are you sure you want to make this account?",
                Dialog.Buttons.YesNo);
    if (result == Dialog.DialogResult.Yes)
    {
      User newUser = new User(
        Username, Password, SelectedType!.ToUpper(), "ACTIVE", user.Username,
        DateTime.Now.ToShortDateString()
      );
      if (dbService.AddUser(newUser))
      {
        await Dialog.Show("New Account Added.", Dialog.Buttons.Ok);
        Close();
      }
    }
  }

  [RelayCommand]
  private void Close()
  {
    OnClose?.Invoke();
  }
}