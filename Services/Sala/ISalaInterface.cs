using WebApiSmartClinic.Dto.Sala;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Sala;

public interface ISalaInterface
{
    Task<ResponseModel<List<SalaModel>>> Listar(int pageNumber = 1, int pageSize = 10, int? codigoFiltro = null, string? nomeFiltro = null, string? localFiltro = null, int? capacidadeFiltro = null, bool paginar = true);
    Task<ResponseModel<SalaModel>> BuscarPorId(int idSala);
    Task<ResponseModel<List<SalaModel>>> Criar(SalaCreateDto salaCreateDto, int pageNumber = 1, int pageSize = 10);
    Task<ResponseModel<List<SalaModel>>> Editar(SalaEdicaoDto salaEdicaoDto, int pageNumber = 1, int pageSize = 10);
    Task<ResponseModel<List<SalaModel>>> Delete(int idSala, int pageNumber = 1, int pageSize = 10);

    
}
