# TASK-001: Comentários nos tickets

## Contexto

A aplicação permite às equipas internas criar e acompanhar tickets. Neste momento, um ticket pode ser criado, consultado e movido entre estados, mas não existe forma de registar contexto durante o acompanhamento.

## Pedido vago

Adicionar comentários aos tickets para que as pessoas consigam registar contexto enquanto um ticket está a ser acompanhado.

## Trabalho esperado dos participantes

Antes da implementação, definir o requisito em `REQ-001-ticket-comments.md`.

O requisito deve incluir:

- Nome da funcionalidade
- Objetivo
- Comportamento esperado
- Dados envolvidos
- Regras de negócio
- Critérios de aceitação
- Casos de erro

Depois de o requisito estar claro, implementar a funcionalidade e validá-la com testes ou com um plano de teste manual documentado.

## Limites de implementação

Implementar apenas comentários nos tickets.

Não adicionar:

- Utilizadores
- Autenticação
- Autorização
- Anexos
- Categorias
- Notificações
- Dashboard

## Modelo de dados sugerido

```text
TicketComment
- Id
- TicketId
- AuthorName
- Content
- CreatedAt
```

## Âmbito de UI

Na página de detalhe do ticket:

- Mostrar comentários existentes
- Adicionar novo comentário
- Validar comentário vazio
