# FR1 - Listar tickets

| Parte da spec | O que contém |
| --- | --- |
| Nome da funcionalidade | Listar tickets |
| Estado | Implementada |
| Objetivo | Permitir consultar os pedidos internos atualmente registados na aplicação. |
| Rotas envolvidas | `/` e `/Tickets` |
| Comportamento esperado | A aplicação apresenta uma lista de tickets na página principal e na página de tickets. Cada ticket deve mostrar informação suficiente para o utilizador identificar o pedido e aceder ao detalhe. |
| Dados envolvidos | `Ticket.Id`, `Ticket.Title`, `Ticket.Description`, `Ticket.Priority`, `Ticket.Status`, `Ticket.CreatedAt`. |
| Regras de negócio | Os tickets devem ser apresentados do mais recente para o mais antigo, com base no campo `CreatedAt`. A rota raiz `/` deve apresentar a lista de tickets. A aplicação deve criar tickets de exemplo quando a base de dados está vazia. |
| Regras técnicas | A listagem deve obter os tickets a partir da base de dados. A ordenação deve ser feita de forma consistente pela query à base de dados. |
| Critérios de aceitação | Dado que existem tickets registados, quando o utilizador abre `/`, então vê a lista de tickets.<br><br>Dado que existem tickets registados, quando o utilizador abre `/Tickets`, então vê a lista de tickets.<br><br>Dado que existem tickets com datas diferentes, quando a lista é apresentada, então o ticket mais recente aparece antes dos mais antigos.<br><br>Dado que a base de dados está vazia no arranque, quando a aplicação inicializa, então são criados 5 tickets de exemplo. |
| Casos de erro | Se não existirem tickets e o seed não correr, a página deve continuar a carregar sem falhar e apresentar uma lista vazia. |
