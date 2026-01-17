using RendrUI.Components.Utils;

namespace RendrUI.Components.Select;


internal static class SelectClasses
{
    private const string Base =
        "flex w-full items-center justify-between rounded-sm border bg-transparent " +
        "px-3 pr-10 text-sm transition-colors " +
        "focus-visible:outline-none focus-visible:ring-1 focus-visible:ring-ring " +
        "disabled:cursor-not-allowed disabled:opacity-50 " +
        "placeholder:text-muted-foreground";

    private const string NormalClasses =
        "border-input";

    private const string ErrorClasses =
        "border-destructive focus-visible:ring-destructive";

    private static readonly Dictionary<SelectSize, string> Sizes = new()
    {
        [SelectSize.Sm] = "h-9",
        [SelectSize.Default] = "h-10",
        [SelectSize.Lg] = "h-11"
    };

    public static string BuildTrigger(bool hasError, SelectSize size, string? extra)
    {
        var state = hasError ? ErrorClasses : NormalClasses;

        return TailwindMerger.Merge(
            Base,
            state,
            Sizes[size],
            extra ?? ""
        );
    }

    public const string Content =
        "relative z-50 min-w-[8rem] overflow-hidden rounded-md border " +
        "bg-popover text-popover-foreground shadow-md " +
        "animate-in fade-in-0 zoom-in-95";

    public static string BuildContent(string? extras) =>
        TailwindMerger.Merge(Content, extras ?? "");

    public const string Item =
        "relative flex w-full cursor-pointer select-none items-center " +
        "rounded-sm px-2 py-1.5 text-sm outline-none " +
        "focus:bg-accent focus:text-accent-foreground " +
        "data-[disabled]:pointer-events-none data-[disabled]:opacity-50";

    public static string BuildItem(string? extras) =>
        TailwindMerger.Merge(Content, extras ?? "");
}