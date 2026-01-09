using RendrUI.Components.Utils;

namespace RendrUI.Components.Switch;

public sealed class SwitchClasses
{
    private const string TrackBase =
        "peer inline-flex shrink-0 cursor-pointer items-center " +
        "rounded-full border-2 border-transparent " +
        "transition-colors focus-visible:outline-none " +
        "focus-visible:ring-2 focus-visible:ring-ring " +
        "focus-visible:ring-offset-2 ring-offset-background " +
        "disabled:cursor-not-allowed disabled:opacity-50";
    private const string TrackUnchecked =
        "bg-input";

    private const string TrackChecked =
        "bg-primary";

    private static readonly Dictionary<SwitchSize, string> TrackSizes = new()
    {
        [SwitchSize.Sm] = "h-4 w-7",
        [SwitchSize.Default] = "h-5 w-9",
        [SwitchSize.Lg] = "h-6 w-11"
    };

    public static string BuildTrack(SwitchSize size, bool isChecked, string? extras)
        => TailwindMerger.Merge(
            TrackBase,
            isChecked ? TrackChecked : TrackUnchecked,
            TrackSizes[size],
            extras ?? string.Empty);


    private const string ThumbBase =
        "pointer-events-none block rounded-full bg-background shadow-lg " +
        "ring-0 transition-transform";

    private const string ThumbUnchecked =
        "translate-x-0";

    private const string ThumbCheckedSm =
        "translate-x-3";

    private const string ThumbCheckedDefault =
        "translate-x-4";

    private const string ThumbCheckedLg =
        "translate-x-5";

    private static readonly Dictionary<SwitchSize, string> ThumbSizes = new()
    {
        [SwitchSize.Sm] = "h-3 w-3",
        [SwitchSize.Default] = "h-4 w-4",
        [SwitchSize.Lg] = "h-5 w-5"
    };

    public static string BuildThumb(SwitchSize size, bool isChecked)
    {
        var translate = size switch
        {
            SwitchSize.Sm => isChecked ? ThumbCheckedSm : ThumbUnchecked,
            SwitchSize.Lg => isChecked ? ThumbCheckedLg : ThumbUnchecked,
            _ => isChecked ? ThumbCheckedDefault : ThumbUnchecked
        };

        return TailwindMerger.Merge(
            ThumbBase,
            ThumbSizes[size],
            translate);
    }
}