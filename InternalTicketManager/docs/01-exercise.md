# Exercício: Comentários nos tickets

## Situação

A aplicação permite às equipas internas criar e acompanhar tickets, mas não existe forma de deixar comentários num ticket.

Os comentários são necessários para registar contexto durante o acompanhamento.

## Tarefa

Implementar comentários nos tickets.

Antes de escrever código, transformar o pedido vago num requisito claro.

## Fluxo obrigatório

1. Ler `docs/TASK-001-ticket-comments.md`.
2. Preencher `docs/REQ-001-ticket-comments.md`.
3. Pedir à IA para rever o requisito.
4. Melhorar o requisito com base na revisão.
5. Pedir à IA um plano técnico.
6. Implementar o plano em passos pequenos.
7. Correr build e testes.
8. Testar manualmente no browser.
9. Rever o diff antes de aceitar o trabalho.

## Comportamento da aplicação base

A aplicação base já permite:

- Listar tickets
- Criar tickets
- Ver detalhe de ticket
- Editar estado de ticket

A aplicação base não suporta comentários de forma intencional.

## Limites

Implementar apenas comentários nos tickets.

Não adicionar:

- Utilizadores
- Login
- Permissões
- Categorias
- Anexos
- Notificações
- Dashboard

## Critério de conclusão

O exercício está concluído quando:

- o requisito está claro e testável;
- os comentários podem ser vistos na página de detalhe do ticket;
- é possível adicionar um comentário a um ticket existente;
- conteúdo de comentário vazio é rejeitado;
- build e testes passam;
- o diff contém apenas alterações necessárias para comentários nos tickets.
