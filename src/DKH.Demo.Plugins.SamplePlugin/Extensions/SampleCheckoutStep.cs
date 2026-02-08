using DKH.Platform.Plugins.Extensions;
using DKH.Platform.Plugins.Sdk.Extensions;
using Microsoft.Extensions.Logging;

namespace DKH.Demo.Plugins.SamplePlugin.Extensions;

public sealed partial class SampleCheckoutStep(ILogger<SampleCheckoutStep> logger)
    : PlatformCheckoutStepBase
{
    public override int Order => 100;

    public override Task<PlatformCheckoutStepResult> ExecuteAsync(
        PlatformCheckoutContext context,
        CancellationToken cancellationToken = default)
    {
        Log.Executing(logger, context.OrderId, context.CustomerId);
        return Task.FromResult(new PlatformCheckoutStepResult { Success = true });
    }

    private static partial class Log
    {
        [LoggerMessage(Level = LogLevel.Information,
            Message = "Executing demo checkout step for Order {OrderId}, Customer {CustomerId}")]
        public static partial void Executing(ILogger logger, Guid orderId, Guid customerId);
    }
}
