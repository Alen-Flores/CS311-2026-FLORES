using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CS311_CS3A_2026_Flores.Services;
using CS311_CS3A_2026_Flores.Views;

namespace CS311_CS3A_2026_Flores.ViewModels;

public partial class AccountsViewModel : ObservableObject
{
  [ObservableProperty]
  private ObservableCollection<User> _users;

  [ObservableProperty]
  private ObservableObject? _currentControl;

  [ObservableProperty]
  private int _selectedIndex = -1;

  private IUserService userService;
  private ILoggingService logService;
  private IAuthService authService;

  public AccountsViewModel(IAuthService authService, IUserService userService, ILoggingService logService)
  {
    this.userService = userService;
    this.logService = logService;
    this.authService = authService;
    Users = new ObservableCollection<User>(userService.GetUsers());
  }

  [RelayCommand]
  private async Task Search(string query)
  {
    if (query.Length == 0)
    {
      Users = new ObservableCollection<User>(userService.GetUsers());
    }
    else
    {
      Users = new ObservableCollection<User>(userService.GetUsersByQuery(query));
    }
  }

  [RelayCommand]
  private async Task Add()
  {
    var accountViewModel = new AddAccountViewModel(authService, userService);
    CurrentControl = accountViewModel;
    accountViewModel.OnClose += () =>
    {
      CurrentControl = null;
    };
  }

  [RelayCommand]
  private async Task Update()
  {
    User curUser = await authService.GetUser();
    User? selectedUser = Users.ElementAtOrDefault(SelectedIndex);
    if (selectedUser is null) return;

    var updateAccountViewModel =
      new UpdateAccountViewModel(curUser.Username, selectedUser, userService, logService);
    CurrentControl = updateAccountViewModel;
    updateAccountViewModel.OnClose += () =>
    {
      CurrentControl = null;
    };
  }

  [RelayCommand]
  private void Refresh()
  {
    Users = new ObservableCollection<User>(userService.GetUsers());
  }

  [RelayCommand]
  private async Task Delete()
  {
    User user = await authService.GetUser();
    User? selectedUser = Users.ElementAtOrDefault(SelectedIndex);
    if (selectedUser is null) return;

    var dr = await Dialog.Show("Are you sure you want to delete this account?", Dialog.Buttons.YesNo);
    if (dr == Dialog.DialogResult.Yes)
    {
      if (userService.DeleteUser(selectedUser.Username))
      {
        SelectedIndex = -1;
        logService.LogAction(new Log(
          DateTime.Now.ToString("dd/MM/yyyy"), DateTime.Now.ToString("h:mm tt") // ToShortTimeString causes an error
          , "Delete account", "Account Management", user.Username, selectedUser.Username
          ));
        await Dialog.Show("Account Deleted!", Dialog.Buttons.Ok);
      }
    }

  }
}