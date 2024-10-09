
using System.Text.Json.Serialization;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Dto.Convenio
{
    public class ConvenioEdicaoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string RegistroAvs { get; set; }
        public string PeriodoCarencia { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; }
    }
}