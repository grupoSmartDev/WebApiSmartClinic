
using System.Text.Json.Serialization;

namespace WebApiSmartClinic.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
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
    }

    public enum Permissao
    {
        A,
        S,
        N
    }
}