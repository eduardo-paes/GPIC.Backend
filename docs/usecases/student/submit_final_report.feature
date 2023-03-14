## Funcionalidade - Acompanhar entrega de relatório final de projetos
  Feature: Acompanhar a entrega de relatório final de um projeto Encerrado
    Como aluno logado na plataforma
    Quero acessar a página de Projetos
    Para acompanhar a Entrega de Relatório Final de um projeto encerrado a qual estou associado.

  Scenario: Acessar página de Entrega de Relatório Final de um projeto Encerrado
    Dado que estou na página de projetos
    Quando seleciono a aba de Projetos Encerrados 
    E estão sendo listados meus projetos encerrados
    E seleciono a opção de Acompanhar de um projeto cujo status está como Encerrado
    Então sou redirecionado para a página de Acompanhamento do Projeto
    E posso acompanhar a entrega do relatório final.