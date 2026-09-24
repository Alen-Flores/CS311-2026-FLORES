using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CS311_CS3A_2026_Flores.Models;
using CS311_CS3A_2026_Flores.Services;
using CS311_CS3A_2026_Flores.Views;

namespace CS311_CS3A_2026_Flores.ViewModels;

public partial class AddTicketViewModel : ObservableValidator
{
  [ObservableProperty]
  private string _ticketNumber;

  [ObservableProperty]
  [Required]
  private ProblemType? _problem;

  [ObservableProperty]
  private string? _details;

  public event Action? exit;

  private readonly ITicketService _ticketService;
  private readonly IAuthService _authService;
  private readonly ILoggingService _loggingService;

  public AddTicketViewModel(
    ITicketService ticketService,
    IAuthService authService,
    ILoggingService loggingService
    )
  {
    _ticketNumber = DateTime.Now.ToString("yyyyMMddHHmmss");
    _ticketService = ticketService;
    _authService = authService;
    _loggingService = loggingService;
  }

  [RelayCommand]
  private async Task Save()
  {
    ValidateAllProperties();
    if (HasErrors || Problem is null)
    {
      return;
    }

    _ticketService.AddTicket(new Ticket(
      TicketNumber,
       Problem.Value,
       Details!,
       StatusType.Pending,
       (await _authService.GetUser()).Username,
       DateTime.Now,
       null, null, null, null, null
    ));
    _loggingService.LogAction(Log.WithCurrentTimeStamp(
     "Created Ticket",
     "Ticket Management",
     (await _authService.GetUser()).Username,
     TicketNumber
    ));
    await Dialog.Show("Created new ticket", Dialog.Buttons.Ok);
    exit?.Invoke();
  }

  [RelayCommand]
  private void Cancel()
  {
    exit?.Invoke();
  }
}
