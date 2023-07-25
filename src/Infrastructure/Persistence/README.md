# Infraestructure.Persistence

## DataBase - PostgreSQL

Para levantar o banco de dados é necessário executar o comando abaixo na pasta raiz:

```bash
cd docker
docker compose up -d
```

Em seguida, é preciso acessar o pgAdmin através da rota abaixo:

- [PGAdmin](http://localhost:16543/browser)

E criar um servidor utilizando as informações de _host_, _username_, _password_ e _database_ que estão informadas no arquivo docker-compose.yaml utilizado.
Exemplo:

- **host**: copet-system-db
- **username**: copet-admin
- **password**: Copet@123
- **database**: COPET_DB
- **port**: 5432

## Migrations

Criando as Migrations para criação e atualização das tabelas do banco de dados:

```bash
cd src/Infrastructure/WebAPI
dotnet ef migrations add UpdateProjectActivities --project ../Persistence/Persistence.csproj
```

Executando as Migrations:

```bash
dotnet ef database update
```

Removendo as Migrations:

```bash
cd src/Infrastructure/WebAPI
dotnet ef migrations remove --project ../Persistence/Persistence.csproj
```
