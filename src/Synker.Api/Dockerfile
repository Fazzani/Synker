FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["src/Synker.Api/Synker.Api.csproj", "Synker.Api/"]
RUN dotnet restore "Synker.Api/Synker.Api.csproj"
COPY ./src .
WORKDIR "/src/Synker.Api"
RUN dotnet build "Synker.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Synker.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Synker.Api.dll"]