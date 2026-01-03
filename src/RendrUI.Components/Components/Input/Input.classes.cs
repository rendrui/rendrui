namespace RendrUI.Components.Input;

internal static class InputClasses
{
    private const string Base =
        "flex h-9 w-full rounded-md border bg-transparent px-3 py-1 text-sm shadow-sm " +
        "transition-colors file:border-0 file:bg-transparent file:text-sm file:font-medium " +
        "placeholder:text-muted-foreground focus-visible:outline-none " +
        "focus-visible:ring-1 focus-visible:ring-ring " +
        "disabled:cursor-not-allowed disabled:opacity-50";

    private const string NormalClasses =
        "border-input";

    private const string ErrorClasses =
        "border-destructive focus-visible:ring-destructive";

    public static string Build(bool hasValidationErrors, string? extra)
        => string.Join(
            " ",
            Base,
            hasValidationErrors ? ErrorClasses : NormalClasses);
}