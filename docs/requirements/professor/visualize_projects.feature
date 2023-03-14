## Funcionalidade - Visualizar projetos
  Feature: Visualizar meus Projetos
    Como professor logado na plataforma
    Quero acessar a página de Projetos
    Para visualizar meus projetos abertos ou encerrados.

  Scenario: Acessar a página de gerenciamento dos meus projetos 
    Dado que estou logado na plataforma
    Quando seleciono a opção de acesso à página de Projetos
    Então sou direcionado para a página de Projetos.

  Scenario: Visualizar meus projetos em andamento
    Dado que estou na página de projetos
    Quando seleciono a aba de Projetos em Andamento
    Então são listados todos os meus projetos em andamento.

  Scenario: Visualizar meus projetos encerrados
    Dado que estou na página de projetos
    Quando seleciono a aba de Projetos Encerrados
    Então são listados todos os meus projetos encerrados.

  Scenario: Acompanhar andamento dos meus projetos
    Dado que estou na página de projetos
    Quando seleciono a aba de Projetos em Aberto 
    E estão sendo listados os meus projetos em aberto
    E seleciono a opção de Visualizar Parecer de um projeto
    Então me é informado detalhes sobre o status e parecer do meu projeto.
