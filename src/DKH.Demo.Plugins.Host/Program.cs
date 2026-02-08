using DKH.Platform;
using DKH.Platform.Health;
using DKH.Platform.Logging;
using DKH.Platform.MinimalApi;
using DKH.Platform.Plugins;
using DKH.Platform.Plugins.Extensions;
using DKH.Platform.Plugins.Host;

var app = Platform
    .CreateWeb(args)
    .AddPlatformLogging()
    .AddPlatformPlugins()
    .AddPlatformMinimalApi()
    .ConfigurePlatformWebApplicationBuilder(builder =>
        builder.Services.AddHealthChecks().AddPluginHostHealthCheck())
    .AddPlatformHealthChecks()
    .Build();

var plugins = app.MapGroup("/api/plugins");

plugins.MapGet("/", async (IPlatformPluginRegistry registry, CancellationToken ct) =>
    Results.Ok(await registry.GetAllAsync(ct)));

plugins.MapGet("/{id}", async (string id, IPlatformPluginRegistry registry, CancellationToken ct) =>
{
    var plugin = await registry.GetByIdAsync(id, ct);
    return plugin is null ? Results.NotFound() : Results.Ok(plugin);
});

plugins.MapPost("/{id}/enable", async (string id, IPlatformPluginRegistry registry, CancellationToken ct) =>
{
    await registry.EnableAsync(id, ct);
    return Results.Ok();
});

plugins.MapPost("/{id}/disable", async (string id, IPlatformPluginRegistry registry, CancellationToken ct) =>
{
    await registry.DisableAsync(id, ct);
    return Results.Ok();
});

var extensions = app.MapGroup("/api/extensions");

extensions.MapGet("/product/{productId}/enrich",
    async (Guid productId, IPlatformPluginExtensionDispatcher dispatcher, CancellationToken ct) =>
    {
        var data = new Dictionary<string, object?>();
        await dispatcher.DispatchAsync<IPlatformProductExtension>(
            (ext, token) => ext.EnrichAsync(productId, data, token), ct);
        return Results.Ok(data);
    });

extensions.MapGet("/navigation",
    async (IPlatformPluginExtensionDispatcher dispatcher, CancellationToken ct) =>
    {
        var items = await dispatcher.CollectAsync<IPlatformNavigationProvider, IReadOnlyList<PlatformNavigationItem>>(
            (ext, token) => ext.GetItemsAsync(token), ct);
        return Results.Ok(items.SelectMany(static i => i));
    });

extensions.MapGet("/widgets",
    async (IPlatformPluginExtensionDispatcher dispatcher, CancellationToken ct) =>
    {
        var widgets = await dispatcher.CollectAsync<IPlatformWidgetProvider, IReadOnlyList<PlatformWidgetDescriptor>>(
            (ext, token) => ext.GetWidgetsAsync(token), ct);
        return Results.Ok(widgets.SelectMany(static w => w));
    });

extensions.MapPost("/checkout",
    async (PlatformCheckoutContext context, IPlatformPluginExtensionDispatcher dispatcher, CancellationToken ct) =>
    {
        var steps = dispatcher.GetAll<IPlatformCheckoutStep>().OrderBy(static s => s.Order);
        var results = new List<PlatformCheckoutStepResult>();
        foreach (var step in steps)
        {
            var result = await step.ExecuteAsync(context, ct);
            results.Add(result);
            if (!result.Success)
            {
                break;
            }
        }

        return Results.Ok(results);
    });

await app.RunAsync();
