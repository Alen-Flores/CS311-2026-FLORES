using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CS311_CS3A_2026_Flores.Services;
using CS311_CS3A_2026_Flores.Views;

namespace CS311_CS3A_2026_Flores.ViewModels;

public partial class LoginViewModel : ObservableValidator
{

  public event Action<User>? OnLoginSuccess;

  private readonly IDatabaseService _dbService;

  public LoginViewModel(IDatabaseService dbService)
  {
    _dbService = dbService;
  }

  [ObservableProperty]
  [Required]
  private string username = "";

  [ObservableProperty]
  [Required]
  private string password = "";

  [RelayCommand]
  private async Task Login()
  {
    User? user = _dbService.GetUserByLogin(Username, Password);
    if (user is null || user.Status == "INACTIVE")
    {
      await Dialog.Show("Incorrect account details or account is inactive", Dialog.Buttons.Ok);
    }
    else
    {
      Clear();
      OnLoginSuccess?.Invoke(user);
    }

  }

  [RelayCommand]
  private void Clear()
  {
    Username = "";
    Password = "";
  }
}