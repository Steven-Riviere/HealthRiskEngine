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
COPY ["PatientsAPI/PatientsAPI.csproj", "PatientsAPI/"]
COPY ["Shared/Shared.csproj", "Shared/"]
RUN dotnet restore "PatientsAPI/PatientsAPI.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "PatientsAPI/PatientsAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
WORKDIR "/src"
RUN dotnet publish "PatientsAPI/PatientsAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PatientsAPI.dll"]