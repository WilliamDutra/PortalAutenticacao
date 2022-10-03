FROM mcr.microsoft.com/dotnet/sdk:3.1
WORKDIR /portalautenticacao
COPY . .

ENV ASPNETCORE_ENVIRONMENT=Development
ENV DOTNET_USE_POLLING_FILE_WATCHER=true  
ENV ASPNETCORE_URLS=http://+:80  

RUN dotnet restore ./PortalAutenticacao.UI/PortalAutenticacao.UI.csproj
RUN dotnet publish ./PortalAutenticacao.UI/PortalAutenticacao.UI.csproj -c Release -o out

COPY ./PortalAutenticacao.UI/bin/Release/netcoreapp3.1/publish/ .
ENTRYPOINT ["dotnet", "PortalAutenticacao.UI.dll"]

EXPOSE 80