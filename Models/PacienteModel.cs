using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WebApiSmartClinic.Dto.Paciente;

namespace WebApiSmartClinic.Models
{
    public class PacienteModel
    {
        public int Id { get; set; }
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

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        public string? Cpf { get; set; }

        private DateTime? _dataNascimento;

        [DataType(DataType.Date)]
        public DateTime? DataNascimento
        {
            get => _dataNascimento?.ToLocalTime();
            set => _dataNascimento = value.HasValue ? DateTime.SpecifyKind(value.Value.ToUniversalTime(), DateTimeKind.Utc) : null;
        }

        
        public string? Email { get; set; }
        public string? Uf { get; set; }
        public string? EstadoCivil { get; set; }
        public string? Logradouro { get; set; }
        public string? Medicamento { get; set; }
        public int? ProfissionalId { get; set; }

        [JsonIgnore]
        public ProfissionalModel? Profissional { get; set; }

        [Required(ErrorMessage = "O nome � obrigat�rio.")]
        public string? Nome { get; set; }
        public string? Numero { get; set; }
        public string? Pais { get; set; }
        public bool PermitirLembretes { get; set; }
        public string? PreferenciaDeContato { get; set; }
        public string? Profissao { get; set; }
        public bool Responsavel { get; set; }
        public string? Rg { get; set; }
        public string? Sexo { get; set; }
        public string? Telefone { get; set; }

        public int? PlanoId { get; set; }
   
        public PlanoModel? Plano { get; set; }

        public List<EvolucaoModel>? Evolucoes { get; set; }

        private DateTime? _dataUltimoAtendimento;
        private DateTime? _dataCadastro;

        [DataType(DataType.Date)]
        public DateTime? DataUltimoAtendimento 
        { 
            get => _dataUltimoAtendimento?.ToLocalTime(); 
            set => _dataUltimoAtendimento = value.HasValue ? DateTime.SpecifyKind(value.Value.ToUniversalTime(), DateTimeKind.Utc) : null;
        }

        [DataType(DataType.Date)]
        public DateTime? DataCadastro
        {
            get => _dataCadastro?.ToLocalTime();
            set => _dataCadastro = value.HasValue ? DateTime.SpecifyKind(value.Value.ToUniversalTime(), DateTimeKind.Utc) : null;
        }

        public List<Financ_ReceberModel>? FinancReceber { get; set; }

        public PacienteModel()
        {
            Evolucoes = new List<EvolucaoModel>();
           // Plano = new PlanoModel();
        }
        public bool Ativo { get; set; } = true;
        private DateTime? _DataAlteracao;
        public DateTime? DataAlteracao
        {
            get => _DataAlteracao;
            set => _DataAlteracao = value.HasValue ? DateTime.SpecifyKind(value.Value, DateTimeKind.Utc) : null;
        }

        public int? FichaAvaliacaoId { get; set; }
        public FichaAvaliacaoModel? FichaAvaliacao { get; set; }
        public List<RecorrenciaPacienteDto>? Recorrencias { get; set; } = new List<RecorrenciaPacienteDto>();
        public DateTime? DataFimRecorrencia { get; set; }
        public virtual ICollection<AgendaModel> Agendamentos { get; set; } = new List<AgendaModel>();
        public virtual ICollection<PacientePlanoHistoricoModel> HistoricoPlanos { get; set; } = new List<PacientePlanoHistoricoModel>();
    }
}
