# Usa una imagen base de .NET SDK para construir la app
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copia y restaura los paquetes NuGet
COPY . .
RUN dotnet restore

# Publica la aplicación en modo Release
RUN dotnet publish -c Release -o /app/publish

# Usa una imagen base de .NET Runtime para ejecutar la app
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "IESv1API.dll"]
