#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/web/NSE.WebApi.MVC/NSE.WebApi.MVC.csproj", "src/web/NSE.WebApi.MVC/"]
RUN dotnet restore "./src/web/NSE.WebApi.MVC/NSE.WebApi.MVC.csproj"
COPY . .
WORKDIR "/src/src/web/NSE.WebApi.MVC"
RUN dotnet build "./NSE.WebApi.MVC.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./NSE.WebApi.MVC.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NSE.WebApi.MVC.dll"]