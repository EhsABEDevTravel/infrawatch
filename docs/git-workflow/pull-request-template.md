# Pull Request Template

## 1. Purpose

This template standardizes Pull Requests (PRs) to make changes easier to understand, review, test, and merge.

All developers should use this template when submitting changes to `main`.

## 2. Pull Request Title

Use the Conventional Commits format:

```text
<type>[optional scope]: <description>
```

The `<type>` must be one of the types listed in the [commit convention](git-commit-convention.md#2-commit-types). The title must be 100 characters or fewer. GitHub appends ` (#N)` to the squash commit subject, so a title close to 100 characters can reach about 108 characters on `main`. This trade-off is accepted.

Examples:

```text
feat: add disk usage endpoint
fix: correct CPU usage calculation
docs: add branching strategy
refactor(monitoring): simplify monitoring service
feat!: remove the legacy disk endpoint
```

Add `!` after the type or scope for a breaking change. See [Breaking Changes](git-commit-convention.md#7-breaking-changes).

## 3. Pull Request Description

Use the following template when creating a Pull Request. GitHub loads a PR template only from the repository root, `docs/`, or `.github/`. The template GitHub uses is `.github/pull_request_template.md`; this copy in `docs/git-workflow/` is the reference, and the two must match.

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
- Request 1 approval from another developer before merging. An author cannot approve their own PR. Self-review and AI reviewer comments do not count as an approval. While the team has one maintainer, the maintainer self-reviews, records it in the PR, and merges with an admin bypass; write the reason in the PR. See [Git Convention Tools](git-convention-tools.md).
- Resolve review comments before merging.
- Ensure relevant tests and quality checks pass.
- Merge only changes that are ready for `main`.
- Delete the working branch after merging.

## 5. Merge Strategy

Use **Squash and merge** for every Pull Request. This keeps the `main` history clean, with one commit per PR.

Squash merge creates one commit on `main`, using the PR title as its message. The PR title must therefore follow the Conventional Commits format, and it must include `!` for a breaking change. The title becomes the commit subject on `main`, so put the full breaking-change details, migration steps, and `Closes #<issue>` in the PR description. Commit footers may not survive a squash merge.

Example:

```text
feat: add disk usage endpoint
```

## 6. General Rules

- Keep PRs small enough to review effectively. About 400 changed lines is a suggested limit, not a standard.
- Link related issues when available.
- Include screenshots or logs for UI changes or behavior that benefits from visual evidence.
- Clearly document breaking changes and required configuration updates.
- Do not merge incomplete or unverified changes into `main`.

