using System.Collections.Generic;

// assetnumber increased from 20 to 25
//   (example used a 21 length string)

// Increased Department length from 20 to 50

public record class Equipment(
  string Assetnumber,
  string Serialnumber,
  string Type,
  string Manufacturer,
  string Yearmodel,
  string Description,
  string Branch,
  string Department,
  string Status,
  string Createdby,
  string Datecreated
)
{
  public static readonly List<string> Branches = [
   "JSC", "JRC", "EEC", "JASC", "PC", "ABC"
  ];

  public static readonly List<string> Types = [
    "Monitor", "CPU", "Keyboard", "Mouse", "AVR", "MAC", "Printer", "Projector"
  ];

  public static readonly List<string> Statuses = [
    "WORKING", "ON-REPAIR", "RETIRED"
  ];

  public static readonly List<string> Departments = [
    "Graduate School Of Education",
    "Graduate School Of Nursing",
    "Graduate School Of Business",
    "College Of Arts And Sciences",
    "College Of Criminal Justice Education",
    "Institute Of Accountancy",
    "School Of Computer Science",
    "School Of Business And Administration",
    "School Of Education",
    "School Of Hospitality And Tourism Management"
  ];
}
