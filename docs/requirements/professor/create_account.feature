## Funcionalidade - Criar conta
  Feature: Criação de conta do professor
    Como professor
    Quero acessar a página de Cadastro da plataforma
    Para criar minha de acesso à plataforma.

  Scenario: Criar conta com sucesso
    Dado que sou um professor e estou na página de Cadastro da plataforma
    Quando eu inserir meus dados de cadastro na página
    Então minha conta será criada com sucesso.

  Scenario: Tentativa de criação de conta com dados inválidos 
    Dado que estou na página de Cadastro da plataforma
    Quando eu inserir meus dados de cadastro na página, havendo alguma informação inválida
    Então minha conta não será criada.
