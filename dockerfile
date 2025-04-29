# Imagen base para build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Instalar Node.js (incluye npm)
RUN curl -fsSL https://deb.nodesource.com/setup_18.x | bash - && \
    apt-get install -y nodejs

# Copiar los .csproj
COPY QualfixAdmin/QualfixAdmin.csproj QualfixAdmin/
COPY QualfixAdmin.dal/QualfixAdmin.dal.csproj QualfixAdmin.dal/
COPY QualfixAdmin.model/QualfixAdmin.model.csproj QualfixAdmin.model/
COPY QualfixAdmin.Services/QualfixAdmin.Services.csproj QualfixAdmin.Services/
COPY QualfixAdmin.DataModel/QualfixAdmin.DataModel.csproj QualfixAdmin.DataModel/
COPY QualfixAdmin.ioc/QualfixAdmin.ioc.csproj QualfixAdmin.ioc/

# Otros .csproj si los tienes...

# Restaurar
RUN dotnet restore QualfixAdmin/QualfixAdmin.csproj

# Copiar el resto del código
COPY . .

# Publicar
WORKDIR /src/QualfixAdmin
RUN dotnet publish -c Release -o /app/publish

