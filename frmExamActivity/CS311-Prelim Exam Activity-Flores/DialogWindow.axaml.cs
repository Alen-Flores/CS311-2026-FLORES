
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace CS311_Prelim_Exam_Activity_Flores;

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