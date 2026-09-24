using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Input;
using CommunityToolkit.Mvvm.Input;

namespace CS311_CS3A_2026_Flores.Views;

public partial class Dialog : Window
{
    public enum Buttons
    {
        Ok,
        YesNo

    }

    public enum DialogResult
    {
        None,
        Ok,
        Cancel,
        Yes,
        No
    }

    public Buttons DialogButtons { get; }
    public DialogResult Result { get; private set; } = DialogResult.None;

    public bool ShowOk => DialogButtons == Buttons.Ok;
    public bool ShowYes => DialogButtons == Buttons.YesNo;
    public bool ShowNo => DialogButtons == Buttons.YesNo;

    public Dialog() : this(String.Empty, Buttons.Ok)
    {
    }
    
    public Dialog(string message) : this(message, Buttons.Ok)
    {
    }

    public Dialog(string message, Buttons buttons)
    {
        DialogButtons = buttons;
        InitializeComponent();
        DataContext = this;
        txtMessage.Text = message;
    }


    [RelayCommand]
    private void SelectResult(DialogResult result)
    {
        Result = result;
        Close(result);
    }


    private void onDialog_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Escape)
        {
            Close();
        }
    }

    public static async Task<DialogResult?> Show(string message, Buttons buttons)
    {
        Dialog dialog = new Dialog(message, buttons);

        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            return await dialog.ShowDialog<DialogResult>(desktop.MainWindow!);
        }

        throw new NullReferenceException("No current application instance");
    }
}
