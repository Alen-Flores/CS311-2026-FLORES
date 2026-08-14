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

  private readonly IDatabaseService dbService;
  private readonly User user;

  public AccountsViewModel(User user, IDatabaseService dbService)
  {
    this.dbService = dbService;
    Users = new ObservableCollection<User>(dbService.GetUsers());
    this.user = user;
  }

  [RelayCommand]
  private async Task Search(string query)
  {
    if (query.Length == 0)
    {
      Users = new ObservableCollection<User>(dbService.GetUsers());
    }
    else
    {
      Users = new ObservableCollection<User>(dbService.GetUsersByQuery(query));
    }
  }

  [RelayCommand]
  private async Task Add()
  {
    var accountViewModel = new AddAccountViewModel(user, dbService);
    CurrentControl = accountViewModel;
    accountViewModel.OnClose += () =>
    {
      CurrentControl = null;
    };
  }

  [RelayCommand]
  private async Task Update()
  {
    User? selectedUser = Users.ElementAtOrDefault(SelectedIndex);
    if (selectedUser is null) return;

    var updateAccountViewModel =
      new UpdateAccountViewModel(user.Username, selectedUser, dbService);
    CurrentControl = updateAccountViewModel;
    updateAccountViewModel.OnClose += () =>
    {
      CurrentControl = null;
    };
  }

  [RelayCommand]
  private void Refresh()
  {
    Users = new ObservableCollection<User>(dbService.GetUsers());
  }

  [RelayCommand]
  private async Task Delete()
  {
    User? selectedUser = Users.ElementAtOrDefault(SelectedIndex);
    if (selectedUser is null) return;

    var dr = await Dialog.Show("Are you sure you want to delete this account?", Dialog.Buttons.YesNo);
    if (dr == Dialog.DialogResult.Yes)
    {
      if (dbService.DeleteUser(selectedUser.Username))
      {

        dbService.LogAction(new Log(
          DateTime.Now.ToString("dd/MM/yyyy"), DateTime.Now.ToString("h:mm tt") // ToShortTimeString causes an error
          , "Delete account", "Account Management", user.Username, selectedUser.Username
          ));
        await Dialog.Show("Account Deleted!", Dialog.Buttons.Ok);
      }
    }

  }
}