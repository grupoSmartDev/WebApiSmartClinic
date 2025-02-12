
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Dto.CentroCusto;

public class CentroCustoCreateDto
{
    public string Tipo { get; set; }
    public string Descricao { get; set; }
    public List<SubCentroCustoModel>? subCentroCusto { get; set; }
}