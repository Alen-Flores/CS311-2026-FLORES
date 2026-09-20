using Avalonia.Controls;

namespace CS311_CS3A_2026_Flores.Views;
public partial class UpdateAccountView : UserControl
{
  public UpdateAccountView()
  {
    InitializeComponent();
    Loaded += (s,e) => Focus();
  }
}