using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Helpers;

public class TenantMiddleware
{
    private readonly RequestDelegate _next;

    public TenantMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IConnectionStringProvider provider, DataConnectionContext db)
    {
        var tenantKey = context.User?.FindFirst("UserKey")?.Value;

        if (!string.IsNullOrWhiteSpace(tenantKey))
        {
            var conn = await db.DataConnection
                .Where(x => x.Key == tenantKey)
                .Select(x => x.StringConnection)
                .FirstOrDefaultAsync();

            if (!string.IsNullOrWhiteSpace(conn))
                provider.SetConnectionString(conn);
        }

        await _next(context);
    }
}
