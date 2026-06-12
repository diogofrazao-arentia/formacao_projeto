# Tarefa por fazer: Comentarios nos tickets

## Contexto

A aplicacao permite as equipas internas criar e acompanhar tickets. Neste momento, um ticket pode ser criado, consultado e movido entre estados, mas nao existe forma de registar contexto durante o acompanhamento.

## Pedido

Adicionar comentarios aos tickets para que as pessoas consigam registar contexto enquanto um ticket esta a ser acompanhado.

## O que fazer

Antes da implementacao, escrever um requisito claro e testavel para esta funcionalidade.

Depois, implementar a funcionalidade e valida-la com testes ou com um plano de teste manual documentado.

## Fluxo sugerido

1. Escrever o requisito com base nesta tarefa.
2. Pedir a IA para rever o requisito.
3. Melhorar o requisito com base na revisao.
4. Pedir a IA um plano tecnico pequeno.
5. Implementar o plano em passos pequenos.
6. Correr build e testes.
7. Testar manualmente no browser.
8. Rever o diff antes de aceitar o trabalho.

## Comportamento da aplicacao base

A aplicacao base ja permite:

- Listar tickets
- Criar tickets
- Ver detalhe de ticket
- Editar estado de ticket

A aplicacao base nao suporta comentarios de forma intencional.

## Criterio de conclusao

O exercicio esta concluido quando:

- o requisito esta claro e testavel;
- os comentarios podem ser vistos na pagina de detalhe do ticket;
- e possivel adicionar um comentario a um ticket existente;
- conteudo de comentario vazio e rejeitado;
- build e testes passam;
- o diff contem apenas alteracoes necessarias para comentarios nos tickets.

## Prompts uteis

### Rever um requisito

```text
Analisa este requisito funcional.

Indica:
- ambiguidades;
- regras em falta;
- criterios de aceitacao fracos;
- casos de erro em falta;
- perguntas que devo esclarecer antes de pedir codigo.

Requisito:
[colar requisito]
```

### Melhorar um requisito

```text
Com base nesta revisao, reescreve o requisito de forma clara, objetiva e testavel.

Mantem o scope pequeno.
Nao adiciones login, utilizadores, permissoes, categorias, anexos, notificacoes ou dashboard.

Requisito atual:
[colar requisito]

Revisao:
[colar revisao]
```

### Pedir um plano tecnico

```text
Com base neste requisito e na estrutura do projeto, propoe um plano tecnico pequeno e verificavel.

Nao escrevas codigo ainda.
O plano deve indicar:
- alteracoes principais;
- ficheiros provaveis;
- validacoes;
- testes;
- riscos ou duvidas.

Requisito:
[colar requisito]
```

### Implementar um passo

```text
Implementa apenas o primeiro passo do plano.

Mantem o scope pequeno.
Nao adiciones login, utilizadores, permissoes, categorias, anexos, notificacoes ou dashboard.
Depois indica o que foi alterado e que validacao devo correr.

Plano:
[colar plano]
```

### Continuar a implementacao

```text
Continua para o proximo passo do plano.

Nao alteres partes nao relacionadas da aplicacao.
Mantem os testes alinhados com o comportamento definido no requisito.

Estado atual:
[descrever estado ou colar resumo do diff]
```

### Rever um diff

```text
Reve este diff como senior developer.

Procura:
- bugs;
- problemas de validacao;
- casos nao tratados;
- alteracoes desnecessarias;
- scope creep;
- testes em falta.

Diff:
[colar diff]
```

### Sugerir testes

```text
Sugere testes de integracao para validar a funcionalidade de comentarios nos tickets.

Inclui casos felizes, validacao e erros.
Mantem os testes adequados a ASP.NET Core MVC com EF Core e SQL Server.

Requisito:
[colar requisito]
```