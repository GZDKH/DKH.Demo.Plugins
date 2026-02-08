using DKH.Platform.Plugins;
using DKH.Platform.Plugins.Extensions;
using DKH.Platform.Plugins.Sdk.Extensions;

namespace DKH.Demo.Plugins.SamplePlugin.Extensions;

public sealed class SampleWidgetProvider : PlatformWidgetProviderBase
{
    public override Task<IReadOnlyList<PlatformWidgetDescriptor>> GetWidgetsAsync(
        CancellationToken cancellationToken = default)
    {
        IReadOnlyList<PlatformWidgetDescriptor> widgets =
        [
            new PlatformWidgetDescriptor(
                Id: "demo-stats",
                Label: new LocalizedString
                {
                    ["en"] = "Demo Stats",
                    ["ru"] = "Демо-статистика"
                },
                Module: "DemoStatsWidget",
                Slot: "admin.dashboard",
                Order: 900)
        ];

        return Task.FromResult(widgets);
    }
}
