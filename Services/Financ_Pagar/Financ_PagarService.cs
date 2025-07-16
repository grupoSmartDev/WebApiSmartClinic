using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Dto.Financ_Pagar;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Services.Banco;
using WebApiSmartClinic.Dto.Financ_Receber;
using System.Linq;
using WebApiSmartClinic.Helpers;

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

    public async Task<ResponseModel<List<Financ_PagarModel>>> Criar(Financ_PagarCreateDto financ_pagarCreateDto, int pageNumber = 1, int pageSize = 10)
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
                BancoId = financ_pagarCreateDto.BancoId,
                PacienteId = financ_pagarCreateDto.PacienteId,
                TipoPagamentoId = financ_pagarCreateDto.TipoPagamentoId
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
                        //TipoPagamentoId = parcela.TipoPagamentoId,
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

            var query = _context.Financ_Pagar
                .Include(x => x.subFinancPagar)
                .AsQueryable();

            resposta = await PaginationHelper.PaginateAsync(query, pageNumber, pageSize);
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

    public async Task<ResponseModel<List<Financ_PagarModel>>> Delete(int idFinanc_Pagar, int pageNumber = 1, int pageSize = 10)
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

            var query = _context.Financ_Pagar
                .Include(x => x.subFinancPagar)
                .AsQueryable();

            resposta = await PaginationHelper.PaginateAsync(query, pageNumber, pageSize);
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

    public async Task<ResponseModel<List<Financ_PagarModel>>> Editar(Financ_PagarEdicaoDto financ_pagarEdicaoDto, int pageNumber = 1, int pageSize = 10)
    {
        ResponseModel<List<Financ_PagarModel>> resposta = new ResponseModel<List<Financ_PagarModel>>();
        try
        {
            var financ_pagar = await _context.Financ_Pagar
                .Include(x => x.subFinancPagar)
                .FirstOrDefaultAsync(x => x.Id == financ_pagarEdicaoDto.Id);

            if (financ_pagar == null)
            {
                resposta.Mensagem = "Financ_Pagar não encontrado";
                return resposta;
            }

            // Atualiza dados principais
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
            financ_pagar.TipoPagamentoId = financ_pagarEdicaoDto.TipoPagamentoId;

            // Atualiza subitens
            if (financ_pagarEdicaoDto.subFinancPagar != null)
            {
                foreach (var parcelaDto in financ_pagarEdicaoDto.subFinancPagar)
                {
                    // Procura se já existe o subitem
                    var subItemExistente = financ_pagar.subFinancPagar
                        .FirstOrDefault(x => x.Id == parcelaDto.Id);

                    if (subItemExistente != null)
                    {
                        // Atualiza o item existente
                        subItemExistente.Parcela = parcelaDto.Parcela;
                        subItemExistente.Valor = parcelaDto.Valor;
                        subItemExistente.TipoPagamentoId = parcelaDto.TipoPagamentoId;
                        subItemExistente.FormaPagamentoId = parcelaDto.FormaPagamentoId;
                        subItemExistente.DataPagamento = parcelaDto.DataPagamento;
                        subItemExistente.Desconto = parcelaDto.Desconto;
                        subItemExistente.Juros = parcelaDto.Juros;
                        subItemExistente.Multa = parcelaDto.Multa;
                        subItemExistente.DataVencimento = parcelaDto.DataVencimento;
                        subItemExistente.Observacao = parcelaDto.Observacao;
                    }
                    else
                    {
                        // Cria novo item se não existir
                        var novoSubItem = new Financ_PagarSubModel
                        {
                            financPagarId = financ_pagar.Id,
                            Parcela = parcelaDto.Parcela,
                            Valor = parcelaDto.Valor,
                            TipoPagamentoId = parcelaDto.TipoPagamentoId,
                            FormaPagamentoId = parcelaDto.FormaPagamentoId,
                            DataPagamento = parcelaDto.DataPagamento,
                            Desconto = parcelaDto.Desconto,
                            Juros = parcelaDto.Juros,
                            Multa = parcelaDto.Multa,
                            DataVencimento = parcelaDto.DataVencimento,
                            Observacao = parcelaDto.Observacao
                        };
                        financ_pagar.subFinancPagar.Add(novoSubItem);
                    }
                }

                // Remove apenas os itens que não estão mais na lista
                var idsParaManterAtivos = financ_pagarEdicaoDto.subFinancPagar.Select(x => x.Id).ToList();
                var itensParaRemover = financ_pagar.subFinancPagar
                    .Where(x => x.Id > 0 && !idsParaManterAtivos.Contains(x.Id))
                    .ToList();

                foreach (var itemRemover in itensParaRemover)
                {
                    _context.Remove(itemRemover);
                }
            }

            await _context.SaveChangesAsync();

            var query = _context.Financ_Pagar
                .Include(x => x.subFinancPagar)
                .AsQueryable();

            resposta = await PaginationHelper.PaginateAsync(query, pageNumber, pageSize);
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

    public async Task<ResponseModel<List<Financ_PagarModel>>> Listar(int pageNumber = 1, int pageSize = 10, int? codigoFiltro = null, string? descricaoFiltro = null, DateTime? dataEmissaoInicio = null, DateTime? dataEmissaoFim = null,
        decimal? valorMinimoFiltro = null, decimal? valorMaximoFiltro = null, int? parcelaNumeroFiltro = null, DateTime? vencimentoInicio = null, DateTime? vencimentoFim = null, bool paginar = true)
    {
        ResponseModel<List<Financ_PagarModel>> resposta = new ResponseModel<List<Financ_PagarModel>>();

        try
        {
            var query = _context.Financ_Pagar
                .Include(x => x.subFinancPagar)
                .AsQueryable();

            query = query.Where(x =>
                (!codigoFiltro.HasValue || x.Id == codigoFiltro) &&
                (string.IsNullOrEmpty(descricaoFiltro) || x.Descricao.Contains(descricaoFiltro)) &&
                (!dataEmissaoInicio.HasValue || x.DataEmissao >= dataEmissaoInicio.Value) &&
                (!dataEmissaoFim.HasValue || x.DataEmissao <= dataEmissaoFim.Value) &&
                (!valorMinimoFiltro.HasValue || x.Valor >= valorMinimoFiltro) &&
                (!valorMaximoFiltro.HasValue || x.Valor <= valorMaximoFiltro)
            );

            if (parcelaNumeroFiltro.HasValue || vencimentoInicio.HasValue || vencimentoFim.HasValue)
            {
                query = query.Where(x => x.subFinancPagar.Any(p =>
                    (!parcelaNumeroFiltro.HasValue || p.Parcela == parcelaNumeroFiltro) &&
                    (!vencimentoInicio.HasValue || p.DataVencimento >= vencimentoInicio) &&
                    (!vencimentoFim.HasValue || p.DataVencimento <= vencimentoFim)
                ));
            }

            query = query.OrderBy(x => x.Id);

            resposta = paginar ? await PaginationHelper.PaginateAsync(query, pageNumber, pageSize) : new ResponseModel<List<Financ_PagarModel>> { Dados = await query.ToListAsync() };
            resposta.Mensagem = "Todos os contas à pagar foram encontrados";
            return resposta;
        }
        catch (Exception e)
        {
            resposta.Mensagem = e.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<Financ_PagarSubModel>>> ListarSintetico(int pageNumber = 1, int pageSize = 10, int? idPaiFiltro = null, int? parcelaNumeroFiltro = null, string? dataBaseFiltro = null, DateTime? dataFiltroInicio = null, DateTime? dataFiltroFim = null, bool paginar = true)
    {
        ResponseModel<List<Financ_PagarSubModel>> resposta = new ResponseModel<List<Financ_PagarSubModel>>();

        try
        {
            var query = _context.Financ_PagarSub
                .Include(x => x.FinancPagar)
                .AsQueryable();

            query = query.Where(p =>
                (!idPaiFiltro.HasValue || p.financPagarId == idPaiFiltro) &&
                (!parcelaNumeroFiltro.HasValue || p.Id == parcelaNumeroFiltro)
            );

            if (dataBaseFiltro == "V")
            {
                query = query.Where(p =>
                    (!dataFiltroInicio.HasValue || p.DataVencimento >= Funcoes.FormataDataTimeFiltros(dataFiltroInicio, "I")) &&
                    (!dataFiltroFim.HasValue || p.DataVencimento <= Funcoes.FormataDataTimeFiltros(dataFiltroFim, "F"))
                );
            }

            if (dataBaseFiltro == "E")
            {
                query = query.Where(p =>
                    (!dataFiltroInicio.HasValue || p.FinancPagar!.DataEmissao >= Funcoes.FormataDataTimeFiltros(dataFiltroInicio, "I")) &&
                    (!dataFiltroFim.HasValue || p.FinancPagar!.DataEmissao <= Funcoes.FormataDataTimeFiltros(dataFiltroFim, "F"))
                );
            }

            if (dataBaseFiltro == "P")
            {
                query = query.Where(p =>
                    (!dataFiltroInicio.HasValue || p.DataPagamento >= Funcoes.FormataDataTimeFiltros(dataFiltroInicio, "I")) &&
                    (!dataFiltroFim.HasValue || p.DataPagamento <= Funcoes.FormataDataTimeFiltros(dataFiltroFim, "F"))
                );
            }

            query = query.OrderBy(p => p.financPagarId).ThenBy(p => p.Parcela);

            resposta = paginar ? await PaginationHelper.PaginateAsync(query, pageNumber, pageSize) : new ResponseModel<List<Financ_PagarSubModel>> { Dados = await query.ToListAsync() };
            resposta.Mensagem = "Todas as parcelas foram encontradas";

            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<Financ_PagarModel>>> ListarAnalitico(int pageNumber = 1, int pageSize = 10, int? codigoFiltro = null, string? descricaoFiltro = null, DateTime? dataEmissaoInicio = null, DateTime? dataEmissaoFim = null,
        decimal? valorMinimoFiltro = null, decimal? valorMaximoFiltro = null, int? parcelaNumeroFiltro = null, DateTime? vencimentoInicio = null, DateTime? vencimentoFim = null, bool paginar = true)
    {
        ResponseModel<List<Financ_PagarModel>> resposta = new ResponseModel<List<Financ_PagarModel>>();

        try
        {
            var query = _context.Financ_Pagar
                .Include(x => x.subFinancPagar)
                .AsQueryable();

            query = query.Where(x =>
                (!codigoFiltro.HasValue || x.Id == codigoFiltro) &&
                (string.IsNullOrEmpty(descricaoFiltro) || x.Descricao.Contains(descricaoFiltro)) &&
                (!dataEmissaoInicio.HasValue || x.DataEmissao >= dataEmissaoInicio.Value) &&
                (!dataEmissaoFim.HasValue || x.DataEmissao <= dataEmissaoFim.Value) &&
                (!valorMinimoFiltro.HasValue || x.Valor >= valorMinimoFiltro) &&
                (!valorMaximoFiltro.HasValue || x.Valor <= valorMaximoFiltro)
            );

            if (parcelaNumeroFiltro.HasValue || vencimentoInicio.HasValue || vencimentoFim.HasValue)
            {
                query = query.Where(x => x.subFinancPagar.Any(p =>
                    (!parcelaNumeroFiltro.HasValue || p.Parcela == parcelaNumeroFiltro) &&
                    (!vencimentoInicio.HasValue || p.DataVencimento >= vencimentoInicio) &&
                    (!vencimentoFim.HasValue || p.DataVencimento <= vencimentoFim)
                ));
            }

            query = query.OrderBy(x => x.Id);

            resposta = paginar ? await PaginationHelper.PaginateAsync(query, pageNumber, pageSize) : new ResponseModel<List<Financ_PagarModel>> { Dados = await query.ToListAsync() };
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
                    DataVencimento = DateTime.Now.AddMonths(1), // Ajuste conforme parametrização
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
}