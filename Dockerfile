#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CopetSystem.API/CopetSystem.API.csproj", "CopetSystem.API/"]
COPY ["CopetSystem.Infra.IoC/CopetSystem.Infra.IoC.csproj", "CopetSystem.Infra.IoC/"]
COPY ["CopetSystem.Application/CopetSystem.Application.csproj", "CopetSystem.Application/"]
COPY ["CopetSystem.Domain/CopetSystem.Domain.csproj", "CopetSystem.Domain/"]
COPY ["CopetSystem.Infra.Data/CopetSystem.Infra.Data.csproj", "CopetSystem.Infra.Data/"]
RUN dotnet restore "CopetSystem.API/CopetSystem.API.csproj"
COPY . .
WORKDIR "/src/CopetSystem.API"
RUN dotnet build "CopetSystem.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CopetSystem.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CopetSystem.API.dll"]
