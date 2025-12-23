namespace RendrUI.Components.Button;

internal static class ButtonClasses
{
    private const string Base =
        "inline-flex items-center justify-center rounded-md text-sm font-medium " +
        "transition-colors focus-visible:outline-none focus-visible:ring-2 " +
        "focus-visible:ring-ring focus-visible:ring-offset-2 " +
        "disabled:pointer-events-none disabled:opacity-50 cursor-pointer";

    private static readonly Dictionary<ButtonVariant, string> Variants = new()
    {
        [ButtonVariant.Default] =
            "bg-primary text-primary-foreground hover:bg-primary/90",

        [ButtonVariant.Destructive] =
            "bg-red-500 text-white hover:bg-red-500/90",

        [ButtonVariant.Outline] =
            "border border-input bg-background hover:bg-accent hover:text-accent-foreground",

        [ButtonVariant.Secondary] =
            "bg-secondary text-secondary-foreground hover:bg-secondary/80",

        [ButtonVariant.Ghost] =
            "hover:bg-accent hover:text-accent-foreground",

        [ButtonVariant.Link] =
            "text-primary underline-offset-4 hover:underline"
    };

    private static readonly Dictionary<ButtonSize, string> Sizes = new()
    {
        [ButtonSize.Default] = "h-10 px-4 py-2",
        [ButtonSize.Sm] = "h-9 rounded-md px-3",
        [ButtonSize.Lg] = "h-11 rounded-md px-8",
        [ButtonSize.Icon] = "h-10 w-10"
    };

    public static string Build(ButtonVariant variant, ButtonSize size, string? extra)
        => string.Join(
            " ",
            Base,
            Variants[variant],
            Sizes[size],
            extra
        );
}