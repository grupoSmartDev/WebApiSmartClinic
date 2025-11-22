using WebApiSmartClinic.Dto.Pacote;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Pacote;

public interface IPacoteInterface
{
    // ========== PACOTE (TEMPLATE) ==========
    Task<ResponseModel<List<PacoteModel>>> Listar(int pageNumber = 1, int pageSize = 10, string? descricaoFiltro = null, bool paginar = true);
    Task<ResponseModel<PacoteModel>> BuscarPorId(int idPacote);
    Task<ResponseModel<List<PacoteModel>>> Criar(PacoteCreateDto pacoteCreateDto, int pageNumber = 1, int pageSize = 10);
    Task<ResponseModel<List<PacoteModel>>> Editar(PacoteEdicaoDto pacoteEdicaoDto, int pageNumber = 1, int pageSize = 10);
    Task<ResponseModel<List<PacoteModel>>> Delete(int idPacote, int pageNumber = 1, int pageSize = 10);

    // ========== VENDA DE PACOTE ==========
    Task<ResponseModel<PacotePacienteModel>> VenderPacote(PacoteVendaDto pacoteVendaDto);

    // ========== PACOTE DO PACIENTE ==========
    Task<ResponseModel<List<PacotePacienteModel>>> ListarPacotesPaciente(int pacienteId);
    Task<ResponseModel<PacotePacienteModel>> BuscarPacotePacientePorId(int idPacotePaciente);

    // ========== USO DO PACOTE ==========
    Task<ResponseModel<PacoteUsoModel>> ConsumirSessao(PacoteUsoDto pacoteUsoDto);
    Task<ResponseModel<List<PacoteUsoModel>>> ListarHistoricoUso(int pacotePacienteId);
    Task<ResponseModel<string>> EstornarUso(int idUso);
}