
using System.Text.Json.Serialization;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Dto.Plano
{
    public class PlanoEdicaoDto
    {
        private decimal _valorPlano;
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int TempoMinutos { get; set; } = 0;
        public int? CentroCustoId { get; set; } = 0; // Relacionamento com a tabela de CentroCusto
        [JsonIgnore] 
        public CentroCustoModel? CentroCusto { get; set; }
        public decimal ValorPlano{ get; set; }
        public bool PlanoBimestral { get; set; }
        public decimal? ValorMesBimestral { get; set; } = 0;
        public decimal? ValorTotalBimestral { get; set; } = 0;
        public decimal? DescontoMesBimestral { get; set; } = 0;

        public bool PlanoTrimestral { get; set; }
        public decimal? ValorMesTrimestral { get; set; } = 0;
        public decimal? ValorTotalTrimestral { get; set; } = 0;
        public decimal? DescontoMesTrimestral { get; set; } = 0;

        public bool PlanoQuadrimestral { get; set; }
        public decimal? ValorMesQuadrimestral { get; set; } = 0;
        public decimal? ValorTotalQuadrimestral { get; set; } = 0;
        public decimal? DescontoMesQuadrimestral { get; set; } = 0;

        public bool PlanoSemestral { get; set; }
        public decimal? ValorMesSemestral { get; set; } = 0;
        public decimal? ValorTotalSemestral { get; set; } = 0;
        public decimal? DescontoMesSemestral { get; set; } = 0;

        public bool PlanoAnual { get; set; }
        public decimal? ValorMesAnual { get; set; } = 0;
        public decimal? ValorTotalAnual { get; set; } = 0;
        public decimal? DescontoMesAnual { get; set; } = 0;
    }
}