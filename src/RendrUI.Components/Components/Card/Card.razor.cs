using Microsoft.AspNetCore.Components;

namespace RendrUI.Components.Card;


public partial class Card : ComponentBase
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter(CaptureUnmatchedValues = true)]
    public IReadOnlyDictionary<string, object>? AdditionalAttributes { get; set; }

    private string ClassNames
    {
        get
        {
            var additionalClass = string.Empty;
            if (AdditionalAttributes is not null &&
                AdditionalAttributes.TryGetValue("class", out var @class))
            {
                additionalClass = @class?.ToString() ?? string.Empty;
            }
            return CardClasses.Build(CardType.Card, additionalClass);
        }
    }
}