using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CS311_CS3A_2026_Flores.Services;
using CS311_CS3A_2026_Flores.Views;

namespace CS311_CS3A_2026_Flores.ViewModels;

public partial class MainWindowViewModel : ObservableValidator
{


  [ObservableProperty]
  private User _User;

  [ObservableProperty]
  private ObservableObject? _currentPage;

  private readonly IDatabaseService DbService;

  public event Action? OnLogout;

  public bool AccountsMenuItemVisible => User.Usertype == "ADMINISTRATOR";
  public bool EquipmentsMenuItemVisible => true;

  public MainWindowViewModel(User user, IDatabaseService dbService)
  {
    _User = user;
    DbService = dbService;
  }

  [RelayCommand]
  private async Task Logout()
  {
    var result = await Dialog.Show("Are you sure you want to log out?", Dialog.Buttons.YesNo);
    if (result == Dialog.DialogResult.Yes)
    {
      OnLogout!.Invoke();
    }
  }

  [RelayCommand]
  private async Task Accounts() => CurrentPage = new AccountsViewModel(User, DbService);
}