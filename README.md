# Infrastructure.WebAPI

Restful API created in .NET 7.0.0 to support SPA.

## DataBase - PostgreSQL

Para levantar o banco de dados é necessário executar o comando abaixo na pasta raiz:

```
  docker compose up -d
```

Em seguida, é preciso acessar o pgAdmin através da rota abaixo:

> http://localhost:16543/browser/

E criar um servidor utilizando as informações de _host_, _username_, _password_ e _database_ que estão informadas no arquivo docker-compose.yaml utilizado.
Exemplo:

- **host**: copet-system-db
- **username**: copet-admin
- **password**: Copet@123
- **database**: COPET_DB
- **port**: 5432

## Migrations

Criando as Migrations iniciais para criação das tabelas do banco de dados:

```
dotnet ef migrations add Initialize --project ./Infrastructure/Persistence/Persistence.csproj
```

Executando as Migrations:

```
dotnet ef database update
```

Removendo as Migrations:

```
dotnet ef migrations remove
```

## Execução com Dotnet

Comandos para execução do projeto .NET utilizando CLI:

```
cd Infrastructure.WebAPI
dotnet build
dotnet run
```

## Execucação com Docker

Comandos para execução do projeto .NET utilizando Docker:

```
docker build . -t copet-system-api:dev
docker run --name copet-system-api -p 8080:80 -d copet-system-api:dev
```
