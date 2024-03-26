#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["NotificationWorkerService/NotificationWorkerService.csproj", "NotificationWorkerService/"]
COPY ["CommonLib/CommonLib.csproj", "CommonLib/"]
RUN dotnet restore "./NotificationWorkerService/./NotificationWorkerService.csproj"
COPY . .
WORKDIR "/src/NotificationWorkerService"
RUN dotnet build "./NotificationWorkerService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./NotificationWorkerService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NotificationWorkerService.dll"]