using DKH.Platform.Plugins.Sdk.Extensions;
using Microsoft.Extensions.Logging;

namespace DKH.Demo.Plugins.SamplePlugin.Extensions;

public sealed partial class SampleProductExtension(ILogger<SampleProductExtension> logger)
    : PlatformProductExtensionBase
{
    public override Task EnrichAsync(
        Guid productId,
        IDictionary<string, object?> data,
        CancellationToken cancellationToken = default)
    {
        data["demo_enriched"] = true;
        data["demo_timestamp"] = DateTimeOffset.UtcNow.ToString("O");
        Log.Enriched(logger, productId);
        return Task.CompletedTask;
    }

    public override Task OnCreatedAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        Log.ProductCreated(logger, productId);
        return Task.CompletedTask;
    }

    public override Task OnUpdatedAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        Log.ProductUpdated(logger, productId);
        return Task.CompletedTask;
    }

    public override Task OnDeletedAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        Log.ProductDeleted(logger, productId);
        return Task.CompletedTask;
    }

    private static partial class Log
    {
        [LoggerMessage(Level = LogLevel.Information, Message = "Enriched product {ProductId} with demo data")]
        public static partial void Enriched(ILogger logger, Guid productId);

        [LoggerMessage(Level = LogLevel.Information, Message = "Product {ProductId} created — demo plugin notified")]
        public static partial void ProductCreated(ILogger logger, Guid productId);

        [LoggerMessage(Level = LogLevel.Information, Message = "Product {ProductId} updated — demo plugin notified")]
        public static partial void ProductUpdated(ILogger logger, Guid productId);

        [LoggerMessage(Level = LogLevel.Information, Message = "Product {ProductId} deleted — demo plugin notified")]
        public static partial void ProductDeleted(ILogger logger, Guid productId);
    }
}
