
using System.Text.Json.Serialization;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Dto.Procedimento
{
    public sealed class ProcedimentoCreateDto
    {
        public string Nome { get; set; } // Nome do Procedimento
        public string Descricao { get; set; } // Descrição detalhada do Procedimento
        public decimal Preco { get; set; } // Preço do Procedimento
        public string? Duracao { get; set; } // Duração estimada do Procedimento (horas/minutos)
        public bool Ativo { get; set; } = true; // Indica se o procedimento está ativo ou não no sistema
        public int? CategoriaId { get; set; }
        public decimal? PercentualComissao { get; set; }

        // Informações sobre materiais/equipamentos ---- criar uma relação com o model "Produtos" depois
        public string? MateriaisNecessarios { get; set; } // Lista de materiais/equipamentos necessários (se aplicável)
        // 

        // Esperar Jhonatan criar agendamento para vincular
        //public virtual ICollection<Agendamento> Agendamentos { get; set; } // Relação de 1 para N com Agendamentos
    }
}