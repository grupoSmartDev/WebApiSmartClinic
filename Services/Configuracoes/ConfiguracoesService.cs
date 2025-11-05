using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Dto.Configuracoes;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Configuracoes;

public sealed class ConfiguracoesService : IConfiguracoesInterface
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public ConfiguracoesService(AppDbContext context,  IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ResponseModel<EmpresaModel>> BuscarPorId(int idEmpresa)
    {
        ResponseModel<EmpresaModel> resposta = new ResponseModel<EmpresaModel>();

        try
        {
            var empresa = await _context.Empresas.AsNoTracking().FirstOrDefaultAsync(x => x.Id == idEmpresa);
            if (empresa == null)
            {
                resposta.Mensagem = "Nenhuma empresa encontrado";
                return resposta;
            }

            resposta.Dados = empresa;
            resposta.Mensagem = "Configurações da empresa encontradas";
            
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = "Erro ao buscar empresa";
            resposta.Status = false;

            return resposta;
        }
    }

    public async Task Editar(ConfiguracoesEdicaoDto configuracoesEdicaoDto)
    {
        ResponseModel<List<EmpresaModel>> resposta = new ResponseModel<List<EmpresaModel>>();

        try
        {
            var empresa = _context.Empresas.FirstOrDefault(x => x.Id == configuracoesEdicaoDto.Id);
            if (empresa == null)
            {
                resposta.Mensagem = "Nenhuma empresa encontrada";
            }

            _mapper.Map(configuracoesEdicaoDto, empresa);
            await _context.SaveChangesAsync();

            resposta.Mensagem = "Configurações atualizadas com sucesso";
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
        }
    }
}