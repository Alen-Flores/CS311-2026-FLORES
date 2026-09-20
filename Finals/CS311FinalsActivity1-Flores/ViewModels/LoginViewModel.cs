using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Channels;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CS311_CS3A_2026_Flores.Services;
using CS311_CS3A_2026_Flores.Views;

namespace CS311_CS3A_2026_Flores.ViewModels;

public partial class LoginViewModel : ObservableValidator
{
  private IUserService userService;

  public LoginViewModel(IUserService userService)
  {
    this.userService = userService;
  }

  [ObservableProperty]
  [Required]
  private string username = "";

  [ObservableProperty]
  [Required]
  private string password = "";

  private Channel<User> userChannel = Channel.CreateBounded<User>(1);
  public async Task<User> GetUser()
  {
    return await userChannel.Reader.ReadAsync();
  }

  [RelayCommand]
  private async Task Login()
  {
    User? user = userService.GetUserByLogin(Username, Password);
    if (user is null || user.Status == "INACTIVE")
    {
      await Dialog.Show("Incorrect account details or account is inactive", Dialog.Buttons.Ok);
    }
    else
    {
      Clear();
      await userChannel.Writer.WriteAsync(user);
    }

  }

  [RelayCommand]
  private void Clear()
  {
    Username = "";
    Password = "";
  }
}