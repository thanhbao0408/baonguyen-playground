#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Playground.IdentityServer/src/Playground.IdentityServer.Admin.Api/Playground.IdentityServer.Admin.Api.csproj", "Playground.IdentityServer/src/Playground.IdentityServer.Admin.Api/"]
COPY ["Playground.IdentityServer/src/Playground.IdentityServer.Admin.EntityFramework.SqlServer/Playground.IdentityServer.Admin.EntityFramework.SqlServer.csproj", "Playground.IdentityServer/src/Playground.IdentityServer.Admin.EntityFramework.SqlServer/"]
COPY ["Playground.IdentityServer/src/Playground.IdentityServer.Admin.EntityFramework.Shared/Playground.IdentityServer.Admin.EntityFramework.Shared.csproj", "Playground.IdentityServer/src/Playground.IdentityServer.Admin.EntityFramework.Shared/"]
COPY ["Playground.IdentityServer/src/Playground.IdentityServer.Admin.EntityFramework.PostgreSQL/Playground.IdentityServer.Admin.EntityFramework.PostgreSQL.csproj", "Playground.IdentityServer/src/Playground.IdentityServer.Admin.EntityFramework.PostgreSQL/"]
COPY ["Playground.IdentityServer/src/Playground.IdentityServer.Admin.EntityFramework.MySql/Playground.IdentityServer.Admin.EntityFramework.MySql.csproj", "Playground.IdentityServer/src/Playground.IdentityServer.Admin.EntityFramework.MySql/"]
COPY ["Playground.IdentityServer/src/Playground.IdentityServer.Shared/Playground.IdentityServer.Shared.csproj", "Playground.IdentityServer/src/Playground.IdentityServer.Shared/"]
RUN dotnet restore "Playground.IdentityServer/src/Playground.IdentityServer.Admin.Api/Playground.IdentityServer.Admin.Api.csproj"
COPY . .
WORKDIR "/src/Playground.IdentityServer/src/Playground.IdentityServer.Admin.Api"
RUN dotnet build "Playground.IdentityServer.Admin.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Playground.IdentityServer.Admin.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Playground.IdentityServer.Admin.Api.dll"]