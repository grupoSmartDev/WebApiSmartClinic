
namespace WebApiSmartClinic.Dto.Profissional
{
    public class ProfissionalEdicaoDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public string? Cpf { get; set; }
        public string Celular { get; set; }
        public string? Sexo { get; set; }
        public int? ConselhoId { get; set; }
        public string? RegistroConselho { get; set; }
        public string? UfConselho { get; set; }
        public int? ProfissaoId { get; set; }
        public string? Cbo { get; set; }
        public string? Rqe { get; set; }
        public string? Cnes { get; set; }

        // Propriedades para pagamento
        public string? TipoPagamento { get; set; } // Ex: "PIX", "Banco", "Dinheiro"
        public string? ChavePix { get; set; }
        public string? BancoNome { get; set; }
        public string? BancoAgencia { get; set; }
        public string? BancoConta { get; set; }
        public string? BancoTipoConta { get; set; } // Ex: "Conta Corrente", "Conta Poupan�a"
        public string? BancoCpfTitular { get; set; } // CPF do titular da conta para confirma��o

        // Propriedade para controle de acesso
        public bool EhUsuario { get; set; } = false; // Identifica se o profissional � um usu�rio do sistema

        // Opcional: Data de cadastro para rastrear a cria��o do registro
        public DateTime? DataCadastro { get; set; } = DateTime.Now;
    }
}