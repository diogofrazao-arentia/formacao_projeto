# AGENTS.md

## Project Snapshot

Internal Ticket Manager is a small ASP.NET Core MVC app on .NET 8 with one solution, one web project, and one test project. The app uses SQLite locally and creates its schema and seed data on startup.

## Scope

Keep changes aligned with the current ticket-management scope. Avoid adding users, authentication, authorization, categories, attachments, notifications, dashboards, external integrations, or workflow automation unless the user explicitly asks for them.

## Working Rules

- Read the relevant source, tests, and docs before editing.
- Prefer the smallest change that matches the current functional spec.
- For feature work, check [docs/specs/README.md](docs/specs/README.md) first, then update the relevant spec in `planned/` or `implemented/`.
- Keep changes small, reviewable, and consistent with the existing project structure and naming.
- Update or add tests when behavior changes.
- Link to existing documentation instead of repeating it. Keep this file as the quick-start guide for agents.
- Use the repo docs for standards and conventions: [README.md](README.md), [docs/documentation-standard.md](docs/documentation-standard.md), [docs/commit-messages.md](docs/commit-messages.md), and [docs/exercises/00-training-flow.md](docs/exercises/00-training-flow.md).

## Commands

- Restore: `dotnet restore InternalTicketManager.sln`
- Format check: `dotnet format InternalTicketManager.sln --verify-no-changes`
- Build: `dotnet build InternalTicketManager.sln`
- Test: `dotnet test InternalTicketManager.sln`
- Run locally: `./scripts/start-app.ps1`
- Run on a different port: `./scripts/start-app.ps1 -Port 5130`
- Generate docs: `./docs/generate-docs.ps1 -Open`
- Install Git hooks: `./scripts/install-git-hooks.ps1`

## Code Conventions

- Public types, controllers, controller actions, and public properties should have XML documentation comments.
- Prefer async MVC actions and constructor injection with private readonly fields.
- Use data annotations for validation, with Portuguese user-facing messages where applicable.
- Keep antiforgery protection on POST actions and account for it in tests.
- EF Core maps ticket enums as strings; the database schema is managed with EF Core migrations.

## Testing Notes

- Integration tests use `WebApplicationFactory` and isolated SQLite databases per run.
- POST tests often need antiforgery token extraction from HTML.
- Treat warnings seriously: the build is configured to fail on warnings.

## Commit Style

Use Conventional Commits. See [docs/commit-messages.md](docs/commit-messages.md) for the accepted types and examples.
