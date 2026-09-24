using System;
using System.Data;
using System.Linq;
using System.Threading.Channels;
using System.Threading.Tasks;
using CS311_CS3A_2026_Flores.Views;

namespace CS311_CS3A_2026_Flores.Services;

public interface IAuthService
{
  public event Action<User?>? AccountChanged;
  public event Action<ChannelWriter<User>>? RequestLogin;
  Task<User> GetUser();
  Task Login();
  void Logout();
}

public class AuthService : IAuthService
{
  private User? user;
  public event Action<User?>? AccountChanged;
  public event Action<ChannelWriter<User>>? RequestLogin;


  public async Task<User> GetUser()
  {
    if (user != null) return user;
    await Login();
    return user!;
  }

  public async Task Login()
  {
    Channel<User> userChannel = Channel.CreateBounded<User>(1);
    RequestLogin?.Invoke(userChannel.Writer);

    user = await userChannel.Reader.ReadAsync();
    AccountChanged?.Invoke(user);
  }

  public void Logout()
  {
    user = null;
    AccountChanged?.Invoke(null);
  }
}