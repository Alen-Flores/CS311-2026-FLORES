using System;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace frmPrelimActivity1;

public class CleanException : System.Exception
{
    public CleanException(string message) : base(message) { }

    // Overriding this removes the "System.Exception:" prefix when printing the exception
    public override string ToString()
    {
        return Message;
    }
}

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private bool parseOpposite(out double opposite)
    {
        DataValidationErrors.ClearErrors(OppositeInput);
        if (String.IsNullOrEmpty(OppositeInput.Text))
        {
            DataValidationErrors.SetError(OppositeInput,
            new CleanException("Opposite should not be empty."));
            opposite = 0;
            return false;
        }

        if (!double.TryParse(OppositeInput.Text, out opposite))
        {
            DataValidationErrors.SetError(OppositeInput,
            new CleanException("Opposite should be a valid number."));
            return false;
        }

        return true;
    }

    private bool parseHypotenuse(out double hypotenuse)
    {
        DataValidationErrors.ClearErrors(HypotenuseInput);
        if (String.IsNullOrEmpty(HypotenuseInput.Text))
        {
            DataValidationErrors.SetError(HypotenuseInput,
            new CleanException("Hypotenuse should not be empty."));
            hypotenuse = 0;
            return false;
        }

        if (!double.TryParse(HypotenuseInput.Text, out hypotenuse))
        {
            DataValidationErrors.SetError(HypotenuseInput,
            new CleanException("Hypotenuse should be a valid number."));
            return false;
        }

        return true;
    }

    private bool validateAdjacent(out double adjacent)
    {
        DataValidationErrors.ClearErrors(AdjacentInput);
        if (String.IsNullOrEmpty(AdjacentInput.Text))
        {
            DataValidationErrors.SetError(AdjacentInput,
            new CleanException("Adjacent should not be empty."));
            adjacent = 0;
            return false;
        }

        if (!double.TryParse(AdjacentInput.Text, out adjacent))
        {
            DataValidationErrors.SetError(AdjacentInput,
            new CleanException("Adjacent should be a valid number."));
            return false;
        }

        return true;
    }

    private void ComputeSineButton_OnCLick(object? sender, RoutedEventArgs e)
    {
        
        DataValidationErrors.ClearErrors(AdjacentInput);
        // singular & to disable short-circuiting
        if (parseOpposite(out double opposite) & parseHypotenuse(out double hypotenuse))
        {
            double sine = opposite / hypotenuse;
            SineOutput.Text = sine.ToString("0.00");
        }
    }

    private void ComputeCosineButton_OnClick(object? sender, RoutedEventArgs e)
    {
        DataValidationErrors.ClearErrors(OppositeInput);
        if (validateAdjacent(out double adjacent) & parseHypotenuse(out double hypotenuse))
        {
            double cosine = adjacent / hypotenuse;
            CosineOutput.Text = cosine.ToString("0.00");
        }
    }

    private void ComputeTangentButton_OnClick(object? sender, RoutedEventArgs e)
    {
        DataValidationErrors.ClearErrors(HypotenuseInput);
        if (parseOpposite(out double opposite) & validateAdjacent(out double adjacent))
        {
            double tangent = opposite / adjacent;
            TangentOutput.Text = tangent.ToString("0.00");
        }
    }

    private void ClearAllButton_OnClick(object? sender, RoutedEventArgs e)
    {
        DataValidationErrors.ClearErrors(OppositeInput);
        DataValidationErrors.ClearErrors(HypotenuseInput);
        DataValidationErrors.ClearErrors(AdjacentInput);
        OppositeInput.Text = "";
        HypotenuseInput.Text = "";
        AdjacentInput.Text = "";
        SineOutput.Text = "";
        CosineOutput.Text = "";
        TangentOutput.Text = "";
    }

    private void ClearButton_OnClick(object? sender, RoutedEventArgs e)
    {
        OppositeInput.Text = "";
        HypotenuseInput.Text = "";
        AdjacentInput.Text = "";
        DataValidationErrors.ClearErrors(OppositeInput);
        DataValidationErrors.ClearErrors(HypotenuseInput);
        DataValidationErrors.ClearErrors(AdjacentInput);
        SineOutput.Text = "";
        CosineOutput.Text = "";
        TangentOutput.Text = "";
    }

    private void ComputeAllButton_OnClick(object? sender, RoutedEventArgs e)
    {
        if (parseOpposite(out double opposite) & parseHypotenuse(out double hypotenuse) & validateAdjacent(out double adjacent))
        {
            double sine = opposite / hypotenuse;
            SineOutput.Text = sine.ToString("0.00");
            double cosine = adjacent / hypotenuse;
            CosineOutput.Text = cosine.ToString("0.00");
            double tangent = opposite / adjacent;
            TangentOutput.Text = tangent.ToString("0.00");
        }
    }
}
