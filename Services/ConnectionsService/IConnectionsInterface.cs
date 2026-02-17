using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using WebApiSmartClinic.Helpers;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.ConnectionsService;

public interface IConnectionsRepository
{
    Task<DataConnections?> GetConnectionsStringByKeyAsync(string key);
}

public sealed class ConnectionsRepository : IConnectionsRepository
{
    private const string CachePrefix = "tenant-connection:";
    private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(10);

    private readonly DataConnectionContext _connectionContext;
    private readonly IMemoryCache _cache;

    public ConnectionsRepository(DataConnectionContext context, IMemoryCache cache)
    {
        _connectionContext = context;
        _cache = cache;
    }

    public async Task<DataConnections?> GetConnectionsStringByKeyAsync(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            return null;
        }

        var normalizedKey = key.Trim();
        var cacheKey = $"{CachePrefix}{normalizedKey}";

        if (_cache.TryGetValue<DataConnections?>(cacheKey, out var cachedConnection))
        {
            return cachedConnection;
        }

        var connection = await _connectionContext.DataConnection
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Key == normalizedKey);

        if (connection is null)
        {
            return null;
        }

        _cache.Set(cacheKey, connection, CacheDuration);
        return connection;
    }
}
