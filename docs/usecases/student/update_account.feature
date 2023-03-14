## Funcionalidade - Alterar perfil
  Feature: Inserir/alterar informações do perfil
    Como aluno logado na plataforma
    Quero acessar a página de Perfil
    Para inserir/alterar dados básicos e bancários do perfil.

  Scenario: Acessar página de Perfil
    Dado que estou na página de Perfil
    Quando seleciono a opção de Editar
    Então sou direcionado para a página de Edição de Perfil.

  Scenario: Alterar o perfil com sucesso
    Dado que estou na página de Edição de Perfil
    Quando altero alguns campos permitidos com dados válidos
    E seleciono a opção de Salvar
    Então recebo a informação de que meu perfil foi alterado com sucesso
    E sou direcionado para a página de visualizar meu perfil.

  Scenario: Tentativa de alterar o perfil com dados inválidos
    Dado que estou na página de Edição de Perfil
    Quando altero alguns campos permitidos com dados inválidos
    E seleciono a opção de Salvar
    Então recebo a informação de que meu perfil não pôde ser atualizado
    E os campos com dados inválidos são informados.
