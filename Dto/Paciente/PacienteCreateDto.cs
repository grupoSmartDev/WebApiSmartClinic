
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Dto.Paciente;

public class PacienteCreateDto
{
    public string? Bairro { get; set; }
    public string? BreveDiagnostico { get; set; }
    public string? Celular { get; set; }
    public string? Cep { get; set; }
    public string? Cidade { get; set; }
    public string? ComoConheceu { get; set; }
    public string? Complemento { get; set; }

    public int? ConvenioId { get; set; }

    [JsonIgnore]
    public ConvenioModel? Convenio { get; set; }

    public string? Cpf { get; set; }

    [DataType(DataType.Date)]
    public DateTime? DataNascimento { get; set; }

    [EmailAddress(ErrorMessage = "O email deve ser válido.")]
    public string? Email { get; set; }

    public string? Uf { get; set; }
    public string? EstadoCivil { get; set; }
    public string? Logradouro { get; set; }
    public string? Medicamento { get; set; }
    public int? ProfissionalId { get; set; }
    public string? Nome { get; set; }
    public string? Numero { get; set; }
    public string? Pais { get; set; }
    public bool? PermitirLembretes { get; set; }
    public string? PreferenciaDeContato { get; set; }
    public string? Profissao { get; set; }
    public bool? Responsavel { get; set; }
    public string? Rg { get; set; }
    public string? Sexo { get; set; }
    public string? Telefone { get; set; }
    public int? PlanoId { get; set; }


}