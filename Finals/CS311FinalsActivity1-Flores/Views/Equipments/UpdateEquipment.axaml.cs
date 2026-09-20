using Avalonia.Controls;

namespace CS311_CS3A_2026_Flores.Views;

public partial class UpdateEquipmentView : UserControl
{
  public UpdateEquipmentView()
  {
    InitializeComponent();
    Loaded += (_,_) => Focus();
  }
}
