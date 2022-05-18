#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/ApiGateways/Playground.OcelotApiGw/Playground.OcelotApiGw.csproj", "src/ApiGateways/Playground.OcelotApiGw/"]
COPY ["src/BuildingBlocks/BN.Common/BN.Common.csproj", "src/BuildingBlocks/BN.Common/"]
RUN dotnet restore "src/ApiGateways/Playground.OcelotApiGw/Playground.OcelotApiGw.csproj"
COPY . .
WORKDIR "/src/src/ApiGateways/Playground.OcelotApiGw"
RUN dotnet build "Playground.OcelotApiGw.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Playground.OcelotApiGw.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Playground.OcelotApiGw.dll"]