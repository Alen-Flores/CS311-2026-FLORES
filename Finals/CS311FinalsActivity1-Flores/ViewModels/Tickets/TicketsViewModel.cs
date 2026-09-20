using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CS311_CS3A_2026_Flores.Models;
using CS311_CS3A_2026_Flores.Services;
using CS311_CS3A_2026_Flores.Views;

namespace CS311_CS3A_2026_Flores.ViewModels;

public partial class TicketsViewModel : ObservableObject
{

  [ObservableProperty]
  object? _currentControl;

  [ObservableProperty]
  private List<Ticket> _tickets;

  [ObservableProperty]
  private int _selectedIndex;

  private readonly ITicketService _ticketService;
  private readonly IAuthService _authService;
  private readonly ILoggingService _loggingService;

  public TicketsViewModel(
    ITicketService ticketService,
    IAuthService authService,
    ILoggingService loggingService
    )
  {
    _ticketService = ticketService;
    _authService = authService;
    _loggingService = loggingService;
    _tickets = ticketService.GetTicketsByUser(
      authService.GetUser().GetAwaiter().GetResult().Username
    );
  }

  private bool CurrentIsNull()
  {
    return CurrentControl is null;
  }

  [RelayCommand(CanExecute = nameof(CurrentIsNull))]
  private void Add()
  {
    var addTicketViewModel =
      new AddTicketViewModel(_ticketService, _authService, _loggingService);
    addTicketViewModel.exit += () => CurrentControl = null;
    CurrentControl = addTicketViewModel;
  }

  [RelayCommand(CanExecute = nameof(CurrentIsNull))]
  private void Update()
  {
    var target = Tickets[SelectedIndex];
    var addTicketViewModel =
      new UpdateTicketViewModel(_ticketService, _authService, _loggingService, target);
    addTicketViewModel.exit += () => CurrentControl = null;
    CurrentControl = addTicketViewModel;
  }

  [RelayCommand(CanExecute = nameof(CurrentIsNull))]
  private async Task Delete()
  {
    var target = Tickets[SelectedIndex];
    var choice = await Dialog.Show("Are you sure you want to delete this ticket?",
       Dialog.Buttons.YesNo);
    switch (choice)
    {
      case Dialog.DialogResult.Yes:
        _ticketService.DeleteTicket(target.TicketNumber);
        _loggingService.LogAction(
          Log.WithCurrentTimeStamp("Delete Ticket", "Ticket Management",
                (await _authService.GetUser()).Username, target.TicketNumber
        ));
        SelectedIndex = -1;
        break;
      default:
        break;
    }
  }

  [RelayCommand(CanExecute = nameof(CurrentIsNull))]
  private async Task Refresh()
  {
    SelectedIndex = -1;
    Tickets = _ticketService.GetTicketsByUser(
      (await _authService.GetUser()).Username
    );
  }

  [RelayCommand]
  private async Task Search(string query)
  {
    Tickets = _ticketService.QueryTickets(
      query,
      (await _authService.GetUser()).Username
    );
  }
}