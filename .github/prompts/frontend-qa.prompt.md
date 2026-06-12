---
description: Verify frontend changes visually and interactively in a local browser after UI work.
---

# Frontend QA

Run frontend QA for visible UI changes.

Follow this workflow:

1. Read project agent rules and identify the app's dev, build, lint, and test commands.
2. Start or reuse the local dev server. If the port is occupied, use the project's normal fallback or a nearby available port.
3. Open the relevant route in the browser.
4. Verify the changed workflow manually, not just the page load.
5. Check at least one desktop viewport and one mobile/narrow viewport.
6. Inspect loading, empty, error, disabled, validation, long text, and permission states when relevant to the change.
7. Fix obvious UI defects before finishing.
8. Run relevant automated checks when available.

Visual quality checks:

- Text must not overflow buttons, cards, tables, nav, dialogs, or form controls.
- Elements must not overlap or jump unexpectedly across common viewport sizes.
- Interactive controls must have clear hover, focus, disabled, selected, loading, and error states when applicable.
- Layout must remain usable with long labels, empty data, and narrow screens.
- Forms must show validation feedback close to the relevant field.
- Modals, menus, popovers, and sticky elements must stay within the viewport.
- Contrast and spacing must be readable and consistent with the existing design system.

Do not claim frontend QA is complete unless a local browser check was performed or there is a clear reason it was impossible.
