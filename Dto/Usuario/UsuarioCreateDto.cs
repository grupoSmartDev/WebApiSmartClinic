
using System.Text.Json.Serialization;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Dto.Usuario
{
    public class UsuarioCreateDto
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public ICollection<Permissao> Permissao { get; set; }
        public string CPF { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public bool Ativo { get; set; } = true;
        public int? ProfissionalId { get; set; } // Relacionamento com Profissional

        [JsonIgnore]
        public ProfissionalModel? Profissional { get; set; }
        public int EmpresaId { get; set; }
        [JsonIgnore]
        public EmpresaModel? Empresa { get; set; }
    }
}