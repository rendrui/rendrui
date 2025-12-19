using Microsoft.AspNetCore.Components;

namespace Rendr.UIKit.Components.Button;

public partial class Button : ComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public ButtonVariant Variant { get; set; } = ButtonVariant.Default;
    [Parameter] public ButtonSize Size { get; set; } = ButtonSize.Default;

    [Parameter] public bool Disabled { get; set; }

    [Parameter] public string Type { get; set; } = "button";

    [Parameter(CaptureUnmatchedValues = true)]
    public IReadOnlyDictionary<string, object>? AdditionalAttributes { get; set; }

    protected string CssClass =>
        ButtonClasses.Build(Variant, Size, AdditionalAttributes?["class"]?.ToString());
}
