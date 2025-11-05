using System.Text.Json.Serialization;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Dto.Categoria;

public class CategoriaCreateDto
{
    public string Nome { get; set; }
    
    [JsonIgnore]
    public virtual ICollection<ProcedimentoModel>? Procedimentos { get; set; }
}