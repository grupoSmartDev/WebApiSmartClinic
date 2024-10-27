
using WebApiSmartClinic.Dto.Profissional;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Profissional
{
    public interface IProfissionalInterface
    {
        Task<ResponseModel<List<ProfissionalModel>>> Listar();
        Task<ResponseModel<List<ProfissionalModel>>> Delete(int idProfissional);
        Task<ResponseModel<ProfissionalModel>> BuscarPorId(int idProfissional);
        Task<ResponseModel<List<ProfissionalModel>>> Criar(ProfissionalCreateDto profissionalCreateDto);
        Task<ResponseModel<List<ProfissionalModel>>> Editar(ProfissionalEdicaoDto profissionalEdicaoDto);
    }
}