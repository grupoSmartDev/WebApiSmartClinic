using WebApiSmartClinic.Dto.CRM;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.CRM;

public interface ILeadInterface
{
    Task<ResponseModel<List<LeadModel>>> Listar(int pageNumber = 1, int pageSize = 10, int? etapaId = null, int? profissionalId = null, string? canalOrigem = null, string? search = null, bool paginar = true);
    Task<ResponseModel<List<KanbanColunaDto>>> ListarKanban(int? profissionalId = null, string? canalOrigem = null, string? search = null);
    Task<ResponseModel<LeadModel>> BuscarPorId(int id);
    Task<ResponseModel<List<LeadModel>>> Criar(LeadCreateDto dto, int pageNumber = 1, int pageSize = 10);
    Task<ResponseModel<List<LeadModel>>> Editar(LeadEdicaoDto dto, int pageNumber = 1, int pageSize = 10);
    Task<ResponseModel<LeadModel>> MoverEtapa(LeadMoverEtapaDto dto);
    Task<ResponseModel<List<LeadModel>>> Delete(int id, int pageNumber = 1, int pageSize = 10);
    Task<ResponseModel<LeadModel>> ConverterEmPaciente(int leadId);
}
