
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Dto.Comissao
{
    public sealed class ComissaoCreateDto
    {
        public int ProfissionalId { get; set; }
        public ProfissionalModel Profissional { get; set; }  // Relação com o profissional

        public int ProcedimentoId { get; set; }
        public ProcedimentoModel Procedimento { get; set; }  // Relação com o procedimento realizado

        public DateTime DataAtendimento { get; set; }  // Data do atendimento que gerou a comissão
        public decimal ValorProcedimento { get; set; }  // Valor do procedimento
        public decimal PercentualComissao { get; set; }  // Percentual da comissão sobre o valor do procedimento
        public decimal ValorComissao { get; set; }  // Valor da comissão calculada

        public bool Pago { get; set; }  // Indica se a comissão já foi paga ao profissional
        public DateTime? DataPagamento { get; set; }  // Data de pagamento da comissão, se aplicável
    }
}