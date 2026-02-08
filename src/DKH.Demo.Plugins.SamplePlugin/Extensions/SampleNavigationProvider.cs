using DKH.Platform.Plugins;
using DKH.Platform.Plugins.Extensions;
using DKH.Platform.Plugins.Sdk.Extensions;

namespace DKH.Demo.Plugins.SamplePlugin.Extensions;

public sealed class SampleNavigationProvider : PlatformNavigationProviderBase
{
    public override Task<IReadOnlyList<PlatformNavigationItem>> GetItemsAsync(
        CancellationToken cancellationToken = default)
    {
        IReadOnlyList<PlatformNavigationItem> items =
        [
            new PlatformNavigationItem(
                Id: "demo-settings",
                Label: new LocalizedString
                {
                    ["en"] = "Demo Settings",
                    ["ru"] = "Демо-настройки"
                },
                Icon: "cog",
                Route: "/demo/settings",
                Order: 900)
        ];

        return Task.FromResult(items);
    }
}
