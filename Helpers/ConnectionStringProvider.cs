namespace WebApiSmartClinic.Helpers;

public interface IConnectionStringProvider
{
    void SetConnectionString(string connectionString);
    string? GetConnectionString();
}

public sealed class ConnectionStringProvider : IConnectionStringProvider
{
    private static readonly AsyncLocal<string?> ConnectionString = new();

    public void SetConnectionString(string connectionString)
    {
        ConnectionString.Value = connectionString;
    }

    public string? GetConnectionString()
    {
        return ConnectionString.Value;
    }
}
