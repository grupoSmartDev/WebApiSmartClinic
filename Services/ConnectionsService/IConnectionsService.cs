namespace WebApiSmartClinic.Services.ConnectionsService;

public interface IConnectionsService
{
    Task<string> GetConnectionsStringByKeyAsync(string key);
}

public class ConnectionsService : IConnectionsService
{
    private readonly IConnectionsRepository _connectionsRepository;

    public ConnectionsService(IConnectionsRepository connectionsRepository)
    {
        _connectionsRepository = connectionsRepository;
    }

    public async Task<string> GetConnectionsStringByKeyAsync(string key)
    {
        // Aqui voc� deve buscar no seu DbContext pela chave fornecida
        var Connections = await _connectionsRepository.GetConnectionsStringByKeyAsync(key);

        if (Connections == null)
        {
            throw new KeyNotFoundException("Chave de conexão não encontrada");
        }

        return Connections.StringConnection;
    }
}
