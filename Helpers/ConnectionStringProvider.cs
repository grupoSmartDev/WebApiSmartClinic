using Microsoft.EntityFrameworkCore;

namespace WebApiSmartClinic.Helpers;

// IConnectionStringProvider.cs
public interface IConnectionStringProvider
{
    void SetConnectionString(string connectionString);
    string? GetConnectionString();
}

// ConnectionStringProvider.cs
public class ConnectionStringProvider : IConnectionStringProvider
{
    private static readonly AsyncLocal<string?> _connectionString = new AsyncLocal<string?>();

    public void SetConnectionString(string connectionString)
    {
        _connectionString.Value = connectionString;
    }

    public string? GetConnectionString()
    {
        return _connectionString.Value;
    }
}

// TenantMiddleware.cs
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
            {
                provider.SetConnectionString(conn);
            }
        }

        await _next(context);
    }
}

//public interface IConnectionStringProvider
//{
//    string GetConnectionString();
//    void SetConnectionString(string connectionString);
//}

//public class ConnectionStringProvider : IConnectionStringProvider
//{
//    private string _connectionString;

//    public string GetConnectionString()
//    {
//        return _connectionString;
//    }

//    public void SetConnectionString(string connectionString)
//    {
//        _connectionString = connectionString;
//    }
//}

