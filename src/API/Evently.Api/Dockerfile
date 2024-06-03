FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Build.props", "."]
COPY ["src/API/Evently.Api/Evently.Api.csproj", "src/API/Evently.Api/"]
COPY ["src/Modules/Events/Evently.Modules.Events.Infrastructure/Evently.Modules.Events.Infrastructure.csproj", "src/Modules/Events/Evently.Modules.Events.Infrastructure/"]
COPY ["src/Modules/Events/Evently.Modules.Events.Application/Evently.Modules.Events.Application.csproj", "src/Modules/Events/Evently.Modules.Events.Application/"]
COPY ["src/Modules/Events/Evently.Modules.Events.Domain/Evently.Modules.Events.Domain.csproj", "src/Modules/Events/Evently.Modules.Events.Domain/"]
COPY ["src/Modules/Events/Evently.Modules.Events.Presentation/Evently.Modules.Events.Presentation.csproj", "src/Modules/Events/Evently.Modules.Events.Presentation/"]
RUN dotnet restore "./src/API/Evently.Api/Evently.Api.csproj"
COPY . .
WORKDIR "/src/src/API/Evently.Api"
RUN dotnet build "./Evently.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Evently.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Evently.Api.dll"]