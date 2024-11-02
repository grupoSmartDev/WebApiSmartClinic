
namespace WebApiSmartClinic.Models
{
    public class ComissaoModel
    {
        public int Id { get; set; }
        public int ProfissionalId { get; set; }
        public ProfissionalModel Profissional { get; set; }  // Rela��o com o profissional

        public int ProcedimentoId { get; set; }
        public ProcedimentoModel Procedimento { get; set; }  // Rela��o com o procedimento realizado

        public DateTime DataAtendimento { get; set; }  // Data do atendimento que gerou a comiss�o
        public decimal ValorProcedimento { get; set; }  // Valor do procedimento
        public decimal PercentualComissao { get; set; }  // Percentual da comiss�o sobre o valor do procedimento
        public decimal ValorComissao { get; set; }  // Valor da comiss�o calculada

        public bool Pago { get; set; }  // Indica se a comiss�o j� foi paga ao profissional
        public DateTime? DataPagamento { get; set; }  // Data de pagamento da comiss�o, se aplic�vel
    }
}