
using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Dto.Financ_Pagar;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Financ_Pagar;

public class Financ_PagarService : IFinanc_PagarInterface
{
    private readonly AppDbContext _context;
    public Financ_PagarService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseModel<Financ_PagarModel>> BuscarPorId(int idFinanc_Pagar)
    {
        ResponseModel<Financ_PagarModel> resposta = new ResponseModel<Financ_PagarModel>();
        try
        {
            var financ_pagar = await _context.Financ_Pagar.FirstOrDefaultAsync(x => x.Id == idFinanc_Pagar);
            if (financ_pagar == null)
            {
                resposta.Mensagem = "Nenhum Financ_Pagar encontrado";
                return resposta;
            }

            resposta.Dados = financ_pagar;
            resposta.Mensagem = "Financ_Pagar Encontrado";
            return resposta;
        }
        catch (Exception ex)
        {

            resposta.Mensagem = "Erro ao buscar Financ_Pagar";
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<Financ_PagarModel>>> Criar(Financ_PagarCreateDto financ_pagarCreateDto)
    {
        ResponseModel<List<Financ_PagarModel>> resposta = new ResponseModel<List<Financ_PagarModel>>();

        try
        {
            var financ_pagar = new Financ_PagarModel();

            financ_pagar.IdOrigem = financ_pagarCreateDto.IdOrigem;
            financ_pagar.NrDocto = financ_pagarCreateDto.NrDocto;
            financ_pagar.DataEmissao = financ_pagarCreateDto.DataEmissao;
            financ_pagar.DataVencimento = financ_pagarCreateDto.DataVencimento;
            financ_pagar.DataPagamento = financ_pagarCreateDto.DataPagamento;
            financ_pagar.ValorOriginal = financ_pagarCreateDto.ValorOriginal;
            financ_pagar.ValorPago = financ_pagarCreateDto.ValorPago;
            financ_pagar.Status = financ_pagarCreateDto.Status;
            financ_pagar.NotaFiscal = financ_pagarCreateDto.NotaFiscal;
            financ_pagar.Descricao = financ_pagarCreateDto.Descricao;
            financ_pagar.Parcela = financ_pagarCreateDto.Parcela;
            financ_pagar.Classificacao = financ_pagarCreateDto.Classificacao;
            financ_pagar.Desconto = financ_pagarCreateDto.Desconto;
            financ_pagar.Juros = financ_pagarCreateDto.Juros;
            financ_pagar.Multa = financ_pagarCreateDto.Multa;
            financ_pagar.Observacao = financ_pagarCreateDto.Observacao;
            financ_pagar.FornecedorId = financ_pagarCreateDto.FornecedorId;
            financ_pagar.CentroCustoId = financ_pagarCreateDto.CentroCustoId;
            financ_pagar.TipoPagamentoId = financ_pagarCreateDto.TipoPagamentoId;
            financ_pagar.FormaPagamentoId = financ_pagarCreateDto.FormaPagamentoId;
            financ_pagar.BancoId = financ_pagarCreateDto.BancoId;

            _context.Add(financ_pagar);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Financ_Pagar.ToListAsync();
            resposta.Mensagem = "Financ_Pagar criado com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<Financ_PagarModel>>> Delete(int idFinanc_Pagar)
    {
        ResponseModel<List<Financ_PagarModel>> resposta = new ResponseModel<List<Financ_PagarModel>>();

        try
        {
            var financ_pagar = await _context.Financ_Pagar.FirstOrDefaultAsync(x => x.Id == idFinanc_Pagar);
            if (financ_pagar == null)
            {
                resposta.Mensagem = "Nenhum Financ_Pagar encontrado";
                return resposta;
            }

            _context.Remove(financ_pagar);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Financ_Pagar.ToListAsync();
            resposta.Mensagem = "Financ_Pagar Excluido com sucesso";
            return resposta;

        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<Financ_PagarModel>>> Editar(Financ_PagarEdicaoDto financ_pagarEdicaoDto)
    {
        ResponseModel<List<Financ_PagarModel>> resposta = new ResponseModel<List<Financ_PagarModel>>();

        try
        {
            var financ_pagar = _context.Financ_Pagar.FirstOrDefault(x => x.Id == financ_pagarEdicaoDto.Id);
            if (financ_pagar == null)
            {
                resposta.Mensagem = "Financ_Pagar não encontrado";
                return resposta;
            }

            financ_pagar.Id = financ_pagarEdicaoDto.Id;
            financ_pagar.IdOrigem = financ_pagarEdicaoDto.IdOrigem;
            financ_pagar.NrDocto = financ_pagarEdicaoDto.NrDocto;
            financ_pagar.DataEmissao = financ_pagarEdicaoDto.DataEmissao;
            financ_pagar.DataVencimento = financ_pagarEdicaoDto.DataVencimento;
            financ_pagar.DataPagamento = financ_pagarEdicaoDto.DataPagamento;
            financ_pagar.ValorOriginal = financ_pagarEdicaoDto.ValorOriginal;
            financ_pagar.ValorPago = financ_pagarEdicaoDto.ValorPago;
            financ_pagar.Status = financ_pagarEdicaoDto.Status;
            financ_pagar.NotaFiscal = financ_pagarEdicaoDto.NotaFiscal;
            financ_pagar.Descricao = financ_pagarEdicaoDto.Descricao;
            financ_pagar.Parcela = financ_pagarEdicaoDto.Parcela;
            financ_pagar.Classificacao = financ_pagarEdicaoDto.Classificacao;
            financ_pagar.Desconto = financ_pagarEdicaoDto.Desconto;
            financ_pagar.Juros = financ_pagarEdicaoDto.Juros;
            financ_pagar.Multa = financ_pagarEdicaoDto.Multa;
            financ_pagar.Observacao = financ_pagarEdicaoDto.Observacao;
            financ_pagar.FornecedorId = financ_pagarEdicaoDto.FornecedorId;
            financ_pagar.CentroCustoId = financ_pagarEdicaoDto.CentroCustoId;
            financ_pagar.TipoPagamentoId = financ_pagarEdicaoDto.TipoPagamentoId;
            financ_pagar.FormaPagamentoId = financ_pagarEdicaoDto.FormaPagamentoId;
            financ_pagar.BancoId = financ_pagarEdicaoDto.BancoId;

            _context.Update(financ_pagar);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Financ_Pagar.ToListAsync();
            resposta.Mensagem = "Financ_Pagar Atualizado com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<Financ_PagarModel>>> Listar()
    {
        ResponseModel<List<Financ_PagarModel>> resposta = new ResponseModel<List<Financ_PagarModel>>();

        try
        {
            var financ_pagar = await _context.Financ_Pagar.ToListAsync();

            resposta.Dados = financ_pagar;
            resposta.Mensagem = "Todos os Financ_Pagar foram encontrados";
            return resposta;
        }
        catch (Exception e)
        {
            resposta.Mensagem = e.Message;
            resposta.Status = false;
            return resposta;
        }
    }


    public async Task<ResponseModel<Financ_PagarModel>> BaixarPagamento(int idFinanc_Pagar, decimal valorPago, DateTime? dataPagamento = null)
    {
        ResponseModel<Financ_PagarModel> resposta = new ResponseModel<Financ_PagarModel>();

        try
        {
            var financ_pagar = await _context.Financ_Pagar.FirstOrDefaultAsync(x => x.Id == idFinanc_Pagar);
            if (financ_pagar == null)
            {
                resposta.Mensagem = "Pagamento não encontrado";
                return resposta;
            }

            // Atualiza o valor pago e define a data de pagamento
            financ_pagar.ValorPago += valorPago;
            financ_pagar.DataPagamento = dataPagamento ?? DateTime.Now;

            // Calcula o valor restante
            decimal? valorRestante = financ_pagar.ValorOriginal - financ_pagar.ValorPago;

            if (valorRestante > 0)
            {
                // Pagamento parcial: cria novo lançamento com o valor restante
                financ_pagar.Status = "Parcialmente Pago";

                // Cria novo lançamento para o valor pendente
                var novoLancamento = new Financ_PagarModel
                {
                    IdOrigem = financ_pagar.IdOrigem,
                    NrDocto = financ_pagar.NrDocto,
                    DataEmissao = financ_pagar.DataEmissao,
                    DataVencimento = financ_pagar.DataVencimento, // mantemos a mesma data de vencimento
                    ValorOriginal = valorRestante,
                    ValorPago = 0,
                    Status = "Em Aberto",
                    NotaFiscal = financ_pagar.NotaFiscal,
                    Descricao = financ_pagar.Descricao,
                    Parcela = financ_pagar.Parcela,
                    Classificacao = financ_pagar.Classificacao,
                    Desconto = financ_pagar.Desconto,
                    Juros = financ_pagar.Juros,
                    Multa = financ_pagar.Multa,
                    Observacao = financ_pagar.Observacao,
                    FornecedorId = financ_pagar.FornecedorId,
                    CentroCustoId = financ_pagar.CentroCustoId,
                    TipoPagamentoId = financ_pagar.TipoPagamentoId,
                    FormaPagamentoId = financ_pagar.FormaPagamentoId,
                    BancoId = financ_pagar.BancoId
                };

                _context.Add(novoLancamento);
            }
            else
            {
                // Pagamento completo: marca como "Pago"
                financ_pagar.Status = "Pago";
            }

            _context.Update(financ_pagar);
            await _context.SaveChangesAsync();

            resposta.Dados = financ_pagar;
            resposta.Mensagem = "Pagamento baixado com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }


}