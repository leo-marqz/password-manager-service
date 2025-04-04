FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiamos solo el .csproj primero
COPY src/PasswordGeneratorService/PasswordGeneratorService.API/PasswordGeneratorService.API.csproj PasswordGeneratorService.API/
RUN dotnet restore PasswordGeneratorService.API/PasswordGeneratorService.API.csproj

# Ahora copiamos el resto del código
COPY src/PasswordGeneratorService/ ./
WORKDIR /src/PasswordGeneratorService.API
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PasswordGeneratorService.API.dll"]

