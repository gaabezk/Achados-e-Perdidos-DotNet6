FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY ["WebApi/WebApi.csproj", "WebApi/"]
COPY ["IoC/IoC.csproj", "IoC/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Data/Data.csproj", "Data/"]
RUN dotnet restore "WebApi/WebApi.csproj"
COPY . .
WORKDIR "/src/WebApi"
RUN dotnet build "WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Adicione os comandos para as migrações
RUN dotnet tool install --global dotnet-ef --version 7.0.0
COPY run-database-update.sh /app/run-database-update.sh
RUN chmod +x /app/run-database-update.sh

# Defina o script de entrada como o comando de execução
ENTRYPOINT ["/app/run-database-update.sh", "dotnet", "WebApi.dll"]
