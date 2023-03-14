## Funcionalidade - Entregar documentos de projeto
  Feature: Realizar a entrega de documentos de um projeto
    Como aluno logado na plataforma
    Quero acessar a página de Projetos
    Para realizar a Entrega de Documentos de um projeto a qual estou associado quando solicitado.

  Scenario: Acessar página de Entrega de Documento de um projeto
    Dado que estou na página de projeto
    Quando seleciono o projeto a qual estou associado
    E seleciono a opção de Entregar Documentos 
    Então sou redirecionado para a página de Entrega de Documentos.

  Scenario: Realizar a Entrega de Documentos com sucesso
    Dado que estou na página de Entrega de Documentos de um projeto
    Quando realizo o upload dos arquivos solicitados
    E esses são carregados com sucesso
    E seleciono a opção de salvar
    Então a entrega de documentos é concluída com sucesso
    E sou direcionado para a página de visualizar meu projeto.
