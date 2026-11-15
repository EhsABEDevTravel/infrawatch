# Pull Request Template

## 1. Purpose

This template standardizes Pull Requests (PRs) to make changes easier to understand, review, test, and merge.

All developers should use this template when submitting changes to `main`.

## 2. Pull Request Title

Use the Conventional Commits format:

```text
<type>[optional scope]: <description>
```

The `<type>` must be one of the types listed in the [commit convention](git-commit-convention.md#2-commit-types). The title must be 72 characters or fewer.

Examples:

```text
feat(api): add disk usage endpoint
fix(monitoring): correct CPU usage calculation
docs(git): add branching strategy
refactor(api): simplify monitoring service
feat(api)!: remove the legacy disk endpoint
```

Add `!` after the type or scope for a breaking change. See [Breaking Changes](git-commit-convention.md#7-breaking-changes).

## 3. Pull Request Description

Use the following template when creating a Pull Request.

```markdown
## Summary

<!-- Describe what this PR changes and why. -->

## Changes

- 
- 

## Breaking Change

<!-- Describe the impact and the migration steps. Write "None" if this PR has no breaking change. -->

None

## Related Issue

<!-- Link the related issue, if applicable. Use "Closes #<n>" to close an issue on merge, or "Refs #<n>" for a related item that should stay open. -->

Closes #

## Testing

<!-- Describe how the changes were tested. -->

- [ ] Unit tests pass
- [ ] Integration tests pass, if applicable
- [ ] Manual testing completed, if applicable

## Checklist

- [ ] The PR title follows the Conventional Commits format.
- [ ] The branch follows the naming convention.
- [ ] Commit messages follow Conventional Commits.
- [ ] Breaking changes are marked with `!` in the title and described above.
- [ ] The code follows project conventions.
- [ ] No unrelated changes are included.
- [ ] Documentation is updated, if needed.
- [ ] No secrets or sensitive configuration are committed.
- [ ] All relevant tests pass.
```

## 4. Review and Merge Rules

- Create working branches from `main`.
- Do not push directly to `main`. Every change reaches `main` through a Pull Request.
- Keep each PR focused on one logical change.
- Explain the purpose and impact of the changes.
- Request at least one approval from another developer before merging.
- Resolve review comments before merging.
- Ensure relevant tests and quality checks pass.
- Merge only changes that are ready for `main`.
- Delete the working branch after merging.

## 5. Merge Strategy

Use **Squash and merge** for every Pull Request. This keeps the `main` history clean, with one commit per PR.

Squash merge creates one commit on `main`, using the PR title as its message. The PR title must therefore follow the Conventional Commits format, and it must include `!` for a breaking change. Only the title reaches `main`, so put the full breaking-change details and migration steps in the PR description, and in `CHANGELOG.md` when the change is released.

Example:

```text
feat(api): add disk usage endpoint
```

## 6. General Rules

- Keep PRs small enough to review effectively. Aim for about 400 changed lines or fewer where practical.
- Link related issues when available.
- Include screenshots or logs for UI changes or behavior that benefits from visual evidence.
- Clearly document breaking changes and required configuration updates.
- Do not merge incomplete or unverified changes into `main`.

