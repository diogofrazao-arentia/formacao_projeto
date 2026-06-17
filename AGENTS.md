# Instructions for agents

<!--
Este ficheiro define o comportamento esperado por defeito para agentes de
IA/codigo que trabalhem neste repositorio. Deve manter-se orientado ao produto,
estavel e independente de qualquer contexto temporario de entrega.
-->

## Project purpose

<!--
Comecar pelo objetivo do produto ajuda os agentes a perceberem para que serve
o repositorio antes de proporem detalhes de implementacao.
-->

This repository contains a small internal ticket management application.
The application should remain simple, predictable, and easy to maintain.

Treat the current product as a default starter app for managing internal support
or operations tickets. Avoid assumptions tied to a specific company, team, or
one-off scenario.

## Application scope

<!--
Definir explicitamente o limite atual do produto ajuda os agentes a evitarem
adicionar funcionalidades grandes so porque sao comuns em sistemas de tickets.
-->

The core application supports:

- Listing tickets
- Creating tickets
- Viewing ticket details
- Updating ticket status
- Working with seeded example tickets

Keep new functionality aligned with this scope unless the requested change
clearly expands it. Prefer practical ticket-management behavior over generic
platform features.

## Working guidelines

<!--
Estas regras orientam como os agentes devem fazer alteracoes: analisar primeiro,
manter alteracoes pequenas, preservar o estilo existente e validar o
comportamento com testes/builds.
-->

When making changes:

1. Read the relevant source, tests, and documentation before editing.
2. Keep requirements and behavior explicit, concise, and testable.
3. Prefer small, reviewable changes over broad rewrites.
4. Follow the existing project structure, naming, and coding style.
5. Update or add tests when behavior changes.
6. Run `dotnet build` and `dotnet test` when the change affects code.
7. Update `README.md` or other documentation when setup, usage, or behavior
   changes.
8. Follow the commit-message convention documented in
   `docs/commit-messages.md`.

## Product constraints

<!--
As restricoes estao descritas pela negativa porque estas funcionalidades
aumentariam bastante a complexidade da app e devem exigir aprovacao explicita.
-->

Do not add users, authentication, authorization, categories, attachments,
notifications, dashboards, external integrations, or workflow automation unless
the user explicitly requests them.

If a requested feature could grow beyond the current scope, implement the
smallest useful version first and document any important assumptions.

## Documentation

<!--
A documentacao deve descrever o produto como ele existe. Os agentes devem usar
os documentos como contexto, mas o codigo fonte e o pedido do utilizador
continuam a ser a referencia para decisoes de implementacao.
-->

Use project documentation as supporting context, but validate requested behavior
against the current application scope before implementing it.

When adding product-facing documentation, write it as documentation for the
application itself.

Use XML documentation comments for public domain classes, enums, controller
actions, and data access types. Keep comments concise and useful; avoid
comments that only repeat the member name.

When adding public API endpoints, document them with `<summary>`, `<param>`,
`<returns>`, and response metadata where useful.

Generate the code documentation with:

```powershell
.\docs\generate-docs.ps1 -Open
```

Detailed rules are in `docs/documentation-standard.md`.
