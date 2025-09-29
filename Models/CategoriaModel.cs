
using System.Text.Json.Serialization;

namespace WebApiSmartClinic.Models
{
    public class CategoriaModel
    {
        public int Id { get; set; }
        public string Nome { get; set; } // Nome da Categoria (ex: Estética, Fisioterapia, Pilates)
        public bool IsSystemDefault { get; set; } = true;
        public bool Ativo { get; set; } = true;
        [JsonIgnore]
        public virtual ICollection<ProcedimentoModel>? Procedimentos { get; set; } // Relacionamento com Procedimentos
    }
}