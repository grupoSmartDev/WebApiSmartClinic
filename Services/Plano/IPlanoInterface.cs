
using Microsoft.AspNetCore.Mvc;
using WebApiSmartClinic.Dto.Plano;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Plano
{
    public interface IPlanoInterface
    {
        Task<ResponseModel<PlanoModel>> BuscarPorId(int idPlano);
        Task<ResponseModel<List<PlanoModel>>> Delete(int idPlano, int pageNumber = 1, int pageSize = 10);
        Task<ResponseModel<List<PlanoModel>>> Criar(PlanoCreateDto planoCreateDto, int pageNumber = 1, int pageSize = 10);
        Task<ResponseModel<PlanoModel>> PlanoParaPaciente(PlanoCreateDto planoCreateDto);
        Task<ResponseModel<List<PlanoModel>>> Editar(PlanoEdicaoDto planoEdicaoDto, int pageNumber = 1, int pageSize = 10);
        Task<ResponseModel<List<PlanoModel>>> Listar(int pageNumber = 1, int pageSize = 10, int? codigoFiltro = null, string? descricaoFiltro = null, bool paginar = true);
    }
}