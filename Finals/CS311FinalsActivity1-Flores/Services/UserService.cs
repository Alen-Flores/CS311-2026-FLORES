using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CS311_CS3A_2026_Flores.Services;

public interface IUserService
{
  User? GetUserByUsername(string username);
  User? GetUserByLogin(string username, string password);
  List<User> GetUsers();
  List<User> GetUsersByQuery(string query);
  bool AddUser(User user);
  bool UpdateUser(User user);
  bool DeleteUser(string username);
}

public class UserService : IUserService
{
  private DatabaseService dbService;
  public UserService(DatabaseService dbService)
  {
    this.dbService = dbService;
  }

  public User? GetUserByUsername(string username)
  {
    DataTable dt = dbService.GetData($"""
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
    DataTable dt = dbService.GetData($"""
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
    DataTable dt = dbService.GetData($"""
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
    DataTable dt = dbService.GetData($"""
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
    int rowAffected = dbService.executeSQL($"""
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

    return rowAffected > 0;
  }

  public bool UpdateUser(User user)
  {
    int rowsAffected = dbService.executeSQL($"""
    UPDATE tblaccounts
      SET password = "{user.Password}", 
          usertype = "{user.Usertype}",
          status = "{user.Status}"
      WHERE username = "{user.Username}"
    """);
    return rowsAffected > 0;
  }


  public bool DeleteUser(string username)
  {
    int rowAffected = dbService.executeSQL($"""
    DELETE from tblaccounts
      WHERE username = "{username}"
    """);

    return rowAffected > 0;
  }
}