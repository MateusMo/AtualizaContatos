# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .

# Restaura e publica separadamente com caminhos absolutos
RUN dotnet restore "Produtor/Produtor.csproj" && \
    dotnet publish "Produtor/Produtor.csproj" -c Release -o /app/publish/produtor

RUN dotnet restore "Consumidor/Consumidor.csproj" && \
    dotnet publish "Consumidor/Consumidor.csproj" -c Release -o /app/publish/consumidor

# Etapa final
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copia mantendo estrutura de diretórios
COPY --from=build /app/publish/produtor ./produtor
COPY --from=build /app/publish/consumidor ./consumidor