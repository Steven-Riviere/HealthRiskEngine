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
COPY ["NotesAPI.csproj", "."]
RUN dotnet restore "NotesAPI.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "NotesAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Cette étape permet de publier le projet de service à copier dans la phase finale
FROM build AS publish
WORKDIR "/src"
RUN dotnet publish "NotesAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Image finale pour exécuter l'app
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# Commande de démarrage du conteneur
ENTRYPOINT ["dotnet", "NotesAPI.dll"]