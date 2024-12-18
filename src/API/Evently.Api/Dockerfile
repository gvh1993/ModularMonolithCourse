FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["API/Evently.Api/Evently.Api.csproj", "API/Evently.Api/"]
COPY ["Common/Evently.Common.Application/Evently.Common.Application.csproj", "Common/Evently.Common.Application/"]
COPY ["Common/Evently.Common.Domain/Evently.Common.Domain.csproj", "Common/Evently.Common.Domain/"]
COPY ["Common/Evently.Common.Infrastructure/Evently.Common.Infrastructure.csproj", "Common/Evently.Common.Infrastructure/"]
COPY ["Common/Evently.Common.Presentation/Evently.Common.Presentation.csproj", "Common/Evently.Common.Presentation/"]
COPY ["Modules/Attendance/Evently.Modules.Attendance.Application/Evently.Modules.Attendance.Application.csproj", "Modules/Attendance/Evently.Modules.Attendance.Application/"]
COPY ["Modules/Attendance/Evently.Modules.Attendance.Domain/Evently.Modules.Attendance.Domain.csproj", "Modules/Attendance/Evently.Modules.Attendance.Domain/"]
COPY ["Modules/Attendance/Evently.Modules.Attendance.Infrastructure/Evently.Modules.Attendance.Infrastructure.csproj", "Modules/Attendance/Evently.Modules.Attendance.Infrastructure/"]
COPY ["Modules/Attendance/Evently.Modules.Attendance.IntegrationEvents/Evently.Modules.Attendance.IntegrationEvents.csproj", "Modules/Attendance/Evently.Modules.Attendance.IntegrationEvents/"]
COPY ["Modules/Attendance/Evently.Modules.Attendance.Presentation/Evently.Modules.Attendance.Presentation.csproj", "Modules/Attendance/Evently.Modules.Attendance.Presentation/"]
COPY ["Modules/Events/Evently.Modules.Events.Application/Evently.Modules.Events.Application.csproj", "Modules/Events/Evently.Modules.Events.Application/"]
COPY ["Modules/Events/Evently.Modules.Events.Domain/Evently.Modules.Events.Domain.csproj", "Modules/Events/Evently.Modules.Events.Domain/"]
COPY ["Modules/Events/Evently.Modules.Events.Infrastructure/Evently.Modules.Events.Infrastructure.csproj", "Modules/Events/Evently.Modules.Events.Infrastructure/"]
COPY ["Modules/Events/Evently.Modules.Events.IntegrationEvents/Evently.Modules.Events.IntegrationEvents.csproj", "Modules/Events/Evently.Modules.Events.IntegrationEvents/"]
COPY ["Modules/Events/Evently.Modules.Events.Presentation/Evently.Modules.Events.Presentation.csproj", "Modules/Events/Evently.Modules.Events.Presentation/"]
COPY ["Modules/Ticketing/Evently.Modules.Ticketing.Application/Evently.Modules.Ticketing.Application.csproj", "Modules/Ticketing/Evently.Modules.Ticketing.Application/"]
COPY ["Modules/Ticketing/Evently.Modules.Ticketing.Domain/Evently.Modules.Ticketing.Domain.csproj", "Modules/Ticketing/Evently.Modules.Ticketing.Domain/"]
COPY ["Modules/Ticketing/Evently.Modules.Ticketing.Infrastructure/Evently.Modules.Ticketing.Infrastructure.csproj", "Modules/Ticketing/Evently.Modules.Ticketing.Infrastructure/"]
COPY ["Modules/Ticketing/Evently.Modules.Ticketing.IntegrationEvents/Evently.Modules.Ticketing.IntegrationEvents.csproj", "Modules/Ticketing/Evently.Modules.Ticketing.IntegrationEvents/"]
COPY ["Modules/Ticketing/Evently.Modules.Ticketing.Presentation/Evently.Modules.Ticketing.Presentation.csproj", "Modules/Ticketing/Evently.Modules.Ticketing.Presentation/"]
COPY ["Modules/Users/Evently.Modules.Users.Application/Evently.Modules.Users.Application.csproj", "Modules/Users/Evently.Modules.Users.Application/"]
COPY ["Modules/Users/Evently.Modules.Users.Domain/Evently.Modules.Users.Domain.csproj", "Modules/Users/Evently.Modules.Users.Domain/"]
COPY ["Modules/Users/Evently.Modules.Users.Infrastructure/Evently.Modules.Users.Infrastructure.csproj", "Modules/Users/Evently.Modules.Users.Infrastructure/"]
COPY ["Modules/Users/Evently.Modules.Users.IntegrationEvents/Evently.Modules.Users.IntegrationEvents.csproj", "Modules/Users/Evently.Modules.Users.IntegrationEvents/"]
COPY ["Modules/Users/Evently.Modules.Users.Presentation/Evently.Modules.Users.Presentation.csproj", "Modules/Users/Evently.Modules.Users.Presentation/"]
RUN dotnet restore "./API/Evently.Api/Evently.Api.csproj"
COPY . .
WORKDIR "/src/API/Evently.Api"
RUN dotnet build "./Evently.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Evently.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Evently.Api.dll"]