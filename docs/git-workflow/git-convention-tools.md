# Git Convention Tools

This guide shows which tools help the team follow the [Git Commit Convention](git-commit-convention.md), the [Branching Strategy](branching-strategy.md), and the [Pull Request Template](pull-request-template.md). 

## 1. Tool Status

| Tool | Purpose | Status |
|---|---|---|
| Git commit template | Shows the commit format when you commit | Available now, set up per developer |
| Git identity with noreply email | Keeps personal email addresses out of the public history | Available now, set up per developer |
| GitHub CLI (`gh`) | Creates and merges PRs, and creates releases, from the terminal | Available now |
| GitHub branch ruleset and PR settings | Blocks direct pushes to `main`, and enforces PRs and squash merge | Recommended, set by a repository admin |
| commitlint | Rejects commit messages that break the convention | Phase 2, not yet configured |
| PR title check | Rejects PR titles that break the convention | Phase 2, not yet configured |
| gitleaks | Detects secrets in commits and files | Phase 2, not yet configured |

Not used:
- **gitmoji**: emoji in a commit header breaks Conventional Commits parsing.
- **conventional-changelog**: releases are manual, and `CHANGELOG.md` is written by hand.

## 2. Set Up Your Git Identity

Use your GitHub noreply address, so your personal email is not published in the public repository history.

1. Open GitHub → Settings → Emails, and copy your noreply address (`<id>+<username>@users.noreply.github.com`).
2. Turn on the option to block command line pushes that expose your email.
3. Run:

```powershell
git config --global user.name "Anh Nguyen"
git config --global user.email "<id>+<username>@users.noreply.github.com"
```

Past commits keep their original author email. Do not rewrite public history only to change it.

## 3. Commit Message Template

Git opens this template in the editor when you run `git commit` without `-m`. Lines that start with `#` are removed when you save.

Create `%USERPROFILE%\.gitmessage.txt` with this content:

```text
# <type>(<scope>): <description>
#
# Types: build, chore, ci, docs, feat, fix, perf, refactor, revert, style, test
# Header: 72 characters or fewer, lowercase, imperative, no trailing period
#
# Body (optional): explain why the change was made. Wrap lines at 72 characters.
#
# Footer (optional): BREAKING CHANGE: ..., Fixes #<n>, Closes #<n>, Refs #<n>
```

Register the template:

```powershell
git config --global commit.template ~/.gitmessage.txt
git config --global --get commit.template
```

`git commit -m "..."` skips the editor, so the template does not apply to it.

## 4. Repository Settings (Recommended)

A repository admin applies these settings on GitHub. They are not applied by the repository files. The repository is public, so these features are free.

**Pull request settings** (Settings → General → Pull Requests):
- Allow squash merging only. Turn off merge commits and rebase merging.
- Set the default squash commit message to the pull request title.
- Turn on automatically deleting head branches.

**Branch ruleset for `main`** (Settings → Rules → Rulesets → New branch ruleset):
- Target the `main` branch.
- Require a pull request before merging, with at least 1 required approval, as [section 4 of the PR template](pull-request-template.md#4-review-and-merge-rules) states. Use 0 if one person works alone, because an author cannot approve their own PR.
- Require conversation resolution before merging.
- Require linear history.
- Block force pushes and deletions.
- Leave the bypass list empty.
- Required status checks are added in Phase 2.

## 5. Daily Workflow

```powershell
# Start from the latest main
git switch main
git pull --ff-only

# Create a working branch: <type>/<DeveloperName>/<description>
git switch -c feat/AnhNguyen/disk-monitoring

# Commit. The template opens because -m is omitted.
git add <files>
git commit

# Push and open a draft PR. gh asks for the body: paste the template from the PR template, section 3.
git push -u origin HEAD
gh pr create --draft --title "feat(monitoring): add disk usage endpoint"

# When the PR is ready for review
gh pr ready

# After approval and passing checks, squash-merge and delete the branch
gh pr merge --squash --delete-branch
```

## 6. Pull Request Template

GitHub fills in a PR description automatically only from a file named `pull_request_template.md` in the repository root, in `docs/`, or in `.github/`. The file in this folder is named `pull-request-template.md` and sits in `docs/git-workflow/`, so GitHub does not use it.

Until Phase 2 adds a copy at `.github/pull_request_template.md`, paste the template from [section 3 of the PR template](pull-request-template.md#3-pull-request-description) into each PR description.

## 7. Manual Release

Follow the [Release Process](release-process.md). After the release commit is on `main`:

```powershell
git switch main
git pull --ff-only
git tag -a v1.5.0 -m "v1.5.0"
git push origin v1.5.0

# Optional: create a GitHub release from the pushed tag
gh release create v1.5.0 --verify-tag --title "v1.5.0" --generate-notes
```

`--verify-tag` stops the command if the tag has not been pushed. `--generate-notes` builds the release notes from merged PR titles.

## 8. Phase 2 Tools (Not Yet Configured)

These tools are set up in Phase 2, after separate approval. Until then, the rules in these docs depend on review.

- **commitlint** with `@commitlint/config-conventional`. Set `header-max-length` to 72, because the default is 100. It runs on each commit through a git hook (husky or lefthook, to be decided) and on PR commits in CI.
- **PR title check** using `amannn/action-semantic-pull-request`. It runs in CI on each PR title and becomes a required status check.
- **gitleaks** for secret scanning:
  - `gitleaks git` scans the full Git history. Run it now, because the repository is public and its history is already visible.
  - `gitleaks dir` scans the working tree.
  - `gitleaks git --pre-commit --staged` scans staged changes in a pre-commit hook.

After these checks are in place, add them as required status checks in the branch ruleset.

## 9. Troubleshooting

| Problem | Fix |
|---|---|
| Commit template does not open | Run `git config --global --get commit.template`. If it is empty, rerun the command in section 3. Check that `~/.gitmessage.txt` exists. |
| Template is skipped | `git commit -m` skips the editor. Run `git commit` without `-m`. |
| Commit header is too long | Shorten the header to 72 characters or fewer, and move details into the body. |
| Wrong commit message, not yet pushed | `git commit --amend` |
| Wrong commit message, already pushed to your branch | `git commit --amend`, then `git push --force-with-lease`. Never force-push `main`. |
| Wrong PR title | `gh pr edit <number> --title "fix(api): ..."`. The title becomes the commit message on `main`. |
| Personal email appears in new commits | Check `git config user.email`, then set the noreply address from section 2. |

## 10. References

- [Conventional Commits](https://www.conventionalcommits.org/en/v1.0.0/)
- [commitlint](https://commitlint.js.org/guides/getting-started.html)
- [husky](https://typicode.github.io/husky/)
- [lefthook](https://github.com/evilmartians/lefthook)
- [action-semantic-pull-request](https://github.com/amannn/action-semantic-pull-request)
- [gitleaks](https://github.com/gitleaks/gitleaks)
- [GitHub rulesets](https://docs.github.com/en/repositories/configuring-branches-and-merges-in-your-repository/managing-rulesets)

