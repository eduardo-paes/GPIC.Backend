# GPIC - Backend

Bem-vindo ao código do backend do sistema Gerenciamento Institucional de Processos de Iniciação Científica (GPIC)! Este repositório contém o código das aplicações de backend do sistema GPIC, uma solução web-based para a otimização do processo de gestão do Programa Institucional de Bolsas de Iniciação Científica (PIBIC) do Centro Federal de Educação Tecnológica Celso Suckow da Fonseca (CEFET/RJ) Campus Petrópolis.

Este repositório contém o código-fonte e a definição de testes automatizados para garantir a qualidade e a correção do Backend do GPIC. A base de código é continuamente construída, testada e integrada através de um processo de integração contínua que utiliza a estrutura GitHub Actions para acelerar o desenvolvimento e avaliar o padrão de qualidade do código.

O backend faz parte do sistema GPIC, composto pelo frontend, o backend propriamente dito e um servidor isolado para a execução de processos em segundo plano que não requerem a interação do utilizador. O frontend está armazenado no repositório [GPIC.WebUI](https://github.com/eduardo-paes/CopetSystem.WebUI), e o servidor de fundo (background worker) está localizado no subdiretório `src/GPIC.WebFunction` deste repositório.

## Tabela de Conteúdo

- [GPIC - Backend](#gpic---backend)
  - [Tabela de Conteúdo](#tabela-de-conteúdo)
  - [Introdução](#introdução)
  - [Arquitetura do Sistema](#arquitetura-do-sistema)
  - [Pré-Requisitos](#pré-requisitos)
  - [Contribuição](#contribuição)
  - [Licença](#licença)

## Introdução

O Backend do GPIC é responsável por gerenciar e servir dados para as aplicações web através de uma interface REST API utilizando C# e .NET e interage com um banco de dados PostgreSQL. Foi concebido utilizando uma arquitetura cliente-servidor, uma arquitetura de microsserviços que utiliza contentores Docker e estruturas de Arquitetura Limpa para garantir a separação em camadas das preocupações e a testabilidade.

## Arquitetura do Sistema

A arquitetura do sistema foi desenvolvida com base no modelo Cliente-Servidor. As responsabilidades foram separadas em camadas de front-end e back-end, permitindo a independência entre ambas. A adoção da Clean Architecture para arquitetura das aplicações que compõem o sistema foi baseada no intuito de alcançar coesão, redução de acoplamento e escalabilidade.

A interface de usuário foi desenvolvida como uma Single Page Application (SPA), utilizando o framework ReactJS para aprimorar a usabilidade e a experiência do usuário. O back-end foi desenvolvido utilizando C# e .NET, adotando a Arquitetura Limpa para camadas do sistema. O banco de dados utilizado foi PostgreSQL.

O sistema foi construído por meio de três aplicações distintas, cada uma operando em contêineres separados: GPIC.WebUI (responsável pela interface do usuário), GPIC.WebAPI (que oferece funcionalidades conforme as regras de negócio) e GPIC.WebFunctions (responsável por execuções periódicas de rotinas assíncronas).

A seguir, há um diagrama ilustrando a arquitetura do sistema:

![Diagrama arquitetural do sistema (GPIC)](/docs/images/system_design.png)

## Pré-Requisitos

Antes de executar o backend do sistema, você deverá garantir que os seguintes pontos sejam atendidos:

- O sistema operacional suporta a instalação da plataforma .NET;
- O banco de dados PostgreSQL está instalado e configurado;
- As variáveis de ambiente necessárias estão definidas (ex: string de conexão do banco de dados).

Para executar o backend, siga os seguintes passos:

1. Faça um clone do repositório.
2. Navegue até o diretório raiz `/src`.
3. Execute o comando "dotnet run" na linha de comando.

## Contribuição

As contribuições para este repositório são bem-vindas! Para contribuir, siga estes passos:

1. Faça um fork deste repositório na sua conta GitHub.
2. Crie um novo ramo para a sua contribuição.
3. Adicione os seus exemplos de código ou projectos ao diretório apropriado.
4. Escreva ficheiros README claros e informativos para as suas adições.
5. Crie um pull request para juntar as suas alterações a este repositório.

As suas contribuições ajudarão a aprimorar o processo de análise e controle dos projetos de IC do CEFET.

## Licença

Este projeto está licenciado sob a Licença MIT - veja o ficheiro [LICENSE](./LICENSE) para mais detalhes.
