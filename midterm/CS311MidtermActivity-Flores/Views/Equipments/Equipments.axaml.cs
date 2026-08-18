using Avalonia.Controls;

namespace CS311_CS3A_2026_Flores.Views;
public partial class EquipmentsView : UserControl
{
  public EquipmentsView()
  {
    InitializeComponent();
    Loaded += (s,e) => Focus();
  }

}