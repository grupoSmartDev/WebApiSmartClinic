
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
        Task<ResponseModel<List<AgendaModel>>> AtualizarStatus(int id, int satusNovo);
        Task<ResponseModel<List<AgendaModel>>> Reagendar(int id, int statusNovo, DateTime dataNova, string horaInicioNovo, string horaFimNovo);
        Task<ResponseModel<List<ContadoresDashboard>>> ObterContadoresDashboard(int? profissionalId, DateTime? dataInicio = null, DateTime? dataFim = null);
        Task<ResponseModel<List<AgendaModel>>> ListarGeral(int pageNumber = 1, int pageSize = 10, int? idFiltro = null, string? pacienteIdFiltro = null, string? profissionalIdFiltro = null, string? statusIdFiltro = null, DateTime? dataFiltroInicio = null, DateTime? dataFiltroFim = null, bool paginar = true);
    }
}