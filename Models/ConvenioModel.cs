
using System.Text.Json.Serialization;

namespace WebApiSmartClinic.Models
{
    public class ConvenioModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string RegistroAvs { get; set; }
        public string PeriodoCarencia { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; } = true;

        [JsonIgnore]
        public ICollection<PacienteModel> Pacientes { get; set; }
    }
}