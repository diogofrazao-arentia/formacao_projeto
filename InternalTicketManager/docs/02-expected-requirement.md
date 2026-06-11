# Requisito esperado: Comentários nos tickets

Este documento é a referência do formador. Não deve ser entregue aos participantes no início do exercício.

## Nome da funcionalidade

Comentários nos tickets

## Objetivo

Permitir registar contexto, notas e atualizações de acompanhamento num ticket.

## Comportamento esperado

Na página de detalhe do ticket, o utilizador consegue:

- ver os comentários existentes desse ticket;
- adicionar um novo comentário;
- ver o novo comentário depois de ser guardado.

Os comentários são apresentados com nome do autor, conteúdo e data de criação.

## Dados envolvidos

```text
TicketComment
- Id
- TicketId
- AuthorName
- Content
- CreatedAt
```

## Regras de negócio

- Um comentário pertence sempre a um ticket existente.
- `AuthorName` é obrigatório.
- `Content` é obrigatório.
- `Content` tem o máximo de 1000 caracteres.
- `CreatedAt` é preenchido automaticamente pela aplicação.
- Os comentários são apresentados do mais antigo para o mais recente.

## Critérios de aceitação

- Dado um ticket existente, quando o utilizador abre a página de detalhe, então os comentários desse ticket são apresentados.
- Dado um ticket existente, quando o utilizador submete um comentário válido, então o comentário é guardado e aparece na página de detalhe.
- Dado um comentário vazio, quando o utilizador submete o formulário, então o comentário não é guardado e é apresentada uma mensagem de validação.
- Dado um id de ticket inexistente, quando o utilizador tenta ver ou comentar esse ticket, então a aplicação devolve `404`.
- O comportamento existente dos tickets continua a funcionar.

## Casos de erro

- `AuthorName` vazio.
- `Content` vazio.
- `Content` com mais de 1000 caracteres.
- Ticket inexistente.

## Validação

Correr:

```powershell
dotnet build .\InternalTicketManager.sln --configuration Release
dotnet test .\InternalTicketManager.sln --configuration Release
```

Teste manual:

1. Abrir a aplicação.
2. Abrir a página de detalhe de um ticket.
3. Adicionar um comentário válido.
4. Confirmar que o comentário aparece.
5. Submeter um comentário vazio.
6. Confirmar que aparece uma mensagem de validação.
7. Confirmar que lista, criação, detalhe e edição de estado de tickets continuam a funcionar.
