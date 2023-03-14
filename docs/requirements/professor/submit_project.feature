## Funcionalidade - Submeter projetos
  Feature: Submeter projetos
    Como professor logado na plataforma
    Quero acessar as páginas de Submissão de Projeto
    Para submeter meu projeto em aberto.

  Scenario: Acessar página de submissão
    Dado que estou na página de Projetos
    Quando seleciono meu projeto com status Aberto
    E seleciono a opção de Submeter
    Então sou direcionado para a página de Submeter Projeto.

  Scenario: Submeter um projeto com sucesso
    Dado que estou na página de Submeter Projeto
    Quando preencho todos os campos obrigatórios com dados válidos
    E indico o aluno que fará parte do projeto (opcional)
    E seleciono a opção de salvar
    Então recebo a informação de que meu projeto foi submetido com sucesso
    E meu projeto passa para o status de Em Avaliação
    E sou direcionado para a página de visualizar meus projetos.

  # Indicação de aluno é opcional, torna-se obrigatória após a aprovação do projeto
  Scenario: Submeter um projeto indicando um aluno COM cadastro na plataforma
    Dado que estou na página de Submeter Projeto
    Quando preencho todos os campos obrigatórios com dados válidos
    E indico o aluno com cadastro na plataforma que fará parte do projeto 
    E seleciono a opção de salvar
    Então recebo a informação de que meu projeto foi submetido com sucesso
    E sou direcionado para a página de visualizar meus projetos.

  Scenario: Submeter um projeto indicando um aluno SEM cadastro na plataforma
    Dado que estou na página de Submeter Projeto
    Quando preencho todos os campos obrigatórios com dados válidos
    E indico o aluno sem cadastro na plataforma que fará parte do projeto
    E seleciono a opção de salvar
    Então recebo a informação de que meu projeto foi submetido com sucesso
    E o aluno recebe um e-mail para criar seu cadastro na plataforma informando apenas seus dados básicos
    E sou direcionado para a página de visualizar meus projetos.
