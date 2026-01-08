using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace RendrUI.Components.Checkbox;

public partial class Checkbox : InputBase<bool>
{
    [Parameter]
    public CheckboxSize Size { get; set; } = CheckboxSize.Default;


    private string ClassNames
    {
        get
        {
            var additionalClass = string.Empty;
            if (AdditionalAttributes is not null &&
            AdditionalAttributes.TryGetValue("class", out var @class))
            {
                additionalClass = @class.ToString() ?? string.Empty;
            }

            return CheckboxClasses.Build(Size, additionalClass);
        }
    }

#pragma warning disable CS8765 
    // method should never be used for checkboxes..
    protected override bool TryParseValueFromString(
        string? value,
        out bool result,
        out string? validationErrorMessage)
    {
        result = CurrentValue;
        validationErrorMessage = null;
        return true;
    }
#pragma warning restore CS8765


    private void OnChange(ChangeEventArgs e)
    {
        if (e.Value is bool value)
        {
            CurrentValue = value;
        }
    }
}