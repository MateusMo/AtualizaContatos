# Usa a imagem oficial do SDK do .NET para compilar o projeto
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .

# Restaura e publica ambos os projetos
RUN dotnet restore "Produtor/Produtor.csproj" && \
    dotnet restore "Consumidor/Consumidor.csproj" && \
    dotnet publish "Produtor/Produtor.csproj" -c Release -o /app/publish/produtor && \
    dotnet publish "Consumidor/Consumidor.csproj" -c Release -o /app/publish/consumidor

# Fase final
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish/produtor ./produtor
COPY --from=build /app/publish/consumidor ./consumidor
EXPOSE 8080