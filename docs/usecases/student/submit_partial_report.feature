## Funcionalidade - Acompanhar entrega de relatório parcial de projetos
  Feature: Acompanhar a entrega de relatório parcial de um projeto Iniciado
    Como aluno logado na plataforma
    Quero acessar a página de Projetos
    Para acompanhar a Entrega de Relatório Parcial de um projeto iniciado a qual estou associado.

  Scenario: Acessar página de Entrega de Relatório Parcial de um projeto Iniciado
    Dado que estou na página de projetos
    Quando seleciono a aba de Projetos em Andamento 
    E estão sendo listados meus projetos em aberto
    E seleciono a opção de Acompanhar de um projeto cujo status está como Iniciado
    Então sou redirecionado para a página de Acompanhamento do Projeto
    E consigo acompanhar a entrega do relatório parcial.
