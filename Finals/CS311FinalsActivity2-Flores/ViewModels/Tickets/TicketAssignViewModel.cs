using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CS311_CS3A_2026_Flores.Models;
using CS311_CS3A_2026_Flores.Services;
using CS311_CS3A_2026_Flores.Views;

namespace CS311_CS3A_2026_Flores.ViewModels;

public partial class TicketAssignViewModel : ObservableValidator
{
  public event Action? exit;

  [ObservableProperty]
  private Ticket _ticket;

  public static ValidationResult? ValidateInRange(
    int selectedUser,
    ValidationContext context
    )
  {
    var viewModel = (TicketAssignViewModel)context.ObjectInstance;

    if (selectedUser < 0 || selectedUser > viewModel.TechnicalUsers.Count)
      return new ValidationResult($"Select a valid user");

    return ValidationResult.Success;
  }

  [ObservableProperty]
  [CustomValidation(typeof(TicketAssignViewModel), nameof(ValidateInRange))]
  [Required]
  private int _selectedUser;


  [ObservableProperty]
  private List<string> _technicalUsers;

  private readonly ITicketService _ticketService;
  private readonly ILoggingService _loggingService;
  private readonly IAuthService _authService;

  public TicketAssignViewModel(
    Ticket ticket, 
    IUserService userService,
    ITicketService ticketService,
    ILoggingService loggingService,
    IAuthService authService
    )
  {
    _ticket = ticket;
    _ticketService = ticketService;
    _loggingService = loggingService;
    _authService = authService;
    _technicalUsers = userService.GetUsers()
        .FindAll(x => x.Usertype == "TECHNICAL")
        .Select( x => x.Username )
        .ToList();
  }

  [RelayCommand]
  private void Close()
  {
    exit?.Invoke();
  }

  [RelayCommand]
  private async Task Save()
  {
    ValidateAllProperties();
    if (HasErrors) return;

    var target = Ticket with
    {
      AssignedTo = TechnicalUsers[SelectedUser],
      DateAssigned = DateTime.Now
    };

    _ticketService.UpdateTicket(target);
    _loggingService.LogAction(Log.WithCurrentTimeStamp(
     "Assign Ticket",
     "Ticket Management",
     (await _authService.GetUser()).Username,
     target.TicketNumber
    ));

    await Dialog.Show("Assigned ticket", Dialog.Buttons.Ok);
    exit?.Invoke();
  }
}
