using Avalonia.Controls;


namespace CS311_Midterm_Recitation2_Flores;
public partial class FrmNewAccount : Window {

  private string Username = "";

  public FrmNewAccount()
  {
    InitializeComponent();
    DataContext = this;
  }

  public FrmNewAccount(string username) : this()
  {
    this.Username = username;
  }
  
}