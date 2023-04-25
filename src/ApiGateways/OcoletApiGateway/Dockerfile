#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/ApiGateways/OcoletApiGateway/OcoletApiGateway.csproj", "src/ApiGateways/OcoletApiGateway/"]
RUN dotnet restore "src/ApiGateways/OcoletApiGateway/OcoletApiGateway.csproj"
COPY . .
WORKDIR "/src/src/ApiGateways/OcoletApiGateway"
RUN dotnet build "OcoletApiGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OcoletApiGateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OcoletApiGateway.dll"]
