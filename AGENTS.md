# DKH.Demo.Plugins


<!-- BEGIN REQUIRED-READING -->

## Required Reading (MUST read before working)

Before starting any task in this repository, read the shared DKH.AgentRules entrypoint:

1. **[AGENTS.md](../../agents/DKH.AgentRules/AGENTS.md)** — shared Codex entrypoint and on-demand trigger index

Profiles, skills, build gates, contracts, releases, and docs rules are lazy-loaded from `agents/DKH.AgentRules`. Use `../../agents/DKH.AgentRules/rules/codex/triggers.md` to decide what else to open for the current task.

---

<!-- END REQUIRED-READING -->
Demo repository for validating the DKH.Platform plugin system.

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
