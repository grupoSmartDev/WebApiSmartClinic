
using System.Text.Json.Serialization;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Dto.Plano
{
    public class PlanoCreateDto
    {
        private decimal _valorPlano;
        public string Descricao { get; set; }
        public int TempoMinutos { get; set; } = 0;
        [JsonIgnore] public CentroCustoModel CentroCusto { get; set; }
        public int? CentroCustoId { get; set; } = 0; // Relacionamento com a tabela de CentroCusto
        public ICollection<TipoCobranca> TipoCobranca { get; set; }
        public bool PlanoGratuito { get; set; }
        public decimal ValorPlano
        {
            get => PlanoGratuito ? 0 : _valorPlano; // Retorna 0 se for gratuito
            set
            {
                if (!PlanoGratuito)
                {
                    _valorPlano = value; // Permite definir valor apenas se não for gratuito
                }
            }
        }

        [JsonIgnore] public SalaModel? Sala { get; set; }
        public int? SalaId { get; set; } = 0;
        [JsonIgnore] public ProfissionalModel? Profissional { get; set; }
        public int? ProfissionalId { get; set; } = 0;

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