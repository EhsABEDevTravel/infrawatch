# Git Workflow

## Overview

InfraWatch follows **GitHub Flow**, using short-lived working branches, Pull Requests, and `main` as the primary branch.

This documentation defines the Git conventions and development workflow that all contributors should follow when working on the repository.

## Documentation

| Document | Description |
|---|---|
| [Branching Strategy](branching-strategy.md) | Branch types, naming conventions, and development flow. |
| [Git Commit Convention](git-commit-convention.md) | Commit message format, types, and writing guidelines. |
| [Git Convention Tools](git-convention-tools.md) | Tools and scripts for validating Git conventions locally. |
| [Pull Request Template](pull-request-template.md) | PR title format, description template, checklist, and review rules. |
| [Release Process](release-process.md) | Semantic Versioning, release preparation, Git tags, and deployment. |

## Development Workflow

Follow this workflow when contributing changes:

1. Create a working branch from `main`.
2. Make focused changes and write commits following the repository's commit convention.
3. Push the branch and open a Pull Request.
4. Complete the required review and quality checks.
5. Merge the Pull Request into `main`.
6. Delete the working branch after merging.

## Core Rules

- Keep `main` production-ready.
- Use one working branch for one logical change.
- Follow the branch naming convention.
- Use Conventional Commits for commit messages and Pull Request titles.
- Test changes before requesting a review.
- Do not commit secrets, credentials, or sensitive configuration.
- Follow the release process when preparing a release.

## Getting Started

Before contributing, read the documents in this order:

1. [Branching Strategy](branching-strategy.md)
2. [Git Commit Convention](git-commit-convention.md)
3. [Git Convention Tools](git-convention-tools.md)
4. [Pull Request Template](pull-request-template.md)
5. [Release Process](release-process.md)
