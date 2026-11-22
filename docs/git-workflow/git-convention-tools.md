# Git Convention Tools

This guide shows which tools help the team follow the [Git Commit Convention](git-commit-convention.md), the [Branching Strategy](branching-strategy.md), and the [Pull Request Template](pull-request-template.md). 

## 1. Tool Status

| Tool | Purpose | Status |
|---|---|---|
| Git commit template (`.gitmessage`) | Shows the commit format when you run `git commit` | In the repository; each clone points Git at it once |
| Git identity with noreply email | Keeps personal email addresses out of the public history | Set up per developer |
| GitHub CLI (`gh`) | Creates and merges PRs, and creates releases, from the terminal | Available now; needs `gh auth login` once |
| GitHub branch ruleset and PR settings | Blocks direct pushes to `main`, and enforces PRs and squash merge | Recommended, set by a repository admin |
| commitlint (commit-msg hook) | Rejects commit messages that break the convention | In the repository; run `npm install` once per clone |
| PR title check | Rejects PR titles that break the convention | In the repository; blocks merges after an admin makes it required |
| gitleaks | Detects secrets in commits and files | Documented only; run manually |

Not used:
- **gitmoji**: emoji in a commit header breaks Conventional Commits parsing.
- **conventional-changelog**: releases are manual, and the git tag is the only version record.
- **lefthook** and the Python `pre-commit` tool: the team uses one hook runner, husky. `pre-commit` also refuses to run when `core.hooksPath` is set.
- **semantic-release** and **release-please**: releases are manual; see the [Release Process](release-process.md).

## 2. Set Up Your Git Identity

Use your GitHub noreply address, so your personal email is not published in the public repository history.

1. Open GitHub → Settings → Emails, and copy your noreply address (`<id>+<username>@users.noreply.github.com`).
2. Turn on the option to block command line pushes that expose your email.
3. Run:

```powershell
git config --global user.name "<Your Name>"
git config --global user.email "<id>+<username>@users.noreply.github.com"
```

Check which identity a repository uses with `git config --show-origin user.email`. A value in the repository's `.git/config` overrides the `--global` value.

Past commits keep their original author email. Do not rewrite public history only to change it.

## 3. Commit Message Template

The repository includes a `.gitmessage` file at its root. Git opens it in the editor when you run `git commit` without `-m`. Lines that start with `#` are removed when you save.

Point Git at the file once per clone. Use local config, not `--global`, because the path belongs to this repository. Use an absolute path:

```powershell
git -C C:\path\to\infrawatch config commit.template "C:/path/to/infrawatch/.gitmessage"
git -C C:\path\to\infrawatch config --get commit.template
```

The file contains this content:

```text
# <type>[(scope)][!]: <subject>
#
# type: build, chore, ci, docs, feat, fix, perf, refactor, revert, style, test
# scope (optional): a short lowercase word for the area changed. Free text, not checked.
# !: add only for a breaking change
# Header: 100 characters or fewer, lowercase type, imperative subject, no trailing period
#
# Body (optional): why the change was made. Wrap lines at 100 characters.
#
# Footers (optional): BREAKING CHANGE: <what breaks and how to migrate>, Refs #<n>
```

`git commit -m "..."` skips the editor, so the template does not apply to it. The commit-msg hook still checks `-m` messages.

## 4. Repository Settings (Recommended)

A repository admin applies these settings on GitHub. Repository files cannot set them. The `main` ruleset already exists, so compare it with this list before changing anything.

**Pull request settings** (Settings → General → Pull Requests):
- Allow squash merging only. Turn off merge commits and rebase merging.
- Set the default squash commit message to the pull request title.
- Turn on automatically deleting head branches.

