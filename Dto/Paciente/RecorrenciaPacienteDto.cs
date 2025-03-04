using System;

namespace WebApiSmartClinic.Dto.Paciente
{
    public class RecorrenciaPacienteDto
    {
        public int Id { get; set; }
        public DayOfWeek DiaSemana { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFim { get; set; }
        public int ProfissionalId { get; set; }
        public int SalaId { get; set; }
    }
}