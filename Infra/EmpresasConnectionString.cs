namespace WebApiSmartClinic.Infra;

public class EmpresasConnectionString
{
    private static readonly AsyncLocal<string> _connectionString = new AsyncLocal<string>();

    public static string Current
    {
        get => _connectionString.Value;
        set => _connectionString.Value = value;
    }
}
