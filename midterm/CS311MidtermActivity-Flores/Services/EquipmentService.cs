using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CS311_CS3A_2026_Flores.Services;

public interface IEquipmentService
{
  List<Equipment> GetEquipments();
  Equipment? GetEquipmentBySerialnumber(string serialnumber);
  List<Equipment> GetEquipmentsByQuery(string query);
  bool AddEquipment(Equipment equipment);
  bool UpdateEquipment(Equipment equipment);
  bool DeleteEquipment(string assetnumber);
}

public class EquipmentService : IEquipmentService
{

  private IDatabaseService dbService;
  public EquipmentService(IDatabaseService dbService)
  {
    this.dbService = dbService;
  }
  private Equipment equipmentFromRow(DataRow row)
  {
    return new Equipment
      (
        row.Field<string>("assetnumber")!,
        row.Field<string>("serialnumber")!,
        row.Field<string>("type")!,
        row.Field<string>("manufacturer")!,
        row.Field<string>("yearmodel")!,
        row.Field<string>("description")!,
        row.Field<string>("branch")!,
        row.Field<string>("department")!,
        row.Field<string>("status")!,
        row.Field<string>("createdby")!,
        row.Field<string>("datecreated")!
      );
  }

  public List<Equipment> GetEquipments()
  {
    DataTable dt = dbService.GetData($"""
        SELECT * FROM tblequipments
          ORDER BY assetnumber
      """);

    return dt.AsEnumerable().Select(equipmentFromRow).ToList();
  }

  public List<Equipment> GetEquipmentsByQuery(string query)
  {
    DataTable dt = dbService.GetData($"""
        SELECT * FROM tblequipments
          WHERE assetnumber like '%{query}%'
             OR serialnumber like '%{query}%'
             OR type like '%{query}%'
             OR branch like '%{query}%'
          ORDER BY assetnumber
      """);

    return dt.AsEnumerable().Select(equipmentFromRow).ToList();
  }

  public Equipment? GetEquipmentBySerialnumber(string serialnumber)
  {

    DataTable dt = dbService.GetData($"""
        SELECT * FROM tblequipments 
          WHERE serialnumber = '{serialnumber}'
      """);

    DataRow? row = dt.AsEnumerable().FirstOrDefault();
    if (row == null) return null;

    return equipmentFromRow(row);
  }

  public bool AddEquipment(Equipment equipment)
  {
    int rowsAffected = dbService.executeSQL($"""
    INSERT INTO tblequipments (
     assetnumber, 	
     serialnumber, 	
     type, 	
     manufacturer, 	
     yearmodel, 	
     description, 	
     branch, 	
     department, 	
     status, 	
     createdby, 	
     datecreated 
    ) VALUES (
     "{equipment.Assetnumber}", 	
     "{equipment.Serialnumber}", 	
     "{equipment.Type}", 	
     "{equipment.Manufacturer}", 	
     "{equipment.Yearmodel}", 	
     "{equipment.Description}", 	
     "{equipment.Branch}", 	
     "{equipment.Department}", 	
     "{equipment.Status}", 	
     "{equipment.Createdby}", 	
     "{equipment.Datecreated}"
      )
    """);
    return rowsAffected > 0;
  }

  public bool UpdateEquipment(Equipment equipment)
  {
    int rowAffected = dbService.executeSQL($"""
    UPDATE tblequipments
      SET serialnumber = '{equipment.Serialnumber}',
          type = '{equipment.Type}',
          manufacturer = '{equipment.Manufacturer}',
          yearmodel = '{equipment.Yearmodel}',
          description = '{equipment.Description}',
          branch = '{equipment.Branch}',
          department = '{equipment.Department}',
          status = '{equipment.Status}'
      WHERE assetnumber = '{equipment.Assetnumber}'
    """);
    return rowAffected > 0;
  }

  public bool DeleteEquipment(string assetnumber)
  {
    int rowAffected = dbService.executeSQL($"""
    DELETE from tblequipments
      WHERE assetnumber = '{assetnumber}'
    """);
    return rowAffected > 0;
  }
}