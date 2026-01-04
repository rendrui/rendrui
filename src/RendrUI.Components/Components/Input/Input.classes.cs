using RendrUI.Components.Utils;

namespace RendrUI.Components.Input;

internal static class InputClasses
{
    private const string Base =
        "flex h-10 w-full rounded-sm border bg-transparent px-3 text-sm " +
        "transition-colors file:border-0 file:bg-transparent file:text-sm file:font-medium " +
        "placeholder:text-muted-foreground focus-visible:outline-none " +
        "focus-visible:ring-1 focus-visible:ring-ring " +
        "disabled:cursor-not-allowed disabled:opacity-50";

    private const string NormalClasses =
        "border-input";

    private const string ErrorClasses =
        "border-destructive focus-visible:ring-destructive";

    private static readonly Dictionary<InputSize, string> Sizes = new()
    {
        [InputSize.Sm] = "h-9 w-32",
        [InputSize.Default] = "h-10 w-64",
        [InputSize.Lg] = "h-11 w-96"
    };

    public static string Build(bool hasValidationErrors, InputSize size, string? extra)
    {
        var stateClasses = hasValidationErrors ? ErrorClasses : NormalClasses;
        return TailwindMerger.Merge(Base, stateClasses, Sizes[size], extra ?? "");
    }
}