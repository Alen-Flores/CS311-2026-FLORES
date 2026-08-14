using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Input;

namespace CS311_Prelim_Exam_Activity_Flores;

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
        else if (!double.TryParse(textBox.Text, out double value))
        {
            errorMsg = $"{textBox.Name} is not a valid number.";
            return errorMsg;
        }
        else
        {
            return null;
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