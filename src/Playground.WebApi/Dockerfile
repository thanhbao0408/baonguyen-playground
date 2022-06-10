#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Playground.WebApi/Playground.WebApi.csproj", "src/Playground.WebApi/"]
COPY ["src/Playground.Application/Playground.Application.csproj", "src/Playground.Application/"]
COPY ["src/Playground.Core/Playground.Core.csproj", "src/Playground.Core/"]
COPY ["BN.CleanArchitecture/BN.CleanArchitecture.Core/BN.CleanArchitecture.Core.csproj", "BN.CleanArchitecture/BN.CleanArchitecture.Core/"]
COPY ["src/Playground.Application.Contracts/Playground.Application.Contracts.csproj", "src/Playground.Application.Contracts/"]
COPY ["BN.CleanArchitecture/BN.CleanArchitecture.Contracts/BN.CleanArchitecture.Contracts.csproj", "BN.CleanArchitecture/BN.CleanArchitecture.Contracts/"]
COPY ["src/Playground.Infrastructure/Playground.Infrastructure.csproj", "src/Playground.Infrastructure/"]
COPY ["BN.CleanArchitecture/BN.CleanArchitecture.Infrastructure/BN.CleanArchitecture.Infrastructure.csproj", "BN.CleanArchitecture/BN.CleanArchitecture.Infrastructure/"]
COPY ["src/Playground.Infrastructure.EfCore/Playground.Infrastructure.EfCore.csproj", "src/Playground.Infrastructure.EfCore/"]
COPY ["BN.CleanArchitecture/BN.CleanArchitecture.Infrastructure.EfCore/BN.CleanArchitecture.Infrastructure.EfCore.csproj", "BN.CleanArchitecture/BN.CleanArchitecture.Infrastructure.EfCore/"]
RUN dotnet restore "src/Playground.WebApi/Playground.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Playground.WebApi"
RUN dotnet build "Playground.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Playground.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Playground.WebApi.dll"]
