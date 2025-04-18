
using System.Text.Json.Serialization;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Dto.Procedimento
{
    public class ProcedimentoCreateDto
    {
        public string Nome { get; set; } // Nome do Procedimento
        public string Descricao { get; set; } // Descri��o detalhada do Procedimento
        public decimal Preco { get; set; } // Pre�o do Procedimento
        public string? Duracao { get; set; } // Dura��o estimada do Procedimento (horas/minutos)
        public bool Ativo { get; set; } = true; // Indica se o procedimento est� ativo ou n�o no sistema
        public int? CategoriaId { get; set; }
        public decimal? PercentualComissao { get; set; }

        // Informa��es sobre materiais/equipamentos ---- criar uma rela��o com o model "Produtos" depois
        public string? MateriaisNecessarios { get; set; } // Lista de materiais/equipamentos necess�rios (se aplic�vel)
        // 

        // Esperar Jhonatan criar agendamento para vincular
        //public virtual ICollection<Agendamento> Agendamentos { get; set; } // Rela��o de 1 para N com Agendamentos
    }
}