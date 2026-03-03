# Image de base pour exécuter l'application ASP.NET
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Cette phase est utilisée pour générer le projet de service
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
# Copie le fichier projet pour restaurer les dépendances
COPY ["Frontend/Frontend.csproj", "Frontend/"]
COPY ["Shared/Shared.csproj", "Shared/"]
RUN dotnet restore "Frontend/Frontend.csproj"
# Copie le reste du code source
COPY . .
WORKDIR "/src"
# Compile le projet
RUN dotnet build "Frontend/Frontend.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Cette étape permet de publier le projet de service à copier dans la phase finale
FROM build AS publish
WORKDIR "/src"
RUN dotnet publish "Frontend/Frontend.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Image finale pour exécuter l'app
FROM base AS final
WORKDIR /app
# Copie les fichiers publiés depuis l'étape précédente
COPY --from=publish /app/publish .
# Commande de démarrage du conteneur
ENTRYPOINT ["dotnet", "Frontend.dll"]