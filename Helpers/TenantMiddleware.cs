using Microsoft.EntityFrameworkCore;

namespace WebApiSmartClinic.Helpers;

public sealed class TenantMiddleware
{
    private readonly RequestDelegate _next;

    public TenantMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IConnectionStringProvider provider, DataConnectionContext db)
    {
        if (!string.IsNullOrWhiteSpace(provider.GetConnectionString()))
        {
            await _next(context);
            return;
        }

        var tenantKey = context.User?.FindFirst("UserKey")?.Value;

        if (!string.IsNullOrWhiteSpace(tenantKey))
        {
            var conn = await db.DataConnection
                .AsNoTracking()
                .Where(x => x.Key == tenantKey)
                .Select(x => x.StringConnection)
                .FirstOrDefaultAsync();

            if (!string.IsNullOrWhiteSpace(conn))
            {
                provider.SetConnectionString(conn);
            }
        }

        await _next(context);
    }
}
