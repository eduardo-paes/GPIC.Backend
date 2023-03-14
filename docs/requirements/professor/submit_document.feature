## Funcionalidade - Entregar documento de projetos
  Feature: Realizar a entrega de documentos de um projeto Deferido
    Como professor logado na plataforma
    Quero acessar a página de Projetos
    Para realizar a Entrega de Documentos de um projeto deferido.

  # Indicação de aluno é obrigatório nesta etapa, caso o aluno ainda não tenha sido associado ao projeto
  Scenario: Acessar página de Entrega de Documento de um projeto Deferido
    Dado que estou na página de projetos
    Quando seleciono a aba de Projetos em Andamento 
    E estão sendo listados meus projetos em aberto
    E seleciono a opção de Visualizar Parecer de um projeto cujo status está como Deferido
    E seleciono a opção de Entregar Documentos 
    Então sou redirecionado para a página de Entrega de Documentos.

  Scenario: Realizar a Entrega de Documentos com sucesso
    Dado que estou na página de Entrega de Documentos de um projeto Deferido
    Quando realizo o upload dos arquivos solicitados
    E esses são carregados com sucesso
    E seleciono a opção de salvar
    Então a entrega de documentos é concluída com sucesso
    E meu projeto passa para o status de Em Análise
    E sou direcionado para a página de visualizar meus projetos.
