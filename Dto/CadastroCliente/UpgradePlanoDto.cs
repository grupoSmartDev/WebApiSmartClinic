namespace WebApiSmartClinic.Dto.CadastroCliente
{
    public sealed class UpgradePlanoDto
    {
        public string NovoPlano { get; set; } // "Basic" | "Plus" | "Premium"
        public string TipoPagamento { get; set; } // "PIX" | "BOLETO" | "CREDIT_CARD"
        public string Periodo { get; set; } // "mensal" | "semestral" | "anual"
        public CreditCardDataDto? DadosCartao { get; set; } // obrigatório só quando TipoPagamento == "CREDIT_CARD"
    }
}
