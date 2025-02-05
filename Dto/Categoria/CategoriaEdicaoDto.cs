
using System.Text.Json.Serialization;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Dto.Categoria
{
    public class CategoriaEdicaoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        [JsonIgnore]
        public virtual ICollection<ProcedimentoModel>? Procedimentos { get; set; }
    }
}