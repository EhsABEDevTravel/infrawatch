# Branching Strategy

InfraWatch follows **GitHub Flow**: a simple branch model designed for continuous development and deployment.

## 1. Branch Structure

```text
main
 │
 ├── feat/<Developer>/<description>
 │
 ├── fix/<Developer>/<description>
 │
 └── docs/<Developer>/<description>
```

### `main`

- The primary branch of the repository.
- Always contains production-ready code.
- New work should start from `main`.
- Changes are merged into `main` through Pull Requests.

### Working Branches

Developers create short-lived branches from `main` for a specific change. The branch prefix is the commit type that best describes the change, so each prefix matches a type in the [commit convention](git-commit-convention.md#2-commit-types).

| Branch | Purpose | Example |
|---|---|---|
| `feat/` | New functionality | `feat/AnhNguyen/disk-monitoring` |
| `fix/` | Bug fixes | `fix/AnhNguyen/disk-usage` |
| `perf/` | Performance improvements | `perf/AnhNguyen/disk-collection` |
| `refactor/` | Code restructuring | `refactor/AnhNguyen/monitoring-service` |
| `docs/` | Documentation | `docs/AnhNguyen/git-workflow` |
| `test/` | Test changes | `test/AnhNguyen/monitoring-api` |
| `build/` | Build system or external dependencies | `build/AnhNguyen/update-dotnet-packages` |
| `ci/` | CI configuration | `ci/AnhNguyen/add-build-workflow` |
| `chore/` | Maintenance tasks | `chore/AnhNguyen/update-docker-config` |
| `style/` | Formatting only | `style/AnhNguyen/format-monitoring` |
| `revert/` | Reverting a previous change | `revert/AnhNguyen/disk-endpoint` |

The `release/` prefix is also used, for release preparation only. See [Release Branch](#5-release-branch).

## 2. Development Flow

```text
                    ┌───────────────┐
                    │     main      │
                    └───────┬───────┘
                            │
                       create branch
                            │
                            ▼
              ┌─────────────────────────┐
              │ feat/<Developer>/...    │
              └────────────┬────────────┘
                           │
                       development
                           │
                       commits
                           │
                           ▼
                    ┌─────────────┐
                    │ Pull Request│
                    └──────┬──────┘
                           │
                    Review & Checks
                           │
                           ▼
                    ┌─────────────┐
                    │    main     │
                    └──────┬──────┘
                           │
                         Deploy
```

## 3. Branch Naming Convention

Use:

```text
<type>/<DeveloperName>/<lowercase-kebab-description>
```

Examples:

```text
feat/AnhNguyen/disk-monitoring
fix/AnhNguyen/cpu-calculation
docs/AnhNguyen/git-workflow
refactor/AnhNguyen/monitoring-service
```

Rules:

- `<type>` is a commit type from the [commit convention](git-commit-convention.md#2-commit-types), in lowercase. 
- `<DeveloperName>` is your name in CamelCase, without spaces. It is the only segment that uses uppercase letters.
- `<lowercase-kebab-description>` uses lowercase letters, digits, and hyphens.
- Use `/` to separate the three segments.
- Keep the description short and meaningful.
- Use one branch for one logical change.
- Delete the branch after it has been merged.
- The `release/<version>` branch is the exception. See [Release Branch](#5-release-branch).

## 4. Core Rules

```text
main
 │
 ├── Create short-lived working branch
 │
 ├── Make focused commits
 │
 ├── Push branch
 │
 ├── Open Pull Request
 │
 ├── Review + checks
 │
 ├── Merge into main
 │
 └── Delete working branch
```

**There is no permanent `develop` branch in InfraWatch.**

This keeps the workflow simple: **`main` is the source of truth, and all development happens through short-lived branches.**

Do not push directly to `main`. Every change reaches `main` through a Pull Request.

The only other branch type is `release/`, a short-lived branch used only when preparing a release. See [Release Branch](#5-release-branch).

## 5. Release Branch

A `release/` branch is created from `main` when a release requires a dedicated stabilization or release-preparation phase.

```text
main
 │
 ├── feat/<Developer>/...
 ├── fix/<Developer>/...
 │
 └── release/1.5.0
          │
          ├── release preparation
          ├── version update
          ├── final fixes
          └── release validation
                    │
                    ▼
                  main
                    │
                  Tag
                    │
                  Deploy
```

### Naming Convention

```text
release/<version>
```

Examples:

```text
release/1.5.0
release/2.0.0
```

### Rules

- Create the release branch from `main`.
- Use the release branch only for release preparation and stabilization.
- Do not develop new features on a release branch.
- Only release-related fixes should be added.
- Update the project version files on the release branch, if any.
- Merge the completed release back into `main`.
- Create the corresponding Git tag from the released commit.
- Delete the release branch after the release is completed.

### When to Use

A release branch is **optional**.

For small releases that can be deployed directly from `main`, no release branch is required.

```text
Normal release:

main ────────────────► tag ───► deploy


Release requiring stabilization:

main ──► release/1.5.0 ──► main ──► tag ──► deploy
```

This keeps `main` as the primary branch while still giving InfraWatch a controlled path for larger or more sensitive releases.

## 6. References
- [Git Branching Strategies: A Comprehensive Guide](https://dev.to/karmpatel/git-branching-strategies-a-comprehensive-guide-24kh)