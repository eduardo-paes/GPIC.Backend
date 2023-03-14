## Funcionalidade - Alterar informações da conta
  Feature: Alterar informações da conta
    Como usuário da plataforma
    Quero acessar a página de informações da conta
    Para modificar os dados cadastrados.

  Scenario: Acessar a página de informações da conta
    Dado que estou logado na plataforma
    Quando seleciono a opção Informações da Conta
    Então sou direcionado para a página de informações da minha conta.

  Scenario: Alterar informações da conta
    Dado que estou na página de informações da minha conta na plataforma
    Quando realizo alteração nos campos relacionados às minhas informações pessoais
    E seleciono a opção de salvar
    Então minhas informações são atualizadas na plataforma.

  Scenario: Alterar senha de acesso da conta
    Dado que estou na página de informações da minha conta na plataforma
    Quando realizo alteração no campo de senha
    E confirmo a nova senha informada
    E seleciono a opção de salvar
    Então minha senha de acesso à conta é atualizada na plataforma.
