using WebApiSmartClinic.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
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
                TipoPagamentoId = (int)financ_receberCreateDto.TipoPagamentoId,
                subFinancReceber = new List<Financ_ReceberSubModel>()
            };

            

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

            resposta = await PaginationHelper.PaginateAsync(query, pageNumber, pageSize);
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

            resposta = await PaginationHelper.PaginateAsync(query, pageNumber, pageSize);
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
            financ_receber.PacienteId = financ_receberEdicaoDto.PacienteId;
            financ_receber.TipoPagamentoId = financ_receberEdicaoDto.TipoPagamentoId;

            _context.Financ_ReceberSub.RemoveRange(financ_receber.subFinancReceber);
            financ_receber.subFinancReceber.Clear();

            foreach (var parcelaDto in financ_receberEdicaoDto.subFinancReceber)
            {
                var novaParcela = new Financ_ReceberSubModel
                {
                    financReceberId = financ_receber.Id,
                    Parcela = parcelaDto.Parcela,
                    Valor = parcelaDto.Valor,
                    FormaPagamentoId = parcelaDto.FormaPagamentoId,
                    DataPagamento = parcelaDto.DataPagamento,
                    Desconto = parcelaDto.Desconto,
                    Juros = parcelaDto.Juros,
                    Multa = parcelaDto.Multa,
                    DataVencimento = parcelaDto.DataVencimento,
                    Observacao = parcelaDto.Observacao
                };

                financ_receber.subFinancReceber.Add(novaParcela);
            }

            await _context.SaveChangesAsync();

            var query = _context.Financ_Receber
                .Include(x => x.subFinancReceber)
                .AsQueryable();

            resposta = await PaginationHelper.PaginateAsync(query, pageNumber, pageSize);
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

            resposta = paginar ? await PaginationHelper.PaginateAsync(query, pageNumber, pageSize) : new ResponseModel<List<Financ_ReceberModel>> { Dados = await query.ToListAsync() };
            resposta.Mensagem = "Todos os contas à receber foram encontrados";

            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;

            return resposta;
        }
    }

    public async Task<ResponseModel<List<Financ_ReceberModel>>> ListarAnalitico(
        int pageNumber = 1,
        int pageSize = 10,
        int? idFiltro = null,
        string? descricaoFiltro = null,
        int? pacienteIdFiltro = null,
        string? dataBaseFiltro = "E",
        string? ccFiltro = null,
        DateTime? dataFiltroInicio = null,
        DateTime? dataFiltroFim = null,
        bool paginar = true
        )
    {
        ResponseModel<List<Financ_ReceberModel>> resposta = new ResponseModel<List<Financ_ReceberModel>>();
        try
        {
            var query = _context.Financ_Receber
                .Include(x => x.subFinancReceber)
                .AsQueryable();

            if(idFiltro.HasValue)
                query = query.Where(i => i.Id == idFiltro.Value);

            dataBaseFiltro = "E"; //estou forçando de proposito a ficar assim, para depois ter outros filtros com base na data do filho

            //if adicionado para que se tiver um id não levar em consideração a data 
            if (idFiltro == null)
            {
                if (dataBaseFiltro == "E");
            }


            if (!string.IsNullOrEmpty(descricaoFiltro))
                query = query.Where(p => p.Descricao.Contains(descricaoFiltro));

            if (pacienteIdFiltro.HasValue)
                query = query.Where(p => p.PacienteId == pacienteIdFiltro);

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

    public async Task<ResponseModel<List<Financ_ReceberSubModel>>> ListarSintetico(int pageNumber = 1,
        int pageSize = 10,
        int? idPaiFiltro = null,
        int? parcelaNumeroFiltro = null,
        string? dataBaseFiltro = "V",
        DateTime? dataFiltroInicio = null,
        DateTime? dataFiltroFim = null,
        bool parcelasVencidasFiltro = false,
        bool paginar = true)
    {
        ResponseModel<List<Financ_ReceberSubModel>> resposta = new ResponseModel<List<Financ_ReceberSubModel>>();
        try
        {
            // Inicia a query com Include
            var query = _context.Financ_ReceberSub
                .Include(fp => fp.FinancReceber)
                .Include(fp => fp.FinancReceber.Paciente)
                .AsQueryable();

            // Aplica os filtros sem cast
            if (idPaiFiltro.HasValue)
                query = query.Where(p => p.financReceberId == idPaiFiltro);
            if (parcelaNumeroFiltro.HasValue)
                query = query.Where(p => p.Parcela == parcelaNumeroFiltro);

            //if adicionado para que se tiver id pai ou id filho nao pegar por data. 

            if (idPaiFiltro == null && idPaiFiltro == null)
            {

                if(dataBaseFiltro == "V")
                {
                    if (dataFiltroInicio.HasValue)
                    {
                        dataFiltroInicio = Funcoes.FormataDataTimeFiltros(dataFiltroInicio, "I");
                        query = query.Where(p => p.DataVencimento >= dataFiltroInicio);
                    }


                    if (dataFiltroFim.HasValue)
                    {
                        dataFiltroFim = Funcoes.FormataDataTimeFiltros(dataFiltroFim, "F");
                        query = query.Where(p => p.DataVencimento <= dataFiltroFim);
                    }
                }
                else if (dataBaseFiltro == "P")
                {
                    if (dataFiltroInicio.HasValue)
                    {
                        dataFiltroInicio = DateTime.SpecifyKind(dataFiltroInicio.Value, DateTimeKind.Utc);
                        query = query.Where(p => p.DataPagamento >= dataFiltroInicio);
                    }


                    if (dataFiltroFim.HasValue)
                    {
                        dataFiltroFim = DateTime.SpecifyKind(dataFiltroFim.Value, DateTimeKind.Utc);
                        query = query.Where(p => p.DataPagamento <= dataFiltroFim);
                    }
                }
                else
                {
                    if (dataFiltroInicio.HasValue)
                    {
                        dataFiltroInicio = DateTime.SpecifyKind(dataFiltroInicio.Value, DateTimeKind.Utc);
                        query = query.Where(p => p.FinancReceber.DataEmissao >= dataFiltroInicio);
                    }

                    if (dataFiltroFim.HasValue)
                    {

                        dataFiltroFim = DateTime.SpecifyKind(dataFiltroFim.Value, DateTimeKind.Utc);
                        query = query.Where(p => p.FinancReceber.DataEmissao <= dataFiltroFim);
                    }
                }
            }


            if (parcelasVencidasFiltro)
                query = query.Where(p => p.DataVencimento <= DateTime.Now && p.DataPagamento == null);

            // Aplica ordenação
            query = query.OrderBy(p => p.financReceberId)
                        .ThenBy(p => p.Parcela);

            // Executa a query com ou sem paginação
            resposta.Dados = paginar
                ? (await PaginationHelper.PaginateAsync(query, pageNumber, pageSize)).Dados
                : await query.ToListAsync();

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


    public async Task<ResponseModel<Financ_ReceberSubModel>> BaixarParcela(Financ_ReceberSubEdicaoDto financ_receberSubEdicaoDto)
    {
        var resposta = new ResponseModel<Financ_ReceberSubModel>();

        try
        {
            var parcela = await _context.Financ_ReceberSub.FirstOrDefaultAsync(p => p.Id == financ_receberSubEdicaoDto.Id);

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

            if (financ_receberSubEdicaoDto.ValorPago < parcela.Valor)
            {
                // Gerar nova parcela para o valor residual
                var valorRestante = parcela.Valor - financ_receberSubEdicaoDto.ValorPago;

                var novaParcela = new Financ_ReceberSubModel
                {
                    financReceberId = parcela.financReceberId,
                    Parcela = parcela.Parcela,
                    Valor = valorRestante,
                    DataVencimento = DateTime.Now.AddMonths(1),
                    Observacao = " - Gerada automaticamente por pagamento parcial."
                };

                _context.Add(novaParcela);
            }
            else if (financ_receberSubEdicaoDto.ValorPago > parcela.Valor)
            {
                resposta.Mensagem = "Valor pago excede o valor da parcela.";
                resposta.Status = false;
                return resposta;
            }

            parcela.ValorPago = financ_receberSubEdicaoDto.ValorPago;
            parcela.DataPagamento = financ_receberSubEdicaoDto.DataPagamento;

            // Atualiza o status do pai
            var financReceber = await _context.Financ_Receber.FirstOrDefaultAsync(f => f.Id == parcela.financReceberId);
            financReceber.ValorPago += financ_receberSubEdicaoDto.ValorPago;

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