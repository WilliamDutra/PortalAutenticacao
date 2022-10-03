FROM mcr.microsoft.com/dotnet/sdk:3.1
WORKDIR /portalautenticacao
COPY . .

RUN dotnet restore ./PortalAutenticacao.UI/PortalAutenticacao.UI.csproj
RUN dotnet publish ./PortalAutenticacao.UI/PortalAutenticacao.UI.csproj -c Release -o out

COPY ./PortalAutenticacao.UI/bin/Release/netcoreapp3.1/publish/ ./portalautenticacao/publish
ENTRYPOINT ["dotnet", "PortalAutenticacao.UI.dll"]