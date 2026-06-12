# FR4 - Editar estado de ticket

| Parte da spec | O que contém |
| --- | --- |
| Nome da funcionalidade | Editar estado de ticket |
| Estado | Implementada |
| Objetivo | Permitir atualizar o estado de acompanhamento de um ticket sem alterar os restantes dados do pedido. |
| Rotas envolvidas | `/Tickets/Edit/{id}` e `/Tickets/Details/{id}` |
| Comportamento esperado | O utilizador abre o formulário de edição de estado, seleciona um novo estado e submete. A aplicação guarda o novo estado e redireciona para o detalhe do ticket. |
| Dados envolvidos | `Ticket.Id`, `Ticket.Status`. Os restantes campos do ticket existem, mas não devem ser alterados por esta funcionalidade. |
| Regras de negócio | O estado deve ser um valor válido: `Open`, `InProgress` ou `Closed`. A edição de estado não pode alterar `Title`, `Description`, `Priority` nem `CreatedAt`. |
| Regras técnicas | O formulário deve usar token antiforgery. A aplicação deve procurar o ticket pelo `id` recebido na rota antes de atualizar o estado. Apenas o campo `Status` deve ser alterado. |
| Critérios de aceitação | Dado um ticket existente, quando o utilizador abre `/Tickets/Edit/{id}`, então vê o formulário de edição de estado.<br><br>Dado um ticket existente e um estado válido, quando o formulário é submetido, então o estado é atualizado e a aplicação redireciona para `/Tickets/Details/{id}`.<br><br>Dado que o estado é atualizado, então `Title`, `Description`, `Priority` e `CreatedAt` permanecem iguais. |
| Casos de erro | Id inexistente no formulário de edição: a aplicação devolve `404 Not Found`.<br><br>Estado inválido: o ticket não é atualizado e o formulário volta a ser apresentado.<br><br>Pedido sem token antiforgery válido: a aplicação devolve `400 Bad Request` e não atualiza o ticket. |
