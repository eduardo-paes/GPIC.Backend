# Infrastructure.WebAPI

Restful API created in .NET 7.0.0 to support SPA.

## Execução com Dotnet

Comandos para execução do projeto .NET utilizando CLI:

```bash
cd Infrastructure.WebAPI
dotnet build
dotnet run
```

## Execucação com Docker

Comandos para execução do projeto .NET utilizando Docker:

```bash
docker build . -t copet-system-api:dev
docker run --name copet-system-api -p 8080:80 -d copet-system-api:dev
```
