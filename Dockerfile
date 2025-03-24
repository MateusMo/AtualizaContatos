FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .

# Restaura e publica ambos os projetos
RUN dotnet restore "Produtor/Produtor.csproj"
RUN dotnet restore "Consumidor/Consumidor.csproj"
RUN dotnet publish "Produtor/Produtor.csproj" -c Release -o /app/publish/produtor
RUN dotnet publish "Consumidor/Consumidor.csproj" -c Release -o /app/publish/consumidor

# Fase final
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app/produtor  # Diretório específico para o Produtor
COPY --from=build /app/publish/produtor .
EXPOSE 8080