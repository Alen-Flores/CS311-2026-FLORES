using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;

namespace CS311_Midterm_Recitation2_Flores;

public partial class MainForm : Window

{

    private string username = "";

    public MainForm()
    {
        InitializeComponent();
        DataContext = this;
    }

    public MainForm(string username, string usertype) : this()
    {
        toolstripStatusLabel1.Text = username;
        toolstripStatusLabel2.Text = usertype;
        this.username = username;
    }

    [RelayCommand]
    private async Task Logout()
    {
        DialogWindow dialog = new DialogWindow("Are you sure you want to log out?", DialogWindow.Buttons.YesNo);
        var result = await dialog.ShowDialog<DialogWindow.DialogResult>(this);
        if (result == DialogWindow.DialogResult.Yes)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            Close();
        }
    }

    [RelayCommand]
    private async Task Accounts()
    {
        FrmAccounts frmAccounts = new FrmAccounts(username);
        await frmAccounts.ShowDialog(this);
    }
}
