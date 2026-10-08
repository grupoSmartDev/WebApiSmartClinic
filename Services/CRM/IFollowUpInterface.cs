using WebApiSmartClinic.Dto.CRM;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.CRM;

public interface IFollowUpInterface
{
    Task<ResponseModel<List<FollowUpModel>>> Listar(int leadId);
    Task<ResponseModel<List<FollowUpModel>>> Criar(FollowUpCreateDto dto);
    Task<ResponseModel<List<FollowUpModel>>> Editar(FollowUpEdicaoDto dto);
    Task<ResponseModel<List<FollowUpModel>>> Delete(int id);
}
