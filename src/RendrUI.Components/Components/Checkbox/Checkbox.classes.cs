using RendrUI.Components.Utils;

namespace RendrUI.Components.Checkbox;


internal static class CheckboxClasses
{
    private const string Base =
        "peer shrink-0 rounded-sm border border-input bg-background " +
        "transition-colors " +
        "focus-visible:outline-none focus-visible:ring-1 focus-visible:ring-ring " +
        "disabled:cursor-not-allowed disabled:opacity-50";

    private static Dictionary<CheckboxSize, string> Sizes = new()
    {
        [CheckboxSize.Default] = "h-4 w-4",
        [CheckboxSize.Sm] = "h-3 w-3",
        [CheckboxSize.Lg] = "h-6 w-6"
    };

    public static string Build(CheckboxSize size, string? extras)
        => TailwindMerger.Merge(
            Base,
            Sizes[size],
            extras ?? string.Empty);
}