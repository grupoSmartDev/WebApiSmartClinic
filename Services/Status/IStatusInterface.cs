using WebApiSmartClinic.Dto.Status;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Status;

public interface IStatusInterface
{
    Task<ResponseModel<List<StatusModel>>> ListarStatus();
    Task<ResponseModel<StatusModel>> BuscarStatusPorId(int idStatus);
    Task<ResponseModel<List<StatusModel>>> CriarStatus(StatusCreateDto statusCreateDto);
    Task<ResponseModel<List<StatusModel>>> EditarStatus(StatusEdicaoDto statusEdicaoDto);
    Task<ResponseModel<List<StatusModel>>> DeleteStatus(int idStatus);

    
}
