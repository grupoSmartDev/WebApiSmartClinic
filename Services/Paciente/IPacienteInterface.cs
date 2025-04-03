
using WebApiSmartClinic.Dto.Paciente;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Paciente
{
    public interface IPacienteInterface
    {
        Task<ResponseModel<List<PacienteModel>>> Listar(int pageNumber = 1, int pageSize = 10, int? codigoFiltro = null, string? nomeFiltro = null, string? cpfFiltro = null, string? celularFiltro = null, bool paginar = true);
        Task<ResponseModel<List<PacienteModel>>> Delete(int idPaciente, int pageNumber = 1, int pageSize = 10);
        Task<ResponseModel<PacienteModel>> BuscarPorId(int idPaciente);
        Task<ResponseModel<List<PacienteModel>>> Criar(PacienteCreateDto pacienteCreateDto, int pageNumber = 1, int pageSize = 10);
        Task<ResponseModel<PacienteModel>> CadastroRapido(PacienteCreateDto pacienteCreateDto, int pageNumber = 1, int pageSize = 10);
        Task<ResponseModel<List<PacienteModel>>> Editar(PacienteEdicaoDto pacienteEdicaoDto, int pageNumber = 1, int pageSize = 10);
        Task<ResponseModel<PacienteModel>> ObterPorCpf(string cpf);
        Task<ResponseModel<List<PacienteModel>>> ObterPorNome(string nome);
    }
}