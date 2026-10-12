# Git Commit Convention

This repository follows the [Conventional Commits](https://www.conventionalcommits.org/en/v1.0.0/) specification. The stylistic rules are inherited from the [Angular commit convention](https://github.com/angular/angular/blob/main/contributing-docs/commit-message-guidelines.md).
Install Git Convention Extension at 
The goal is to keep commit history consistent, readable, and easy to understand.

---

## 1. Commit Message Format

Use the following format:

```text
<type>(<optional scope>): <description>
    <BLANK LINE>
<optional body>
    <BLANK LINE>
<optional footer>
```

The parts are:
- ***type** — describes the Commit Type
- **scope (optional)** — identifies the affected area
- ***description** — briefly explains what changed 

Examples:
- `feat(monitoring): add disk usage endpoint`
- `feat: add disk usage endpoint`

## 2. Commit Types
Use one of the following commit types:
| Type | Description | Example |
|---|---|---|
| `build` | Changes that affect the build system or external dependencies | `build: update .NET dependencies` |
| `ci` | Changes to CI configuration and scripts | `ci: add build workflow` |
| `docs` | Documentation-only changes | `docs: update Git workflow` |
| `feat` | A new feature | `feat: add disk usage endpoint` |
| `fix` | A bug fix | `fix: handle unavailable drives` |
| `perf` | A code change that improves performance | `perf: optimize disk usage collection` |
| `refactor` | A code change that neither fixes a bug nor adds a feature | `refactor: simplify monitoring service` |
| `test` | Adding missing tests or correcting existing tests | `test: add disk usage tests` |
| `chore` | Maintenance tasks that do not modify application behavior | `chore: update Docker configuration` |

Examples:
```
feat(monitoring): add disk usage endpoint
test(monitoring): add disk usage endpoint tests
refactor(monitoring): simplify disk usage service
ci: add dotnet build workflow
build: update ASP.NET Core dependencies
docs(git): add branching strategy
chore(docker): update development configuration
```

## 3. Scope (Optional)

Use a scope when it provides useful context about the affected area.

⚠️ Do not add a scope just for the sake of adding one. 

## 4. Commit Description
Write a short and clear description of the change.

**💁🏻 Guidelines**: 
- Use lowercase, for both `type` and `scope`.
- Do not capitalize the first letter
- Do not end the description with a period (.).
- Write the description in the imperative mood.
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
- Use the imperative, present tense: "change" not "changed" nor "changes"


## 6. Footer (Optional)
- The footer can contain information about **breaking changes** and **deprecations** 
- It is also the place to reference GitHub issues and other PRs that this commit closes or is related to. 

For example:
```
BREAKING CHANGE: <breaking change summary>
    <BLANK LINE>
<breaking change description + migration instructions>
    <BLANK LINE>
    <BLANK LINE>
Fixes #<issue number>
```

```
DEPRECATED: <what is deprecated>
    <BLANK LINE>
<deprecation description + recommended update path>
    <BLANK LINE>
    <BLANK LINE>
Closes #<pr number>
```

## 7. Breaking Changes
- For changes that break the existing API or behavior, add ! after the type or scope.

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
feat(api)!: send an email to the customer when a product is shipped

```

## 8. Revert Commit
If a commit reverts a previous commit:
- Start the commit with revert:, followed by the header of the reverted commit.
- Include the SHA of the reverted commit in the body using:
- Clearly explain why the commit is being reverted.

Example:
```
revert: feat(monitoring): add disk usage endpoint

This reverts commit a1b2c3d4.

The endpoint causes unexpected disk access errors in production.

```

📍Note

>One important improvement over your original wording is **"backward-compatible"**. A `feat` normally means a new feature, but if the feature changes an existing API in a breaking way, it belongs to the **MAJOR** version instead of MINOR.

> `fix` type commits should be translated to **PATCH** releases. `feat` type commits should be translated to **MINOR** releases. Commits with `BREAKING CHANGE` in the commits, regardless of type, should be translated to **MAJOR** releases. ([Conventional Commits](https://www.conventionalcommits.org/en/v1.0.0/#how-does-this-relate-to-semver))

