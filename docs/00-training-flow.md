# Fluxo de trabalho

Este projeto deve ser trabalhado sempre em passos pequenos, verificaveis e com
um registo claro do que foi decidido.

## 1. Iniciar

Antes de alterar codigo:

- abrir o projeto em `InternalTicketManager`;
- ler o `README.md`, `AGENTS.md` e a spec relevante em `docs/specs`;
- confirmar o comportamento atual da aplicacao;
- confirmar que a tarefa esta dentro do scope da app.

Comandos uteis:

```powershell
cd InternalTicketManager
dotnet restore InternalTicketManager.sln
dotnet run --project src/TicketManager.Web
```

## 2. Fazer a spec

Antes de pedir implementacao, escrever ou completar a spec funcional.

Para o exercicio principal, partir do pedido vago em:

```text
docs/exercises/tarefa-por-fazer.md
```

Depois completar a spec em:

```text
docs/specs/planned/FR6-comentarios-nos-tickets.md
```

A spec deve deixar claro:

- objetivo;
- rotas envolvidas;
- comportamento esperado;
- dados envolvidos;
- regras de negocio;
- regras tecnicas;
- criterios de aceitacao;
- casos de erro;
- estado atual.

## 3. Pedir revisao da spec

Pedir a IA para encontrar ambiguidades antes de pedir codigo.

Exemplo:

```text
Analisa esta spec funcional. Indica ambiguidades, regras em falta, criterios de aceitacao fracos e perguntas que devo esclarecer antes de pedir codigo.
```

Atualizar a spec com as decisoes tomadas.

## 4. Pedir plano tecnico

Com a spec fechada, pedir um plano pequeno e verificavel.

O plano deve indicar:

- alteracoes principais;
- ficheiros provaveis;
- validacoes;
- testes;
- riscos ou duvidas.

Nao pedir codigo nesta fase.

## 5. Implementar

Implementar por passos pequenos.

Depois de cada passo relevante:

- rever o diff;
- confirmar que nao entrou scope extra;
- manter a implementacao alinhada com a spec.

Nao adicionar login, utilizadores reais, permissoes, anexos, categorias,
dashboard ou notificacoes sem pedido explicito.

## 6. Testar

Validar localmente:

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

Tambem fazer teste manual no browser quando houver alteracoes de UI.

## 7. Atualizar docs

Atualizar a documentacao quando a funcionalidade, comandos, setup ou comportamento
mudarem.

Documentos provaveis:

- `README.md`;
- `docs/specs/...`;
- `docs/documentation-standard.md`, se houver alteracoes nas regras de docs.

Se forem adicionados tipos publicos, actions ou classes de dominio, manter os
comentarios XML alinhados com `docs/documentation-standard.md`.

## 8. Commit

Antes do commit:

- rever o diff completo;
- confirmar que os testes passaram;
- confirmar que a documentacao necessaria foi atualizada;
- usar uma mensagem de commit curta e concreta.

Exemplo:

```text
Add ticket comments
```

## 9. Verificar CI

Depois de fazer push ou abrir pull request, verificar a pipeline de CI.

A CI deve confirmar:

- restore;
- formatacao;
- build;
- testes;
- publish.

Se a CI falhar, corrigir a causa no codigo ou nos testes e repetir o ciclo:

```text
corrigir -> testar localmente -> commit -> push -> verificar CI
```
