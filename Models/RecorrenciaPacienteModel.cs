using System;
using System.ComponentModel.DataAnnotations;

namespace WebApiSmartClinic.Models
{
    public class RecorrenciaPacienteModel
    {
        public int Id { get; set; }

        [Required]
        public int PacienteId { get; set; }

        [Required]
        public DayOfWeek DiaSemana { get; set; }

        [Required]
        public TimeSpan HoraInicio { get; set; }

        [Required]
        public TimeSpan HoraFim { get; set; }

        public int ProfissionalId { get; set; }
        public int SalaId { get; set; }
    }
}