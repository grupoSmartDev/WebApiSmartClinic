
using System.Text.Json.Serialization;

namespace WebApiSmartClinic.Models;

public class CentroCustoModel
{
    public int Id { get; set; }
    public string Tipo { get; set; }
    public string Descricao { get; set; }
    //public List<SubCentroCustoModel> SubCentrosCusto { get; set; } = new List<SubCentroCustoModel>();

    public List<SubCentroCustoModel>? SubCentrosCusto { get; set; } = new List<SubCentroCustoModel>();
    public bool IsSystemDefault { get; internal set; }
}