**Branch ruleset for `main`** (Settings → Rules → Rulesets):
- Target the `main` branch.
- Require a pull request before merging, with 1 required approval, as [section 4 of the PR template](pull-request-template.md#4-review-and-merge-rules) states. Dismiss stale approvals when new commits are pushed. An author cannot approve their own PR.
- Require conversation resolution before merging.
- Require linear history.
- Block force pushes and deletions.
- Leave the bypass list empty, except for the solo maintainer while the team has one reviewer (see the solo maintainer note below).
- The existing rule requires signed commits. Keep it only if every contributor can sign commits.
- The existing code scanning rule (CodeQL) can block merges when its analysis does not report on a PR. Confirm it runs on pull requests before you rely on it.
- Add the PR title check as a required status check, after it has run once.

**Solo maintainer (current setup):** the only reviewer is the maintainer. The maintainer self-reviews the PR and runs an AI reviewer agent before merging. Neither counts as a GitHub approval, because the rule needs an approval from another account. So the maintainer merges with an admin bypass, and writes the reason in the PR (for example, "solo maintainer; self-review and AI review done"). Keep the bypass limited to the maintainer. When a second reviewer joins, remove the bypass and require their approval. Do not set approvals to 0 without team agreement.

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

# Push and open a draft PR with a title that follows the convention
git push -u origin HEAD
gh pr create --draft --title "feat: add disk usage endpoint"

# When the PR is ready for review
gh pr ready

# After approval and passing checks, squash-merge and delete the branch
gh pr merge --squash --delete-branch
```

`gh pr create` asks for the description. Paste the template from [section 3 of the PR template](pull-request-template.md#3-pull-request-description). With `.github/pull_request_template.md` in the repository, GitHub fills in the template for you.

## 6. Pull Request Template

GitHub fills in a PR description automatically only from a file named `pull_request_template.md` in the repository root, in `docs/`, or in `.github/`. The file in this folder is named `pull-request-template.md` and sits in `docs/git-workflow/`, so GitHub does not use it.

The copy at `.github/pull_request_template.md` is the one GitHub loads. Keep it in sync with [section 3 of the PR template](pull-request-template.md#3-pull-request-description).

## 7. Manual Release

Follow the [Release Process](release-process.md). Each component has its own tag, named `<component>/vMAJOR.MINOR.PATCH`. The git tag is the version record. Each component also has a `CHANGELOG.md` that the release PR updates. The steps below are the short form of [§5 Create Release Tag](release-process.md#5-create-release-tag) and [§10 Release Walkthrough](release-process.md#10-release-walkthrough).

```powershell
# Release from the exact commit on main that you built and tested
git switch main
git pull --ff-only

# Create an annotated tag for each released component (the first release is <component>/v0.1.0)
git tag -a backend/v0.3.0 -m "backend v0.3.0" <release-commit>
git push origin backend/v0.3.0

# Create the GitHub release from the pushed tag, with that component's notes only
# (needs gh auth login once; extract the section first, see release-process.md §5)
gh release create backend/v0.3.0 --verify-tag --title "backend v0.3.0" --notes-file notes-backend.md
```

Push tags one at a time, as shown. Do not use `git push --tags`, and never reuse a tag. `--verify-tag` stops the command if the tag has not reached GitHub. Use `--notes-file` with the newest section of the component's `CHANGELOG.md`, extracted as shown in [release-process.md §5](release-process.md#5-create-release-tag). Do not use `--generate-notes`, because it cannot filter by path.

## 8. Commit and PR Tools

These tools are in the repository. Local hooks can be skipped with `--no-verify`, so the PR title check is the server-side backstop.

- **commitlint** with `@commitlint/config-conventional`, configured in the root `commitlint.config.js`. It sets `header-max-length` to 100 and does not restrict scopes. Run `npm install` once per clone so the tool exists locally. commitlint ignores `Merge ...` and `Revert ...` messages by default, so the local hook does not check them.
- **husky** runs commitlint in a `commit-msg` hook. `--no-verify` skips local hooks, so the CI checks are the backstop.
- **PR title check** in `.github/workflows/pr-title.yml`, using `amannn/action-semantic-pull-request`. It checks each PR title. Add it as a required status check after it has run once.
- **gitleaks** for secret scanning. The repository documents it, but no hook runs it. Install with `winget install Gitleaks.Gitleaks`, and run `gitleaks git --help` to confirm the flags first:
  - `gitleaks git --log-opts="--all"` scans the full Git history. Run it now, because the repository is public and its history is already visible.
  - `gitleaks dir <path>` scans files in the working tree.
  - `gitleaks git --pre-commit --staged` scans staged changes, if you want to run it manually before committing.

  Write any report outside the repository. If it finds a secret, rotate the secret. Do not paste the value into a PR, issue, or chat.

## 9. Troubleshooting

| Problem | Fix |
|---|---|
| Template does not open | Run `git config --get commit.template` in the repository. If it is empty, run the command in section 3. Check that `.gitmessage` exists at the path it prints. |
| Template is skipped | `git commit -m` skips the editor. Run `git commit` without `-m`. |
| Header is too long | Shorten the header to 100 characters or fewer, and move details into the body. |
| Hook fails with `commitlint: not found` | Run `npm install` in the repository root. |
| Hook does not run | Run `npm install` again, then check `git config --get core.hooksPath`. It should print `.husky/_`. |
| `git revert` commits without the hook check | Git's revert skips the commit-msg hook and uses `Revert "..."`. Run `git revert --no-commit <sha>`, then `git commit` and keep git's default `Revert "..."` header. |
| Wrong commit message, not yet pushed | `git commit --amend` |
| Wrong commit message, already pushed to your branch | `git commit --amend`, then `git push --force-with-lease`. Never force-push `main`. |
| Wrong PR title | `gh pr edit <number> --title "fix: ..."`. Squash merge uses the title as the commit message on `main`. |
| PR title is `Revert "..."` after GitHub's Revert button | Rename it to `revert: feat: ...` with `gh pr edit`, before merging. The title check may reject the default title. |
| `gh` says you are not logged in | Run `gh auth login` once. |
| Personal email appears in new commits | Run `git config --show-origin user.email` to find where the address comes from, then set the noreply address from section 2. |

## 10. References

- [Conventional Commits](https://www.conventionalcommits.org/en/v1.0.0/)
- [commitlint getting started](https://commitlint.js.org/guides/getting-started.html)
- [husky](https://typicode.github.io/husky/)
- [action-semantic-pull-request](https://github.com/amannn/action-semantic-pull-request)
- [gitleaks](https://github.com/gitleaks/gitleaks)
- [GitHub Flow](https://docs.github.com/en/get-started/using-github/github-flow)
- [GitHub rulesets](https://docs.github.com/en/repositories/configuring-branches-and-merges-in-your-repository/managing-rulesets/about-rulesets)

