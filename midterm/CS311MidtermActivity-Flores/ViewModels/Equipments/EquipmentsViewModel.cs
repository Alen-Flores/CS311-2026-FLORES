using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CS311_CS3A_2026_Flores.Services;
using CS311_CS3A_2026_Flores.Views;

namespace CS311_CS3A_2026_Flores.ViewModels;

public partial class EquipmentsViewModel : ObservableObject
{

  [ObservableProperty]
  private List<Equipment> _equipments;

  [ObservableProperty]
  private int _selectedIndex;

  [ObservableProperty]
  private ObservableObject? _currentControl;
  private IEquipmentService equipmentService;
  private ILoggingService loggingService;
  private IAuthService authService;

  private AddEquipmentViewModel addEquipmentVM;
  public EquipmentsViewModel(IEquipmentService equipmentService, ILoggingService loggingService, IAuthService authService)
  {
    this.equipmentService = equipmentService;
    this.loggingService = loggingService;
    this.authService = authService;
    Equipments = equipmentService.GetEquipments();

    addEquipmentVM = new AddEquipmentViewModel(authService, equipmentService, loggingService);
    addEquipmentVM.OnClose += () => CurrentControl = null;

  }


  [RelayCommand]
  private void Search(string query)
  {
    Equipments = equipmentService.GetEquipmentsByQuery(query);
  }

  [RelayCommand]
  private void Add()
  {
    CurrentControl = addEquipmentVM;
  }

  [RelayCommand]
  private void Update()
  {
    Equipment? selection = Equipments.ElementAtOrDefault(SelectedIndex);
    if (selection is null) return;

    UpdateEquipmentViewModel updateEquipmentVM =
      new UpdateEquipmentViewModel(selection, authService, equipmentService, loggingService);
    CurrentControl = updateEquipmentVM;
    updateEquipmentVM.OnClose += () => CurrentControl = null;
  }

  [RelayCommand]
  private async Task Delete()
  {
    Equipment? selection = Equipments.ElementAtOrDefault(SelectedIndex);
    if (selection is null) return;

    var dr = await Dialog.Show("Are you sure you want to delete this equipment?", Dialog.Buttons.YesNo);
    if (dr == Dialog.DialogResult.Yes)
    {
      if (equipmentService.DeleteEquipment(selection.Assetnumber))
      {
        SelectedIndex = -1;
        loggingService.LogAction(
          Log.WithCurrentTimeStamp("Delete Equipment", "Equipment Management",
                (await authService.GetUser()).Username, selection.Assetnumber
        ));
        await Dialog.Show("Equipment deleted!", Dialog.Buttons.Ok);
      }
      else
      {
        await Dialog.Show("Error in deleting equipment.", Dialog.Buttons.Ok);
      }
    }
  }

  [RelayCommand]
  private void Refresh()
  {
    Equipments = equipmentService.GetEquipments();
  }
}