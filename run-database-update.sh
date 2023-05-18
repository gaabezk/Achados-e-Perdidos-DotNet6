#!/bin/bash
set -e

# Executa as migrações usando o dotnet ef
dotnet ef database update --project Data\Data.csproj --startup-project WebApi\WebApi.csproj --context Data.Context.MySqlContext --configuration Debug 20230518171848_databasev1

# Executa o comando principal da sua aplicação
exec "$@"
