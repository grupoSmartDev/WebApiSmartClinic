
using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Dto.CentroCusto;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.CentroCusto;

public class CentroCustoService : ICentroCustoInterface
{
    private readonly AppDbContext _context;
    public CentroCustoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseModel<CentroCustoModel>> BuscarCentroCustoPorId(int idCentroCusto)
    {
        ResponseModel<CentroCustoModel> resposta = new ResponseModel<CentroCustoModel>();
        try
        {
            var centrocusto = await _context.CentroCusto.Include(c => c.SubCentrosCusto).FirstOrDefaultAsync(x => x.Id == idCentroCusto);

            if (centrocusto == null)
            {
                resposta.Mensagem = "Nenhum centro de custo encontrado";
                return resposta;
            }

            resposta.Dados = centrocusto;
            resposta.Mensagem = "Centro de custo Encontrado";

            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = "Erro ao buscar centro de custo";
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<CentroCustoModel>>> CriarCentroCusto(CentroCustoCreateDto centrocustoCreateDto)
    {
        ResponseModel<List<CentroCustoModel>> resposta = new ResponseModel<List<CentroCustoModel>>();

        try
        {
            var centrocusto = new CentroCustoModel();

            centrocusto.Descricao = centrocustoCreateDto.Descricao;
            centrocusto.Tipo = centrocustoCreateDto.Tipo;

            _context.CentroCusto.Add(centrocusto);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.CentroCusto.ToListAsync();
            resposta.Mensagem = "Centro de Custo criado com sucesso";

            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<CentroCustoModel>>> DeleteCentroCusto(int id)
    {
        var resposta = new ResponseModel<List<CentroCustoModel>>();

        try
        {
            var centroCusto = await _context.CentroCusto.Include(c => c.SubCentrosCusto).FirstOrDefaultAsync(x => x.Id == id);
            if (centroCusto == null)
            {
                resposta.Mensagem = "Centro de Custo não encontrado";
                return resposta;
            }

            _context.CentroCusto.Remove(centroCusto);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.CentroCusto.Include(c => c.SubCentrosCusto).ToListAsync();
            resposta.Mensagem = "Centro de Custo excluído com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<CentroCustoModel>>> EditarCentroCusto(CentroCustoEdicaoDto centrocustoEdicaoDto)
    {
        ResponseModel<List<CentroCustoModel>> resposta = new ResponseModel<List<CentroCustoModel>>();

        try
        {
            var centrocusto = _context.CentroCusto.FirstOrDefault(x => x.Id == centrocustoEdicaoDto.Id);
            if (centrocusto == null)
            {
                resposta.Mensagem = "Centro de custo não encontrado";
                return resposta;
            }

            centrocusto.Id = centrocustoEdicaoDto.Id;
            centrocusto.Descricao = centrocustoEdicaoDto.Descricao;
            centrocusto.Tipo = centrocustoEdicaoDto.Tipo;

            _context.Update(centrocusto);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.CentroCusto.ToListAsync();
            resposta.Mensagem = "Centro de Custo atualizado com sucesso";

            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<CentroCustoModel>>> ListarCentroCusto()
    {
        ResponseModel<List<CentroCustoModel>> resposta = new ResponseModel<List<CentroCustoModel>>();

        try
        {
            var centrocusto = await _context.CentroCusto.Include(c => c.SubCentrosCusto).ToListAsync();

            resposta.Dados = centrocusto;
            resposta.Mensagem = "Todos os centros de custos foram encontrados";
            return resposta;
        }
        catch (Exception e)
        {
            resposta.Mensagem = e.Message;
            resposta.Status = false;
            return resposta;
        }
    }
}