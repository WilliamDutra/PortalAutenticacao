#ESTAGIO 1
FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

#ESTAGIO 2 - ( COPIA OS ARQUIVOS DA SOLUÇÃO PARA A PASTA SRC,  MANTENDO A MESMA ESTRUTURA DE PASTAS )
FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["PortalAutenticacao.UI/PortalAutenticacao.UI.csproj", "PortalAutenticacao.UI/"]
COPY ["PortalAutenticacao.Entities/PortalAutenticacao.Entities.csproj", "PortalAutenticacao.Entities/"]
COPY ["PortalAutenticacao.Domain/PortalAutenticacao.Domain.csproj", "PortalAutenticacao.Domain.csproj"]
RUN dotnet restore "PortalAutenticacao.UI/PortalAutenticacao.UI.csproj"

#ESTAGIO 3 - ( BUILDA O CSPROJ )
COPY . .
WORKDIR "/src/PortalAutenticacao.UI"
RUN dotnet build "PortalAutenticacao.UI.csproj" -c Release -o /app/build

#ESTAGIO 4 - ( PUBLICA O PROJETO )
FROM build AS publish
RUN dotnet publish "PortalAutenticacao.UI.csproj" -c Release -o /app/publish

#ESTAGIO 5 ( EXECUTA )
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PortalAutenticacao.UI.dll"]
