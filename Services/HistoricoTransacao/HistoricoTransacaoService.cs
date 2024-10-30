
using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Dto.HistoricoTransacao;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.HistoricoTransacao;

public class HistoricoTransacaoService : IHistoricoTransacaoInterface
{
    private readonly AppDbContext _context;
    public HistoricoTransacaoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseModel<HistoricoTransacaoModel>> BuscarPorId(int idHistoricoTransacao)
    {
        ResponseModel<HistoricoTransacaoModel> resposta = new ResponseModel<HistoricoTransacaoModel>();
        try
        {
            var historicotransacao = await _context.HistoricoTransacao.FirstOrDefaultAsync(x => x.Id == idHistoricoTransacao);
            if (historicotransacao == null)
            {
                resposta.Mensagem = "Nenhum HistoricoTransacao encontrado";
                return resposta;
            }

            resposta.Dados = historicotransacao;
            resposta.Mensagem = "HistoricoTransacao Encontrado";
            return resposta;
        }
        catch (Exception ex)
        {

            resposta.Mensagem = "Erro ao buscar HistoricoTransacao";
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<HistoricoTransacaoModel>>> Criar(HistoricoTransacaoCreateDto historicotransacaoCreateDto)
    {
        ResponseModel<List<HistoricoTransacaoModel>> resposta = new ResponseModel<List<HistoricoTransacaoModel>>();

        try
        {
            var historicotransacao = new HistoricoTransacaoModel();

            // Atualizar para o código de acordo com o necessário
            //historicotransacao.HistoricoTransacao = historicotransacaoCreateDto.HistoricoTransacao;

            _context.Add(historicotransacao);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.HistoricoTransacao.ToListAsync();
            resposta.Mensagem = "HistoricoTransacao criado com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }

    }

    public async Task<ResponseModel<List<HistoricoTransacaoModel>>> Delete(int idHistoricoTransacao)
    {
        ResponseModel<List<HistoricoTransacaoModel>> resposta = new ResponseModel<List<HistoricoTransacaoModel>>();

        try
        {
            var historicotransacao = await _context.HistoricoTransacao.FirstOrDefaultAsync(x => x.Id == idHistoricoTransacao);
            if (historicotransacao == null)
            {
                resposta.Mensagem = "Nenhum HistoricoTransacao encontrado";
                return resposta;
            }

            _context.Remove(historicotransacao);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.HistoricoTransacao.ToListAsync();
            resposta.Mensagem = "HistoricoTransacao Excluido com sucesso";
            return resposta;

        }
        catch (Exception ex)
        {

            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<HistoricoTransacaoModel>>> Editar(HistoricoTransacaoEdicaoDto historicotransacaoEdicaoDto)
    {
        ResponseModel<List<HistoricoTransacaoModel>> resposta = new ResponseModel<List<HistoricoTransacaoModel>>();

        try
        {
            var historicotransacao = _context.HistoricoTransacao.FirstOrDefault(x => x.Id == historicotransacaoEdicaoDto.Id);
            if (historicotransacao == null)
            {
                resposta.Mensagem = "HistoricoTransacao não encontrado";
                return resposta;
            }

            // Atualizar para o código de acordo com o necessário
            //historicotransacao.HistoricoTransacao = historicotransacaoEdicaoDto.HistoricoTransacao;

            _context.Update(historicotransacao);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.HistoricoTransacao.ToListAsync();
            resposta.Mensagem = "HistoricoTransacao Atualizado com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {

            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<HistoricoTransacaoModel>>> Listar()
    {
        ResponseModel<List<HistoricoTransacaoModel>> resposta = new ResponseModel<List<HistoricoTransacaoModel>>();

        try
        {
            var historicotransacao = await _context.HistoricoTransacao.ToListAsync();

            resposta.Dados = historicotransacao;
            resposta.Mensagem = "Todos os HistoricoTransacao foram encontrados";
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