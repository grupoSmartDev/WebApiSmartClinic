using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Dto.Financ_Pagar;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Services.Banco;
using WebApiSmartClinic.Dto.Financ_Receber;
using System.Linq;

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
            // Criação do cabeçalho (pai)
            var financ_pagar = new Financ_PagarModel
            {
                IdOrigem = financ_pagarCreateDto.IdOrigem,
                NrDocto = financ_pagarCreateDto.NrDocto,
                DataEmissao = financ_pagarCreateDto.DataEmissao,
                ValorOriginal = financ_pagarCreateDto.ValorOriginal,
                ValorPago = financ_pagarCreateDto.ValorPago,
                Valor = financ_pagarCreateDto.Valor,
                Status = financ_pagarCreateDto.Status,
                NotaFiscal = financ_pagarCreateDto.NotaFiscal,
                Descricao = financ_pagarCreateDto.Descricao,
                Parcela = financ_pagarCreateDto.Parcela,
                Classificacao = financ_pagarCreateDto.Classificacao,
                Observacao = financ_pagarCreateDto.Observacao,
                FornecedorId = financ_pagarCreateDto.FornecedorId,
                CentroCustoId = financ_pagarCreateDto.CentroCustoId,
                BancoId = financ_pagarCreateDto.BancoId
            };

            _context.Add(financ_pagar);
            await _context.SaveChangesAsync();

            // Adicionando subitens (filhos)
            if (financ_pagarCreateDto.subFinancPagar != null && financ_pagarCreateDto.subFinancPagar.Any())
            {
                foreach (var parcela in financ_pagarCreateDto.subFinancPagar)
                {
                    var subItem = new Financ_PagarSubModel
                    {
                        financPagarId = financ_pagar.Id, // Relaciona com o pai
                        Parcela = parcela.Parcela,
                        Valor = parcela.Valor,
                        TipoPagamentoId = parcela.TipoPagamentoId,
                        FormaPagamentoId = parcela.FormaPagamentoId,
                        DataPagamento = parcela.DataPagamento,
                        Desconto = parcela.Desconto,
                        Juros = parcela.Juros,
                        Multa = parcela.Multa,
                        DataVencimento = parcela.DataVencimento,
                        Observacao = parcela.Observacao
                    };

                    _context.Add(subItem);
                }

                await _context.SaveChangesAsync();
            }

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

            financ_pagar.IdOrigem = financ_pagarEdicaoDto.IdOrigem;
            financ_pagar.NrDocto = financ_pagarEdicaoDto.NrDocto;
            financ_pagar.DataEmissao = financ_pagarEdicaoDto.DataEmissao;
            financ_pagar.ValorOriginal = financ_pagarEdicaoDto.ValorOriginal;
            financ_pagar.ValorPago = financ_pagarEdicaoDto.ValorPago;
            financ_pagar.Valor = financ_pagarEdicaoDto.Valor;
            financ_pagar.Status = financ_pagarEdicaoDto.Status;
            financ_pagar.NotaFiscal = financ_pagarEdicaoDto.NotaFiscal;
            financ_pagar.Descricao = financ_pagarEdicaoDto.Descricao;
            financ_pagar.Parcela = financ_pagarEdicaoDto.Parcela;
            financ_pagar.Classificacao = financ_pagarEdicaoDto.Classificacao;
            financ_pagar.Observacao = financ_pagarEdicaoDto.Observacao;
            financ_pagar.FornecedorId = financ_pagarEdicaoDto.FornecedorId;
            financ_pagar.CentroCustoId = financ_pagarEdicaoDto.CentroCustoId;
            financ_pagar.BancoId = financ_pagarEdicaoDto.BancoId;

            _context.Update(financ_pagar);
            await _context.SaveChangesAsync();

            // Adicionando subitens (filhos)
            if (financ_pagarEdicaoDto.subFinancPagar != null && financ_pagarEdicaoDto.subFinancPagar.Any())
            {
                foreach (var parcela in financ_pagarEdicaoDto.subFinancPagar)
                {
                    var subItem = new Financ_PagarSubEdicaoDto
                    {
                        financPagarId = financ_pagar.Id, // Relaciona com o pai
                        Parcela = parcela.Parcela,
                        Valor = parcela.Valor,
                        TipoPagamentoId = parcela.TipoPagamentoId,
                        FormaPagamentoId = parcela.FormaPagamentoId,
                        DataPagamento = parcela.DataPagamento,
                        Desconto = parcela.Desconto,
                        Juros = parcela.Juros,
                        Multa = parcela.Multa,
                        DataVencimento = parcela.DataVencimento,
                        Observacao = parcela.Observacao
                    };

                    _context.Update(subItem);
                }

                await _context.SaveChangesAsync();
            }

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

    public async Task<ResponseModel<Financ_PagarModel>> BaixarParcela(int parcelaId, decimal valorPago)
    {
        var resposta = new ResponseModel<Financ_PagarModel>();
        try
        {
            var parcela = await _context.Financ_PagarSub.FirstOrDefaultAsync(x => x.Id == parcelaId);

            if (parcela == null)
            {
                resposta.Mensagem = "Parcela não encontrada.";
                resposta.Status = false;
                return resposta;
            }

            if (parcela.DataPagamento != null)
            {
                resposta.Mensagem = "Parcela já foi paga.";
                resposta.Status = false;
                return resposta;
            }

            if (valorPago < parcela.Valor)
            {
                // Valor pago é menor que o valor da parcela
                var valorRestante = parcela.Valor - valorPago;

                // Criar uma nova parcela com o valor restante
                var novaParcela = new Financ_PagarSubModel
                {
                    financPagarId = parcela.financPagarId,
                    Parcela = parcela.Parcela,
                    Valor = valorRestante,
                    TipoPagamentoId = parcela.TipoPagamentoId,
                    FormaPagamentoId = parcela.FormaPagamentoId,
                    DataVencimento = DateTime.Now.AddMonths(1), // Ajuste conforme necessário
                    Observacao = "Parcela gerada automaticamente devido a pagamento parcial."
                };
                _context.Financ_PagarSub.Add(novaParcela);
            }
            else if (valorPago > parcela.Valor)
            {
                resposta.Mensagem = "Valor pago é maior que o valor da parcela.";
                resposta.Status = false;
                return resposta;
            }

            parcela.DataPagamento = DateTime.Now;
            parcela.ValorPago = valorPago;

            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Financ_Pagar.FirstOrDefaultAsync(x => x.Id == parcela.financPagarId);
            resposta.Mensagem = "Parcela baixada com sucesso.";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<string>> AgruparParcelas(int idPai, List<int> parcelasFilhasIds, decimal valorPago)
    {
        var resposta = new ResponseModel<string>();
        try
        {
            var parcelasFilhas = await _context.Financ_PagarSub
                .Where(x => parcelasFilhasIds.Contains((int)x.Id) && x.DataPagamento == null)
                .ToListAsync();

            if (!parcelasFilhas.Any())
            {
                resposta.Mensagem = "Nenhuma parcela válida encontrada para agrupamento.";
                resposta.Status = false;
                return resposta;
            }

            decimal totalValor = parcelasFilhas.Sum(p => p.Valor);

            if (valorPago < totalValor)
            {
                var valorRestante = totalValor - valorPago;
                resposta.Mensagem = $"Valor pago é insuficiente. Faltam {valorRestante:C}.";
                resposta.Status = false;
                return resposta;
            }
            else if (valorPago > totalValor)
            {
                resposta.Mensagem = "Valor pago é maior que o total das parcelas.";
                resposta.Status = false;
                return resposta;
            }

            foreach (var parcela in parcelasFilhas)
            {
                parcela.DataPagamento = DateTime.Now;
                parcela.ValorPago = parcela.Valor;
            }

            await _context.SaveChangesAsync();

            resposta.Mensagem = "Parcelas agrupadas e baixadas com sucesso.";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<string>> EstornarParcela(int parcelaId)
    {
        var resposta = new ResponseModel<string>();
        try
        {
            var parcela = await _context.Financ_PagarSub.FirstOrDefaultAsync(x => x.Id == parcelaId);

            if (parcela == null || parcela.DataPagamento == null)
            {
                resposta.Mensagem = "Parcela não encontrada ou já está em aberto.";
                resposta.Status = false;
                return resposta;
            }

            parcela.DataPagamento = null;
            parcela.ValorPago = 0;

            await _context.SaveChangesAsync();

            resposta.Mensagem = "Baixa da parcela estornada com sucesso.";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<string>> EstornarAgrupamento(List<int> parcelasIds)
    {
        var resposta = new ResponseModel<string>();
        try
        {
            var parcelas = await _context.Financ_PagarSub
                .Where(x => parcelasIds.Contains((int)x.Id) && x.DataPagamento != null)
                .ToListAsync();

            if (!parcelas.Any())
            {
                resposta.Mensagem = "Nenhuma parcela válida encontrada para estorno.";
                resposta.Status = false;
                return resposta;
            }

            foreach (var parcela in parcelas)
            {
                parcela.DataPagamento = null;
                parcela.ValorPago = 0;
            }

            await _context.SaveChangesAsync();

            resposta.Mensagem = "Estorno de agrupamento realizado com sucesso.";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<Financ_PagarSubModel>>> ObterContasAbertas()
    {
        var resposta = new ResponseModel<List<Financ_PagarSubModel>>();
        try
        {
            var contasAbertas = await _context.Financ_PagarSub
                .Where(x => x.DataPagamento == null)
                .ToListAsync();

            resposta.Dados = contasAbertas;
            resposta.Mensagem = "Contas em aberto obtidas com sucesso.";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    //public async Task<ResponseModel<Financ_PagarModel>> BaixarPagamento(int idFinanc_Pagar, decimal valorPago, DateTime? dataPagamento = null)
    //{
    //    var resposta = new ResponseModel<Financ_PagarModel>();

    //    try
    //    {
    //        var financ_pagar = await _context.Financ_Pagar.FirstOrDefaultAsync(x => x.Id == idFinanc_Pagar);
    //        if (financ_pagar == null)
    //        {
    //            resposta.Mensagem = "Pagamento não encontrado.";
    //            return resposta;
    //        }

    //        // Validação de saldo suficiente
    //        BancoService _bancoService = new BancoService(_context);

    //        //BRUNAO VERIFICAR

    //        //var bancoResposta = await _bancoService.DebitarSaldo(financ_pagar.BancoId, valorPago);
    //        //if (!bancoResposta.Status)
    //        //{
    //        //    resposta.Mensagem = bancoResposta.Mensagem;
    //        //    resposta.Status = false;
    //        //    return resposta;
    //        //}

    //        // Atualização do valor pago e status do pagamento
    //        financ_pagar.ValorPago += valorPago;
    //        //BRUNAO VERIFICAR FOI ALTERADO A DATA DE PAGAMENTO AGORA QUE TEMOS UM SUB, VAI MUDAR A LOGICA? 
    //        //financ_pagar.DataPagamento = dataPagamento ?? DateTime.Now;
    //        financ_pagar.Status = financ_pagar.ValorPago >= financ_pagar.ValorOriginal ? "Pago" : "Parcialmente Pago";

    //        if (financ_pagar.ValorPago < financ_pagar.ValorOriginal)
    //        {
    //            // Criar novo lançamento para o valor restante
    //            var valorRestante = financ_pagar.ValorOriginal - financ_pagar.ValorPago;
    //            var novoLancamento = new Financ_PagarModel
    //            {
    //                IdOrigem = financ_pagar.IdOrigem,
    //                NrDocto = financ_pagar.NrDocto,
    //                DataEmissao = financ_pagar.DataEmissao,
    //                //DataVencimento = financ_pagar.DataVencimento,
    //                ValorOriginal = valorRestante,
    //                ValorPago = 0,
    //                Status = "Em Aberto",
    //                NotaFiscal = financ_pagar.NotaFiscal,
    //                Descricao = financ_pagar.Descricao,
    //                FornecedorId = financ_pagar.FornecedorId,
    //                BancoId = financ_pagar.BancoId,
    //                CentroCustoId = financ_pagar.CentroCustoId
    //            };

    //            _context.Add(novoLancamento);
    //        }

    //        _context.Update(financ_pagar);
    //        await _context.SaveChangesAsync();

    //        resposta.Dados = financ_pagar;
    //        resposta.Mensagem = "Pagamento baixado com sucesso.";
    //        return resposta;
    //    }
    //    catch (Exception ex)
    //    {
    //        resposta.Mensagem = ex.Message;
    //        resposta.Status = false;
    //        return resposta;
    //    }
    //}

    //public async Task<ResponseModel<Financ_PagarModel>> EstornarPagamento(int idFinanc_Pagar)
    //{
    //    var resposta = new ResponseModel<Financ_PagarModel>();

    //    try
    //    {
    //        var financ_pagar = await _context.Financ_Pagar.FirstOrDefaultAsync(x => x.Id == idFinanc_Pagar);
    //        if (financ_pagar == null)
    //        {
    //            resposta.Mensagem = "Pagamento não encontrado.";
    //            return resposta;
    //        }

    //        // Crédita o valor de volta ao saldo da conta bancária
    //        BancoService _bancoService = new BancoService(_context);
    //        await _bancoService.CreditarSaldo((int)financ_pagar.BancoId, (decimal)financ_pagar.ValorPago);

    //        // Zera o valor pago e redefine o status
    //        financ_pagar.ValorPago = 0;
    //        // financ_pagar.DataPagamento = null;
    //        financ_pagar.Status = "Em Aberto";

    //        // Registro de histórico do estorno
    //        await _context.HistoricoTransacao.AddAsync(new HistoricoTransacaoModel
    //        {
    //            //BancoId = financ_pagar.BancoId,
    //            //Valor = financ_pagar.ValorPago,
    //            TipoTransacao = "Estorno",
    //            DataTransacao = DateTime.Now
    //        });

    //        _context.Update(financ_pagar);
    //        await _context.SaveChangesAsync();

    //        resposta.Dados = financ_pagar;
    //        resposta.Mensagem = "Pagamento estornado com sucesso.";
    //        return resposta;
    //    }
    //    catch (Exception ex)
    //    {
    //        resposta.Mensagem = ex.Message;
    //        resposta.Status = false;
    //        return resposta;
    //    }
    //}

    // ** NECESSITA TER A PARAMETRIZAÇÃO CRIADA ** //
    //public decimal CalcularValorComJurosMultas(Decimal valorOriginal, DateTime dataVencimento, DateTime dataPagamento)
    //{
    //    decimal valorFinal = valorOriginal;
    //    if (dataPagamento > dataVencimento)
    //    {
    //        // Aplica juros e multa
    //        var diasAtraso = (dataPagamento - dataVencimento).Days;
    //        var juros = diasAtraso * 0.01m * valorOriginal; // 1% de juros ao dia
    //        var multa = valorOriginal * 0.05m; // Multa de 5%
    //        valorFinal += juros + multa;
    //    }
    //    else if (dataPagamento < dataVencimento)
    //    {
    //        // Aplica desconto para pagamento antecipado
    //        var desconto = valorOriginal * 0.02m; // 2% de desconto
    //        valorFinal -= desconto;
    //    }
    //    return valorFinal;
    //}

}