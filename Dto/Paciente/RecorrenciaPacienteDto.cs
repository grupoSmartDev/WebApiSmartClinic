using System;

namespace WebApiSmartClinic.Dto.Paciente
{
    public class RecorrenciaPacienteDto
    {
        public int Id { get; set; }
        public int? PacienteId { get; set; }
        public DayOfWeek DiaSemana { get; set; }
        public string? HoraInicio { get; set; }
        public string? HoraFim { get; set; }
        public int? ProfissionalId { get; set; }
        public int? SalaId { get; set; }
    }
}