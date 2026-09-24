using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CS311_CS3A_2026_Flores.Models;
using CS311_CS3A_2026_Flores.Services;
using CS311_CS3A_2026_Flores.Views;

namespace CS311_CS3A_2026_Flores.ViewModels;

public partial class UpdateTicketViewModel : ObservableValidator
{

  public event Action? exit;

  [ObservableProperty]
  private Ticket _target;

  [ObservableProperty]
  [Required]
  private ProblemType? _problem;

  [ObservableProperty]
  private string? _details;

  private readonly ITicketService _ticketService;
  private readonly IAuthService _authService;
  private readonly ILoggingService _loggingService;

  public UpdateTicketViewModel(
    ITicketService ticketService,
    IAuthService authService,
    ILoggingService loggingService,
    Ticket target
    )
  {
    _ticketService = ticketService;
    _authService = authService;
    _loggingService = loggingService;
    _target = target;
    _problem = target.Problem;
    _details = target.Details;
  }

  [RelayCommand]
  public async Task Save()
  {
    ValidateAllProperties();
    if (HasErrors || Problem is null)
    {
      return;
    }

    _ticketService.UpdateTicket(Target with {
        Problem = Problem.Value,
        Details = Details!
    });
    _loggingService.LogAction(Log.WithCurrentTimeStamp(
     "Updated Ticket",
     "Ticket Management",
     (await _authService.GetUser()).Username,
     Target.TicketNumber
    ));
    await Dialog.Show("Updated ticket", Dialog.Buttons.Ok);
    exit?.Invoke();
  }

  [RelayCommand]
  public void Cancel()
  {
    exit?.Invoke();
  }
}
