using WebApiSmartClinic.Dto.Status;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Status;

public interface IStatusInterface
{
    Task<ResponseModel<List<StatusModel>>> Listar(string status, string cor, int page, int pageSize);
    Task<ResponseModel<StatusModel>> BuscarStatusPorId(int idStatus);
    Task<ResponseModel<List<StatusModel>>> CriarStatus(StatusCreateDto statusCreateDto);
    Task<ResponseModel<List<StatusModel>>> EditarStatus(StatusEdicaoDto statusEdicaoDto);
    Task<ResponseModel<List<StatusModel>>> DeleteStatus(int idStatus);

    
}
