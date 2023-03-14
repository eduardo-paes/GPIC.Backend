## Funcionalidade - Cancelar projetos
 Feature: Cancelar projetos
    Como professor logado na plataforma
    Quero acessar a página de Cancelar Projeto
    Para cancelar meu projeto em andamento.

  Scenario: Acessar página de cancelar projeto
    Dado que estou na página de projetos
    E tendo selecionado a aba de Projetos em Andamento
    Quando identifico o projeto que desejo cancelar
    E seleciono a opção de Cancelar
    Então sou direcionado para a página de Cancelar Projeto.

  Scenario: Cancelar projeto
    Dado que estou na página de Cancelar Projeto
    Quando preencho o motivo de cancelamento
    E seleciono a opção de Salvar
    Então recebo a informação de que meu projeto foi cancelado com sucesso
    E sou direcionado para a página de visualizar meus projetos.
