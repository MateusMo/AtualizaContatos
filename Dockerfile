# Usa a imagem oficial do SDK do .NET para compilar o projeto
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia os arquivos do projeto
COPY AtualizaContatos.sln . 
COPY Produtor/ Produtor/

# Restaura as dependências
WORKDIR /app/Produtor/AtualizaContatos.Producer.API
RUN dotnet restore

# Publica o projeto
RUN dotnet publish -c Release -o out

# Usa a imagem do runtime do .NET para rodar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copia os arquivos publicados
COPY --from=build /app/Produtor/AtualizaContatos.Producer.API/out . 

# Expõe a porta 8080
EXPOSE 8080

# Define o ponto de entrada correto
ENTRYPOINT ["dotnet", "AtualizaContatos.Producer.API.dll"]
