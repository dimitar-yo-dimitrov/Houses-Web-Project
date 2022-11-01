#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Houses.Web/Houses.Web.csproj", "Houses.Web/"]
COPY ["Houses.Core/Houses.Core.csproj", "Houses.Core/"]
COPY ["Houses.Infrastructure/Houses.Infrastructure.csproj", "Houses.Infrastructure/"]
RUN dotnet restore "Houses.Web/Houses.Web.csproj"
COPY . .
WORKDIR "/src/Houses.Web"
RUN dotnet build "Houses.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Houses.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Houses.Web.dll"]