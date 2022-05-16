#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/Blog/Blog.WebApi/Blog.WebApi.csproj", "src/Services/Blog/Blog.WebApi/"]
COPY ["src/Services/Blog/Blog.Infrastructure/Blog.Infrastructure.csproj", "src/Services/Blog/Blog.Infrastructure/"]
COPY ["src/Services/Blog/Blog.Infrastructure.EfCore/Blog.Infrastructure.EfCore.csproj", "src/Services/Blog/Blog.Infrastructure.EfCore/"]
COPY ["src/Services/Blog/Blog.Core/Blog.Core.csproj", "src/Services/Blog/Blog.Core/"]
COPY ["src/BN.CleanArchitecture/BN.CleanArchitecture.Core/BN.CleanArchitecture.Core.csproj", "src/BN.CleanArchitecture/BN.CleanArchitecture.Core/"]
COPY ["src/BuildingBlocks/BN.Common/BN.Common.csproj", "src/BuildingBlocks/BN.Common/"]
COPY ["src/BN.CleanArchitecture/BN.CleanArchitecture.Infrastructure.EfCore/BN.CleanArchitecture.Infrastructure.EfCore.csproj", "src/BN.CleanArchitecture/BN.CleanArchitecture.Infrastructure.EfCore/"]
COPY ["src/BN.CleanArchitecture/BN.CleanArchitecture.Infrastructure/BN.CleanArchitecture.Infrastructure.csproj", "src/BN.CleanArchitecture/BN.CleanArchitecture.Infrastructure/"]
RUN dotnet restore "src/Services/Blog/Blog.WebApi/Blog.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Services/Blog/Blog.WebApi"
RUN dotnet build "Blog.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Blog.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Blog.WebApi.dll"]
