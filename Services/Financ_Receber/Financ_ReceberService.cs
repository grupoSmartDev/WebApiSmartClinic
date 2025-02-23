
using Microsoft.EntityFrameworkCore;
using System.Linq;
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
            var financ_receber = await _context.Financ_Receber
                .Include(f => f.subFinancReceber) // Inclui as parcelas no resultado
                .FirstOrDefaultAsync(x => x.Id == idFinanc_Receber);

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

    public async Task<ResponseModel<List<Financ_ReceberModel>>> Criar(Financ_ReceberCreateDto financ_receberCreateDto, int pageNumber = 1, int pageSize = 10)
    {
        ResponseModel<List<Financ_ReceberModel>> resposta = new ResponseModel<List<Financ_ReceberModel>>();
        try
        {
            // Criação do cabeçalho (pai)
            var financ_receber = new Financ_ReceberModel
            {
                IdOrigem = financ_receberCreateDto.IdOrigem ?? 0,
                NrDocto = financ_receberCreateDto.NrDocto ?? 0,
                DataEmissao = financ_receberCreateDto.DataEmissao,
                ValorOriginal = financ_receberCreateDto.ValorOriginal,
                ValorPago = financ_receberCreateDto.ValorPago,
                Valor = financ_receberCreateDto.Valor,
                Status = financ_receberCreateDto.Status,
                NotaFiscal = financ_receberCreateDto.NotaFiscal,
                Descricao = financ_receberCreateDto.Descricao,
                Parcela = financ_receberCreateDto.Parcela,
                Classificacao = financ_receberCreateDto.Classificacao,
                Observacao = financ_receberCreateDto.Observacao,
                FornecedorId = financ_receberCreateDto.FornecedorId,
                CentroCustoId = financ_receberCreateDto.CentroCustoId,
                PacienteId = financ_receberCreateDto.PacienteId,
                BancoId = financ_receberCreateDto.BancoId,
                subFinancReceber = new List<Financ_ReceberSubModel>()
            };

            financ_receber.PacienteId = 9;

            _context.Add(financ_receber);
            await _context.SaveChangesAsync();

            // Adicionando subitens (filhos)
            if (financ_receberCreateDto != null)
            {
                foreach (var parcela in financ_receberCreateDto.subFinancReceber)
                {
                    var subItem = new Financ_ReceberSubModel
                    {
                        financReceberId = financ_receber.Id, // Relaciona com o pai
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

                    financ_receber.subFinancReceber.Add(subItem);
                }
            }



            await _context.SaveChangesAsync();
            var query = _context.Financ_Receber
                .Include(x => x.subFinancReceber)
                .AsQueryable();

            resposta.Dados = (await PaginationHelper.PaginateAsync(query, pageNumber, pageSize)).Dados;
            resposta.Mensagem = "Financ_Receber e parcelas criados com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<Financ_ReceberModel>>> Delete(int idFinanc_Receber, int pageNumber = 1, int pageSize = 10)
    {
        ResponseModel<List<Financ_ReceberModel>> resposta = new ResponseModel<List<Financ_ReceberModel>>();

        try
        {
            var financ_receber = await _context.Financ_Receber
                .Include(f => f.subFinancReceber) // Inclui as parcelas para exclusão em cascata
                .FirstOrDefaultAsync(x => x.Id == idFinanc_Receber);

            if (financ_receber == null)
            {
                resposta.Mensagem = "Nenhum Financ_Receber encontrado";
                return resposta;
            }

            _context.Remove(financ_receber);
            await _context.SaveChangesAsync();

            var query = _context.Financ_Receber
                .Include(x => x.subFinancReceber)
                .AsQueryable();

            resposta.Dados = (await PaginationHelper.PaginateAsync(query, pageNumber, pageSize)).Dados;
            resposta.Mensagem = "Financ_Receber Excluído com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<Financ_ReceberModel>>> Editar(Financ_ReceberEdicaoDto financ_receberEdicaoDto, int pageNumber = 1, int pageSize = 10)
    {
        ResponseModel<List<Financ_ReceberModel>> resposta = new ResponseModel<List<Financ_ReceberModel>>();
        try
        {
            var financ_receber = await _context.Financ_Receber
                .Include(f => f.subFinancReceber)
                .FirstOrDefaultAsync(x => x.Id == financ_receberEdicaoDto.Id);

            if (financ_receber == null)
            {
                resposta.Mensagem = "Financ_Receber não encontrado";
                return resposta;
            }

            // Atualiza dados principais
            financ_receber.IdOrigem = financ_receberEdicaoDto.IdOrigem;
            financ_receber.NrDocto = financ_receberEdicaoDto.NrDocto;
            financ_receber.DataEmissao = financ_receberEdicaoDto.DataEmissao;
            financ_receber.ValorOriginal = financ_receberEdicaoDto.ValorOriginal;
            financ_receber.ValorPago = financ_receberEdicaoDto.ValorPago;
            financ_receber.Valor = financ_receberEdicaoDto.Valor;
            financ_receber.Status = financ_receberEdicaoDto.Status;
            financ_receber.NotaFiscal = financ_receberEdicaoDto.NotaFiscal;
            financ_receber.Descricao = financ_receberEdicaoDto.Descricao;
            financ_receber.Parcela = financ_receberEdicaoDto.Parcela;
            financ_receber.Classificacao = financ_receberEdicaoDto.Classificacao;
            financ_receber.Observacao = financ_receberEdicaoDto.Observacao;
            financ_receber.FornecedorId = financ_receberEdicaoDto.FornecedorId;
            financ_receber.CentroCustoId = financ_receberEdicaoDto.CentroCustoId;
            financ_receber.BancoId = financ_receberEdicaoDto.BancoId;

            // Atualiza subitens
            if (financ_receberEdicaoDto.subFinancReceber != null)
            {
                foreach (var parcelaDto in financ_receberEdicaoDto.subFinancReceber)
                {
                    // Procura se já existe o subitem
                    var subItemExistente = financ_receber.subFinancReceber
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
                        var novoSubItem = new Financ_ReceberSubModel  // Alterado para a entidade correta
                        {
                            financReceberId = financ_receber.Id,  // Ajustado nome da propriedade
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
                        financ_receber.subFinancReceber.Add(novoSubItem);
                    }
                }

                // Remove apenas os itens que não estão mais na lista
                var idsParaManterAtivos = financ_receberEdicaoDto.subFinancReceber.Select(x => x.Id).ToList();
                var itensParaRemover = financ_receber.subFinancReceber
                    .Where(x => x.Id > 0 && !idsParaManterAtivos.Contains((int)x.Id))
                    .ToList();

                foreach (var itemRemover in itensParaRemover)
                {
                    _context.Remove(itemRemover);
                }
            }

            await _context.SaveChangesAsync();

            var query = _context.Financ_Receber
                .Include(x => x.subFinancReceber)
                .AsQueryable();

            resposta.Dados = (await PaginationHelper.PaginateAsync(query, pageNumber, pageSize)).Dados;
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

    public async Task<ResponseModel<List<Financ_ReceberModel>>> Listar(int pageNumber = 1, int pageSize = 10, int? codigoFiltro = null, string? descricaoFiltro = null, DateTime? dataEmissaoInicio = null, DateTime? dataEmissaoFim = null,
        decimal? valorMinimoFiltro = null, decimal? valorMaximoFiltro = null, int? parcelaNumeroFiltro = null, DateTime? vencimentoInicio = null, DateTime? vencimentoFim = null, bool paginar = true)
    {
        ResponseModel<List<Financ_ReceberModel>> resposta = new ResponseModel<List<Financ_ReceberModel>>();
        try
        {
            var query = _context.Financ_Receber
                .Include(x => x.subFinancReceber)
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
                query = query.Where(x => x.subFinancReceber.Any(p =>
                    (!parcelaNumeroFiltro.HasValue || p.Parcela == parcelaNumeroFiltro) &&
                    (!vencimentoInicio.HasValue || p.DataVencimento >= vencimentoInicio) &&
                    (!vencimentoFim.HasValue || p.DataVencimento <= vencimentoFim)
                ));
            }

            query = query.OrderBy(x => x.Id);

            resposta.Dados = paginar ? (await PaginationHelper.PaginateAsync(query, pageNumber, pageSize)).Dados : await query.ToListAsync();
            resposta.Mensagem = "Todos os Financ_Receber foram encontrados";

            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;

            return resposta;
        }
    }

    public async Task<ResponseModel<List<Financ_ReceberModel>>> Listara(
    int pageNumber = 1,
    int pageSize = 10,
    int? codigoFiltro = null,
    string? descricaoFiltro = null,
    DateTime? dataEmissaoInicio = null,
    DateTime? dataEmissaoFim = null,
    decimal? valorMinimoFiltro = null,
    decimal? valorMaximoFiltro = null,
    int? parcelaNumeroFiltro = null,
    DateTime? vencimentoInicio = null,
    DateTime? vencimentoFim = null,
    bool paginar = true)
    {
        ResponseModel<List<Financ_ReceberModel>> resposta = new ResponseModel<List<Financ_ReceberModel>>();
        try
        {
            // 1. Query base
            var query = _context.Financ_Receber.AsQueryable();

            // 2. Aplicando filtros principais um por um
            if (codigoFiltro.HasValue)
            {
                query = query.Where(x => x.Id == codigoFiltro);
            }

            if (!string.IsNullOrEmpty(descricaoFiltro))
            {
                query = query.Where(x => x.Descricao.Contains(descricaoFiltro));
            }

            if (dataEmissaoInicio.HasValue)
            {
                query = query.Where(x => x.DataEmissao >= dataEmissaoInicio.Value);
            }

            if (dataEmissaoFim.HasValue)
            {
                query = query.Where(x => x.DataEmissao <= dataEmissaoFim.Value);
            }

            if (valorMinimoFiltro.HasValue)
            {
                query = query.Where(x => x.Valor >= valorMinimoFiltro);
            }

            if (valorMaximoFiltro.HasValue)
            {
                query = query.Where(x => x.Valor <= valorMaximoFiltro);
            }

            // 3. Aplicando filtros relacionados à subFinancReceber
            if (parcelaNumeroFiltro.HasValue || vencimentoInicio.HasValue || vencimentoFim.HasValue)
            {
                query = query.Where(x => x.subFinancReceber.Any(p =>
                    (!parcelaNumeroFiltro.HasValue || p.Parcela == parcelaNumeroFiltro) &&
                    (!vencimentoInicio.HasValue || p.DataVencimento >= vencimentoInicio) &&
                    (!vencimentoFim.HasValue || p.DataVencimento <= vencimentoFim)
                ));
            }

            // 4. Incluindo relacionamentos
            query = query.Include(x => x.subFinancReceber);

            // 5. Aplicando ordenação
            query = query.OrderBy(x => x.Id);

            // 6. Executando a query com ou sem paginação
            List<Financ_ReceberModel> resultado;

            if (paginar)
            {
                try
                {
                    var paginado = await PaginationHelper.PaginateAsync(query, pageNumber, pageSize);
                    resultado = paginado.Dados;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Erro na paginação: {ex.Message}", ex);
                }
            }
            else
            {
                try
                {
                    resultado = await query.ToListAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Erro ao executar query sem paginação: {ex.Message}", ex);
                }
            }

            // 7. Montando a resposta
            resposta.Dados = resultado;
            resposta.Status = true;
            resposta.Mensagem = "Dados encontrados com sucesso";

            return resposta;
        }
        catch (Exception ex)
        {
            // Log detalhado do erro
            var errorMessage = $"Erro ao listar Financ_Receber: {ex.Message}";
            if (ex.InnerException != null)
            {
                errorMessage += $"\nInner Exception: {ex.InnerException.Message}";
            }
            errorMessage += $"\nStack Trace: {ex.StackTrace}";

            // Configurando a resposta de erro
            resposta.Mensagem = errorMessage;
            resposta.Status = false;
            resposta.Dados = null;

            return resposta;
        }
    }

    public async Task<ResponseModel<List<Financ_ReceberModel>>> BuscarContasEmAberto()
    {
        ResponseModel<List<Financ_ReceberModel>> resposta = new ResponseModel<List<Financ_ReceberModel>>();

        try
        {
            var contasEmAberto = await _context.Financ_Receber
                .Include(f => f.subFinancReceber)
                .Where(f => f.Status == "Em Aberto" || f.Status == "Parcial")
                .ToListAsync();

            resposta.Dados = contasEmAberto;
            resposta.Mensagem = "Contas em aberto encontradas com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<string>> EstornarParcela(int idParcela)
    {
        var resposta = new ResponseModel<string>();

        try
        {
            var parcela = await _context.Financ_ReceberSub.FirstOrDefaultAsync(p => p.Id == idParcela);

            if (parcela == null || parcela.DataPagamento == null)
            {
                resposta.Mensagem = "Parcela não encontrada ou já está em aberto.";
                resposta.Status = false;
                return resposta;
            }

            parcela.DataPagamento = null;
            parcela.ValorPago = 0;

            var financReceber = await _context.Financ_Receber.FirstOrDefaultAsync(f => f.Id == parcela.financReceberId);
            financReceber.ValorPago -= parcela.Valor;

            financReceber.Status = financReceber.ValorPago > 0 ? "Parcial" : "Em Aberto";

            _context.Update(parcela);
            _context.Update(financReceber);
            await _context.SaveChangesAsync();

            resposta.Mensagem = "Recebimento estornado com sucesso.";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }


    public async Task<ResponseModel<Financ_ReceberSubModel>> QuitarParcela(int idParcela, decimal valorPago)
    {
        var resposta = new ResponseModel<Financ_ReceberSubModel>();

        try
        {
            var parcela = await _context.Financ_ReceberSub.FirstOrDefaultAsync(p => p.Id == idParcela);

            if (parcela == null)
            {
                resposta.Mensagem = "Parcela não encontrada.";
                resposta.Status = false;
                return resposta;
            }

            if (parcela.DataPagamento != null)
            {
                resposta.Mensagem = "Parcela já foi quitada.";
                resposta.Status = false;
                return resposta;
            }

            if (valorPago < parcela.Valor)
            {
                // Gerar nova parcela para o valor residual
                var valorRestante = parcela.Valor - valorPago;

                var novaParcela = new Financ_ReceberSubModel
                {
                    financReceberId = parcela.financReceberId,
                    Parcela = parcela.Parcela,
                    Valor = valorRestante,
                    DataVencimento = DateTime.Now.AddMonths(1),
                    Observacao = "Gerada automaticamente por pagamento parcial."
                };

                _context.Add(novaParcela);
            }
            else if (valorPago > parcela.Valor)
            {
                resposta.Mensagem = "Valor pago excede o valor da parcela.";
                resposta.Status = false;
                return resposta;
            }

            parcela.ValorPago = valorPago;
            parcela.DataPagamento = DateTime.Now;

            // Atualiza o status do pai
            var financReceber = await _context.Financ_Receber.FirstOrDefaultAsync(f => f.Id == parcela.financReceberId);
            financReceber.ValorPago += valorPago;

            if (financReceber.ValorPago >= financReceber.ValorOriginal)
            {
                financReceber.Status = "Quitado";
            }
            else
            {
                financReceber.Status = "Parcial";
            }

            _context.Update(parcela);
            _context.Update(financReceber);
            await _context.SaveChangesAsync();

            resposta.Dados = parcela;
            resposta.Mensagem = "Parcela quitada com sucesso.";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<decimal>> CalcularTotalRecebiveis(int cliente, DateTime? dataInicio = null, DateTime? dataFim = null)
    {
        var resposta = new ResponseModel<decimal>();

        try
        {
            var query = _context.Financ_Receber.AsQueryable();

            if (cliente > 0)
                query = query.Where(f => f.PacienteId == cliente);

            if (dataInicio.HasValue)
                query = query.Where(f => f.DataEmissao >= dataInicio.Value);

            if (dataFim.HasValue)
                query = query.Where(f => f.DataEmissao <= dataFim.Value);

            var total = await query.Where(f => f.Status == "Em Aberto")
                                   .SumAsync(f => f.ValorOriginal - f.ValorPago);

            resposta.Dados = (decimal)total;
            resposta.Mensagem = "Total de recebíveis calculado com sucesso.";
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