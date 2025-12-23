namespace RendrUI.Components.Card;

internal static class CardClasses
{
    private const string Base =
    "";


    private static readonly Dictionary<CardType, string> TypeClasses = new()
    {
        /// rui-card is a component marker can be used to override css on the card component
        [CardType.Card] =
            "rui-card group/card flex flex-col rounded-lg border bg-card text-card-foreground shadow-sm",

        [CardType.CardContent] =
            "rui-card-content",

        [CardType.CardDescription] =
            "rui-card-description",

        [CardType.CardFooter] =
            "rui-card-footer flex items-center",

        [CardType.CardHeader] =
            "rui-card-header group/card-header grid auto-rows-min items-start p-6 " +
            "has-data-[slot=card-action]:grid-cols-[1fr_auto] " +
            "has-data-[slot=card-description]:grid-rows-[auto_auto]",

        [CardType.CardTitle] =
            "rui-card-title",

        [CardType.CardAction] =
            "rui-card-action col-start-2 row-span-2 row-start-1 self-start justify-self-end"
    };

    public static string Build(CardType type, string? extra)
        => string.Join(
            " ",
            Base,
            TypeClasses[type],
            extra
        );
}