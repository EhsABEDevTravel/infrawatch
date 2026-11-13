# Branching Strategy

InfraWatch follows **GitHub Flow**: a simple branch model designed for continuous development and deployment.

## 1. Branch Structure

```text
main
 │
 ├── feature/<developer>/<description>
 │
 ├── fix/<developer>/<description>
 │
 └── docs/<developer>/<description>
```

### `main`

- The primary branch of the repository.
- Always contains production-ready code.
- New work should start from `main`.
- Changes are merged into `main` through Pull Requests.

### Working Branches

Developers create short-lived branches from `main` for a specific change.

| Branch | Purpose | Example |
|---|---|---|
| `feature/` | New functionality | `feature/AnhNguyen/disk-monitoring` |
| `fix/` | Bug fixes | `fix/AnhNguyen/disk-usage` |
| `docs/` | Documentation | `docs/AnhNguyen/git-workflow` |
| `refactor/` | Code restructuring | `refactor/AnhNguyen/monitoring-service` |
| `test/` | Test changes | `test/AnhNguyen/monitoring-api` |
| `chore/` | Maintenance/tooling | `chore/AnhNguyen/update-dependencies` |

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
              │ feature/<developer>/... │
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
<type>/<developer>/<short-description>
```

Examples:

```text
feature/AnhNguyen/disk-monitoring
fix/AnhNguyen/cpu-calculation
docs/AnhNguyen/git-workflow
refactor/AnhNguyen/monitoring-service
```

Rules:

- Use lowercase for the branch type and description.
- Use `/` to separate logical sections.
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

Yes. Since you're using **GitHub Flow**, I would define `release/` as a **short-lived branch used only when preparing a release**, not as a permanent branch.

Add this section:

## 5. Release Branch

A `release/` branch is created from `main` when a release requires a dedicated stabilization or release-preparation phase.

```text
main
 │
 ├── feature/...
 ├── fix/...
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
- Update the project version on the release branch.
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