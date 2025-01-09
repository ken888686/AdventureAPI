FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/AdventureAPI.Web/AdventureAPI.Web.csproj", "src/AdventureAPI.Web/"]
RUN dotnet restore "src/AdventureAPI.Web/AdventureAPI.Web.csproj"
COPY . .
WORKDIR "/src/src/AdventureAPI.Web"
RUN dotnet build "AdventureAPI.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "AdventureAPI.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AdventureAPI.Web.dll"]
