# Étape de construction pour .NET
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-dotnet
WORKDIR /app-dotnet

ARG src="./UserApi/."
# Copie des fichiers du projet .NET dans le conteneur
COPY ${src} .

# Construction de l'application .NET
RUN dotnet restore
RUN dotnet publish -c Release -o out
ENV PATH="${PATH}:/root/.dotnet/tools"
RUN dotnet tool install --global dotnet-ef --version 6.0
WORKDIR ./UserApi
RUN dotnet ef database update

# Utilisation de l'image d'exécution .NET Core pour l'exécution de l'application .NET et de Nginx pour servir l'application Angular
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime

WORKDIR /app

# Copie des fichiers publiés de .NET dans l'image d'exécution
COPY --from=build-dotnet /app-dotnet/out .
COPY --from=build-dotnet /app-dotnet/UserApi/bin/Debug/net6.0/users.db .


EXPOSE 80
EXPOSE 7278

# Commande pour démarrer l'application .NET et Nginx
CMD dotnet "UserApi.dll"