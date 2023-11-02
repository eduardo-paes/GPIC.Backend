#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

# FROM mcr.microsoft.com/azure-functions/dotnet-isolated:4-dotnet-isolated7.0 AS base
FROM mohsinonxrm/azure-functions-dotnet:4-isolated7.0-arm64v8 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/runtime:7.0 as runtime7.0
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

COPY --from=runtime7.0 /usr/share/dotnet/host /usr/share/dotnet/host
COPY --from=runtime7.0 /usr/share/dotnet/shared /usr/share/dotnet/shared

ENV AzureWebJobsStorage="UseDevelopmentStorage=true"
ENV AzureFunctionsJobHost__Logging__Console__IsEnabled=true

WORKDIR /src
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Infrastructure/Services/Services.csproj", "Infrastructure/Services/"]
COPY ["Infrastructure/Persistence/Persistence.csproj", "Infrastructure/Persistence/"]
COPY ["Infrastructure/IoC/IoC.csproj", "Infrastructure/IoC/"]
COPY ["Infrastructure/WebFunctions/WebFunctions.csproj", "Infrastructure/WebFunctions/"]
RUN dotnet restore "Infrastructure/WebFunctions/WebFunctions.csproj"
COPY . .
WORKDIR "/src/Infrastructure/WebFunctions"
RUN dotnet build "WebFunctions.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebFunctions.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .