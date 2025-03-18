using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApiSmartClinic.Models
{

    public class PlanoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [StringLength(255, ErrorMessage = "A descrição deve ter no máximo 255 caracteres.")]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "O tempo em minutos é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "O tempo em minutos deve ser maior que zero.")]
        public int TempoMinutos { get; set; }

        [Required(ErrorMessage = "Os dias da semana são obrigatórios.")]
        [Range(1, 7, ErrorMessage = "Os dias da semana devem estar entre 1 e 7.")]
        public int DiasSemana { get; set; }

        public int? CentroCustoId { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "O valor deve ser maior ou igual a zero.")]
        public decimal? ValorBimestral { get; set; }
        public decimal? ValorTrimestral { get; set; }
        public decimal? ValorQuadrimestral { get; set; }
        public decimal? ValorSemestral { get; set; }
        public decimal? ValorAnual { get; set; }
        public decimal? ValorMensal { get; set; }

        private DateTime? _dataInicio;
        private DateTime? _dataFim;

        [DataType(DataType.Date)]
        public DateTime? DataInicio
        {
            get => _dataInicio;
            set => _dataInicio = value.HasValue ? DateTime.SpecifyKind(value.Value, DateTimeKind.Utc) : null;
        }

        [DataType(DataType.Date)]
        public DateTime? DataFim
        {
            get => _dataFim;
            set => _dataFim = value.HasValue ? DateTime.SpecifyKind(value.Value, DateTimeKind.Utc) : null;
        }

        public bool Ativo { get; set; } = true;

        public int? PacienteId { get; set; }
        [JsonIgnore]
        public PacienteModel? Paciente { get; set; }

        public int? FinanceiroId { get; set; }
        [JsonIgnore]
        public Financ_ReceberModel? Financeiro { get; set; }

        [Required(ErrorMessage = "O tipo de mês é obrigatório.")]
        [StringLength(1, ErrorMessage = "O tipo de mês deve ter apenas um caractere.")]
        public string TipoMes { get; set; } = string.Empty;
    }
}
