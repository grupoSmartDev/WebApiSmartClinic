
using WebApiSmartClinic.Dto.CadastroCliente;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.CadastroCliente
{
    // Marca “entidades que pertencem a uma empresa”.
    public interface IEntidadeEmpresa
    {
        int EmpresaId { get; set; }
    }

    public interface ICadastroClienteInterface
    {
        Task<ResponseModel<List<EmpresaModel>>> Listar();
        Task<ResponseModel<List<EmpresaModel>>> Delete(int idCadastroCliente);
        Task<ResponseModel<EmpresaModel>> Criar(CadastroClienteCreateDto dto);
    }
}