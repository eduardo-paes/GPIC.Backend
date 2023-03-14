## Funcionalidade - Entrega de relatório final
  Feature: Realizar a entrega de relatório final de um projeto Encerrado
    Como professor logado na plataforma
    Quero acessar a página de Projetos
    Para realizar a Entrega de Relatório Final de um projeto encerrado.

  Scenario: Acessar página de Entrega de Relatório Final de um projeto Encerrado
    Dado que estou na página de projetos
    Quando seleciono a aba de Projetos Encerrados 
    E estão sendo listados meus projetos encerrados
    E seleciono a opção de Acompanhar de um projeto cujo status está como Encerrado
    Então sou redirecionado para a página de Acompanhamento do Projeto.

  Scenario: Realizar a Entrega de Relatório Final com sucesso
    Dado que estou na página de Acompanhamento do Projeto de um projeto Encerrado
    Quando realizo o upload do Relatório Final
    E esse é carregado com sucesso
    E seleciono a opção de salvar
    Então a entrega de relatório final é realizada com sucesso
    E sou direcionado para a página de visualizar meus projetos.