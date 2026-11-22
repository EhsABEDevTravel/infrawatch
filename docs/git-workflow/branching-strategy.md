# Branching Strategy

InfraWatch follows **GitHub Flow**: a simple branch model designed for continuous development and deployment.

## 1. Branch Structure

```text
main
 │
 ├── feat/<DeveloperName>/<description>
 │
 ├── fix/<DeveloperName>/<description>
 │
 └── docs/<DeveloperName>/<description>
```

### `main`

- The primary branch of the repository.
- Always contains production-ready code.
- New work should start from `main`.
- Changes are merged into `main` through Pull Requests.

### Working Branches

Developers create short-lived branches from `main` for a specific change. The branch prefix is the commit type that best describes the change, so each prefix matches a type in the [commit convention](git-commit-convention.md#2-commit-types).

| Branch      | Purpose                               | Example                                  |
| ----------- | ------------------------------------- | ---------------------------------------- |
| `feat/`     | New functionality                     | `feat/AnhNguyen/disk-monitoring`         |
| `fix/`      | Bug fixes                             | `fix/AnhNguyen/disk-usage`               |
| `perf/`     | Performance improvements              | `perf/AnhNguyen/disk-collection`         |
| `refactor/` | Code restructuring                    | `refactor/AnhNguyen/monitoring-service`  |
| `docs/`     | Documentation                         | `docs/AnhNguyen/git-workflow`            |
| `test/`     | Test changes                          | `test/AnhNguyen/monitoring-api`          |
| `build/`    | Build system or external dependencies | `build/AnhNguyen/update-dotnet-packages` |
| `ci/`       | CI configuration                      | `ci/AnhNguyen/add-build-workflow`        |
| `chore/`    | Maintenance tasks                     | `chore/AnhNguyen/update-docker-config`   |
| `style/`    | Formatting only                       | `style/AnhNguyen/format-monitoring`      |
| `revert/`   | Reverting a previous change           | `revert/AnhNguyen/disk-endpoint`         |

Releases are cut from `main` without a release branch. See [Release Process](release-process.md).

## 2. Development Flow

```text
                    ┌───────────────┐
                    │     main      │
                    └───────┬───────┘
                            │
                       create branch
                            │
                            ▼
              ┌─────────────────────────────┐
              │ feat/<DeveloperName>/...    │
              └────────────┬────────────────┘
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
                    │ Squash merge│
                    │   to main   │
                    └─────────────┘
```

Deployment and tagging happen after the merge, as described in [Release Process](release-process.md).

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

There is no `release/` branch. Releases are tagged on `main`at current time. See [Release Process](release-process.md).

## 5. References

- [GitHub flow](https://docs.github.com/en/get-started/using-github/github-flow)
