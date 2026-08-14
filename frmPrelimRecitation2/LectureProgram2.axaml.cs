using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace frmPrelimRecitation2;

public partial class LectureProgram2 : Window
{
    public LectureProgram2()
    {
        InitializeComponent();
    }

    Validators validators = new Validators();
    private int num1, num2, result;

    private void btnsubmit_Click(object sender, RoutedEventArgs e)
    {

        if (validators.validateTextBox(txtFirstNumber) is string s)
        {
            DataValidationErrors.SetError(txtFirstNumber, new CleanException(s));
        }
        else
        {
            DataValidationErrors.ClearErrors(txtFirstNumber);
        }

        if (validators.validateTextBox(txtSecondNumber) is string s2)
        {
            DataValidationErrors.SetError(txtSecondNumber, new CleanException(s2));
        }
        else
        {
            DataValidationErrors.ClearErrors(txtSecondNumber);
        }

        if (validators.countErrors() == 0)
        {
            num1 = int.Parse(txtFirstNumber.Text!);
            num2 = int.Parse(txtSecondNumber.Text!);
            if (rbAdd.IsChecked == true)
            {
                result = num1 + num2;
            }
            else if (rbSubtract.IsChecked == true)
            {
                result = num1 - num2;
            }
            else if (rbMultiply.IsChecked == true)
            {
                result = num1 * num2;
            }
            else if (rbDivide.IsChecked == true)
            {
                result = num1 / num2;
            }
            var dialog = new DialogWindow($"The result is: {result}");
            dialog.ShowDialog(this);
        }



    }

    private void txtFirstNumber_KeyDown(object sender, KeyEventArgs e)
    {
        if (validators.keyPressValidator(e, txtFirstNumber) is string s)
        {
            DataValidationErrors.SetError(txtFirstNumber, new CleanException(s));
        }
        else
        {
            DataValidationErrors.ClearErrors(txtFirstNumber);
        }
    }

    private void txtSecondNumber_KeyDown(object sender, KeyEventArgs e)
    {
        if (validators.keyPressValidator(e, txtSecondNumber) is string s)
        {
            DataValidationErrors.SetError(txtSecondNumber, new CleanException(s));
        }
        else
        {
            DataValidationErrors.ClearErrors(txtSecondNumber);
        }
    }

    private void btnclear_Click(object sender, RoutedEventArgs e)
    {
        validators.clearErrors();
        txtFirstNumber.Text = "";
        txtSecondNumber.Text = "";
        rbAdd.IsChecked = true;
        txtFirstNumber.Focus();
    }
}
