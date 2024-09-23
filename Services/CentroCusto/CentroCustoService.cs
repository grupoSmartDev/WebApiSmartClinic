
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
            var centrocusto = await _context.CentroCusto.FirstOrDefaultAsync(x => x.Id == idCentroCusto);
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
            centrocusto.CentroCustoOrigem = centrocustoCreateDto.CentroCustoOrigem;

            _context.Add(centrocusto);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.CentroCusto.ToListAsync();
            
            if (string.IsNullOrEmpty(centrocusto.CentroCustoOrigem))
                resposta.Mensagem = "Centro de custo criado com sucesso";
            else
                resposta.Mensagem = "Sub centro de custo criado com sucesso";

            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }

    }

    public async Task<ResponseModel<List<CentroCustoModel>>> DeleteCentroCusto(int idCentroCusto)
    {
        ResponseModel<List<CentroCustoModel>> resposta = new ResponseModel<List<CentroCustoModel>>();

        try
        {
            var centrocusto = await _context.CentroCusto.FirstOrDefaultAsync(x => x.Id == idCentroCusto);
            if (centrocusto == null)
            {
                resposta.Mensagem = "Nenhum centro de custo encontrado";
                return resposta;
            }

            _context.Remove(centrocusto);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.CentroCusto.ToListAsync();
            resposta.Mensagem = "Centro de custo Excluido com sucesso";
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
            
            if (string.IsNullOrEmpty(centrocusto.CentroCustoOrigem))
                resposta.Mensagem = "Centro de custo atualizado com sucesso";
            else
                resposta.Mensagem = "Sub centro de custo atualizado com sucesso";

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
            var centrocusto = await _context.CentroCusto.ToListAsync();

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