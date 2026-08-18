using System;
using System.Threading.Tasks;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CS311_CS3A_2026_Flores.Services;
using CS311_CS3A_2026_Flores.Views;

namespace CS311_CS3A_2026_Flores.ViewModels;

public partial class MainWindowViewModel : ObservableValidator
{

  [ObservableProperty]
  private ObservableObject? _currentPage;

  private IUserService userService;
  private ILoggingService loggingService;
  private IAuthService authService;
  private IEquipmentService equipmentService;

  public bool AccountsMenuItemVisible => User?.Usertype == "ADMINISTRATOR";
  public bool EquipmentsMenuItemVisible => true;

  [ObservableProperty]
  private User? _user;

  public MainWindowViewModel(
    IAuthService authService, 
    IUserService userService, 
    ILoggingService loggingService,
    IEquipmentService equipmentService
    )
  {
    this.authService = authService;
    this.userService = userService;
    this.loggingService = loggingService;
    this.equipmentService = equipmentService;

    authService.AccountChanged += user =>
    {
      User = user;
      OnPropertyChanged(nameof(AccountsMenuItemVisible));
    };

  }

  [RelayCommand]
  private async Task Logout()
  {
    var result = await Dialog.Show("Are you sure you want to log out?", Dialog.Buttons.YesNo);
    if (result == Dialog.DialogResult.Yes)
    {
      authService.Logout();
    }
  }

  [RelayCommand]
  private async Task Accounts() => CurrentPage =
    new AccountsViewModel(authService, userService, loggingService);

  [RelayCommand]
  private async Task Equipments() => CurrentPage =
    new EquipmentsViewModel(equipmentService, loggingService, authService);
}