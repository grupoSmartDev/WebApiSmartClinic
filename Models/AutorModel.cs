using System.Text.Json.Serialization;

namespace WebApiSmartClinic.Models;

public class AutorModel
{
  public int Id { get; set; }
  public string Nome { get; set; }
  public string Sobrenome { get; set; }

  [JsonIgnore]//esta aqui para nao precisar passar os livros que o autor tem na hora de criar
  public ICollection<LivroModel> Livros { get; set; }

}
