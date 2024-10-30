
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Dto.HistoricoTransacao
{
    public class HistoricoTransacaoEdicaoDto
    {
        public int Id { get; set; }
        public int BancoId { get; set; }  // Relacionamento com a conta banc�ria associada
        public BancoModel Banco { get; set; }  // Propriedade de navega��o para a conta banc�ria
        public DateTime DataTransacao { get; set; }  // Data e hora da transa��o
        public string TipoTransacao { get; set; }  // Tipo da transa��o: "D�bito", "Cr�dito", "Estorno", etc.
        public decimal Valor { get; set; }  // Valor da transa��o (positiva ou negativa, dependendo do tipo)
        public string Descricao { get; set; }  // Descri��o ou observa��o sobre a transa��o
        public string Referencia { get; set; }  // Informa��o adicional, como n�mero de documento ou ID de pagamento
        public int? UsuarioId { get; set; }  // ID do usu�rio que realizou a transa��o (opcional, �til para auditoria)
        public UsuarioModel Usuario { get; set; }  // Propriedade de navega��o para o usu�rio que fez a transa��o
    }
}