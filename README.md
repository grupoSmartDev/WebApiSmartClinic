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

## Recuperação de senha

O fluxo público usa `POST /Auth/solicitar-recuperacao-senha` e `POST /Auth/redefinir-senha`, ambos com o header `UserKey`. O token é gerado pelo ASP.NET Identity, expira em 30 minutos e é invalidado após o primeiro uso. A solicitação sempre retorna uma mensagem genérica para não revelar se uma conta existe.

Configurações:

- `AppSettings:UrlFrontendRecuperacaoSenha`: endereço do Angular usado no link enviado por e-mail;
- `AppSettings:MinutosValidadeTokenRecuperacaoSenha`: validade do token, entre 5 e 1440 minutos;
- `AppSettings:CaminhoChavesProtecaoDados`: diretório persistente das chaves que assinam os tokens.

No Docker Compose, as chaves de proteção ficam no volume nomeado `chaves-protecao-dados-smartclinic`. Preserve esse volume durante atualizações da VPS; removê-lo invalida links de recuperação ainda não utilizados. Em uma hospedagem sem Docker, configure a variável `AppSettings__CaminhoChavesProtecaoDados` com um diretório persistente e protegido contra leitura por outros usuários.
 
