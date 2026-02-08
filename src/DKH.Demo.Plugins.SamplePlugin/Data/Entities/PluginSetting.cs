namespace DKH.Demo.Plugins.SamplePlugin.Data.Entities;

public sealed class PluginSetting
{
    public Guid Id { get; set; }

    public required string Key { get; set; }

    public required string Value { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}
