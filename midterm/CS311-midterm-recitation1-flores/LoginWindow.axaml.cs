using System;
using System.Data;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using ticket_management;

namespace CS311_midterm_recitation1_flores;

public partial class LoginWindow : Window
{
    public LoginWindow()
    {
        InitializeComponent();
    }

    Class1 login = new Class1("127.0.0.1", "CS311-CS3A-2026-FLORES", "marlon", "flores");

    void btnlogin_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            DataTable dt = login.GetData($"""
                SELECT * FROM tblaccounts 
                    WHERE username = '{txtUsername.Text}'
                    AND password = '{txtPassword.Text}'
                    AND status = 'active'
            """);

            if (dt.Rows.Count > 0)
            {
                MainForm mainForm = new MainForm(txtUsername.Text!, dt.Rows[0].Field<string>("usertype")!);
                mainForm.Show();
                Hide();
            }
            else
            {
                new DialogWindow("Incorrect account details or account is inactive", DialogWindow.Buttons.Ok).ShowDialog(this);
            }
        }
        catch (Exception error)

        {
            var msgbox = new DialogWindow($"ERROR on btnlogin_click", DialogWindow.Buttons.Ok);
            Console.WriteLine(error);
            msgbox.ShowDialog(this);
        }

    }

    void btnclear_Click(object sender, RoutedEventArgs e)
    {
        txtUsername.Clear();
        txtPassword.Clear();
        cbShowPassword.IsChecked = false;
        txtUsername.Focus();
    }

    void txtPassword_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            btnlogin_Click(sender, e);
        }
    }



}