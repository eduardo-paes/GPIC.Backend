## Funcionalidade - Logar na plataforma
  Feature: Logar na plataforma
    Como usuário
    Quero fazer login na minha conta
    Para ter acesso às funcionalidades de plataforma.

  Scenario: Logar com sucesso na plataforma
    Dado que estou na tela de login da plataforma
    Quando me autêntico com credenciais válidas
    Então sou direcionado para a página de entrada.

  Scenario: Tentativa inválida de login na plataforma
    Dado que estou na tela de login da plataforma
    Quando tento me autenticar com credenciais inválidas
    Então recebo a informação de que minhas credenciais não são válidas.

