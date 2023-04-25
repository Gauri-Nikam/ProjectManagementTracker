#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/MicroServices/TeamMember/TeamMember.API/TeamMember.API.csproj", "src/MicroServices/TeamMember/TeamMember.API/"]
COPY ["src/BuildingBlocks/EventBus.Messages/EventBus.Messages.csproj", "src/BuildingBlocks/EventBus.Messages/"]
RUN dotnet restore "src/MicroServices/TeamMember/TeamMember.API/TeamMember.API.csproj"
COPY . .
WORKDIR "/src/src/MicroServices/TeamMember/TeamMember.API"
RUN dotnet build "TeamMember.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TeamMember.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TeamMember.API.dll"]
