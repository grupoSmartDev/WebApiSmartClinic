namespace WebApiSmartClinic.Models;

public class EmpresaModel
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string DatabaseConnectionString { get; set; }
    public ICollection<UsuarioModel> Usuarios { get; set; }
}
