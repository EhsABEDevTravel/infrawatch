# Release Process

InfraWatch follows a lightweight release process based on **GitHub Flow**, **Semantic Versioning**, and **Conventional Commits**.

## 1. Release Flow

```text
                         main
                           │
                           ▼
                  Review release changes
                           │
                           ▼
                   Determine version
                           │
             ┌─────────────┼─────────────┐
             │             │             │
             ▼             ▼             ▼
     BREAKING CHANGE     feat           fix
             │             │             │
             ▼             ▼             ▼
           MAJOR         MINOR         PATCH
             │             │             │
             └─────────────┼─────────────┘
                           ▼
                    Update version
                           │
                           ▼
                      Create tag
                           │
                           ▼
                         Deploy
```

## 2. Semantic Versioning

InfraWatch uses:

```text
MAJOR.MINOR.PATCH
```

### MAJOR — Breaking Changes

Increment when the release introduces changes that are not backward compatible.

Examples:

```text
1.4.2 → 2.0.0
```

Typical triggers:

- Breaking API changes
- Breaking configuration changes
- Breaking behavior changes
- `BREAKING CHANGE` in a commit

---

### MINOR — New Features

Increment when the release contains new functionality in a backward-compatible manner.

- A `feat` commit triggers a **MINOR** version.
- Multiple `feat` commits can be included in the same release.
- The release receives **one MINOR version increment**.

Example:

```text
feat(api): add disk usage endpoint
feat(api): add memory usage endpoint
feat(monitoring): add health check
```

Release:

```text
1.4.2 → 1.5.0
```

---

### PATCH — Bug Fixes

Increment when the release contains backward-compatible bug fixes.

- A `fix` commit triggers a **PATCH** version.
- `docs`, `refactor`, `test`, `perf`, and `chore` commits do not automatically trigger a PATCH version.

Example:

```text
fix(api): handle missing server
```

Release:

```text
1.4.2 → 1.4.3
```

## 3. Version Priority

When a release contains different types of changes, use the **highest applicable version level**.

```text
BREAKING CHANGE
      │
      ▼
    MAJOR
      │
      ▼
     feat
      │
      ▼
    MINOR
      │
      ▼
     fix
      │
      ▼
    PATCH
```

Examples:

```text
feat + fix
    → MINOR

fix + docs + refactor
    → PATCH

feat + BREAKING CHANGE
    → MAJOR
```

## 4. Release Preparation

Before creating a release:

- Ensure all intended changes are merged into `main`.
- Ensure tests and required quality checks pass.
- Review the commits included in the release.
- Determine the next version.
- Update the project version where required.
- Review release notes or changelog.

For larger releases that require stabilization, a release branch may be used:

```text
main
 │
 └── release/1.5.0
          │
          ├── release preparation
          ├── final fixes
          └── validation
                 │
                 ▼
                main
```

## 5. Create Release Tag

InfraWatch uses Git tags to identify released versions.

Tag format:

```text
v<MAJOR>.<MINOR>.<PATCH>
```

Examples:

```text
v1.0.0
v1.5.0
v2.0.0
```

Create an annotated tag:

```bash
git tag -a v1.5.0 -m "release: v1.5.0"
```

Push the tag:

```bash
git push origin v1.5.0
```

## 6. Deployment

After the release tag is created:

```text
Release Tag
     │
     ▼
   Build
     │
     ▼
   Test
     │
     ▼
  Deploy
     │
     ▼
  Verify
```

The deployment process depends on the project's infrastructure and CI/CD configuration.

## 7. Hotfix Release

For an urgent production bug:

```text
main
 │
 └── fix/<developer>/<description>
              │
              ▼
         Pull Request
              │
              ▼
            main
              │
              ▼
         PATCH release
```

Example:

```text
Current version: 1.5.0

fix(api): handle invalid server id

New version: 1.5.1
```

## 8. Release Checklist

```text
[ ] All intended changes merged into main
[ ] Tests and quality checks pass
[ ] Review release commits
[ ] Determine MAJOR / MINOR / PATCH
[ ] Update project version
[ ] Review release notes / changelog
[ ] Create Git tag
[ ] Push Git tag
[ ] Build
[ ] Test
[ ] Deploy
[ ] Verify deployment
```

## 9. Release Rules

```text
BREAKING CHANGE → MAJOR
feat            → MINOR
fix             → PATCH
other types     → no automatic version bump
```

**Important:** The version is determined from the **changes included in the release**, not from the number of commits. Multiple `feat` commits still produce a single MINOR version increment.

### Types

Here are the standard types and its version impact when to use each.

This convention follows @commitlint/config-conventional, get reference at here [Conventional Commits Guide: Rules, Tools and CI/CD Enforcement
](https://www.deployhq.com/blog/conventional-commits-a-standardized-approach-to-commit-messages)


| **Type**   | **Purpose**                                           | **Version impact**           |
| ---------- | ----------------------------------------------------- | ---------------------------- |
| `feat`     | A new feature visible to users                        | Minor bump                   |
| `fix`      | A bug fix                                             | Patch bump                   |
| `docs`     | Documentation only                                    | No release                   |
| `style`    | Formatting, semicolons, whitespace -- no logic change | No release                   |
| `refactor` | Code restructuring without changing behaviour         | No release                   |
| `perf`     | Performance improvement                               | Patch bump                   |
| `test`     | Adding or updating tests                              | No release                   |
| `build`    | Build system or external dependency changes           | No release                   |
| `ci`       | CI/CD configuration changes                           | No release                   |
| `chore`    | Maintenance tasks (dependency updates, tooling)       | No release                   |
| `revert`   | Reverts a previous commit                             | Depends on what was reverted |

`feat` and `fix` are the only two types the base specification mandates; everything else is an established convention that the `@commitlint/config-conventional` preset ships with by default. The `revert` type is a common source of confusion -- the spec treats it as a normal commit whose footer references the SHA(s) being reverted (`Refs: <hash>`), and the version impact mirrors whatever change is being undone.