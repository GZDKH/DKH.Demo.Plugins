using DKH.Demo.Plugins.SamplePlugin.Data;
using DKH.Demo.Plugins.SamplePlugin.Data.Entities;
using DKH.Demo.Plugins.SamplePlugin.Extensions;
using DKH.Demo.Plugins.SamplePlugin.Services;
using DKH.Platform.Plugins;
using DKH.Platform.Plugins.Sdk;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DKH.Demo.Plugins.SamplePlugin;

public sealed partial class SamplePlugin : PlatformPluginBase
{
    public override string Id => "demo.sample-plugin";

    public override void ConfigureServices(IPlatformPluginServiceContext context)
    {
        context.RegisterExtension<SampleProductExtension>();
        context.RegisterExtension<SampleCheckoutStep>();
        context.RegisterExtension<SampleNavigationProvider>();
        context.RegisterExtension<SampleWidgetProvider>();

        context.RegisterDbContext<SamplePluginDbContext>();

        context.Services.AddDbContext<SamplePluginDbContext>(options =>
            options.UseSqlite("Data Source=sample-plugin.db"));

        context.Services.AddScoped<ISampleService, SampleService>();
    }

    public override async Task InitializeAsync(
        IPlatformPluginApplicationContext context,
        CancellationToken cancellationToken = default)
    {
        Log.Initializing(context.Logger);

        await using var scope = context.Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<SamplePluginDbContext>();

        var hasSettings = await dbContext.Settings.AnyAsync(cancellationToken);
        if (!hasSettings)
        {
            dbContext.Settings.Add(new PluginSetting
            {
                Id = Guid.NewGuid(),
                Key = "demo.initialized",
                Value = "true"
            });
            await dbContext.SaveChangesAsync(cancellationToken);
            Log.SeededDefaults(context.Logger);
        }

        Log.Initialized(context.Logger);
    }

    private static partial class Log
    {
        [LoggerMessage(Level = LogLevel.Information, Message = "Initializing demo.sample-plugin...")]
        public static partial void Initializing(ILogger logger);

        [LoggerMessage(Level = LogLevel.Information, Message = "Seeded default plugin settings")]
        public static partial void SeededDefaults(ILogger logger);

        [LoggerMessage(Level = LogLevel.Information, Message = "demo.sample-plugin initialized successfully")]
        public static partial void Initialized(ILogger logger);
    }
}
