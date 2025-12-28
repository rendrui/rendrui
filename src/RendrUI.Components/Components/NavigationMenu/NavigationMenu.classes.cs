namespace RendrUI.Components.NavigationMenu;


internal static class NavigationMenuClasses
{
    private const string Base = "";

    private static readonly Dictionary<NavigationMenuType, string> TypeClasses = new()
    {
        [NavigationMenuType.NavigationMenu] =
            "rui-nav-menu relative z-10",

        [NavigationMenuType.NavigationMenuList] =
            "rui-nav-menu-list flex",

        [NavigationMenuType.NavigationMenuItem] =
            "rui-nav-menu-item relative",

        [NavigationMenuType.NavigationMenuTrigger] =
            "rui-nav-menu-trigger flex flex-row items-center gap-1 px-4 py-2 rounded-md hover:bg-gray-100 focus:outline-none focus:ring-2 focus:ring-blue-500",

        [NavigationMenuType.NavigationMenuContent] =
            "rui-nav-menu-content absolute top-full left-0 mt-1 w-56 rounded-md shadow-lg bg-white border border-gray-200 z-50 p-2",

        [NavigationMenuType.BurgerButton] =
            "rui-burger-button flex items-center justify-center p-2 rounded-md hover:bg-gray-100 focus:outline-none focus:ring-2 focus:ring-blue-500",

        [NavigationMenuType.Drawer] =
            "rui-drawer fixed top-0 left-0 h-full w-80 max-w-[80vw] bg-white z-50 shadow-xl transform transition-transform duration-300 ease-in-out overflow-y-auto",

        [NavigationMenuType.Backdrop] =
            "rui-backdrop fixed inset-0 bg-black/50 z-40 transition-opacity duration-300",

        [NavigationMenuType.DrawerCloseButton] =
            "rui-drawer-close p-2 rounded-md hover:bg-gray-100 focus:outline-none"
    };

    private static readonly Dictionary<NavigationMenuType, string> MobileTypeClasses = new()
    {
        [NavigationMenuType.NavigationMenuList] =
            "rui-nav-menu-list flex flex-col space-y-1 p-4",

        [NavigationMenuType.NavigationMenuItem] =
            "rui-nav-menu-item",

        [NavigationMenuType.NavigationMenuTrigger] =
            "rui-nav-menu-trigger flex flex-row items-center justify-between w-full px-4 py-3 rounded-md hover:bg-gray-100 active:bg-gray-200 text-left text-base",

        [NavigationMenuType.NavigationMenuContent] =
            "rui-nav-menu-content pl-4 space-y-1 mt-2 overflow-hidden"
    };

    public static string Build(NavigationMenuType type, string? extra, bool isMobile = false)
    {
        var typeClasses = type switch
        {
            NavigationMenuType.NavigationMenuList when isMobile => MobileTypeClasses[type],
            NavigationMenuType.NavigationMenuItem when isMobile => MobileTypeClasses[type],
            NavigationMenuType.NavigationMenuTrigger when isMobile => MobileTypeClasses[type],
            NavigationMenuType.NavigationMenuContent when isMobile => MobileTypeClasses[type],
            _ => TypeClasses.ContainsKey(type) ? TypeClasses[type] : ""
        };

        return string.Join(" ", Base, typeClasses, extra).Trim();
    }
}
