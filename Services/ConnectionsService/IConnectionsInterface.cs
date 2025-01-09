using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Helpers;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.ConnectionsService;

public interface IConnectionsRepository
{
    Task<DataConnections> GetConnectionsStringByKeyAsync(string key);
}

public class ConnectionsRepository : IConnectionsRepository
{
    private readonly DataConnectionContext _connectionContext;

    public ConnectionsRepository(DataConnectionContext context)
    {
        _connectionContext = context;
    }

    public async Task<DataConnections> GetConnectionsStringByKeyAsync(string key)
    {
        return await _connectionContext.DataConnection.FirstOrDefaultAsync(c => c.Key == key);
    }
}
