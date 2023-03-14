## Funcionalidade - Alterar projetos
  Feature: Alterar projetos
    Como professor logado na plataforma
    Quero acessar a página de Edição de Projeto
    Para alterar meu projeto em andamento.

  Scenario: Acessar página de edição de projeto
    Dado que estou na página de projetos
    E tendo selecionado a aba de Projetos em Andamento
    Quando identifico o projeto que desejo alterar
    E seleciono a opção de Editar
    Então sou direcionado para a página de Editar Projeto.

  Scenario: Alterar um projeto com sucesso
    Dado que estou na página de Editar um projeto
    Quando altero alguns campos permitidos com dados válidos
    E seleciono a opção de Salvar
    Então recebo a informação de que meu projeto foi alterado com sucesso
    E sou direcionado para a página de visualizar meus projetos.

  Scenario: Alterar um projeto indicando um aluno COM cadastro na plataforma
    Dado que estou na página de Editar um projeto
    Quando indico no projeto um outro aluno com cadastro na plataforma
    E seleciono a opção de Salvar
    Então recebo a informação de que meu projeto foi alterado com sucesso
    E sou direcionado para a página de visualizar meus projetos.

  Scenario: Alterar um projeto indicando um aluno SEM cadastro na plataforma
    Dado que estou na página de Editar um projeto
    Quando indico no projeto um outro aluno sem cadastro na plataforma
    E seleciono a opção de Salvar
    Então recebo a informação de que meu projeto foi alterado com sucesso
    E o aluno recebe um e-mail para criar seu cadastro na plataforma informando apenas seus dados básicos
    E sou direcionado para a página de visualizar meus projetos.
