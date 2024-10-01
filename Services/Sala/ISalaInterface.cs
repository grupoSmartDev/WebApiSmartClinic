using WebApiSmartClinic.Dto.Sala;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Sala;

public interface ISalaInterface
{
    Task<ResponseModel<List<SalaModel>>> ListarSala();
    Task<ResponseModel<SalaModel>> BuscarSalaPorId(int idSala);
    Task<ResponseModel<List<SalaModel>>> CriarSala(SalaCreateDto salaCreateDto);
    Task<ResponseModel<List<SalaModel>>> EditarSala(SalaEdicaoDto salaEdicaoDto);
    Task<ResponseModel<List<SalaModel>>> DeleteSala(int idSala);

    
}
