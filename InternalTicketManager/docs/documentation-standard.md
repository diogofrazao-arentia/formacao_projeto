# Documentation Standard

This project uses XML documentation comments in the C# source code as the
source of truth for generated technical documentation.

## What to document

- Public domain classes and their important properties.
- Public enums and each enum value.
- Public controller actions that represent user-facing flows.
- Public data access types such as the application `DbContext`.
- API endpoints, if the optional API layer is added later.

## What not to document

- Private helpers when their behavior is obvious from the calling code.
- Comments that only repeat the member name.
- Implementation details that are likely to change frequently.

## Comment pattern

Use concise XML documentation comments:

```csharp
/// <summary>
/// Displays all tickets ordered from newest to oldest.
/// </summary>
/// <returns>The ticket list page.</returns>
public async Task<IActionResult> Index()
```

Use `<param>` for method arguments and `<returns>` when the return value is part
of the public contract.

Use `<remarks>` only when the extra context is useful for a reader who is
learning the project.

## Generate the HTML documentation

From the `InternalTicketManager` folder, run:

```powershell
.\docs\generate-docs.ps1 -Open
```

The generated documentation is written to:

```text
docs/generated/index.html
```
