using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CS311_CS3A_2026_Flores.Models;

namespace CS311_CS3A_2026_Flores.Services;

public interface ITicketService
{
  List<Ticket> GetTickets();
  List<Ticket> QueryTickets(string query, string username);
  Ticket? GetTicket(int TicketNumber);
  List<Ticket> GetTicketsByUser(string username);
  void AddTicket(Ticket ticket);
  void UpdateTicket(Ticket ticket);
  void DeleteTicket(string TicketNumber);
}

public class TicketService : ITicketService
{

  private DatabaseService dbService;
  public TicketService(DatabaseService dbService)
  {
    this.dbService = dbService;
  }

  public Ticket? GetTicket(int TicketNumber)
  {
    DataTable dt = dbService.GetData($"""
    SELECT * FROM tbltickets
      WHERE TicketNumber = '{TicketNumber}'
    """);

    DataRow? row = dt.AsEnumerable().FirstOrDefault();
    if (row is null) return null;
    return Ticket.FromDataRow(row);
  }

  public List<Ticket> GetTickets()
  {
    DataTable dt = dbService.GetData($"""
    SELECT * FROM tbltickets
      ORDER BY DateApproved DESC
    """);
    return dt.AsEnumerable().Select(Ticket.FromDataRow).ToList();
  }

  public List<Ticket> GetTicketsByUser(string username)
  {
    DataTable dt = dbService.GetData($"""
    SELECT * FROM tbltickets
      WHERE AssignedTo = '{username}'
      ORDER BY DateApproved DESC
    """);
    return dt.AsEnumerable().Select(Ticket.FromDataRow).ToList();
  }

  public List<Ticket> QueryTickets(string query, string username)
  {
    DataTable dt = dbService.GetData($"""
    SELECT * FROM tbltickets
      WHERE TicketNumber LIKE '%{query}%'
        OR  Problem LIKE '%{query}%'
        OR  Status LIKE '%{query}%'
        AND CreatedBy = '{username}'
      ORDER BY DateApproved DESC
    """);
    return dt.AsEnumerable().Select(Ticket.FromDataRow).ToList();
  }

  private string DatetimeOrNull(DateTime? dateTime)
  {
    if (dateTime.HasValue)
    {
      return $"\"{dateTime:yyyy-MM-dd HH:mm:ss}\"";
    }
    else
    {
      return "NULL";
    }
  }

  private string ToStringOrNull(object? ob)
  {
    if (ob is null)
    {
      return "NULL";
    }
    return "\"" + ob.ToString() + "\"";
  }

  public void AddTicket(Ticket ticket)
  {
    dbService.executeSQL($"""
    INSERT INTO tbltickets (
      TicketNumber, Problem, Details,
      Status, CreatedBy, DateCreated,
      AssignedTo, DateAssigned, DateCompleted,
      ApprovedBy, DateApproved
    )
      VALUES (
        "{ticket.TicketNumber}",
        "{ticket.Problem}",
        "{ticket.Details}",
        "{ticket.Status}",
        "{ticket.CreatedBy}",
        {DatetimeOrNull(ticket.DateCreated)},
        {ToStringOrNull(ticket.AssignedTo)},
        {DatetimeOrNull(ticket.DateAssigned)},
        {DatetimeOrNull(ticket.DateCompleted)},
        {ToStringOrNull(ticket.ApprovedBy)},
        {DatetimeOrNull(ticket.DateApproved)}
      )
    """);
  }

  public void UpdateTicket(Ticket ticket)
  {
    dbService.executeSQL($"""
    UPDATE tbltickets
      SET Problem = {ToStringOrNull(ticket.Problem)},
          Details = {ToStringOrNull(ticket.Details)},
          Status = {ToStringOrNull(ticket.Status)},
          CreatedBy = {ToStringOrNull(ticket.CreatedBy)},
          DateCreated = {DatetimeOrNull(ticket.DateCreated)},
          AssignedTo = {ToStringOrNull(ticket.AssignedTo)},
          DateAssigned = {DatetimeOrNull(ticket.DateAssigned)},
          DateCompleted = {DatetimeOrNull(ticket.DateCompleted)},
          ApprovedBy = {ToStringOrNull(ticket.ApprovedBy)},
          DateApproved = {DatetimeOrNull(ticket.DateApproved)}
      WHERE TicketNumber = '{ticket.TicketNumber}'
    """);
  }

  public void DeleteTicket(string TicketNumber)
  {
    dbService.executeSQL($"""
    DELETE FROM tbltickets
      WHERE TicketNumber = '{TicketNumber}'
    """);
  }
}