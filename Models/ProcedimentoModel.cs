
using System.Text.Json.Serialization;
using WebApiSmartClinic.Models.Abstractions;

namespace WebApiSmartClinic.Models
{
    public class ProcedimentoModel : IEntidadeEmpresa, IEntidadeAuditavel
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
        public string Nome { get; set; } // Nome do Procedimento
        public string Descricao { get; set; } // Descrição detalhada do Procedimento
        public decimal Preco { get; set; } // Preço do Procedimento
        public string? Duracao { get; set; } // Duração estimada do Procedimento (minutos)
        //public bool Ativo { get; set; } = true; // Indica se o procedimento está ativo ou não no sistema
        public int? CategoriaId { get; set; }
        public decimal? PercentualComissao { get; set; }

        // Informações sobre materiais/equipamentos ---- criar uma relação com o model "Produtos" depois
        public string? MateriaisNecessarios { get; set; } // Lista de materiais/equipamentos necessários (se aplicável)

        // Esperar Jhonatan criar agendamento para vincular
        //public virtual ICollection<Agendamento> Agendamentos { get; set; } // Relação de 1 para N com Agendamentos
    }
}