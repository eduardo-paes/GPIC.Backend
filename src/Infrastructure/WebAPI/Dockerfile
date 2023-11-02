#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Infrastructure/Services/Services.csproj", "Infrastructure/Services/"]
COPY ["Infrastructure/Persistence/Persistence.csproj", "Infrastructure/Persistence/"]
COPY ["Infrastructure/IoC/IoC.csproj", "Infrastructure/IoC/"]
COPY ["Infrastructure/WebAPI/WebAPI.csproj", "Infrastructure/WebAPI/"]
RUN dotnet restore "Infrastructure/WebAPI/WebAPI.csproj"
COPY . .
WORKDIR "/src/Infrastructure/WebAPI"
RUN dotnet build "WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebAPI.dll"]
