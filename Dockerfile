# Dockerfile corrigido
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .

# Restaura e publica ambos os projetos SEPARADAMENTE
RUN dotnet restore "Produtor/Produtor.csproj" && \
    dotnet publish "Produtor/Produtor.csproj" -c Release -o /app/publish/produtor

RUN dotnet restore "Consumidor/Consumidor.csproj" && \
    dotnet publish "Consumidor/Consumidor.csproj" -c Release -o /app/publish/consumidor

# Fase final
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish/produtor ./produtor
COPY --from=build /app/publish/consumidor ./consumidor