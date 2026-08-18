using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CS311_CS3A_2026_Flores.Services;
using CS311_CS3A_2026_Flores.Views;

namespace CS311_CS3A_2026_Flores.ViewModels;

public partial class UpdateAccountViewModel : ObservableValidator
{

  private IUserService userService;
  private ILoggingService logService;
  private User target;
  public Action? OnClose;

  public string Username => target.Username;

  [ObservableProperty]
  [Required]
  public string? _password;

  [ObservableProperty]
  public ObservableCollection<string> _usertypes = new()
  {
    "Administrator",
    "Technical",
    "User",
  };

  [ObservableProperty]
  public ObservableCollection<string> _statuses = new()
  {
    "Active",
    "Inactive"
  };

  [ObservableProperty]
  [Required(ErrorMessage = "Select a usertype")]
  private string? _selectedType;

  [ObservableProperty]
  [Required(ErrorMessage = "Select a status")]
  private string? _selectedStatus;
  private readonly string creator;

  public UpdateAccountViewModel(string creator, User target, IUserService userService, ILoggingService logService)
  {
    this.userService = userService;
    this.logService = logService;
    this.target = target;
    this.creator = creator;
    Password = target.Password;
    SelectedType = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(target.Usertype.ToLower());
    SelectedStatus = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(target.Status.ToLower());
  }

  [RelayCommand]
  private void Close()
  {
    OnClose?.Invoke();
  }

  [RelayCommand]
  private async Task Save()
  {
    ValidateAllProperties();
    if (HasErrors) return;

    var result = await Dialog.Show("Are you sure you want to update this account?",
                Dialog.Buttons.YesNo);
    if (result == Dialog.DialogResult.Yes)
    {
      User updatedUser = new User(
        target.Username,
        Password!, SelectedType!.ToUpper(), SelectedStatus!.ToUpper(),
        target.CreatedBy, target.DateCreated
      );
      if (userService.UpdateUser(updatedUser))
      {
        logService.LogAction(new Log(
          DateTime.Now.ToString("dd/MM/yyyy"), DateTime.Now.ToString("h:mm tt") // ToShortTimeString causes an error
          , "Update account", "Account Management", creator, target.Username
          ));
        
        await Dialog.Show("Account Updated!", Dialog.Buttons.Ok);
        Close();
      }
    }

  }
}
