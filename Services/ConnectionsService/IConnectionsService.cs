namespace WebApiSmartClinic.Services.ConnectionsService;

public interface IConnectionsService
{
    Task<string> GetConnectionsStringByKeyAsync(string key);
}

public sealed class ConnectionsService : IConnectionsService
{
    private readonly IConnectionsRepository _connectionsRepository;

    public ConnectionsService(IConnectionsRepository connectionsRepository)
    {
        _connectionsRepository = connectionsRepository;
    }

    public async Task<string> GetConnectionsStringByKeyAsync(string key)
    {
        var connection = await _connectionsRepository.GetConnectionsStringByKeyAsync(key);

        if (connection is null)
        {
            throw new KeyNotFoundException("Chave de conexão não encontrada");
        }

        return connection.StringConnection;
    }
}
