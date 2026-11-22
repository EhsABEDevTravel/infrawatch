# Git Commit Convention

This repository follows the [Conventional Commits](https://www.conventionalcommits.org/en/v1.0.0/) specification. The stylistic rules are inherited from the [Angular commit convention](https://github.com/angular/angular/blob/main/contributing-docs/commit-message-guidelines.md).
Setup and tools are described in [Git Convention Tools](git-convention-tools.md). 
The goal is to keep commit history consistent, readable, and easy to understand.

---

## 1. Commit Message Format

Use the following format:

```text
<type>(<optional scope>): <description>

<optional body>

<optional footer>
```

The parts are:
- **type** — the commit type from [section 2](#2-commit-types)
- **scope (optional)** — a short lowercase word for the area changed. Free text, not checked (see [section 3](#3-scope-optional))
- **description** — briefly explains what changed

The header must be 100 characters or fewer. Wrap body and footer lines at 100 characters. 

Examples:
- `feat(monitoring): add disk usage endpoint`
- `feat: add disk usage endpoint`

## 2. Commit Types
Use one of the following commit types:
| Type | Description | Example |
|---|---|---|
| `build` | Changes that affect the build system or external dependencies | `build: update .NET dependencies` |
| `chore` | Maintenance tasks that do not modify application behavior | `chore: update Docker configuration` |
| `ci` | Changes to CI configuration and scripts | `ci: add build workflow` |
| `docs` | Documentation-only changes | `docs: update Git workflow` |
| `feat` | A new feature | `feat: add disk usage endpoint` |
| `fix` | A bug fix | `fix: handle unavailable drives` |
| `perf` | A code change that improves performance | `perf: optimize disk usage collection` |
| `refactor` | A code change that neither fixes a bug nor adds a feature | `refactor: simplify monitoring service` |
| `revert` | Reverts a previous commit (see section 8) | `Revert "feat: add disk usage endpoint"` |
| `style` | Formatting only, with no change to logic | `style: format monitoring service` |
| `test` | Adding missing tests or correcting existing tests | `test: add disk usage tests` |

These are the only allowed types. The release docs refer to this list.

Examples:
```
feat(monitoring): add disk usage endpoint
test(monitoring): add disk usage endpoint tests
refactor(monitoring): simplify disk usage service
ci: add dotnet build workflow
build: update ASP.NET Core dependencies
docs: add branching strategy
chore: update development configuration
```

## 3. Scope (Optional)

The scope is optional. When used, write a short lowercase word for the area the change touches, such as `disk` or `api`. The scope is free text: commitlint and the PR title check do not restrict it.

⚠️ Do not add a scope just for the sake of adding one. 

## 4. Commit Description
Write a short and clear description of the change.

**💁🏻 Guidelines**: 
- Use lowercase, for both `type` and `scope`.
- Do not capitalize the first letter
- Do not end the description with a period (.).
- Keep each commit focused on one logical change.
- Keep the subject short and clear.
- Do not use vague messages such as `update`, `fix stuff`, or `changes`.
- Use the imperative, present tense: "change" not "changed" nor "changes"

✅ Examples Good:

```
feat(api): add disk usage endpoint
fix(api): return 404 when server is unavailable
docs(git): document branching strategy
refactor(service): simplify monitoring logic
test(api): add monitoring endpoint tests
chore(docker): update development image
```

❌ Examples Bad:

```
update code
fix stuff
changes
final version
working
```

## 5. Body (Optional)
- Explain the motivation (reasons) for the change in the commit message body.
- Should wrap lines at most 100 characters. 
## 6. Footer (Optional)
- The footer can contain information about **breaking changes** and **deprecations** 
- It is also the place to reference GitHub issues and PRs. Use `Refs #<number>` for related issues or PRs. Use `Fixes #<issue>` or `Closes #<issue>` to close an issue. Closing keywords do not close PRs, and squash merging may drop commit footers, so put `Closes #<issue>` in the [PR description](pull-request-template.md#3-pull-request-description) as well. 

For example:
```
BREAKING CHANGE: <breaking change summary>

<breaking change description + migration instructions>

Fixes #<issue number>
```

```
DEPRECATED: <what is deprecated>

<deprecation description + recommended update path>

Refs #<pr number>
```

`DEPRECATED:` is a repo-specific footer. It is not a Conventional Commits keyword and has no versioning effect.

## 7. Breaking Changes
- For changes that break the existing API or behavior, add ! after the type or scope. A `BREAKING CHANGE:` footer alone does not trigger a release; the `!` in the PR title does.
- While the version is `0.x`, a breaking change releases a MINOR version. Move to `1.0.0` when the REST endpoints and configuration keys are stable. See [Semantic Versioning](release-process.md#2-semantic-versioning).
- In a squash-merged PR, the `!` must be in the PR title, because the title becomes the commit on `main`. See [Pull Request Template](pull-request-template.md#2-pull-request-title).

- Breaking changes should be described in the [commit footer](#6-footer-optional) section, if the [commit description](#4-commit-description) isn't sufficiently informative

Example:
- Message with description and breaking change footer

```
feat: allow provided config object to extend other configs

BREAKING CHANGE: `extends` key in config file is now used for extending other config files
```

- Message with ! to draw attention to breaking change

```
feat!: send an email to the customer when a product is shipped
feat!: send an email to the customer when a product is shipped

```

## 8. Revert Commit
If a commit reverts a previous commit:
- Create it with `git revert --no-commit <sha>`, then run `git commit`.
- Keep git's default header, `Revert "<header of the reverted commit>"`. Do not rename it.
- Add `This reverts commit <sha>.` to the body yourself. `--no-commit` does not add it.
- If the reverted commit is a breaking change (its header has `!`), add `!` after `revert` in the PR title and the commit header, for example `revert!: feat!: ...`. The revert is MAJOR, and the `!` marks it as breaking.
- Clearly explain why the commit is being reverted.

Example:
```
revert: feat(monitoring): add disk usage endpoint

This reverts commit a1b2c3d4.

The endpoint causes unexpected disk access errors in production.

```

Example of reverting a breaking change:
```
revert!: feat!: send an email to the customer when a product is shipped

This reverts commit e5f6a7b8.

The email job sends duplicate messages and must be redesigned before it ships again.
```


