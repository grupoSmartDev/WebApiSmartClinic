namespace WebApiSmartClinic.Helpers;

public interface IConnectionStringProvider
{
    string GetConnectionString();
    void SetConnectionString(string connectionString);
}

public class ConnectionStringProvider : IConnectionStringProvider
{
    private string _connectionString;

    public string GetConnectionString()
    {
        return _connectionString;
    }

    public void SetConnectionString(string connectionString)
    {
        _connectionString = connectionString;
    }
}

