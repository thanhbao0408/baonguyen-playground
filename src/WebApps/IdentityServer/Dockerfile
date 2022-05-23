#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/WebApps/IdentityServer/IdentityServer.csproj", "src/WebApps/IdentityServer/"]
COPY ["src/BuildingBlocks/Contracts/Playground.AppContracts/Playground.AppContracts.csproj", "src/BuildingBlocks/Contracts/Playground.AppContracts/"]
COPY ["src/BN.CleanArchitecture/BN.CleanArchitecture.Infrastructure/BN.CleanArchitecture.Infrastructure.csproj", "src/BN.CleanArchitecture/BN.CleanArchitecture.Infrastructure/"]
COPY ["src/BN.CleanArchitecture/BN.CleanArchitecture.Core/BN.CleanArchitecture.Core.csproj", "src/BN.CleanArchitecture/BN.CleanArchitecture.Core/"]
RUN dotnet restore "src/WebApps/IdentityServer/IdentityServer.csproj"
COPY . .
WORKDIR "/src/src/WebApps/IdentityServer"
RUN dotnet build "IdentityServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IdentityServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IdentityServer.dll"]