---
name: feature-from-spec
description: Implement features from an existing spec, requirements document, ticket, or acceptance criteria while respecting project agent rules, repository conventions, tests, and frontend validation. Use when the user asks to implement, build, code, complete, or ship a feature from a spec or documented behavior.
---

# Feature From Spec

## Workflow

1. Locate and read the spec, ticket, or acceptance criteria. If the user pasted the spec, treat that as the source of truth.
2. Read project agent rules that apply to the touched area. Look for `AGENTS.md`, `agents.md`, `.agents/`, and nested rule files.
3. Search for existing implementations of similar behavior before adding new patterns.
4. Identify the smallest coherent implementation path: data/model changes, API/service changes, UI changes, tests, and docs.
5. If a decision is blocking or the spec conflicts with project rules, ask the user. Otherwise proceed using explicit assumptions.
6. Implement incrementally and keep edits scoped to the feature.
7. Add or update tests based on the behavior and risk.
8. Run the most relevant checks first, then broader checks when practical.
9. If UI changes are included, use frontend validation: run the app, inspect the browser, and verify relevant responsive states.
10. Finish with a concise summary of changed behavior, files touched, checks run, and any remaining risk.

## Implementation Rules

- Treat project agent rules as policy and the spec as feature intent.
- Prefer existing local helpers, components, API clients, validation patterns, test utilities, and folder conventions.
- Do not turn a narrow feature into a broad refactor.
- Preserve unrelated user changes in the worktree.
- When the spec is ambiguous but not blocked, choose the least surprising behavior and record the assumption in the final response.
- When the spec is too vague to implement safely, create or request a `spec-from-context` pass first.

## Testing Rules

- Test behavior, not implementation details.
- Cover acceptance criteria and at least one meaningful failure or edge case when applicable.
- For regressions, write the test so it would fail before the fix when feasible.
- If tests cannot be run, explain why and give the exact command that was attempted or should be run.

## Frontend Rules

- Use the existing design system and component vocabulary.
- Verify common states: loading, empty, error, disabled, long text, narrow viewport, and normal desktop viewport.
- Avoid shipping UI changes based only on static code inspection when a local browser check is feasible.
