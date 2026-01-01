using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;

namespace RendrUI.Components.NavigationMenu;

public partial class NavigationMenu : ComponentBase, IAsyncDisposable
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter(CaptureUnmatchedValues = true)]
    public IReadOnlyDictionary<string, object>? AdditionalAttributes { get; set; }

    [Parameter]
    public NavigationMenuViewPort ViewPort { get; set; } = NavigationMenuViewPort.Auto;

    [Inject]
    private IJSRuntime JS { get; set; } = default!;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;

    private bool IsMobileMenuOpen { get; set; } = false;
    private bool IsMobileMode { get; set; } = false;
    private int CurrentViewportWidth { get; set; } = 1024;
    private DotNetObjectReference<NavigationMenu>? _dotNetHelper;
    private readonly string _listenerId = Guid.NewGuid().ToString();

    private const int MobileBreakpoint = 768;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender && ViewPort == NavigationMenuViewPort.Auto)
        {
            _dotNetHelper = DotNetObjectReference.Create(this);
            await JS.InvokeVoidAsync("RendrUI.registerViewportListener", _dotNetHelper, _listenerId);
        }

        await base.OnAfterRenderAsync(firstRender);
    }

    protected override void OnInitialized()
    {
        // Set initial mode based on ViewPort parameter
        UpdateMobileMode();

        // Listen for navigation changes to auto-close mobile menu
        NavigationManager.LocationChanged += OnLocationChanged;

        base.OnInitialized();
    }

    [JSInvokable]
    public void OnViewportChanged(int width)
    {
        var previousMode = IsMobileMode;
        CurrentViewportWidth = width;
        UpdateMobileMode();

        // Auto-close drawer when switching from mobile to desktop
        if (previousMode && !IsMobileMode && IsMobileMenuOpen)
        {
            IsMobileMenuOpen = false;
        }

        StateHasChanged();
    }

    private void UpdateMobileMode()
    {
        IsMobileMode = ViewPort switch
        {
            NavigationMenuViewPort.Mobile => true,
            NavigationMenuViewPort.Desktop => false,
            NavigationMenuViewPort.Auto => CurrentViewportWidth < MobileBreakpoint,
            _ => false
        };
    }

    private void ToggleMobileMenu()
    {
        IsMobileMenuOpen = !IsMobileMenuOpen;
    }

    private void CloseMobileMenu()
    {
        IsMobileMenuOpen = false;
    }

    private void OnLocationChanged(object? sender, LocationChangedEventArgs e)
    {
        if (IsMobileMenuOpen)
        {
            IsMobileMenuOpen = false;
            StateHasChanged();
        }
    }

    public async ValueTask DisposeAsync()
    {
        NavigationManager.LocationChanged -= OnLocationChanged;

        if (ViewPort == NavigationMenuViewPort.Auto && _dotNetHelper != null)
        {
            try
            {
                await JS.InvokeVoidAsync("RendrUI.unregisterViewportListener", _listenerId);
            }
            catch (JSDisconnectedException)
            {
                // JS runtime disconnected, ignore
            }
            _dotNetHelper.Dispose();
        }
    }

    // CSS Class Properties
    private string CssClass
    {
        get
        {
            var additionalClass = AdditionalAttributes?.TryGetValue("class", out var @class) == true
                ? @class?.ToString() ?? string.Empty
                : string.Empty;

            return NavigationMenuClasses.Build(NavigationMenuType.NavigationMenu, additionalClass);
        }
    }

    private string DesktopContainerClass => IsMobileMode ? "hidden" : "block";

    private string BurgerButtonClass => IsMobileMode
        ? NavigationMenuClasses.Build(NavigationMenuType.BurgerButton, null)
        : "hidden";

    private string DrawerClass
    {
        get
        {
            var baseClass = NavigationMenuClasses.Build(NavigationMenuType.Drawer, null);
            var openClass = IsMobileMenuOpen ? "translate-x-0" : "-translate-x-full";
            return $"{baseClass} {openClass}";
        }
    }

    private string BackdropClass
    {
        get
        {
            var baseClass = NavigationMenuClasses.Build(NavigationMenuType.Backdrop, null);
            var opacityClass = IsMobileMenuOpen ? "opacity-100" : "opacity-0 pointer-events-none";
            return $"{baseClass} {opacityClass}";
        }
    }

    private string DrawerCloseButtonClass => NavigationMenuClasses.Build(NavigationMenuType.DrawerCloseButton, null);
}
