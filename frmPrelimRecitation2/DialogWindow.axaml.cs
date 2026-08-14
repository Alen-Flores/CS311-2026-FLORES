
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace frmPrelimRecitation2;

public partial class DialogWindow : Window
{
    public DialogWindow()
    {
        InitializeComponent();
    }
    
    public DialogWindow(string message)
    {
        InitializeComponent();
        txtMessage.Text = message;
    }

    private void OnOkClick(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
    
}