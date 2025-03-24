# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia todos os arquivos necessários (certifique-se de ter um .dockerignore para filtrar o que não for necessário)
COPY . .

# Restaura as dependências usando o arquivo de solução
RUN dotnet restore AtualizaContatos.sln

# Publica os projetos desejados
RUN dotnet publish Produtor/Produtor.csproj -c Release -o /src/out/produtor
RUN dotnet publish Consumidor/Consumidor.csproj -c Release -o /src/out/consumidor

# Etapa final
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copia os arquivos publicados para os diretórios finais
COPY --from=build /src/out/produtor ./produtor
COPY --from=build /src/out/consumidor ./consumidor

# Exemplo: expõe a porta utilizada pelo Produtor (API)
EXPOSE 8081

# O ENTRYPOINT será definido no docker-compose
