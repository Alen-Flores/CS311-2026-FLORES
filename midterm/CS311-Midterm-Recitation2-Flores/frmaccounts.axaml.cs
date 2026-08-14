using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using CommunityToolkit.Mvvm.Input;
using ticket_management;

namespace CS311_Midterm_Recitation2_Flores;

public record User(
  string Username,
  string Usertype,
  string Status,
  string CreatedBy,
  string DateCreated);

public partial class FrmAccounts : Window
{

  private string username;

  public ObservableCollection<User> Users { get; } = [];

  public FrmAccounts()
  {
    InitializeComponent();
    DataContext = this;
    this.username = "FOO";
  }

  public FrmAccounts(string username) : this()
  {
    this.username = username;
  }

  Class1 accounts = new Class1(
    "127.0.0.1",
    "CS311-CS3A-2026-FLORES",
    "marlon",
    "flores");

  private void frmaccounts_Load(object sender, RoutedEventArgs e)
  {
    try
    {
      DataTable dt = accounts.GetData($"""
        SELECT username, usertype, status, createdby, datecreated
          FROM tblaccounts
          ORDER BY username
      """
      );

      foreach (DataRow row in dt.Rows)
      {
        Users.Add(new User(
          row["username"].ToString()!,
          row["usertype"].ToString()!,
          row["status"].ToString()!,
          row["createdby"].ToString()!,
          row["datecreated"].ToString()!
          ));
      }

    }
    catch (Exception err)
    {

      new DialogWindow("ERROR on frmaccounts_Load:" + err.Message, DialogWindow.Buttons.Ok);

    }

  }

  [RelayCommand]
  public async Task Search(string query)
  {
    DataTable dt = accounts.GetData($"""
        SELECT username, usertype, status, createdby, datecreated
          FROM tblaccounts
          WHERE username like '%{query}%'
             OR usertype like '%{query}%'
          ORDER BY username
      """
    );

    Users.Clear();
    foreach (DataRow row in dt.Rows)
    {
      Users.Add(new User(
        row["username"].ToString()!,
        row["usertype"].ToString()!,
        row["status"].ToString()!,
        row["createdby"].ToString()!,
        row["datecreated"].ToString()!
        ));
    }
  }

  [RelayCommand]
  public async Task Add()
  {
    FrmNewAccount frmNewAccount = new FrmNewAccount(username);
    await frmNewAccount.ShowDialog(this);
  }
}
