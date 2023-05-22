## Funcionalidade - Criar projetos
  Feature: Criar projetos
    Como professor logado na plataforma
    Quero acessar as páginas de Criação de Projetos
    Para criar meus projetos.

  Scenario: Acessar página de criação de projeto
    Dado que estou na página de projetos
    Quando seleciono a opção de Criar Projeto
    Então sou direcionado para a página de Criar Projeto.

  Scenario: Criar um projeto com sucesso
    Dado que estou na página de Criar Projeto
    Quando preencho todos os campos obrigatórios com dados válidos
    E seleciono a opção de Salvar
    Então recebo a informação de que meu projeto foi aberto com sucesso
    E sou direcionado para a página de visualizar meus projetos.
