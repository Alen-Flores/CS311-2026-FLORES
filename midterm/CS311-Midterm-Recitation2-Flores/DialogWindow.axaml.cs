
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using CommunityToolkit.Mvvm.Input;
namespace CS311_Midterm_Recitation2_Flores;

public partial class DialogWindow : Window
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

    public DialogWindow(string message, Buttons buttons)
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
            this.Close();
        }
    }

}
