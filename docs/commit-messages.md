# Mensagens de commit

Este repositorio usa Conventional Commits para manter o historico claro,
pesquisavel e facil de rever.

## Formato

```text
<type>[optional scope]: <description>

[optional body]

[optional footer(s)]
```

## Tipos

| Tipo | Descricao |
| --- | --- |
| `feat` | Nova funcionalidade |
| `fix` | Correcao de uma anomalia |
| `docs` | Alteracoes apenas na documentacao. Por exemplo alterar o ficheiro README |

## Regras

- Escrever a descricao no imperativo ou como acao curta.
- Usar scope quando ajuda a localizar a alteracao: `readme`, `tickets`,
  `ci`, `docs`, `tests`.
- Manter a primeira linha curta e concreta.
- Usar body quando for necessario explicar contexto, impacto ou decisao.
- Usar footer para referencias como `Resolve: #123`.
- Indicar `BREAKING CHANGE:` no footer quando existir quebra de compatibilidade.

## Exemplos

```text
docs(readme): documentar estrutura do repositorio
```

```text
fix(tickets): validar comentario vazio

Impede a criacao de comentarios sem autor ou texto no detalhe do ticket.

Resolve: #123
```
