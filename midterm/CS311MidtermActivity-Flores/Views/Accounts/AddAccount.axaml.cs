using Avalonia.Controls;

namespace CS311_CS3A_2026_Flores.Views;
public partial class AddAccountView : UserControl
{
  public AddAccountView()
  {
    InitializeComponent();
    Loaded += (s,e) => Focus();
  }
}