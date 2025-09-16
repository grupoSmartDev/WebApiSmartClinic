using System.Text.Json.Serialization;

namespace WebApiSmartClinic.Models;

public class SubCentroCustoModel    {
    public int Id { get; set; }
    public string Nome { get; set; }

    // Relacionamento com o Centro de Custo Pai
    public int CentroCustoId { get; set; }
    
    [JsonIgnore]
    public CentroCustoModel CentroCusto { get; set; }
}

