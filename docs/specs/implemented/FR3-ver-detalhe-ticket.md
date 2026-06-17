# FR3 - Ver detalhe de ticket

| Parte da spec | O que contém |
| --- | --- |
| Nome da funcionalidade | Ver detalhe de ticket |
| Estado | Implementada |
| Objetivo | Permitir consultar a informação completa de um ticket específico. |
| Rotas envolvidas | `/Tickets/Details/{id}` |
| Comportamento esperado | O utilizador abre a página de detalhe e vê os dados do ticket selecionado. |
| Dados envolvidos | `Ticket.Id`, `Ticket.Title`, `Ticket.Description`, `Ticket.Priority`, `Ticket.Status`, `Ticket.CreatedAt`. |
| Regras de negócio | O detalhe só pode ser apresentado para tickets existentes.  |
| Regras técnicas | A aplicação deve procurar o ticket pelo `id` recebido na rota. Se não encontrar o ticket, deve devolver `404 Not Found`. |
| Critérios de aceitação | Dado um ticket existente, quando o utilizador abre `/Tickets/Details/{id}`, então vê o título e a informação do ticket.<br><br>Dado um ticket existente, quando a página de detalhe é apresentada aparece a informação correspondente .<br><br>Dado um ticket inexistente, quando o utilizador abre `/Tickets/Details/{id}`, então a aplicação devolve `404 Not Found`. |
| Casos de erro | Id inexistente: a aplicação devolve `404 Not Found`.<br><br>Rota de detalhe sem id: a aplicação devolve `404 Not Found`. |
