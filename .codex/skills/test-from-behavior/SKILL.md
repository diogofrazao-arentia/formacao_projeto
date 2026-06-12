---
name: test-from-behavior
description: Design, add, or improve tests from expected behavior, a bug report, a spec, acceptance criteria, or changed code. Use when the user asks to create tests, improve coverage, add regression tests, validate a feature, or decide what unit, integration, or end-to-end tests are needed.
---

# Test From Behavior

## Workflow

1. Read the behavior source: user request, bug report, spec, acceptance criteria, or diff.
2. Read project agent rules and existing test guidance.
3. Inspect nearby tests and test utilities before choosing a pattern.
4. Choose the lowest test level that gives confidence:
   - Unit tests for pure logic, validation, formatting, reducers, and isolated services.
   - Integration tests for components, API handlers, database behavior, routing, and cross-module contracts.
   - End-to-end tests for critical user journeys, permissions, browser behavior, and regressions that unit tests cannot represent.
5. Add focused tests for the expected behavior and important edge cases.
6. Run the relevant test command. Expand to broader checks when the touched area is shared or risky.
7. Report what behavior is now covered and any coverage intentionally left out.

## Test Design

- Name tests by observable behavior.
- Prefer realistic fixtures over over-mocked internals.
- Avoid brittle assertions tied to implementation details, generated markup, timing, or incidental ordering.
- For bug fixes, add a regression test whenever feasible.
- Include negative paths: invalid input, missing data, denied permission, network or service failure, empty result, and boundary values.
- Do not add snapshot tests unless the repo already relies on them and the snapshot is genuinely useful.

## When Existing Tests Are Weak

- Follow the local style enough to fit the suite.
- Improve only the test surface needed for the requested behavior.
- If adding a better helper would reduce meaningful duplication, keep it small and local unless the repo already has shared test utilities.

## Output

End with:

- Tests added or changed.
- Commands run and results.
- Behavior covered.
- Residual risk or untested behavior, if any.
