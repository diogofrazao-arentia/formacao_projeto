# FR5 - Criar tickets de exemplo

| Parte da spec | O que contém |
| --- | --- |
| Nome da funcionalidade | Criar tickets de exemplo |
| Estado | Implementada |
| Objetivo | Garantir que a aplicação tem dados visíveis logo no primeiro arranque. |
| Rotas envolvidas | Nao aplicavel diretamente. A funcionalidade ocorre no arranque da aplicacao. |
| Comportamento esperado | Quando a aplicacao arranca e a base de dados nao contem tickets, sao criados 5 tickets de exemplo com prioridades, estados e datas diferentes. |
| Dados envolvidos | 5 registos `Ticket` com `Title`, `Description`, `Priority`, `Status` e `CreatedAt`. |
| Regras de negocio | O seed so deve correr quando nao existe qualquer ticket na base de dados.<br><br>Se ja existir pelo menos um ticket, a aplicacao nao deve adicionar novamente os exemplos.<br><br>Os tickets criados devem ter dados suficientes para testar listagem, detalhe e edicao de estado. |
| Regras tecnicas | A criacao dos dados de demonstracao deve ocorrer no arranque da aplicacao ou durante a inicializacao da base de dados. As datas devem permitir validar a ordenacao da listagem. |
| Criterios de aceitacao | Dada uma base de dados vazia, quando a aplicacao inicializa, entao sao criados 5 tickets de exemplo.<br><br>Dada uma base de dados vazia, quando o utilizador abre a listagem apos o arranque, entao ve os tickets de exemplo.<br><br>Dada uma base de dados com pelo menos um ticket, quando a aplicacao inicializa, entao nao duplica os tickets de exemplo.<br><br>Dado que os tickets de exemplo sao criados, quando a listagem e apresentada, entao existem tickets com diferentes prioridades, estados e datas. |
| Casos de erro | Se a base de dados nao estiver acessivel, a aplicacao nao consegue inicializar corretamente os dados. |
