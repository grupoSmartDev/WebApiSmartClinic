
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Dto.PlanoConta
{
    public sealed class PlanoContaCreateDto
    {
        public string Codigo { get; set; } // Código único para identificação do plano (e.g., 1.01.02)
        public string Nome { get; set; } // Nome do plano de contas
        public string Tipo { get; set; }// Receita, Despesa, etc.
        public bool? Inativo { get; set; } = false; // Ativo, Inativo
        public string? Observacao { get; set; } // Observações sobre o plano
        public ICollection<PlanoContaSubModel>? SubPlanos { get; set; } = new List<PlanoContaSubModel>(); // Subcontas associadas
    }
    public sealed class PlanoContaSubCreateDto
    {
        public int PlanoContaId { get; set; } // Relacionamento com o plano pai
        public PlanoContaModel? PlanoConta { get; set; } // Referência ao plano pai
        public string Codigo { get; set; } // Código único da subconta
        public string Nome { get; set; } // Nome da subconta
        public string Tipo { get; set; } // Receita, Despesa, etc.
    }
}