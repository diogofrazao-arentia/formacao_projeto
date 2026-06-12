---
name: frontend-qa
description: Verify frontend changes visually and interactively in a local browser after UI work. Use when the user asks for frontend QA, visual QA, responsive checks, browser verification, layout validation, or when a feature implementation changes visible UI, interactions, styling, forms, dashboards, or pages.
---

# Frontend QA

## Workflow

1. Read project agent rules and identify the app's dev, build, lint, and test commands.
2. Start or reuse the local dev server. If the port is occupied, use the project's normal fallback or a nearby available port.
3. Open the relevant route in the in-app browser when available.
4. Verify the changed workflow manually, not just the page load.
5. Check at least one desktop viewport and one mobile/narrow viewport.
6. Inspect loading, empty, error, disabled, validation, long text, and permission states when relevant to the change.
7. Fix obvious UI defects before finishing.
8. Run relevant automated checks when available.
9. Report route(s), viewport(s), commands, and any issue fixed or left open.

## Visual Quality Checks

- Text must not overflow buttons, cards, tables, nav, dialogs, or form controls.
- Elements must not overlap or jump unexpectedly across common viewport sizes.
- Interactive controls must have clear hover, focus, disabled, selected, loading, and error states when applicable.
- Layout must remain usable with long labels, empty data, and narrow screens.
- Forms must show validation feedback close to the relevant field.
- Modals, menus, popovers, and sticky elements must stay within the viewport.
- Contrast and spacing must be readable and consistent with the existing design system.

## Browser Checks

- Use browser screenshots or visual inspection when the target is local and available.
- Check console errors when behavior seems broken.
- For canvas, charts, or 3D scenes, verify the rendered area is nonblank and correctly framed.
- For data-heavy screens, verify sorting, filtering, pagination, row density, and empty states when relevant.

## Finish Criteria

Do not claim frontend QA is complete unless a local browser check was performed or there is a clear reason it was impossible. If impossible, state the blocker and the best substitute checks that were run.
