
using WebApiSmartClinic.Dto.Paciente;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Paciente
{
    public interface IPacienteInterface
    {
        Task<ResponseModel<List<PacienteModel>>> Listar();
        Task<ResponseModel<List<PacienteModel>>> Delete(int idPaciente);
        Task<ResponseModel<PacienteModel>> BuscarPorId(int idPaciente);
        Task<ResponseModel<List<PacienteModel>>> Criar(PacienteCreateDto pacienteCreateDto);
        Task<ResponseModel<List<PacienteModel>>> Editar(PacienteEdicaoDto pacienteEdicaoDto);
    }
}