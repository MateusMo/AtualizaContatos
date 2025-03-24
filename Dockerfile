# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia tudo do contexto (assegure-se de ter um .dockerignore para filtrar arquivos desnecessários)
COPY . .

# Restaura as dependências utilizando o arquivo de solução
RUN dotnet restore AtualizaContatos.sln

# Publica os projetos desejados
RUN dotnet publish Produtor/Produtor.csproj -c Release -o out/produtor
RUN dotnet publish Consumidor/Consumidor.csproj -c Release -o out/consumidor

# Etapa final
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copia os arquivos publicados para os diretórios finais
COPY --from=build /app/out/produtor ./produtor
COPY --from=build /app/out/consumidor ./consumidor

# Exemplo: expõe a porta utilizada pelo produtor
EXPOSE 8081

# O ENTRYPOINT não é definido aqui, pois será sobrescrito no docker-compose
