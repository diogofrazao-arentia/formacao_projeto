# Prompts da formação

Usar estes prompts durante o exercício. Substituir as secções entre parênteses retos pelo conteúdo relevante.

## Rever um requisito

```text
Analisa este requisito funcional.

Indica:
- ambiguidades;
- regras em falta;
- critérios de aceitação fracos;
- casos de erro em falta;
- perguntas que devo esclarecer antes de pedir código.

Requisito:
[colar requisito]
```

## Melhorar um requisito

```text
Com base nesta revisão, reescreve o requisito de forma clara, objetiva e testável.

Mantém o scope pequeno.
Não adiciones login, utilizadores, permissões, categorias, anexos, notificações ou dashboard.

Requisito atual:
[colar requisito]

Revisão:
[colar revisão]
```

## Pedir um plano técnico

```text
Com base neste requisito e na estrutura do projeto, propõe um plano técnico pequeno e verificável.

Não escrevas código ainda.
O plano deve indicar:
- alterações principais;
- ficheiros prováveis;
- validações;
- testes;
- riscos ou dúvidas.

Requisito:
[colar requisito]
```

## Implementar um passo

```text
Implementa apenas o primeiro passo do plano.

Mantém o scope pequeno.
Não adiciones login, utilizadores, permissões, categorias, anexos, notificações ou dashboard.
Depois indica o que foi alterado e que validação devo correr.

Plano:
[colar plano]
```

## Continuar a implementação

```text
Continua para o próximo passo do plano.

Não alteres partes não relacionadas da aplicação.
Mantém os testes alinhados com o comportamento definido no requisito.

Estado atual:
[descrever estado ou colar resumo do diff]
```

## Rever um diff

```text
Revê este diff como senior developer.

Procura:
- bugs;
- problemas de validação;
- casos não tratados;
- alterações desnecessárias;
- scope creep;
- testes em falta.

Diff:
[colar diff]
```

## Sugerir testes

```text
Sugere testes de integração para validar a funcionalidade de comentários nos tickets.

Inclui casos felizes, validação e erros.
Mantém os testes adequados a ASP.NET Core MVC com EF Core e SQL Server.

Requisito:
[colar requisito]
```
