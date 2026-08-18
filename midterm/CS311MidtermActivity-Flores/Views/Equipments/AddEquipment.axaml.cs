using Avalonia.Controls;

namespace CS311_CS3A_2026_Flores.Views;

public partial class AddEquipmentView : UserControl
{
  public AddEquipmentView()
  {
    InitializeComponent();
    Loaded += (s,e) => Focus();
  }
}