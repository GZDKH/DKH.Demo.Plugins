# DKH.Demo.Plugins

Demo repository that validates the DKH.Platform plugin system end-to-end: discovery, loading, DI isolation, extension dispatch, DbContext migrations, health checks, and enable/disable lifecycle.

## Structure

```
DKH.Demo.Plugins/
├── src/
│   ├── DKH.Demo.Plugins.SamplePlugin/   # Sample plugin (PlatformPluginBase)
│   └── DKH.Demo.Plugins.Host/           # Host web app with management API
└── tests/
    └── DKH.Demo.Plugins.Tests/           # Unit tests
```

## Quick start

```bash
# Build
dotnet build DKH.Demo.Plugins.slnx -c Release

# Run tests
dotnet test DKH.Demo.Plugins.slnx -c Release

# Run the host (plugin is auto-copied during build)
dotnet run --project src/DKH.Demo.Plugins.Host
```

## API endpoints

| Method | Route | Description |
|--------|-------|-------------|
| GET | `/api/plugins` | List all plugins |
| GET | `/api/plugins/{id}` | Get plugin info |
| POST | `/api/plugins/{id}/enable` | Enable plugin |
| POST | `/api/plugins/{id}/disable` | Disable plugin |
| GET | `/api/extensions/product/{productId}/enrich` | Dispatch product enrichment |
| GET | `/api/extensions/navigation` | Collect navigation items |
| GET | `/api/extensions/widgets` | Collect widget descriptors |
| POST | `/api/extensions/checkout` | Execute checkout pipeline |
| GET | `/health` | Health check (includes plugin-host) |

## Sample plugin

The `SamplePlugin` demonstrates all extension types:

- **SampleProductExtension** — enriches product data with `demo_enriched` and `demo_timestamp`
- **SampleCheckoutStep** — checkout pipeline step (Order=100, always succeeds)
- **SampleNavigationProvider** — adds "Demo Settings" nav item
- **SampleWidgetProvider** — adds "Demo Stats" widget descriptor

It also registers a `SamplePluginDbContext` (SQLite) with a `PluginSetting` entity, and seeds a default setting on initialization.

## Assembly isolation

The plugin references `DKH.Platform.Plugins.Sdk` and `Microsoft.EntityFrameworkCore` with `ExcludeAssets="runtime"`. At runtime, these resolve from the host's default ALC, ensuring type identity across the plugin boundary.

## Platform packages used

- `DKH.Platform` — entry point (`Platform.CreateWeb`)
- `DKH.Platform.Plugins.Host` — plugin discovery, loading, registry, dispatcher
- `DKH.Platform.Plugins.Sdk` — `PlatformPluginBase` and extension base classes
- `DKH.Platform.MinimalApi` — Minimal API with OpenAPI
- `DKH.Platform.Logging` — Serilog structured logging
