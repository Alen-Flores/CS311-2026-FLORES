using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CS311_CS3A_2026_Flores.Models;

namespace CS311_CS3A_2026_Flores.ViewModels;

public partial class DetailsViewModel : ObservableObject
{
  public event Action? exit;

  [ObservableProperty]
  Ticket _ticket;

  public DetailsViewModel(Ticket ticket)
  {
    _ticket = ticket;
  }

  [RelayCommand]
  private void Close()
  {
    exit?.Invoke();
  }
}
