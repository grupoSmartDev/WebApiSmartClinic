namespace WebApiSmartClinic.Helpers;

using Microsoft.AspNetCore.Authorization;
using SixLabors.ImageSharp;
using System.Runtime.Intrinsics.X86;
using WebApiSmartClinic.Services.ConnectionsService;

public class ConnectionStringMiddleware
{
    private readonly RequestDelegate _next;

    public ConnectionStringMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IConnectionStringProvider connectionStringProvider, IConnectionsService connectionsService)
    {

        // pulo middleware para endpoints [AllowAnonymous]
        var endpoint = context.GetEndpoint();
        if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() is not null)
        {
            await _next(context);
            return;
        }

        string userKey = string.Empty;

        //ele esta verificando primeiro se é request.origens e ai nao pega o header 
        // Tentar acessar o cabeçalho 'UserKey' de forma segura
        if (context.Request.Headers.TryGetValue("UserKey", out var headerUserKey))
        {
            userKey = headerUserKey.FirstOrDefault();  // Obtém o primeiro valor, caso haja múltiplos valores
        }

        if (context.Request.Path.StartsWithSegments("/Auth/login") || context.Request.Path.StartsWithSegments("/Auth/register"))
        {
            //userKey = "SmartClinic";
            if (string.IsNullOrEmpty(userKey))
            {
                throw new KeyNotFoundException("Chave de conexão é obrigatória");
            }

            var connectionString = await connectionsService.GetConnectionsStringByKeyAsync(userKey);
            connectionStringProvider.SetConnectionString(connectionString);
        }
        else
        {
            // Recuperar a UserKey das claims do token
            var userKeyClaim = context.User.FindFirst("UserKey")?.Value;

            if (string.IsNullOrEmpty(userKeyClaim))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Acesso negado. Chave de acesso ausente no token.");
                return;
            }

            // se a key do token for diferente da key do header
            if (userKeyClaim != userKey)
            {

                // Verifica se o usuário é Admin ou Support para permitir o acesso a qualquer contexto
                var isUserAllowedToSwitchContext = context.User.IsInRole("Admin") || context.User.IsInRole("Support");

                if (isUserAllowedToSwitchContext)
                {
                    // Se o usuário tiver permissão para mudar de contexto, configura a conexão
                    var connectionString = await connectionsService.GetConnectionsStringByKeyAsync(userKey!);
                    connectionStringProvider.SetConnectionString(connectionString);
                }
                else
                {
                    await connectionsService.GetConnectionsStringByKeyAsync(userKey!);
                    //caso o usuário não tenha permissão administrativa ou support
                    //e tenha tentado acessar uma key que não é igual ao do token dele é retornado 403 direto.
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsync("Acesso negado. Você não tem permissão para acessar este recurso.");
                    return;
                }
            }
            else
            {
                // Para usuários comuns, verifique se a UserKey do token corresponde ao contexto autorizado.
                var connectionString = await connectionsService.GetConnectionsStringByKeyAsync(userKeyClaim);
                // Configura a string de conexão do DbContext para a encontrada, permitindo acesso ao contexto autorizado.
                connectionStringProvider.SetConnectionString(connectionString);
            }


        }


        await _next(context);
    }
}

