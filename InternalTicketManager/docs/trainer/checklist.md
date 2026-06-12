# Checklist do formador

## Preparacao

- Abrir o projeto a partir de `InternalTicketManager`.
- Confirmar que o .NET 8 SDK esta instalado.
- Confirmar que existe SQL Server LocalDB ou Docker disponivel para os testes.
- Correr `dotnet restore InternalTicketManager.sln`.
- Correr `dotnet run --project src/TicketManager.Web`.
- Validar que a lista de tickets abre e mostra dados de exemplo.

## Fluxo da sessao

Usar `docs/00-training-flow.md` como guia principal.

Sequencia esperada:

1. Iniciar e rever contexto.
2. Fazer ou completar a spec.
3. Pedir revisao da spec.
4. Pedir plano tecnico.
5. Implementar por passos pequenos.
6. Testar localmente.
7. Atualizar documentacao.
8. Fazer commit.
9. Verificar CI.

## Checkpoints

- A spec esta clara antes de existir codigo novo.
- O plano tecnico e pequeno e verificavel.
- A implementacao respeita o scope da spec.
- Os testes locais passam.
- A documentacao relevante foi atualizada.
- O diff foi revisto antes do commit.
- A CI foi verificada depois do push ou pull request.

## Validacao local

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
