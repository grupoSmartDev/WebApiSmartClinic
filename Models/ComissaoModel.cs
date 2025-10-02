
using WebApiSmartClinic.Models.Abstractions;

namespace WebApiSmartClinic.Models
{
    public class ComissaoModel : IEntidadeEmpresa, IEntidadeAuditavel, IEntidadeDoProfissional
    {
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        public string? UsuarioCriacaoId { get; set; }
        private DateTime _DataCriacao = DateTime.UtcNow;
        public DateTime DataCriacao
        {
            get => _DataCriacao.ToLocalTime();
            set => _DataCriacao = DateTime.SpecifyKind(value.ToUniversalTime(), DateTimeKind.Utc);
        }
        public string? UsuarioAlteracaoId { get; set; }
        private DateTime? _DataAlteracao;
        public DateTime? DataAlteracao
        {
            get => _DataAlteracao?.ToLocalTime();
            set => _DataAlteracao = value.HasValue ? DateTime.SpecifyKind(value.Value.ToUniversalTime(), DateTimeKind.Utc) : null;
        }
        public bool Ativo { get; set; } = true;        
        public int? ProfissionalId { get; set; }
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