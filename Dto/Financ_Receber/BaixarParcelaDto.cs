namespace WebApiSmartClinic.Dto.Financ_Receber
{
    // DTO específico para BaixarParcela — não herda de Financ_ReceberSubModel de propósito,
    // para não trazer a navegação FinancReceber (e FinancReceber.Paciente) junto no payload.
    // O objeto completo enviado pelo front (linha de uma listagem, com o paciente aninhado)
    // acabava disparando a validação [Required] de PacienteModel.Cpf mesmo sem o usuário
    // estar editando o paciente.
    public class BaixarParcelaDto
    {
        public int Id { get; set; }
        public decimal ValorPago { get; set; }
        public DateTime DataPagamento { get; set; }
        public string? Observacao { get; set; }
        public int? FormaPagamentoId { get; set; }
        public int? TipoPagamentoId { get; set; }
        public DateTime? DataVencimentoResidual { get; set; }
    }
}
