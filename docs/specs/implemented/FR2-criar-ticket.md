# FR2 - Criar ticket

| Parte da spec | O que contém |
| --- | --- |
| Nome da funcionalidade | Criar ticket |
| Estado | Implementada |
| Objetivo | Permitir registar um novo pedido interno para acompanhamento. |
| Rotas envolvidas | `/Tickets/Create` e `/Tickets/Details/{id}` |
| Comportamento esperado | O utilizador abre o formulário de criação, preenche título, descrição e prioridade, submete o formulário e é redirecionado para o detalhe do ticket criado. |
| Dados envolvidos | `Ticket.Title`, `Ticket.Description`, `Ticket.Priority`, `Ticket.Status`, `Ticket.CreatedAt`. |
| Regras de negócio | `Title` é obrigatório e tem no máximo 120 caracteres.<br><br>`Description` é obrigatória e tem no máximo 2000 caracteres.<br><br>`Priority` é obrigatória e deve ser um valor válido: `Low`, `Medium` ou `High`.<br><br>Um ticket criado pela UI fica sempre com `Status = Open`.<br><br>`CreatedAt` é preenchido automaticamente pela aplicação. |
| Regras técnicas | `CreatedAt` deve ser guardado em UTC.<br><br>O formulário deve usar token antiforgery.<br><br>O utilizador não deve conseguir definir manualmente `Status` nem `CreatedAt` no formulário de criação. |
| Critérios de aceitação | Dado que o utilizador abre `/Tickets/Create`, quando a página carrega, então vê um formulário para criar ticket.<br><br>Dado um formulário válido, quando o utilizador cria um ticket, então o ticket é guardado na base de dados.<br><br>Dado um formulário válido, quando o ticket é criado, então a aplicação redireciona para `/Tickets/Details/{id}`.<br><br>Dado um título com 120 caracteres, quando o formulário é submetido, então o ticket é aceite.<br><br>Dado uma descrição com 2000 caracteres, quando o formulário é submetido, então o ticket é aceite.<br><br>Dado um ticket criado pela UI, quando é guardado, então fica com `Status = Open`.<br><br>Dado um ticket criado pela UI, quando é guardado, então `CreatedAt` é preenchido automaticamente em UTC. |
| Casos de erro | Se `Title` estiver vazio, o ticket não é criado e deve ser apresentada validação ao utilizador.<br><br>Se `Description` estiver vazia, o ticket não é criado e deve ser apresentada validação ao utilizador.<br><br>Se `Title` tiver mais de 120 caracteres, o ticket é rejeitado e deve ser apresentada validação ao utilizador.<br><br>Se `Description` tiver mais de 2000 caracteres, o ticket é rejeitado e deve ser apresentada validação ao utilizador.<br><br>Se `Priority` tiver um valor inválido, o ticket não é criado.<br><br>Se o pedido não tiver token antiforgery válido, a aplicação deve devolver `400 Bad Request` e não criar o ticket. |
