# WebApiSmartClinic

API ASP.NET Core 8 com PostgreSQL e Entity Framework Core.

## Desenvolvimento local

Pré-requisitos:

- .NET SDK 8
- PostgreSQL 16 (ou compatível)
- VS Code com C# Dev Kit, Rider ou Visual Studio
- `dotnet-ef` 8

O ambiente local usa dois bancos:

- `smartclinic_connections`: catálogo que relaciona a chave do tenant à conexão
- `smartclinic_dev`: dados e Identity do tenant local

As configurações sensíveis devem ficar em [.NET User Secrets](https://learn.microsoft.com/aspnet/core/security/app-secrets), nunca no `appsettings.json`. As chaves de tenant configuradas para desenvolvimento são `local` (chamadas diretas) e `00000000000` (frontend Angular).

Para executar:

```bash
dotnet restore
dotnet run --launch-profile https
```

Swagger: `https://localhost:7036/swagger`

Todas as chamadas de login e cadastro precisam do header:

```text
UserKey: local
```

No VS Code, abra a pasta do repositório, coloque breakpoints e pressione `F5`; o perfil `WebApiSmartClinic (HTTPS)` compila, inicia a API e abre o Swagger.
 
