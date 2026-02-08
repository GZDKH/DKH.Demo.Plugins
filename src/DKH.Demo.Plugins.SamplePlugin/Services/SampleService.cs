using DKH.Demo.Plugins.SamplePlugin.Data;
using DKH.Demo.Plugins.SamplePlugin.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DKH.Demo.Plugins.SamplePlugin.Services;

public sealed class SampleService(SamplePluginDbContext dbContext) : ISampleService
{
    public async Task<PluginSetting?> GetSettingAsync(
        string key, CancellationToken cancellationToken = default)
    {
        return await dbContext.Settings.FirstOrDefaultAsync(s => s.Key == key, cancellationToken);
    }

    public async Task SetSettingAsync(string key, string value, CancellationToken cancellationToken = default)
    {
        var setting = await dbContext.Settings
            .FirstOrDefaultAsync(s => s.Key == key, cancellationToken);

        if (setting is null)
        {
            setting = new PluginSetting
            {
                Id = Guid.NewGuid(),
                Key = key,
                Value = value
            };
            dbContext.Settings.Add(setting);
        }
        else
        {
            setting.Value = value;
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<PluginSetting>> GetAllSettingsAsync(
        CancellationToken cancellationToken = default)
    {
        return await dbContext.Settings.OrderBy(s => s.Key).ToListAsync(cancellationToken);
    }
}
