
using WebApiSmartClinic.Dto.Profissao;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Profissao
{
    public interface IProfissaoInterface
    {
        Task<ResponseModel<List<ProfissaoModel>>> Listar();
        Task<ResponseModel<List<ProfissaoModel>>> Delete(int idProfissao);
        Task<ResponseModel<ProfissaoModel>> BuscarPorId(int idProfissao);
        Task<ResponseModel<List<ProfissaoModel>>> Criar(ProfissaoCreateDto profissaoCreateDto);
        Task<ResponseModel<List<ProfissaoModel>>> Editar(ProfissaoEdicaoDto profissaoEdicaoDto);
    }
}