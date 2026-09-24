using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CS311_CS3A_2026_Flores.Models;
using CS311_CS3A_2026_Flores.Services;
using CS311_CS3A_2026_Flores.Views;

namespace CS311_CS3A_2026_Flores.ViewModels;

public partial class TicketsViewModel : ObservableValidator
{

  [ObservableProperty]
  object? _currentControl;

  [ObservableProperty]
  private List<Ticket> _tickets;

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
  [CustomValidation(typeof(TicketsViewModel), nameof(ValidateInRange))]
  private int _selectedIndex;

  private readonly ITicketService _ticketService;
  private readonly IAuthService _authService;
  private readonly ILoggingService _loggingService;
  private readonly IUserService _userService;

  [ObservableProperty]
  private bool _isAdmin;
  [ObservableProperty]
  private bool _isTechnical;

  public TicketsViewModel(
    ITicketService ticketService,
    IAuthService authService,
    ILoggingService loggingService,
    IUserService userService
    )
  {
    _ticketService = ticketService;
    _authService = authService;
    _loggingService = loggingService;
    _userService = userService;
    var user = authService.GetUser().GetAwaiter().GetResult();


    IsAdmin = user.Usertype == "ADMINISTRATOR";
    IsTechnical = user.Usertype == "TECHNICAL";

    _tickets = IsAdmin
      ? ticketService.GetTickets()
      : ticketService.GetTicketsByUser(user.Username);

    _authService.AccountChanged += async (e) =>
    {
      IsAdmin = e?.Usertype == "ADMINISTRATOR";
      IsTechnical = e?.Usertype == "TECHNICAL";
      await Refresh();
    };
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
    var updateTicketViewModel =
      new UpdateTicketViewModel(_ticketService, _authService, _loggingService, target);
    updateTicketViewModel.exit += () => CurrentControl = null;
    CurrentControl = updateTicketViewModel;
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
    var user = await _authService.GetUser();
    Tickets = IsAdmin
      ? _ticketService.GetTickets()
      : _ticketService.GetTicketsByUser(user.Username);

  }

  [RelayCommand]
  private async Task Search(string query)
  {
    Tickets = _ticketService.QueryTickets(
      query,
      (await _authService.GetUser()).Username
    );
  }

  [RelayCommand(CanExecute = nameof(CurrentIsNull))]
  private void Details()
  {
    if (SelectedIndex > Tickets.Count || SelectedIndex < 0) return;
    var target = Tickets[SelectedIndex];
    var detailsVM = new DetailsViewModel(target);
    detailsVM.exit += () => CurrentControl = null;
    CurrentControl = detailsVM;
  }

  [RelayCommand(CanExecute = nameof(CurrentIsNull))]
  private void Assign()
  {
    if (SelectedIndex > Tickets.Count || SelectedIndex < 0) return;
    var target = Tickets[SelectedIndex];
    var assignTicket = new TicketAssignViewModel(
      target, _userService, _ticketService, _loggingService, _authService
    );
    assignTicket.exit += () => CurrentControl = null;
    CurrentControl = assignTicket;
  }

  [RelayCommand(CanExecute = nameof(CurrentIsNull))]
  private async Task Approve()
  {
    var user = await _authService.GetUser();
    var target = Tickets[SelectedIndex];
    var choice = await Dialog.Show("Are you sure you want to approve this ticket?",
       Dialog.Buttons.YesNo);
    switch (choice)
    {
      case Dialog.DialogResult.Yes:
        _ticketService.UpdateTicket(target with
        {
          Status = StatusType.Closed,
          DateApproved = DateTime.Now,
          ApprovedBy = user.Username
        });
        _loggingService.LogAction(
          Log.WithCurrentTimeStamp("Approve Ticket", "Ticket Management",
                user.Username, target.TicketNumber
        ));
        break;
      default:
        break;
    }
  }

  [RelayCommand(CanExecute = nameof(CurrentIsNull))]
  private async Task Complete()
  {
    var user = await _authService.GetUser();
    var target = Tickets[SelectedIndex];
    var choice = await Dialog.Show("Are you sure you want to mark this ticket as complete?",
       Dialog.Buttons.YesNo);
    switch (choice)
    {
      case Dialog.DialogResult.Yes:
        _ticketService.UpdateTicket(target with
        {
          Status = StatusType.For_Approval,
          DateCompleted = DateTime.Now
        });
        _loggingService.LogAction(
          Log.WithCurrentTimeStamp("Complete Ticket", "Ticket Management",
                user.Username, target.TicketNumber
        ));
        break;
      default:
        break;
    }
  }
}