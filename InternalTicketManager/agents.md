# Instruções para agentes

## Objetivo do projeto

Este repositório é um projeto de formação. A aplicação base deve manter-se intencionalmente pequena para os participantes praticarem definição de requisitos, implementação, testes e documentação.

## Âmbito da aplicação base

A aplicação base inclui:

- Lista de tickets
- Criação de tickets
- Detalhe de ticket
- Edição de estado de ticket
- Tickets de exemplo

A aplicação base não deve incluir comentários nos tickets. Os comentários são o exercício da formação.

## Fluxo do exercício

Quando for pedido para implementar comentários nos tickets:

1. Ler `docs/01-exercise.md`.
2. Começar por preencher `docs/REQ-001-ticket-comments.md`.
3. Manter o requisito conciso e testável.
4. Pedir ou produzir um plano técnico antes de escrever código.
5. Implementar apenas a funcionalidade acordada de comentários nos tickets.
6. Trabalhar em passos pequenos e fáceis de rever.
7. Adicionar ou atualizar testes quando fizer sentido.
8. Correr `dotnet build` e `dotnet test`.
9. Rever o diff antes de aceitar alterações.
10. Atualizar o `README.md` se o setup ou o comportamento mudarem.

## Restrições

Não adicionar utilizadores, autenticação, autorização, categorias, anexos, notificações ou dashboard, exceto se o formador alterar explicitamente o âmbito.

## Documentos da formação

- `docs/00-training-flow.md`: fluxo completo da sessão.
- `docs/01-exercise.md`: exercício para participantes.
- `docs/TASK-001-ticket-comments.md`: pedido de negócio vago.
- `docs/REQ-001-ticket-comments.md`: template de requisito para participantes.
- `docs/02-expected-requirement.md`: referência do formador.
- `docs/03-prompts.md`: prompts para o exercício.
