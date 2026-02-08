using DKH.Demo.Plugins.SamplePlugin;
using DKH.Demo.Plugins.SamplePlugin.Extensions;
using DKH.Platform.Plugins;
using DKH.Platform.Plugins.Extensions;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute;
using Xunit;

namespace DKH.Demo.Plugins.Tests;

public class SamplePluginTests
{
    [Fact]
    public void Id_ShouldMatchManifest()
    {
        var plugin = new SamplePlugin.SamplePlugin();

        plugin.Id.Should().Be("demo.sample-plugin");
    }

    [Fact]
    public void ConfigureServices_ShouldRegisterAllExtensions()
    {
        var plugin = new SamplePlugin.SamplePlugin();
        var context = Substitute.For<IPlatformPluginServiceContext>();
        context.Services.Returns(new ServiceCollection());

        plugin.ConfigureServices(context);

        context.Received(1).RegisterExtension<SampleProductExtension>();
        context.Received(1).RegisterExtension<SampleCheckoutStep>();
        context.Received(1).RegisterExtension<SampleNavigationProvider>();
        context.Received(1).RegisterExtension<SampleWidgetProvider>();
    }

    [Fact]
    public void ConfigureServices_ShouldRegisterDbContext()
    {
        var plugin = new SamplePlugin.SamplePlugin();
        var context = Substitute.For<IPlatformPluginServiceContext>();
        context.Services.Returns(new ServiceCollection());

        plugin.ConfigureServices(context);

        context.Received(1).RegisterDbContext<SamplePlugin.Data.SamplePluginDbContext>();
    }

    [Fact]
    public async Task SampleProductExtension_EnrichAsync_ShouldAddDemoDataAsync()
    {
        var logger = NullLoggerFactory.Instance.CreateLogger<SampleProductExtension>();
        var extension = new SampleProductExtension(logger);
        var data = new Dictionary<string, object?>();
        var productId = Guid.NewGuid();

        await extension.EnrichAsync(productId, data);

        data.Should().ContainKey("demo_enriched").WhoseValue.Should().Be(true);
        data.Should().ContainKey("demo_timestamp");
    }

    [Fact]
    public async Task SampleCheckoutStep_ShouldReturnSuccessAsync()
    {
        var logger = NullLoggerFactory.Instance.CreateLogger<SampleCheckoutStep>();
        var step = new SampleCheckoutStep(logger);
        var context = new PlatformCheckoutContext
        {
            OrderId = Guid.NewGuid(),
            CustomerId = Guid.NewGuid()
        };

        var result = await step.ExecuteAsync(context);

        result.Success.Should().BeTrue();
        result.ErrorMessage.Should().BeNull();
    }

    [Fact]
    public void SampleCheckoutStep_Order_ShouldBe100()
    {
        var logger = NullLoggerFactory.Instance.CreateLogger<SampleCheckoutStep>();
        var step = new SampleCheckoutStep(logger);

        step.Order.Should().Be(100);
    }

    [Fact]
    public async Task SampleNavigationProvider_ShouldReturnExpectedItemAsync()
    {
        var provider = new SampleNavigationProvider();

        var items = await provider.GetItemsAsync();

        items.Should().ContainSingle();
        var item = items[0];
        item.Id.Should().Be("demo-settings");
        item.Icon.Should().Be("cog");
        item.Route.Should().Be("/demo/settings");
        item.Order.Should().Be(900);
        item.Label.Should().ContainKey("en");
        item.Label.Should().ContainKey("ru");
    }

    [Fact]
    public async Task SampleWidgetProvider_ShouldReturnExpectedWidgetAsync()
    {
        var provider = new SampleWidgetProvider();

        var widgets = await provider.GetWidgetsAsync();

        widgets.Should().ContainSingle();
        var widget = widgets[0];
        widget.Id.Should().Be("demo-stats");
        widget.Module.Should().Be("DemoStatsWidget");
        widget.Slot.Should().Be("admin.dashboard");
        widget.Order.Should().Be(900);
        widget.Label.Should().ContainKey("en");
        widget.Label.Should().ContainKey("ru");
    }
}
