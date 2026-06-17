# Especificacoes funcionais

Este diretorio e o local para especificar requisitos e comportamento funcional
antes de implementar alteracoes na aplicacao.

Organizar as especificacoes por requisito funcional (FR), com um ficheiro por
funcionalidade.

## Como escrever uma spec

Cada spec deve indicar, de forma curta e testavel:

- objetivo da funcionalidade;
- comportamento esperado;
- regras de validacao;
- estados ou mensagens relevantes para o utilizador;
- criterios de aceitacao;
- notas de teste manual ou automatizado.

Manter a spec alinhada com o ambito atual da aplicacao. Evitar incluir
utilizadores, autenticacao, autorizacao, categorias, anexos, notificacoes,
dashboards, integracoes externas ou automacoes sem pedido explicito.

## Implementadas

- FR1: Listar tickets (`implemented/FR1-listar-tickets.md`)
- FR2: Criar ticket (`implemented/FR2-criar-ticket.md`)
- FR3: Ver detalhe de ticket (`implemented/FR3-ver-detalhe-ticket.md`)
- FR4: Editar estado de ticket (`implemented/FR4-editar-estado-ticket.md`)
- FR5: Criar tickets de exemplo (`implemented/FR5-criar-tickets-exemplo.md`)

## Planeadas

- FR6: Comentarios nos tickets (`planned/FR6-comentarios-nos-tickets.md`)

## Fluxo de trabalho

1. Criar ou atualizar a spec em `planned/`.
2. Rever os criterios de aceitacao.
3. Implementar a alteracao.
4. Atualizar testes e documentacao quando necessario.
5. Mover a spec para `implemented/` quando a funcionalidade estiver concluida.

Antes de implementar uma spec, seguir tambem `../00-training-flow.md`.
