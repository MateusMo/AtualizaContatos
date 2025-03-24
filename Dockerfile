# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia o arquivo de solução e os diretórios dos projetos
COPY AtualizaContatos.sln .
COPY Produtor/ Produtor/
COPY Consumidor/ Consumidor/

# Restaura as dependências de todos os projetos da solução
RUN dotnet restore

# Publica os projetos desejados
RUN dotnet publish Produtor/Produtor.csproj -c Release -o out/produtor
RUN dotnet publish Consumidor/Consumidor.csproj -c Release -o out/consumidor

# Etapa final
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copia os arquivos publicados para os diretórios finais
COPY --from=build /app/out/produtor ./produtor
COPY --from=build /app/out/consumidor ./consumidor

# Exemplo: expõe a porta do produtor (caso a aplicação utilize)
EXPOSE 8081

# O ENTRYPOINT não é definido aqui, pois será sobrescrito no docker-compose
