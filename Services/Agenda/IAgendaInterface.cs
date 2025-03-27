
using WebApiSmartClinic.Dto.Agenda;
using WebApiSmartClinic.Models;
using static WebApiSmartClinic.Services.Agenda.AgendaService;

namespace WebApiSmartClinic.Services.Agenda
{
    public interface IAgendaInterface
    {
        Task<ResponseModel<List<AgendaModel>>> Listar();
        Task<ResponseModel<List<AgendaModel>>> Delete(int idAgenda);
        Task<ResponseModel<AgendaModel>> BuscarPorId(int idAgenda);
        Task<ResponseModel<List<AgendaModel>>> Criar(AgendaCreateDto agendaCreateDto);
        Task<ResponseModel<List<AgendaModel>>> CriarPeloPlano(AgendaCreateDto agendaCreateDto);
        Task<ResponseModel<List<AgendaModel>>> Editar(AgendaEdicaoDto agendaEdicaoDto);
        Task<ResponseModel<List<ContadoresDashboard>>> ObterContadoresDashboard(int? profissionalId, DateTime? dataInicio = null, DateTime? dataFim = null);
    }
}