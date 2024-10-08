
using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Dto.FormaPagamento;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.FormaPagamento;

public class FormaPagamentoService : IFormaPagamentoInterface
{
    private readonly AppDbContext _context;
    public FormaPagamentoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseModel<FormaPagamentoModel>> BuscarFormaPagamentoPorId(int idFormaPagamento)
    {
        ResponseModel<FormaPagamentoModel> resposta = new ResponseModel<FormaPagamentoModel>();

        try
        {
            var formapagamento = await _context.FormaPagamento.FirstOrDefaultAsync(x => x.Id == idFormaPagamento);
            if (formapagamento == null)
            {
                resposta.Mensagem = "Nenhuma forma de pagamento encontrada";
                return resposta;
            }

            resposta.Dados = formapagamento;
            resposta.Mensagem = "Forma de pagamento encontrada";

            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = "Erro ao buscar forma de pagamento";
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<FormaPagamentoModel>>> CriarFormaPagamento(FormaPagamentoCreateDto formapagamentoCreateDto)
    {
        ResponseModel<List<FormaPagamentoModel>> resposta = new ResponseModel<List<FormaPagamentoModel>>();

        try
        {
            var formapagamento = new FormaPagamentoModel();

            formapagamento.Descricao = formapagamentoCreateDto.Descricao;
            formapagamento.Parcelas = formapagamentoCreateDto.Parcelas;

            _context.Add(formapagamento);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.FormaPagamento.ToListAsync();
            resposta.Mensagem = "Forma de pagamento criada com sucesso";

            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<FormaPagamentoModel>>> DeleteFormaPagamento(int idFormaPagamento)
    {
        ResponseModel<List<FormaPagamentoModel>> resposta = new ResponseModel<List<FormaPagamentoModel>>();

        try
        {
            var formapagamento = await _context.FormaPagamento.FirstOrDefaultAsync(x => x.Id == idFormaPagamento);
            if (formapagamento == null)
            {
                resposta.Mensagem = "Nenhuma forma de pagamento encontrada";
                return resposta;
            }

            _context.Remove(formapagamento);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.FormaPagamento.ToListAsync();
            resposta.Mensagem = "Forma de pagamento excluida com sucesso";

            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<FormaPagamentoModel>>> EditarFormaPagamento(FormaPagamentoEdicaoDto formapagamentoEdicaoDto)
    {
        ResponseModel<List<FormaPagamentoModel>> resposta = new ResponseModel<List<FormaPagamentoModel>>();

        try
        {
            var formapagamento = _context.FormaPagamento.FirstOrDefault(x => x.Id == formapagamentoEdicaoDto.Id);
            if (formapagamento == null)
            {
                resposta.Mensagem = "Forma de pagamento não encontrada";
                return resposta;
            }

            formapagamento.Id = formapagamentoEdicaoDto.Id;
            formapagamento.Descricao = formapagamentoEdicaoDto.Descricao;
            formapagamento.Parcelas = formapagamentoEdicaoDto.Parcelas;

            _context.Update(formapagamento);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.FormaPagamento.ToListAsync();
            resposta.Mensagem = "Forma de pagamento atualizada com sucesso";

            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<FormaPagamentoModel>>> ListarFormaPagamento()
    {
        ResponseModel<List<FormaPagamentoModel>> resposta = new ResponseModel<List<FormaPagamentoModel>>();

        try
        {
            var formapagamento = await _context.FormaPagamento.ToListAsync();

            resposta.Dados = formapagamento;
            resposta.Mensagem = "Todas as formas de pagamentos foram encontradas";
            
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