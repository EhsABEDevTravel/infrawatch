# Pull Request Template

## 1. Purpose

This template standardizes Pull Requests (PRs) to make changes easier to understand, review, test, and merge.

All developers should use this template when submitting changes to `main`.

## 2. Pull Request Title

Use the Conventional Commits format:

```text
<type>[optional scope]: <description>
```

Examples:

```text
feat(api): add disk usage endpoint
fix(monitoring): correct CPU usage calculation
docs(git): add branching strategy
refactor(api): simplify monitoring service
```

## 3. Pull Request Description

Use the following template when creating a Pull Request.

```markdown
## Summary

<!-- Describe what this PR changes and why. -->

## Changes

- 
- 

## Related Issue

<!-- Link the related issue, if applicable. -->

Closes #

## Testing

<!-- Describe how the changes were tested. -->

- [ ] Unit tests pass
- [ ] Integration tests pass, if applicable
- [ ] Manual testing completed, if applicable

## Checklist

- [ ] The branch follows the naming convention.
- [ ] Commit messages follow Conventional Commits.
- [ ] The code follows project conventions.
- [ ] No unrelated changes are included.
- [ ] Documentation is updated, if needed.
- [ ] No secrets or sensitive configuration are committed.
- [ ] All relevant tests pass.
```

## 4. Review and Merge Rules

- Create working branches from `main`.
- Keep each PR focused on one logical change.
- Explain the purpose and impact of the changes.
- Resolve review comments before merging.
- Ensure relevant tests and quality checks pass.
- Merge only changes that are ready for `main`.
- Delete the working branch after merging.

## 5. Merge Strategy

Use **Squash and merge** when possible to keep the `main` history clean and focused.

The resulting commit on `main` should follow the repository's Conventional Commits format.

Example:

```text
feat(api): add disk usage endpoint
```

## 6. General Rules

- Keep PRs small enough to review effectively.
- Link related issues when available.
- Include screenshots or logs for UI changes or behavior that benefits from visual evidence.
- Clearly document breaking changes and required configuration updates.
- Do not merge incomplete or unverified changes into `main`.
