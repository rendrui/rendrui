using Microsoft.AspNetCore.Components;

namespace RendrUI.Components.Switch;

public partial class Switch : ComponentBase
{
    [Parameter]
    public bool Value { get; set; }

    [Parameter]
    public EventCallback<bool> ValueChanged { get; set; }

    [Parameter]
    public SwitchSize Size { get; set; } = SwitchSize.Default;

    [Parameter(CaptureUnmatchedValues = true)]
    public IReadOnlyDictionary<string, object>? AdditionalAttributes { get; set; }

    private string TrackClassNames
    {
        get
        {
            string? additionalClass = string.Empty;
            if (AdditionalAttributes is not null &&
            AdditionalAttributes.TryGetValue("class", out var @class))
            {
                additionalClass = @class.ToString() ?? string.Empty;
            }

            return SwitchClasses.BuildTrack(
                size: Size,
                isChecked: Value,
                extras: additionalClass);
        }
    }

    private bool Disabled =>
        AdditionalAttributes?.ContainsKey("disabled") == true;

    private string ThumbClassNames
        => SwitchClasses.BuildThumb(
            size: Size,
            isChecked: Value);

    private async Task Toggle()
    {
        if (Disabled)
            return;

        Value = !Value;
        await ValueChanged.InvokeAsync(Value);
    }
}