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

Se não existir SQL Server local, pode ser usada a instância de teste definida em `docker-compose.yml`:

```powershell
docker compose up -d sqlserver
$env:ConnectionStrings__TestConnection="Server=localhost,1433;Database=master;User Id=sa;Password=Your_password123;Encrypt=False;TrustServerCertificate=True"
dotnet test
```

A suite de testes cria uma base de dados única por execução e elimina-a no fim.

## Pipeline de CI

A pipeline de CI está definida em:

```text
../.github/workflows/ci.yml
```

A pipeline corre automaticamente quando há:

- `push`
- `pull_request`

Também pode ser executada manualmente no GitHub:

1. Abrir o repositório no GitHub.
2. Ir a `Actions`.
3. Escolher a workflow `CI`.
4. Clicar em `Run workflow`.

A pipeline executa os seguintes passos:

- restaura os pacotes NuGet;
- corre o linter de formatação com `dotnet format`;
- faz type checking e análise estática através de `dotnet build`;
- falha a build se existirem warnings do compilador ou dos analisadores;
- corre os testes automatizados contra SQL Server;
- confirma que a aplicação consegue ser publicada;
- guarda os resultados dos testes como artefacto.

As regras partilhadas da build estão em:

```text
Directory.Build.props
```

Este ficheiro ativa análise de código, regras de estilo em build e `TreatWarningsAsErrors`, para a CI bloquear código com warnings.

Para reproduzir localmente a parte principal da CI:

```powershell
cd InternalTicketManager
dotnet restore InternalTicketManager.sln
dotnet format InternalTicketManager.sln --verify-no-changes --no-restore
dotnet build InternalTicketManager.sln --configuration Release --no-restore
dotnet test InternalTicketManager.sln --configuration Release --no-build
dotnet publish src/TicketManager.Web/TicketManager.Web.csproj --configuration Release --no-build
```

Nota: para correr os testes localmente, é necessário ter SQL Server disponível e configurar `ConnectionStrings__TestConnection`, como descrito na secção de testes.

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
docs/exercises/tarefa-por-fazer.md
```

Os participantes devem preencher primeiro:

```text
docs/specs/planned/FR6-comentarios-nos-tickets.md
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

Checklist do formador:

```text
docs/trainer/checklist.md
```
