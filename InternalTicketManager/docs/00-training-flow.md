# Fluxo da formação

## Objetivo

Usar uma aplicação pequena e funcional para praticar um fluxo de desenvolvimento assistido por IA:

```text
pedido vago -> requisito -> revisão -> plano técnico -> implementação -> testes -> revisão do diff
```

## Fluxo da sessão

1. Abrir e correr a aplicação base.
2. Confirmar o comportamento atual:
   - é possível listar tickets;
   - é possível criar tickets;
   - é possível ver o detalhe de um ticket;
   - é possível editar o estado de um ticket;
   - comentários nos tickets não existem.
3. Ler `docs/01-exercise.md`.
4. Ler o pedido vago em `docs/TASK-001-ticket-comments.md`.
5. Preencher `docs/REQ-001-ticket-comments.md`.
6. Pedir à IA para rever o requisito.
7. Melhorar o requisito.
8. Pedir à IA um plano técnico pequeno.
9. Implementar a funcionalidade por etapas.
10. Correr build e testes.
11. Testar manualmente no browser.
12. Pedir à IA para rever o diff.
13. Fazer commit final.

## Checkpoints do formador

Usar estes checkpoints para manter o exercício controlado:

- O requisito é escrito antes do código.
- O requisito tem comportamento, dados, regras, critérios de aceitação e casos de erro.
- O plano técnico é revisto antes da implementação.
- A implementação adiciona apenas comentários nos tickets.
- Build e testes passam.
- O teste manual confirma o fluxo de comentários.
- A revisão do diff remove alterações desnecessárias.

## Timebox esperado

- Walkthrough da app: 5 minutos
- Escrita do requisito: 10 minutos
- Revisão e melhoria do requisito: 10 minutos
- Plano técnico: 5 minutos
- Implementação: 25 minutos
- Testes e revisão: 10 minutos
