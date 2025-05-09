FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["projet/projet.csproj", "projet/"]
RUN dotnet restore "./projet/projet.csproj"
COPY . .

WORKDIR "/src/projet"
RUN dotnet build "./projet.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet tool install --global dotnet-ef --version 8.0.*
ENV PATH="$PATH:/root/.dotnet/tools"
RUN dotnet ef --version  
RUN dotnet publish "./projet.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=publish /root/.dotnet/tools /root/.dotnet/tools 
ENV PATH="$PATH:/root/.dotnet/tools"
ENTRYPOINT ["dotnet", "projet.dll"]
