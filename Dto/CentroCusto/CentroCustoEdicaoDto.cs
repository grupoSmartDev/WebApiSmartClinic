using WebApiSmartClinic.Dto.SubCentroCusto;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Dto.CentroCusto
{
    public sealed class CentroCustoEdicaoDto : CentroCustoModel
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public string Descricao { get; set; }
        //public List<SubCentroCustoModel> SubCentrosCusto { get; set; } = new List<SubCentroCustoModel>();

        public List<SubCentroCustoEdicaoDto>? SubCentrosCusto { get; set; } = new List<SubCentroCustoEdicaoDto>();
    }
}