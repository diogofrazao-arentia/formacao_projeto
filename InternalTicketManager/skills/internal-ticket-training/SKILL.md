---
name: internal-ticket-training
description: Usar ao trabalhar no projeto de formação Internal Ticket Manager, especialmente para definir ou implementar o exercício de comentários nos tickets sem expandir o âmbito da aplicação.
---

# Formação Internal Ticket

## Objetivo

Manter o projeto pequeno e focado no resultado da formação: os participantes praticam transformar um pedido vago num requisito e depois implementam e validam a funcionalidade.

## Limites da aplicação base

A aplicação base tem apenas tickets:

- Listar tickets
- Criar ticket
- Ver detalhe de ticket
- Editar estado de ticket

Não implementar comentários na aplicação base. Os comentários são o exercício.

## Fluxo do exercício

Para comentários nos tickets:

1. Ler `docs/01-exercise.md`.
2. Preencher primeiro `docs/REQ-001-ticket-comments.md`.
3. Definir comportamento, dados, regras, critérios de aceitação e casos de erro.
4. Rever e melhorar o requisito antes de escrever código.
5. Criar um plano técnico pequeno.
6. Implementar `TicketComment` só depois de o requisito estar claro.
7. Mostrar e adicionar comentários na página de detalhe do ticket.
8. Validar conteúdo de comentário vazio.
9. Correr build e testes.
10. Rever o diff.
11. Evitar funcionalidades não relacionadas.

## Comportamento pedagógico

- Não dar a solução completa imediatamente.
- Levar o participante a definir primeiro o requisito.
- Manter o feedback concreto e ligado aos critérios de aceitação.
- Preferir pequenos passos de implementação em vez de reescritas grandes.
- Usar `docs/02-expected-requirement.md` apenas como referência do formador ou material de debrief final.

## Guardrails de âmbito

Recusar ou adiar utilizadores, autenticação, autorização, categorias, anexos, notificações e dashboard, exceto se o formador pedir explicitamente.
