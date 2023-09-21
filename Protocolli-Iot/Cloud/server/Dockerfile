#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["server/Cloud.csproj", "server/"]
COPY ["AMQP/AMQP.csproj", "AMQP/"]
COPY ["ApplicationCore/ApplicationCore.csproj", "ApplicationCore/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Mqtt/Mqtt.csproj", "Mqtt/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
RUN dotnet restore "server/Cloud.csproj"
COPY . .
WORKDIR "/src/server"
RUN dotnet build "Cloud.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Cloud.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cloud.dll"]