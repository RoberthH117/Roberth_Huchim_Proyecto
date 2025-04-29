# Imagen base para build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /QualfixAdmin

# Instalar Node.js y Angular CLI
RUN curl -fsSL https://deb.nodesource.com/setup_18.x | bash - && \
    apt-get install -y nodejs
RUN npm install -g @angular/cli

# Copiar los .csproj
COPY QualfixAdmin/QualfixAdmin.csproj QualfixAdmin/
COPY QualfixAdmin.dal/QualfixAdmin.dal.csproj QualfixAdmin.dal/
COPY QualfixAdmin.model/QualfixAdmin.model.csproj QualfixAdmin.model/
COPY QualfixAdmin.Services/QualfixAdmin.Services.csproj QualfixAdmin.Services/
COPY QualfixAdmin.DataModel/QualfixAdmin.DataModel.csproj QualfixAdmin.DataModel/
COPY QualfixAdmin.ioc/QualfixAdmin.ioc.csproj QualfixAdmin.ioc/

# Restaurar dependencias
RUN dotnet restore QualfixAdmin/QualfixAdmin.csproj

# Copiar todo el código fuente
COPY . .

# 👉 Ir a la carpeta que SÍ contiene package.json
WORKDIR /QualfixAdmin/QualfixAdmin/ClientApp
RUN npm install @popperjs/core --legacy-peer-deps
RUN npm install --legacy-peer-deps

# 👉 Volver a la carpeta donde está el .csproj del backend
WORKDIR /QualfixAdmin/QualfixAdmin
RUN dotnet publish -c Release -o /app/publish

# 🔥 Nueva imagen final para producción
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Si Render necesita el puerto dinámico
ENV ASPNETCORE_URLS=http://+:10000

# Arrancar la app
ENTRYPOINT ["dotnet", "QualfixAdmin.dll"]
