# DKH.Demo.Plugins

Demo repository for validating the DKH.Platform plugin system.

## Shared rules

Follow baseline rules from DKH.Architecture:
- @libraries/DKH.Architecture/AGENTS.md
- @libraries/DKH.Architecture/docs/agents-dotnet.md
- @libraries/DKH.Architecture/docs/agents-platform.md

## Build and test

```bash
dotnet build DKH.Demo.Plugins.slnx -c Release   # 0 errors, 0 warnings
dotnet test DKH.Demo.Plugins.slnx -c Release     # all tests pass
```

## Key conventions

- SamplePlugin uses `ExcludeAssets="runtime"` for Platform/EF packages (assembly isolation)
- Plugin output is copied to `src/DKH.Demo.Plugins.Host/plugins/sample-plugin/` by MSBuild target
- The `plugins/` directory is gitignored
- Host uses `Platform.CreateWeb(args)` entry point
- All Platform config under `Platform:*` prefix in appsettings.json
- Conventional Commits: `<type>(<scope>): <summary>`
- Do NOT add `Co-Authored-By` or any author attribution to commits
