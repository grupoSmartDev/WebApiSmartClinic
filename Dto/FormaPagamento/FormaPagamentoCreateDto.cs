
namespace WebApiSmartClinic.Dto.FormaPagamento
{
    public sealed class FormaPagamentoCreateDto
    {
        public int Parcelas { get; set; }
        public string Descricao { get; set; }
    }
}