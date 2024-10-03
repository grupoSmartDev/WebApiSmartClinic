
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Dto.Paciente
{
    public class PacienteCreateDto
    {
        public string Bairro { get; set; }
        public string BreveDiagnostico { get; set; }

        [Phone(ErrorMessage = "O n�mero de celular deve ser v�lido.")]
        public string Celular { get; set; }

        [Required(ErrorMessage = "O CEP � obrigat�rio.")]
        [RegularExpression(@"^\d{5}-\d{3}$", ErrorMessage = "O CEP deve estar no formato 00000-000.")]
        public string Cep { get; set; }
        public string Cidade { get; set; }
        public string ComoConheceu { get; set; } // Select com informa��es sobre como conheceu
        public string Complemento { get; set; }

        public int? ConvenioId { get; set; } // Relacionamento com a tabela de convenio

        [JsonIgnore]
        public ConvenioModel? Convenio { get; set; } // Relacionamento com a tabela de convenio

        [Required(ErrorMessage = "O CPF � obrigat�rio.")]
        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "O CPF deve estar no formato 000.000.000-00.")]
        public string Cpf { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DataNascimento { get; set; }

        [EmailAddress(ErrorMessage = "O email deve ser v�lido.")]
        public string Email { get; set; }
        public string Estado { get; set; }
        public string EstadoCivil { get; set; }
        public string Logradouro { get; set; }
        public string Medicamento { get; set; }
        public string Medico { get; set; }

        [Required(ErrorMessage = "O nome � obrigat�rio.")]
        public string Nome { get; set; }
        public string NomeDaEmpresa { get; set; }
        public string Numero { get; set; }
        public string Pais { get; set; }
        public bool PermitirLembretes { get; set; }
        public string PreferenciaDeContato { get; set; }
        public string Profissao { get; set; }
        public bool Responsavel { get; set; } // Vincular outro cadastro se true
        public string Rg { get; set; }
        public string Sexo { get; set; }

        [Phone(ErrorMessage = "O n�mero de telefone deve ser v�lido.")]
        public string Telefone { get; set; }
    }
}