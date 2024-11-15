# Conventional Commits Specification

The Conventional Commits specification is a lightweight convention for writing commit messages. It provides an easy-to-read format for generating changelogs and version numbers.

Please follow the Conventional Commits specification when writing commit messages for this project.

For more information, see the [Conventional Commits website](https://www.conventionalcommits.org/en/v1.0.0/).

## Format

A conventional commit message consists of a header, an optional body, and an optional footer. The header includes a type, an optional scope, and a description:

```bash
<type>[optional scope]: <description>
```

## Examples

### Feature

```bash
feat: add new feature
```

### Bug Fix

```bash
fix: resolve issue with login
```

### Documentation

```bash
docs: update README
```

### Scope

```bash
feat(login): add new login feature
```
