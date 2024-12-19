
using Microsoft.AspNetCore.Mvc;
using WebApiSmartClinic.Dto.Plano;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Plano
{
    public interface IPlanoInterface
    {
        Task<ResponseModel<List<PlanoModel>>> Listar(string status, string cor, int page = 1, int pageSize = 10);
        Task<ResponseModel<List<PlanoModel>>> Delete(int idPlano);
        Task<ResponseModel<PlanoModel>> BuscarPorId(int idPlano);
        Task<ResponseModel<List<PlanoModel>>> Criar(PlanoCreateDto planoCreateDto);
        Task<ResponseModel<List<PlanoModel>>> Editar(PlanoEdicaoDto planoEdicaoDto);
    }
}