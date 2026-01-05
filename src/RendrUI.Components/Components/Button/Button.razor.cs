using Microsoft.AspNetCore.Components;

namespace RendrUI.Components.Button;

public partial class Button : ComponentBase
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter]
    public ButtonVariant Variant { get; set; } = ButtonVariant.Default;

    [Parameter]
    public ButtonSize Size { get; set; } = ButtonSize.Default;

    [Parameter(CaptureUnmatchedValues = true)]
    public IReadOnlyDictionary<string, object>? AdditionalAttributes { get; set; }

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

            return ButtonClasses.Build(Variant, Size, additionalClass);
        }
    }

    private string TypeAttr
    {
        get
        {
            if (AdditionalAttributes is not null &&
            AdditionalAttributes.TryGetValue("type", out var typeAttr))
            {
                return typeAttr?.ToString() ?? "button";
            }
            else
            {
                return "button";
            }
        }
    }
}
