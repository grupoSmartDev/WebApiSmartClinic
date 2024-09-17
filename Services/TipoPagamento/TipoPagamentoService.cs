
using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Dto.TipoPagamento;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.TipoPagamento;

public class TipoPagamentoService : ITipoPagamentoInterface
{
    private readonly AppDbContext _context;
    public TipoPagamentoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseModel<TipoPagamentoModel>> BuscarTipoPagamentoPorId(int idTipoPagamento)
    {
        ResponseModel<TipoPagamentoModel> resposta = new ResponseModel<TipoPagamentoModel>();
        try
        {
            var tipopagamento = await _context.TipoPagamento.FirstOrDefaultAsync(x => x.Id == idTipoPagamento);
            if (tipopagamento == null)
            {
                resposta.Mensagem = "Nenhum TipoPagamento encontrado";
                return resposta;
            }

            resposta.Dados = tipopagamento;
            resposta.Mensagem = "TipoPagamento Encontrado";
            return resposta;
        }
        catch (Exception ex)
        {

            resposta.Mensagem = "Erro ao buscar TipoPagamento";
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<TipoPagamentoModel>>> CriarTipoPagamento(TipoPagamentoCreateDto tipopagamentoCreateDto)
    {
        ResponseModel<List<TipoPagamentoModel>> resposta = new ResponseModel<List<TipoPagamentoModel>>();

        try
        {
            var tipopagamento = new TipoPagamentoModel();
            if (tipopagamentoCreateDto == null)
            {
                resposta.Mensagem = "Erro ao Criar um TipoPagamento";
                return resposta;
            }

            // Atualizar para o código de acordo com o necessário
            //tipopagamento.TipoPagamento = tipopagamentoCreateDto.TipoPagamento;

            _context.AddAsync(tipopagamento);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.TipoPagamento.ToListAsync();
            resposta.Mensagem = "TipoPagamento criado com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }

    }

    public async Task<ResponseModel<List<TipoPagamentoModel>>> DeleteTipoPagamento(int idTipoPagamento)
    {
        ResponseModel<List<TipoPagamentoModel>> resposta = new ResponseModel<List<TipoPagamentoModel>>();

        try
        {
            var tipopagamento = await _context.TipoPagamento.FirstOrDefaultAsync(x => x.Id == idTipoPagamento);
            if (tipopagamento == null)
            {
                resposta.Mensagem = "Nenhum TipoPagamento encontrado";
                return resposta;
            }

            _context.Remove(tipopagamento);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.TipoPagamento.ToListAsync();
            resposta.Mensagem = "TipoPagamento Excluido com sucesso";
            return resposta;

        }
        catch (Exception ex)
        {

            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<TipoPagamentoModel>>> EditarTipoPagamento(TipoPagamentoEdicaoDto tipopagamentoEdicaoDto)
    {
        ResponseModel<List<TipoPagamentoModel>> resposta = new ResponseModel<List<TipoPagamentoModel>>();

        try
        {
            var tipopagamento = _context.TipoPagamento.FirstOrDefault(x => x.Id == tipopagamentoEdicaoDto.Id);
            if (tipopagamento == null)
            {
                resposta.Mensagem = "TipoPagamento não encontrado";
                return resposta;
            }

            // Atualizar para o código de acordo com o necessário
            //tipopagamento.TipoPagamento = tipopagamentoEdicaoDto.TipoPagamento;

            _context.Update(tipopagamento);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.TipoPagamento.ToListAsync();
            resposta.Mensagem = "TipoPagamento Atualizado com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {

            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<TipoPagamentoModel>>> ListarTipoPagamento()
    {
        ResponseModel<List<TipoPagamentoModel>> resposta = new ResponseModel<List<TipoPagamentoModel>>();

        try
        {
            var tipopagamento = await _context.TipoPagamento.ToListAsync();

            resposta.Dados = tipopagamento;
            resposta.Mensagem = "Todos os TipoPagamento foram encontrados";
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