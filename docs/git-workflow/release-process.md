# Release Process

InfraWatch follows a lightweight release process based on **GitHub Flow**, **Semantic Versioning**, and **Conventional Commits**.

Releases are created manually. There is no release automation, and the version is recorded by the Git tag.

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
     BREAKING CHANGE     feat           fix/perf
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
- `BREAKING CHANGE` in a commit footer, or `!` after the type or scope in a commit or PR title

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

- A `fix` or `perf` commit triggers a **PATCH** version.
- `docs`, `style`, `refactor`, `test`, `build`, `ci`, and `chore` commits do not automatically trigger a PATCH version.

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
- Update the project version files, if the project has them (see [Version Source](#version-source)).
- Update `CHANGELOG.md`.

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

### Version Source

- The Git tag `vX.Y.Z` is the source of truth for the version.
- When the backend and frontend projects exist, keep their version fields in sync with the tag: `<Version>` in the backend `.csproj` and `version` in the frontend `package.json`.
- `CHANGELOG.md` is maintained by hand. Create it with the first release, and write each entry from the commits included in that release.
- The repository does not yet contain these project files or `CHANGELOG.md`. Until they exist, the tag is the only version record.

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
git tag -a v1.5.0 -m "v1.5.0"
```

Push the tag:

```bash
git push origin v1.5.0
```

## 6. Deployment

No deployment pipeline exists yet. Use this section as the target process once one is defined.

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
 └── fix/<DeveloperName>/<description>
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
[ ] Update project version files, if any
[ ] Update CHANGELOG.md
[ ] Create Git tag
[ ] Push Git tag
[ ] Build
[ ] Test
[ ] Deploy
[ ] Verify deployment
```

## 9. Release Rules

```text
BREAKING CHANGE or ! → MAJOR
feat            → MINOR
fix, perf       → PATCH
other types     → no automatic version bump
```

**Important:** The version is determined from the **changes included in the release**, not from the number of commits. Multiple `feat` commits still produce a single MINOR version increment.

### Types

The type list follows the [Conventional Commits](https://www.conventionalcommits.org/en/v1.0.0/) specification and the `@commitlint/config-conventional` preset. See [Commit Types](git-commit-convention.md#2-commit-types) for when to use each type.

| **Type**   | **Version impact**          |
| ---------- | --------------------------- |
| `feat`     | Minor bump                  |
| `fix`      | Patch bump                  |
| `perf`     | Patch bump                  |
| `docs`     | No bump                     |
| `style`    | No bump                     |
| `refactor` | No bump                     |
| `test`     | No bump                     |
| `build`    | No bump                     |
| `ci`       | No bump                     |
| `chore`    | No bump                     |
| `revert`   | Mirrors the reverted change |

`feat` and `fix` are the only two types the base specification mandates. The other types come from the `@commitlint/config-conventional` preset. A `revert` commit's version impact mirrors the change it undoes. See [Revert Commit](git-commit-convention.md#8-revert-commit) for the format.