using System;
using System.Data;

namespace CS311_CS3A_2026_Flores.Models;

public enum ProblemType
{
  Hardware, Software, Connection
};

public enum StatusType
{
  Pending
}


public record Ticket(
  string TicketNumber,
  ProblemType Problem,
  string Details,
  StatusType Status,
  string CreatedBy,
  DateTime DateCreated,
  string? AssignedTo,
  DateTime? DateAssigned,
  DateTime? DateCompleted,
  string? ApprovedBy,
  DateTime? DateApproved
)
{

  public static ProblemType[] ProblemTypes { get; }
    = Enum.GetValues<ProblemType>();
  public static Ticket FromDataRow(DataRow row)
  {
    return new Ticket(
      row.Field<string>("TicketNumber")!,
      Enum.Parse<ProblemType>(row.Field<string>("Problem")!),
      row.Field<string>("Details")!,
      Enum.Parse<StatusType>(row.Field<string>("Status")!),
      row.Field<string>("CreatedBy")!,
      row.Field<DateTime>("DateCreated")!,
      row.Field<string?>("AssignedTo")!,
      row.Field<DateTime?>("DateAssigned")!,
      row.Field<DateTime?>("DateCompleted")!,
      row.Field<string?>("ApprovedBy")!,
      row.Field<DateTime?>("DateApproved")!
    );
  }
}