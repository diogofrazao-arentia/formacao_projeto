# Internal Ticket Manager

Aplicação ASP.NET Core MVC mínima para uma formação de desenvolvimento assistido por IA.

## Objetivo

A aplicação base gere tickets internos e deixa os comentários nos tickets por implementar de forma intencional. Os participantes devem definir primeiro a funcionalidade em falta antes de a desenvolver.

## Âmbito da base

Implementado:

- Listar tickets
- Criar ticket
- Ver detalhe de ticket
- Editar estado de ticket
- Criar 5 tickets de exemplo

Não implementado:

- Comentários nos tickets
- Utilizadores
- Autenticação
- Autorização
- Categorias
- Anexos
- Notificações
- Dashboard

## Stack técnico

- .NET 8
- ASP.NET Core MVC
- Entity Framework Core
- SQL Server LocalDB por defeito

## Requisitos

- .NET 8 SDK
- SQL Server LocalDB ou outra instância de SQL Server

## Executar

A partir da raiz do repositório:

```powershell
cd InternalTicketManager
dotnet restore
dotnet run --project src/TicketManager.Web
```

Abrir o URL indicado pelo `dotnet run`.

A aplicação cria a base de dados automaticamente com `EnsureCreated()` e cria tickets de exemplo se a base de dados estiver vazia.

## Testes

Os testes de integração correm contra SQL Server. Indicar a connection string de teste através de `ConnectionStrings__TestConnection`.

Exemplo com SQL Server disponível em `localhost`:

```powershell
$env:ConnectionStrings__TestConnection="Server=localhost,1433;Database=master;User Id=sa;Password=Your_password123;Encrypt=False;TrustServerCertificate=True"
dotnet test
```

A suite de testes cria uma base de dados única por execução e elimina-a no fim.

## Configurar SQL Server

A connection string por defeito está em:

```text
src/TicketManager.Web/appsettings.json
```

Valor por defeito:

```text
Server=(localdb)\mssqllocaldb;Database=InternalTicketManager;Trusted_Connection=True;MultipleActiveResultSets=true
```

Alterar este valor se os participantes usarem outra instância de SQL Server.

## Exercício da formação

Fluxo da formação:

```text
docs/00-training-flow.md
```

Enunciado para participantes:

```text
docs/01-exercise.md
```

Começar por:

```text
docs/TASK-001-ticket-comments.md
```

Os participantes devem preencher primeiro:

```text
docs/REQ-001-ticket-comments.md
```

Só depois devem implementar:

```text
TicketComment
- Id
- TicketId
- AuthorName
- Content
- CreatedAt
```

Alterações esperadas na página de detalhe:

- Ver comentários
- Adicionar comentário
- Validar comentário vazio

Referência do formador:

```text
docs/02-expected-requirement.md
```

Prompts da formação:

```text
docs/03-prompts.md
```
