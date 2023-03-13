#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Infrastructure.WebAPI/Infrastructure.WebAPI.csproj", "Infrastructure.WebAPI/"]
COPY ["Infrastructure.IoC.IoC/Infrastructure.IoC.IoC.csproj", "Infrastructure.IoC.IoC/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Infrastructure.Persistence/Infrastructure.Persistence.csproj", "Infrastructure.Persistence/"]
RUN dotnet restore "Infrastructure.WebAPI/Infrastructure.WebAPI.csproj"
COPY . .
WORKDIR "/src/Infrastructure.WebAPI"
RUN dotnet build "Infrastructure.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Infrastructure.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Infrastructure.WebAPI.dll"]
