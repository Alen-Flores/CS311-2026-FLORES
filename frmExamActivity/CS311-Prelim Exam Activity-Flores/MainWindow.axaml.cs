using System;
using Avalonia.Controls;

namespace CS311_Prelim_Exam_Activity_Flores;

public partial class MainWindow : Window
{

    Validators validators;
    double grossPay = 0.0;
    double tax = 0.0;
    double sss = 0.0;
    double pagibig = 0.0;
    double philhealth = 0.0;
    double totalDeductions = 0.0;
    double netPay = 0.0;
    double additionalPay = 0.0;
    double totalPay = 0.0;


    public MainWindow()
    {
        InitializeComponent();
        validators = new Validators();
    }

    private void SubmitButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        double rate = 0;
        double hours = 0;
        if (validators.validateTextBox(in RateTextBox) is string s)
        {
            DataValidationErrors.SetError(RateTextBox, new CleanException(s));
        }
        else if (Double.TryParse(RateTextBox.Text, out rate) && (rate < 755 || rate > 3000))
        {
            DataValidationErrors.SetError(RateTextBox, new CleanException("Rate must be between 755 to 3000"));
        }
        else
        {
            DataValidationErrors.ClearErrors(RateTextBox);
        }

        if (validators.validateTextBox(in HoursTextBox) is string s2)
        {
            DataValidationErrors.SetError(HoursTextBox, new CleanException(s2));
        }
        else if (Double.TryParse(HoursTextBox.Text, out hours) && (hours < 40 || hours > 200))
        {
            DataValidationErrors.SetError(HoursTextBox, new CleanException("Hours must be between 40 to 200"));
        }
        else
        {
            DataValidationErrors.ClearErrors(HoursTextBox);
        }

        if (validators.countErrors() == 0)
        {
            grossPay = rate * hours;
            tax = grossPay * 0.12;

            sss = cbSSS.IsChecked.GetValueOrDefault(false) ? grossPay * 0.10 : 0.0;
            pagibig = cbPagibig.IsChecked.GetValueOrDefault(false) ? grossPay * 0.08 : 0.0;
            philhealth = cbPhilhealth.IsChecked.GetValueOrDefault(false) ? grossPay * 0.06 : 0.0;

            totalDeductions = sss + pagibig + philhealth;
            netPay = grossPay - totalDeductions;

            additionalPay = (bool)rbPermanent.IsChecked! ? 500.0 : 0.0;


            totalPay = netPay + additionalPay;

            GrossPayTextBox.Text = $"{grossPay:F2}";
            TaxTextBox.Text = $"{tax:F2}";
            SSSTextBox.Text = sss == 0.0 ? "" : $"{sss:F2}";
            PagibigTextBox.Text = pagibig == 0.0 ? "" : $"{pagibig:F2}";
            PhilhealthTextBox.Text = philhealth == 0.0 ? "" : $"{philhealth:F2}";
            TotalDeductionsTextBox.Text = $"{totalDeductions:F2}";
            NetPayTextBox.Text = $"{netPay:F2}";
            AdditionalPayTextBox.Text = $"{additionalPay:F2}";
            TotalPayTextBox.Text = $"{totalPay:F2}";
        }
    }

    private void ClearButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        RateTextBox.Text = "";
        HoursTextBox.Text = "";
        cbPagibig.IsChecked = false;
        cbPhilhealth.IsChecked = false;
        cbSSS.IsChecked = false;
        rbContractual.IsChecked = true;
        rbPermanent.IsChecked = false;

        GrossPayTextBox.Text = "";
        TaxTextBox.Text = "";

        SSSTextBox.Text = "";
        PagibigTextBox.Text = "";
        PhilhealthTextBox.Text = "";

        TotalDeductionsTextBox.Text = "";
        NetPayTextBox.Text = "";
        AdditionalPayTextBox.Text = "";
        TotalPayTextBox.Text = "";


        grossPay = 0.0;
        tax = 0.0;
        sss = 0.0;
        pagibig = 0.0;
        philhealth = 0.0;
        totalDeductions = 0.0;
        netPay = 0.0;
        additionalPay = 0.0;
        totalPay = 0.0;
        DataValidationErrors.ClearErrors(RateTextBox);
        DataValidationErrors.ClearErrors(HoursTextBox);
    }
}
