using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;

namespace CS311_midterm_recitation1_flores;

public partial class MainForm : Window

{
    public MainForm(string username, string usertype)
    {
        InitializeComponent();
        DataContext = this;
        toolstripStatusLabel1.Text = username;
        toolstripStatusLabel2.Text = usertype;
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
}
