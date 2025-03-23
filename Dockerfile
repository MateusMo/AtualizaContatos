# Usa a imagem oficial do SDK do .NET para compilar o projeto
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia todos os arquivos do projeto
COPY . .

# Restaura as dependências do projeto específico
RUN dotnet restore "Produtor/Produtor.csproj"

# Publica o projeto
RUN dotnet publish "Produtor/Produtor.csproj" -c Release -o /app/publish

# Usa a imagem do runtime do .NET para rodar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copia os arquivos publicados
COPY --from=build /app/publish .

# Expõe a porta 8080
EXPOSE 8080

# Define o ponto de entrada
ENTRYPOINT ["dotnet", "Produtor.dll"]