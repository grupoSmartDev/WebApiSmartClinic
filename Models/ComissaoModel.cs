
namespace WebApiSmartClinic.Models
{
    public class ComissaoModel
    {
        public int Id { get; set; }
        public int ProfissionalId { get; set; }
        public ProfissionalModel Profissional { get; set; }  // Relação com o profissional

        public int ProcedimentoId { get; set; }
        public ProcedimentoModel Procedimento { get; set; }  // Relação com o procedimento realizado

        private DateTime? _DataAtendimento;
        public DateTime? DataAtendimento 
        {
            get => _DataAtendimento;
            set => _DataAtendimento = value.HasValue ? DateTime.SpecifyKind(value.Value, DateTimeKind.Utc) : null;
        }  // Data do atendimento que gerou a comissão

        public decimal ValorProcedimento { get; set; }  // Valor do procedimento
        public decimal PercentualComissao { get; set; }  // Percentual da comissão sobre o valor do procedimento
        public decimal ValorComissao { get; set; }  // Valor da comissão calculada

        public bool Pago { get; set; }  // Indica se a comissão já foi paga ao profissional

        private DateTime? _dataPagamento;
        public DateTime? DataPagamento 
        {
            get => _dataPagamento;
            set => _dataPagamento = value.HasValue ? DateTime.SpecifyKind(value.Value, DateTimeKind.Utc) : null;
        }  // Data de pagamento da comissão, se aplicável
    }
}