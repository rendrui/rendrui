using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace RendrUI.Components.Input;

public partial class Input : InputBase<string>
{
    [Parameter]
    public InputSize Size { get; set; } = InputSize.Default;

    protected bool HasValidationError =>
        EditContext?.GetValidationMessages(FieldIdentifier).Any() == true;

    private string TypeAttr
    {
        get
        {
            if (AdditionalAttributes is not null &&
            AdditionalAttributes.TryGetValue("type", out var typeAttr))
            {
                return typeAttr?.ToString() ?? "text";
            }
            else
            {
                return "text";
            }
        }
    }

    protected string ClassNames
    {
        get
        {
            var additionalClass = string.Empty;
            if (AdditionalAttributes is not null &&
            AdditionalAttributes.TryGetValue("class", out var @class))
            {
                additionalClass = @class?.ToString() ?? string.Empty;
            }

            return InputClasses.Build(HasValidationError, Size, additionalClass);
        }
    }

    protected void OnInput(ChangeEventArgs e)
    {
        CurrentValueAsString = e.Value?.ToString();
    }

    protected override bool TryParseValueFromString(
        string? value,
        [MaybeNullWhen(false)] out string result,
        [NotNullWhen(false)] out string? validationErrorMessage)
    {
        result = value ?? string.Empty;
        validationErrorMessage = null;
        return true;
    }
}