using WebApiSmartClinic.Dto.Configuracoes;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Configuracoes;

public interface IConfiguracoesInterface
{
    Task<ResponseModel<EmpresaModel>> BuscarPorId(int idEmpresa);
    //Task<ResponseModel<List<EmpresaModel>>> Editar(ConfiguracoesEdicaoDto configuracoesEdicaoDto);
    Task Editar(ConfiguracoesEdicaoDto configuracoesEdicaoDto);
}