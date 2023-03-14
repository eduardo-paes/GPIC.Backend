## Funcionalidade - Entregar relatório parcial de projetos
  Feature: Realizar a entrega de relatório parcial de um projeto Iniciado
    Como professor logado na plataforma
    Quero acessar a página de Projetos
    Para realizar a Entrega de Relatório Parcial de um projeto iniciado.

  Scenario: Acessar página de Entrega de Relatório Parcial de um projeto Iniciado
    Dado que estou na página de projetos
    Quando seleciono a aba de Projetos em Andamento 
    E estão sendo listados meus projetos em aberto
    E seleciono a opção de Acompanhar de um projeto cujo status está como Iniciado
    Então sou redirecionado para a página de Acompanhamento do Projeto.

  Scenario: Realizar a Entrega de Relatório Parcial com sucesso
    Dado que estou na página de Acompanhamento do Projeto de um projeto Iniciado
    Quando realizo o upload do Relatório Parcial
    E esse é carregado com sucesso
    E seleciono a opção de salvar
    Então a entrega de relatório parcial é realizada com sucesso
    E sou direcionado para a página de visualizar meus projetos.
