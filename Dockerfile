# Dockerfile ÚNICO para Produtor e Consumidor
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .

# Restaura e publica AMBOS os projetos
RUN dotnet restore "Produtor/Produtor.csproj"
RUN dotnet restore "Consumidor/Consumidor.csproj"
RUN dotnet publish "Produtor/Produtor.csproj" -c Release -o /app/publish/produtor
RUN dotnet publish "Consumidor/Consumidor.csproj" -c Release -o /app/publish/consumidor

# Fase final
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish/produtor ./produtor
COPY --from=build /app/publish/consumidor ./consumidor
EXPOSE 8080