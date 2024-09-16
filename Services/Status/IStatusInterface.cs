using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Status;

public interface IStatusInterface
{
    Task<ResponseModel<List<StatusModel>>> ListarStatus();
    
}
