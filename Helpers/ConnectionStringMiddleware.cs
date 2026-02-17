using Microsoft.AspNetCore.Authorization;
using WebApiSmartClinic.Services.ConnectionsService;

namespace WebApiSmartClinic.Helpers;

public sealed class ConnectionStringMiddleware
{
    private readonly RequestDelegate _next;

    public ConnectionStringMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IConnectionStringProvider connectionStringProvider, IConnectionsService connectionsService)
    {
        var endpoint = context.GetEndpoint();
        if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() is not null)
        {
            await _next(context);
            return;
        }

        var userKeyHeader = context.Request.Headers["UserKey"].FirstOrDefault()?.Trim();

        if (context.Request.Path.StartsWithSegments("/Auth/login", StringComparison.OrdinalIgnoreCase) ||
            context.Request.Path.StartsWithSegments("/Auth/register", StringComparison.OrdinalIgnoreCase))
        {
            if (string.IsNullOrWhiteSpace(userKeyHeader))
            {
                throw new KeyNotFoundException("Chave de conexão é obrigatória");
            }

            var connLogin = await connectionsService.GetConnectionsStringByKeyAsync(userKeyHeader);
            connectionStringProvider.SetConnectionString(connLogin);

            await _next(context);
            return;
        }

        var userKeyClaim = context.User.FindFirst("UserKey")?.Value?.Trim();
        if (string.IsNullOrWhiteSpace(userKeyClaim))
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsync("Acesso negado. Chave de acesso ausente no token.");
            return;
        }

        var userKeySolicitada = string.IsNullOrWhiteSpace(userKeyHeader) ? userKeyClaim : userKeyHeader;

        if (!string.Equals(userKeySolicitada, userKeyClaim, StringComparison.Ordinal) &&
            !PermissionHelper.IsSupportOrAdmin(context.User))
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsync("Acesso negado. Você não tem permissão para acessar este recurso.");
            return;
        }

        var connectionString = await connectionsService.GetConnectionsStringByKeyAsync(userKeySolicitada);
        connectionStringProvider.SetConnectionString(connectionString);

        await _next(context);
    }
}
