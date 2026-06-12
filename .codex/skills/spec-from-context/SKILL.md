---
name: spec-from-context
description: Create implementation-ready feature specifications from a user request, project agent rules, and repository documentation. Use when the user asks to create, draft, refine, or plan a spec, requirements document, acceptance criteria, product behavior, feature brief, or implementation plan before coding.
---

# Spec From Context

## Workflow

1. Read the user request and restate the target outcome in concrete product terms.
2. Read project agent rules that apply to the target area. Look for `AGENTS.md`, `agents.md`, `.agents/`, and nested rule files.
3. Search repository docs before inventing assumptions. Prefer `docs/`, `specs/`, `requirements/`, `planning/`, `architecture/`, `README*`, and domain files referenced from the agent rules.
4. Inspect the existing code only enough to understand current behavior, naming, data models, routes, components, APIs, and test patterns relevant to the spec.
5. Separate facts from inferences. Mark unclear points as assumptions unless they block the spec.
6. Ask the user only for decisions that materially change the product behavior or technical approach. Otherwise proceed with explicit assumptions.
7. Produce a spec that is ready for `feature-from-spec`.

## Spec Format

Use this structure unless the repo already has a stronger local template:

```markdown
# <Feature Name>

## Goal
<What user or business outcome this feature must deliver.>

## Context Used
- <Relevant agent rule, doc, code area, or existing behavior.>

## In Scope
- <Behavior included in this implementation.>

## Out Of Scope
- <Related work intentionally excluded.>

## User Experience / Behavior
- <Concrete behavior, states, interactions, validations, permissions, and errors.>

## Data And Integration
- <Models, API contracts, storage, migrations, external systems, or none.>

## Acceptance Criteria
- <Observable pass/fail criteria.>

## Edge Cases
- <Empty, loading, error, invalid input, authorization, concurrency, responsive states.>

## Implementation Notes
- <Likely files, components, services, tests, risks, and sequencing.>

## Test Plan
- <Unit/integration/e2e/manual checks expected.>

## Assumptions
- <Assumptions made because docs or request were incomplete.>
```

## Quality Bar

- Make the spec specific enough that another Codex run can implement it without redoing product discovery.
- Do not duplicate long project rules from docs; reference the source path and summarize only what matters.
- Keep implementation notes pragmatic, not overly prescriptive, unless the repo already requires a specific pattern.
- Include frontend QA expectations when UI is affected.
