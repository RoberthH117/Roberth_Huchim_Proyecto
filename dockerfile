# Imagen base para build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar todos los .csproj para restaurar dependencias
COPY QualfixAdmin/QualfixAdmin.csproj QualfixAdmin/
COPY QualfixAdmin.dal/QualfixAdmin.dal.csproj QualfixAdmin.dal/
COPY QualfixAdmin.model/QualfixAdmin.model.csproj QualfixAdmin.model/
COPY QualfixAdmin.Services/QualfixAdmin.Services.csproj QualfixAdmin.Services/
# (Agrega más COPY si tienes más proyectos .csproj)

# Restaurar dependencias
RUN dotnet restore QualfixAdmin/QualfixAdmin.csproj

# Copiar todo el código fuente
COPY . .

# Publicar (compilar en modo Release)
WORKDIR /src/QualfixAdmin
RUN dotnet publish -c Release -o /app/publish

# Imagen final (runtime)
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# Indicar el puerto que usa la aplicación (opcional, pero recomendable)
EXPOSE 5000

# Comando para correr la app
ENTRYPOINT ["dotnet", "QualfixAdmin.dll"]
