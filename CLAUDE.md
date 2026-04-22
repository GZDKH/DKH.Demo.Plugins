# DKH.Demo.Plugins

Demo repository for validating the DKH.Platform plugin system.

<!-- BEGIN REQUIRED-READING -->

## Required Reading (MUST read before working)

Before starting any task in this repository, you MUST read these files from DKH.Architecture:

1. **[AGENTS.md](https://github.com/GZDKH/DKH.Architecture/blob/main/AGENTS.md)** — baseline rules for all repos
2. **[agents-dotnet.md](https://github.com/GZDKH/DKH.Architecture/blob/main/docs/agents-dotnet.md)** — .NET specific rules
3. **[github-workflow.md](https://github.com/GZDKH/DKH.Architecture/blob/main/docs/github-workflow.md)** — GitHub Issues & Project Board

These files are located in the DKH.Architecture repository (located in the sibling `libraries/DKH.Architecture` folder relative to your workspace).

---

<!-- END REQUIRED-READING -->

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

## Shared Agent Rules

All mandatory rules (commits, security, gitlab-workflow, github-tasks, plan-execution, docs-after-impl, prod-debugging, prod-config, ralph-loop, contracts, platform-release, build-before-commit, no-duplication) are sourced from `agents/DKH.AgentRules` via the root monorepo `CLAUDE.md` import chain. No local copies are tracked in this repo.
