using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ticket_management;

namespace CS311_CS3A_2026_Flores.Services;

public interface IDatabaseService
{
  User? GetUserByUsername(string username);
  User? GetUserByLogin(string username, string password);
  List<User> GetUsers();
  List<User> GetUsersByQuery(string query);
  bool AddUser(User user);
  bool UpdateUser(User user);
  void LogAction(Log log);
  bool DeleteUser(string username);
}

public class DatabaseService : IDatabaseService
{
  private Class1 DBContext;
  public DatabaseService()
  {
    DBContext = new Class1("127.0.0.1", "CS311-CS3A-2026-FLORES", "marlon", "flores");
  }

  public User? GetUserByUsername(string username)
  {
    DataTable dt = DBContext.GetData($"""
    SELECT * FROM tblaccounts 
      WHERE username = '{username}'
      AND status = 'active'
    """);

    DataRow? row = dt.AsEnumerable().FirstOrDefault();
    if (row is null) return null;

    User user = new User(
      row.Field<string>("username")!,
      row.Field<string>("password")!,
      row.Field<string>("usertype")!,
      row.Field<string>("status")!,
      row.Field<string>("createdby")!,
      row.Field<string>("datecreated")!
    );

    return user;
  }

  public User? GetUserByLogin(string username, string password)
  {
    DataTable dt = DBContext.GetData($"""
    SELECT * FROM tblaccounts 
      WHERE username = '{username}'
      AND status = 'active'
    """);

    DataRow? row = dt.AsEnumerable().FirstOrDefault();
    if (row is null) return null;

    if (password != row.Field<string>("password")!) return null;

    User user = new User(
      row.Field<string>("username")!,
      row.Field<string>("password")!,
      row.Field<string>("usertype")!,
      row.Field<string>("status")!,
      row.Field<string>("createdby")!,
      row.Field<string>("datecreated")!
    );

    return user;
  }

  public List<User> GetUsers()
  {
    DataTable dt = DBContext.GetData($"""
        SELECT username, password, usertype, status, createdby, datecreated
          FROM tblaccounts
          ORDER BY username
      """);

    return dt.AsEnumerable()
      .Select(row => new User
      (
        row.Field<string>("username")!
        , row.Field<string>("password")!
        , row.Field<string>("usertype")!
        , row.Field<string>("status")!
        , row.Field<string>("createdby")!
        , row.Field<string>("datecreated")!
      )).ToList()
      ;
  }

  public List<User> GetUsersByQuery(string query)
  {
    DataTable dt = DBContext.GetData($"""
        SELECT username, password, usertype, status, createdby, datecreated
          FROM tblaccounts
          WHERE username like '%{query}%'
             OR usertype like '%{query}%'
          ORDER BY username
      """);

    return dt.AsEnumerable()
      .Select(row => new User
      (
        row.Field<string>("username")!
        , row.Field<string>("password")!
        , row.Field<string>("usertype")!
        , row.Field<string>("status")!
        , row.Field<string>("createdby")!
        , row.Field<string>("datecreated")!
      )).ToList()
      ;
  }

  public bool AddUser(User user)
  {
    DBContext.executeSQL($"""
    INSERT INTO tblaccounts (username, password, usertype, status,
                             createdby, datecreated)
      VALUES (
        "{user.Username}",
        "{user.Password}",
        "{user.Usertype}",
        "{user.Status}",
        "{user.CreatedBy}",
        "{user.DateCreated}"
      )
    """);

    return DBContext.rowAffected > 0;
  }

  public bool UpdateUser(User user)
  {
    DBContext.executeSQL($"""
    UPDATE tblaccounts
      SET password = "{user.Password}", 
          usertype = "{user.Usertype}",
          status = "{user.Status}"
      WHERE username = "{user.Username}"
    """);
    return DBContext.rowAffected > 0;
  }

  public void LogAction(Log log)
  {
    DBContext.executeSQL($"""
    INSERT INTO tbllogs (datelog, timelog, action, module, performedby, performedto)
      VALUES (
        "{log.datelog}",
        "{log.timelog}",
        "{log.action}",
        "{log.module}",
        "{log.performedby}",
        "{log.performedto}"
      )
    """);
  }

  public bool DeleteUser(string username)
  {
    DBContext.executeSQL($"""
    DELETE from tblaccounts
      WHERE username = "{username}"
    """);

    return DBContext.rowAffected > 0;
  }
}