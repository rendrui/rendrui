namespace RendrUI.Components.Utils;


public static class TailwindMerger
{
    public static string Merge(params string[] classStrings)
    {
        var allClasses = classStrings
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .SelectMany(s => s.Split(' ', StringSplitOptions.RemoveEmptyEntries))
            .ToList();

        var result = new Dictionary<string, string>();

        foreach (var cls in allClasses)
        {
            var prefix = GetPrefix(cls);
            result[prefix] = cls;
        }

        return string.Join(" ", result.Values);
    }


    private static string GetPrefix(string cls)
    {
        var lastDashIndex = cls.LastIndexOf("-");
        if (lastDashIndex <= 0)
            return cls;

        var suffix = cls[(lastDashIndex + 1)..];
        if (IsValueSuffix(suffix))
            return cls[..lastDashIndex];

        return cls;
    }


    private static bool IsValueSuffix(string suffix)
    {
        if (decimal.TryParse(suffix, out var _))
            return true;

        return suffix is 
            "full" or "auto" or "screen" or "min" or "max" or "fit" or
            "none" or "px" or "xs" or "sm" or "md" or "lg" or "xl" or 
            "2xl" or "3xl" or "4xl" or "5xl" or "6xl" or "7xl";
    }
}