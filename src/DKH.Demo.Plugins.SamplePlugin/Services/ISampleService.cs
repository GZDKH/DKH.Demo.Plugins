using DKH.Demo.Plugins.SamplePlugin.Data.Entities;

namespace DKH.Demo.Plugins.SamplePlugin.Services;

public interface ISampleService
{
    Task<PluginSetting?> GetSettingAsync(string key, CancellationToken cancellationToken = default);

    Task SetSettingAsync(string key, string value, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<PluginSetting>> GetAllSettingsAsync(CancellationToken cancellationToken = default);
}
