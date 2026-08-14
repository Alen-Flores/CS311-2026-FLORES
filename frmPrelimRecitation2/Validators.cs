using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Input;

namespace frmPrelimRecitation2;

public class CleanException : System.Exception
{
    public CleanException(string message) : base(message) { }

    // Overriding this removes the "System.Exception:" prefix when printing the exception
    public override string ToString()
    {
        return Message;
    }
}

internal class Validators
{
    private HashSet<TextBox> validatedTextBoxes = new HashSet<TextBox>();
    private string errorMsg = "";

    public string? validateTextBox(in TextBox textBox)
    {
        validatedTextBoxes.Add(textBox);
        if (string.IsNullOrWhiteSpace(textBox.Text))
        {
            errorMsg = $"{textBox.Name} is empty.";
            return errorMsg;
        }
        else
        {
            return null;
        }

    }

    public string? keyPressValidator(KeyEventArgs e, in TextBox textBox)
    {
        validatedTextBoxes.Add(textBox);

        // 34 in avalonia keycode is '0'
        int key = (int)e.Key - 34;
        if (key >= 0 && key <= 9)
        {
            e.Handled = false;
            return null;
        }
        else
        {
            e.Handled = true;
            errorMsg = "Input is not numeric.";
            return errorMsg;
        }
    }

    public int countErrors()
    {
        int errorCount = 0;
        foreach (var textBox in validatedTextBoxes)
        {
            errorCount += DataValidationErrors.GetErrors(textBox)?.Count() ?? 0;
        }
        return errorCount;
    }

    public void clearErrors()
    {
        foreach (var textBox in validatedTextBoxes)
        {
            DataValidationErrors.ClearErrors(textBox);
        }
        validatedTextBoxes.Clear();
    }

}