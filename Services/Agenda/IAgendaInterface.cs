
using WebApiSmartClinic.Dto.Agenda;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Agenda
{
    public interface IAgendaInterface
    {
        Task<ResponseModel<List<AgendaModel>>> Listar();
        Task<ResponseModel<List<AgendaModel>>> Delete(int idAgenda);
        Task<ResponseModel<AgendaModel>> BuscarPorId(int idAgenda);
        Task<ResponseModel<List<AgendaModel>>> Criar(AgendaCreateDto agendaCreateDto);
        Task<ResponseModel<List<AgendaModel>>> Editar(AgendaEdicaoDto agendaEdicaoDto);
    }
}