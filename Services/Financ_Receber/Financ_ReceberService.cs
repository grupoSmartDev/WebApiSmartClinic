
using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Dto.Financ_Receber;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Financ_Receber;

public class Financ_ReceberService : IFinanc_ReceberInterface
{
    private readonly AppDbContext _context;
    public Financ_ReceberService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseModel<Financ_ReceberModel>> BuscarPorId(int idFinanc_Receber)
    {
        ResponseModel<Financ_ReceberModel> resposta = new ResponseModel<Financ_ReceberModel>();
        try
        {
            var financ_receber = await _context.Financ_Receber.FirstOrDefaultAsync(x => x.Id == idFinanc_Receber);
            if (financ_receber == null)
            {
                resposta.Mensagem = "Nenhum Financ_Receber encontrado";
                return resposta;
            }

            resposta.Dados = financ_receber;
            resposta.Mensagem = "Financ_Receber Encontrado";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = "Erro ao buscar Financ_Receber";
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<Financ_ReceberModel>>> Criar(Financ_ReceberCreateDto financ_receberCreateDto)
    {
        ResponseModel<List<Financ_ReceberModel>> resposta = new ResponseModel<List<Financ_ReceberModel>>();

        try
        {
            var financ_receber = new Financ_ReceberModel();

            _context.Add(financ_receber);
            await _context.SaveChangesAsync();

            financ_receber.IdOrigem = financ_receberCreateDto.IdOrigem;
            financ_receber.NrDocto = financ_receberCreateDto.NrDocto;
            financ_receber.DataEmissao = financ_receberCreateDto.DataEmissao;
            financ_receber.DataVencimento = financ_receberCreateDto.DataVencimento;
            financ_receber.DataPagamento = financ_receberCreateDto.DataPagamento;
            financ_receber.ValorOriginal = financ_receberCreateDto.ValorOriginal;
            financ_receber.ValorPago = financ_receberCreateDto.ValorPago;
            financ_receber.Status = financ_receberCreateDto.Status;
            financ_receber.NotaFiscal = financ_receberCreateDto.NotaFiscal;
            financ_receber.Descricao = financ_receberCreateDto.Descricao;
            financ_receber.Parcela = financ_receberCreateDto.Parcela;
            financ_receber.Classificacao = financ_receberCreateDto.Classificacao;
            financ_receber.Desconto = financ_receberCreateDto.Desconto;
            financ_receber.Juros = financ_receberCreateDto.Juros;
            financ_receber.Multa = financ_receberCreateDto.Multa;
            financ_receber.Observacao = financ_receberCreateDto.Observacao;
            financ_receber.FornecedorId = financ_receberCreateDto.FornecedorId;
            financ_receber.CentroCustoId = financ_receberCreateDto.CentroCustoId;
            financ_receber.TipoPagamentoId = financ_receberCreateDto.TipoPagamentoId;
            financ_receber.FormaPagamentoId = financ_receberCreateDto.FormaPagamentoId;
            financ_receber.BancoId = financ_receberCreateDto.BancoId;

            resposta.Dados = await _context.Financ_Receber.ToListAsync();
            resposta.Mensagem = "Financ_Receber criado com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<Financ_ReceberModel>>> Delete(int idFinanc_Receber)
    {
        ResponseModel<List<Financ_ReceberModel>> resposta = new ResponseModel<List<Financ_ReceberModel>>();

        try
        {
            var financ_receber = await _context.Financ_Receber.FirstOrDefaultAsync(x => x.Id == idFinanc_Receber);
            if (financ_receber == null)
            {
                resposta.Mensagem = "Nenhum Financ_Receber encontrado";
                return resposta;
            }

            _context.Remove(financ_receber);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Financ_Receber.ToListAsync();
            resposta.Mensagem = "Financ_Receber Excluido com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<Financ_ReceberModel>>> Editar(Financ_ReceberEdicaoDto financ_receberEdicaoDto)
    {
        ResponseModel<List<Financ_ReceberModel>> resposta = new ResponseModel<List<Financ_ReceberModel>>();

        try
        {
            var financ_receber = _context.Financ_Receber.FirstOrDefault(x => x.Id == financ_receberEdicaoDto.Id);
            if (financ_receber == null)
            {
                resposta.Mensagem = "Financ_Receber não encontrado";
                return resposta;
            }

            financ_receber.Id = financ_receberEdicaoDto.Id;
            financ_receber.IdOrigem = financ_receberEdicaoDto.IdOrigem;
            financ_receber.NrDocto = financ_receberEdicaoDto.NrDocto;
            financ_receber.DataEmissao = financ_receberEdicaoDto.DataEmissao;
            financ_receber.DataVencimento = financ_receberEdicaoDto.DataVencimento;
            financ_receber.DataPagamento = financ_receberEdicaoDto.DataPagamento;
            financ_receber.ValorOriginal = financ_receberEdicaoDto.ValorOriginal;
            financ_receber.ValorPago = financ_receberEdicaoDto.ValorPago;
            financ_receber.Status = financ_receberEdicaoDto.Status;
            financ_receber.NotaFiscal = financ_receberEdicaoDto.NotaFiscal;
            financ_receber.Descricao = financ_receberEdicaoDto.Descricao;
            financ_receber.Parcela = financ_receberEdicaoDto.Parcela;
            financ_receber.Classificacao = financ_receberEdicaoDto.Classificacao;
            financ_receber.Desconto = financ_receberEdicaoDto.Desconto;
            financ_receber.Juros = financ_receberEdicaoDto.Juros;
            financ_receber.Multa = financ_receberEdicaoDto.Multa;
            financ_receber.Observacao = financ_receberEdicaoDto.Observacao;
            financ_receber.FornecedorId = financ_receberEdicaoDto.FornecedorId;
            financ_receber.CentroCustoId = financ_receberEdicaoDto.CentroCustoId;
            financ_receber.TipoPagamentoId = financ_receberEdicaoDto.TipoPagamentoId;
            financ_receber.FormaPagamentoId = financ_receberEdicaoDto.FormaPagamentoId;
            financ_receber.BancoId = financ_receberEdicaoDto.BancoId;

            _context.Update(financ_receber);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Financ_Receber.ToListAsync();
            resposta.Mensagem = "Financ_Receber Atualizado com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<Financ_ReceberModel>>> Listar()
    {
        ResponseModel<List<Financ_ReceberModel>> resposta = new ResponseModel<List<Financ_ReceberModel>>();

        try
        {
            var financ_receber = await _context.Financ_Receber.ToListAsync();

            resposta.Dados = financ_receber;
            resposta.Mensagem = "Todos os Financ_Receber foram encontrados";
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