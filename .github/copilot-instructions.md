# Repository Instructions

This repository contains a small internal ticket management application.
Keep the application simple, predictable, and easy to maintain.

Before making changes:

1. Read the relevant project agent rules, especially `AGENTS.md`.
2. Read nearby source, tests, and documentation before editing.
3. Keep behavior explicit, concise, and testable.
4. Prefer small, reviewable changes over broad rewrites.
5. Follow existing project structure, naming, and coding style.
6. Update or add tests when behavior changes.
7. Run `dotnet build` and `dotnet test` when code changes.
8. Follow the repository commit-message convention in `docs/commit-messages.md`.

Do not add users, authentication, authorization, categories, attachments,
notifications, dashboards, external integrations, or workflow automation unless
the user explicitly requests them.

For larger work, prefer this sequence:

1. Create or refine a spec from project context.
2. Implement the feature from the spec.
3. Add tests from the expected behavior.
4. Run frontend QA when visible UI changes.

Use the prompt files in `.github/prompts/` for those repeatable workflows.
