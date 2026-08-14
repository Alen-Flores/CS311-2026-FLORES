using Avalonia.Controls;

namespace CS311_CS3A_2026_Flores.Views;

public partial class AccountsView : UserControl
{
  public AccountsView()
  {
    InitializeComponent();
    Loaded += (_, _) => Focus();
  }
}