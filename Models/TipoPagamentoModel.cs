
namespace WebApiSmartClinic.Models;

public class TipoPagamentoModel
{
    public int Id { get; set; }
    public string? Descricao { get; set; }
    public bool IsSystemDefault { get; internal set; }
}