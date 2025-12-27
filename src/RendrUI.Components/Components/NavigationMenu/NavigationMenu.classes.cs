using RendrUI.Components.Components.NavigationMenu;

namespace RendrUI.Components.NavigationMenu;


internal static class NavigationMenuClasses
{
    private const string Base = "";

    private static readonly Dictionary<NavigationMenuType, string> TypeClasses = new()
    {
        [NavigationMenuType.NavigationMenu] =
            "rui-nav-menu relative z-10",

        [NavigationMenuType.NavigationMenuList] =
            "rui-nav-menu-list flex space-x-4",

        [NavigationMenuType.NavigationMenuItem] =
            "rui-nav-menu-item relative",

        [NavigationMenuType.NavigationMenuTrigger] =
            "rui-nav-menu-trigger flex flex-row items-center cursor-pointer gap-1 px-4 py-2 rounded-md hover:bg-gray-100 focus:outline-none focus:ring-2 focus:ring-blue-500",

        [NavigationMenuType.NavigationMenuContent] =
            "rui-nav-menu-content absolute top-full left-0 mt-1 w-56 rounded-md shadow-lg bg-white border border-gray-200 z-50 p-2"
    };

    public static string Build(NavigationMenuType type, string? extra)
        => string.Join(
            " ",
            Base,
            TypeClasses[type],
            extra
        ).Trim();
}
