## Funcionalidade - Ressubmeter projetos (Recurso)
  Feature: Realizar a ressubmissão de projetos indeferidos
    Como professor logado na plataforma
    Quero acessar a página de Projetos
    Para realizar a ressubmissão de um projeto indeferido.

  Scenario: Acessar página de ressubmissão de um projeto indeferido
    Dado que estou na página de projetos
    Quando seleciono a aba de Projetos em Andamento 
    E estão sendo listados meus projetos em aberto
    E seleciono a opção de Visualizar Parecer de um projeto cujo status está como Indeferido
    E seleciono a opção de Ressubmeter (Recurso)
    Então sou redirecionado para a página de Ressubmissão.

  # Nenhuma informação do projeto pode ser alterada nessa etapa
  Scenario: Realizar uma ressubmissão com sucesso
    Dado que estou na página de Ressubmissão de um projeto indeferido
    Quando preencho o motivo de Recurso
    E seleciono a opção de salvar
    Então a ressubmissão é concluída com sucesso
    E meu projeto retorna para o status de Em Avaliação
    E sou direcionado para a página de visualizar meus projetos.
