using System;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace frmPrelimRecitation2;

public partial class LectureProgram3 : Window
{
    public LectureProgram3()
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
            if (!cbAdd.IsChecked!.Value && !cbSubtract.IsChecked!.Value && !cbMultiply.IsChecked!.Value && !cbDivide.IsChecked!.Value)
            {
                var errdialog = new DialogWindow("No operation was selected.");
                errdialog.ShowDialog(this);
                return;
            }

            num1 = int.Parse(txtFirstNumber.Text!);
            num2 = int.Parse(txtSecondNumber.Text!);

            string msg = "";
            if (cbAdd.IsChecked!.Value)
            {
                result = num1 + num2;
                msg = $"Sum: {result}" + Environment.NewLine;
            }

            if (cbSubtract.IsChecked!.Value)
            {
                result = num1 - num2;
                msg += $"Difference: {result}" 
                    + Environment.NewLine;
            }

            if (cbMultiply.IsChecked!.Value)
            {
                result = num1 * num2;
                msg += $"Product: {result}"
                    + Environment.NewLine;
            }
            
            if (cbDivide.IsChecked!.Value)
            {
                result = num1 / num2;
                msg += $"Quotient: {result}"
                    + Environment.NewLine;
            }
            var dialog = new DialogWindow(msg);
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
        cbAdd.IsChecked = false;
        cbSubtract.IsChecked = false;
        cbMultiply.IsChecked = false;
        cbDivide.IsChecked = false;
        txtFirstNumber.Focus();
    }
}
