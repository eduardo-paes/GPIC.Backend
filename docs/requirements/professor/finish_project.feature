## Funcionalidade - Encerrar projetos
  Feature: Encerrar projetos
    Como professor logado na plataforma
    Quero acessar a página de Projetos
    Para realizar o Encerramento de um projeto iniciado.

  Scenario: Acessar página de Encerrar Projeto de um projeto Iniciado
    Dado que estou na página de projetos
    Quando seleciono a aba de Projetos em Andamento 
    E estão sendo listados meus projetos em aberto
    E seleciono o projeto com status Iniciado que desejo encerrar
    E esse já atingiu o tempo mínimo de execução do projeto para poder ser encerrado
    E seleciono a opção de Encerrar
    Então sou redirecionado para a página de Encerrar Projeto.

  Scenario: Realizar o encerramento de um projeto com sucesso
    Dado que estou na página de Encerrar Projeto de um projeto Iniciado
    Quando preencho de forma válida os campos obrigatórios para encerramento
    E seleciono a opção de salvar
    Então recebo a informação de que meu projeto foi encerrado com sucesso
    E sou direcionado para a página de visualizar meus projetos.


