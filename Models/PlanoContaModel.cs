
namespace WebApiSmartClinic.Models
{
    public class PlanoContaModel
    {
        public int Id { get; set; }
        public string Codigo { get; set; } // C�digo �nico para identifica��o do plano (e.g., 1.01.02)
        public string Nome { get; set; } // Nome do plano de contas
        public string Tipo { get; set; } // Receita, Despesa, etc.
        public bool? Inativo { get; set; } = false; // Ativo, Inativo
        public string? Observacao { get; set; } // Observa��es sobre o plano
        public ICollection<PlanoContaSubModel>? SubPlanos { get; set; } = new List<PlanoContaSubModel>(); // Subcontas associadas
    }

    public class PlanoContaSubModel
    {
        public int Id { get; set; }
        public int PlanoContaId { get; set; } // Relacionamento com o plano pai
        public PlanoContaModel PlanoConta { get; set; } // Refer�ncia ao plano pai
        public string Codigo { get; set; } // C�digo �nico da subconta
        public string Nome { get; set; } // Nome da subconta
        public string Tipo { get; set; } // Receita, Despesa, etc.
    }

    public enum Tipo
    {
        R,
        D,
        A,
        P
    }
}