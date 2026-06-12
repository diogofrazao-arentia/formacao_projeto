# Checklist do formador

## Antes da sessão

- Confirmar que o projeto abre a partir de `InternalTicketManager`.
- Confirmar que o .NET 8 SDK está instalado.
- Confirmar que existe SQL Server LocalDB ou Docker disponível para os testes.
- Correr `dotnet restore InternalTicketManager.sln`.
- Correr a aplicação com `dotnet run --project src/TicketManager.Web`.
- Validar que a página de tickets abre e mostra dados de exemplo.

## Durante a sessão

- Dar aos participantes `docs/exercises/tarefa-por-fazer.md`.
- Reforçar o fluxo esperado: requisito -> revisão -> plano técnico -> implementação -> testes -> revisão do diff.
- Pedir que preencham `docs/specs/planned/FR6-comentarios-nos-tickets.md` antes de escrever código.
- Usar a secção de prompts em `docs/exercises/tarefa-por-fazer.md` para revisão do requisito, plano técnico e revisão do diff.

## Checkpoints

- O requisito tem objetivo, comportamento, dados, regras, critérios de aceitação e casos de erro.
- O plano técnico limita a alteração a comentários nos tickets.
- A implementação não adiciona login, utilizadores, permissões, categorias, anexos, notificações ou dashboard.
- A página de detalhe permite ver e adicionar comentários.
- Comentários vazios são rejeitados.
- O comportamento existente de tickets continua a funcionar.

## Validação

```powershell
dotnet build InternalTicketManager.sln --configuration Release
dotnet test InternalTicketManager.sln --configuration Release
```

Se os testes precisarem de SQL Server via Docker:

```powershell
docker compose up -d sqlserver
$env:ConnectionStrings__TestConnection="Server=localhost,1433;Database=master;User Id=sa;Password=Your_password123;Encrypt=False;TrustServerCertificate=True"
dotnet test InternalTicketManager.sln --configuration Release
```

## Problemas comuns

- Se os testes falharem por ligação à base de dados, confirmar `ConnectionStrings__TestConnection`.
- Se `dotnet format` falhar na CI, correr `dotnet format InternalTicketManager.sln`.
- Se a aplicação não criar dados de exemplo, confirmar que a base de dados está vazia ou apagar a base local de desenvolvimento.
