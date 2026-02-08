using DKH.Demo.Plugins.SamplePlugin.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DKH.Demo.Plugins.SamplePlugin.Data;

public sealed class SamplePluginDbContext(DbContextOptions<SamplePluginDbContext> options)
    : DbContext(options)
{
    public DbSet<PluginSetting> Settings => Set<PluginSetting>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PluginSetting>(entity =>
        {
            entity.ToTable("plugin_settings");
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Key).IsUnique();
            entity.Property(e => e.Key).IsRequired().HasMaxLength(256);
            entity.Property(e => e.Value).IsRequired();
        });
    }
}
