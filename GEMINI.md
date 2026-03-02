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

<!-- BEGIN LOCAL-GEMINI-RULES -->

## Additional Local Rules (.gemini/rules)

Before starting implementation, you MUST also read and follow these local rule files in this repository:

- `.gemini/rules/build-before-commit.md`
- `.gemini/rules/commits.md`
- `.gemini/rules/contracts.md`
- `.gemini/rules/github-tasks.md`
- `.gemini/rules/gitlab-workflow.md`
- `.gemini/rules/no-duplication.md`
- `.gemini/rules/prod-config.md`
- `.gemini/rules/security.md`

These rules are mandatory and complement the baseline `AGENTS.md` and `DKH.Architecture` guidance.

<!-- END LOCAL-GEMINI-RULES -->
