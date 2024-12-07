
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

    public async Task<ResponseModel<List<Financ_ReceberModel>>> Criar(Financ_ReceberCreateDto financ_receberCreateDto)
    {
        ResponseModel<List<Financ_ReceberModel>> resposta = new ResponseModel<List<Financ_ReceberModel>>();
        try
        {
            // Criação do cabeçalho (pai)
            var financ_receber = new Financ_ReceberModel
            {
                IdOrigem = financ_receberCreateDto.IdOrigem,
                NrDocto = financ_receberCreateDto.NrDocto,
                DataEmissao = financ_receberCreateDto.DataEmissao,
                ValorOriginal = financ_receberCreateDto.ValorOriginal,
                ValorPago = financ_receberCreateDto.ValorPago,
                Status = financ_receberCreateDto.Status,
                NotaFiscal = financ_receberCreateDto.NotaFiscal,
                Descricao = financ_receberCreateDto.Descricao,
                Parcela = financ_receberCreateDto.Parcela,
                Classificacao = financ_receberCreateDto.Classificacao,
                Desconto = financ_receberCreateDto.Desconto,
                Juros = financ_receberCreateDto.Juros,
                Multa = financ_receberCreateDto.Multa,
                Observacao = financ_receberCreateDto.Observacao,
                FornecedorId = financ_receberCreateDto.FornecedorId,
                CentroCustoId = financ_receberCreateDto.CentroCustoId,
                BancoId = financ_receberCreateDto.BancoId
            };

            _context.Add(financ_receber);
            await _context.SaveChangesAsync();

            // Adicionando subitens (filhos)
            if (financ_receberCreateDto.subFinancReceber != null && financ_receberCreateDto.subFinancReceber.Any())
            {
                foreach (var parcela in financ_receberCreateDto.subFinancReceber)
                {
                    var subItem = new Financ_ReceberSubModel
                    {
                        financReceberId = financ_receber.Id, // Relaciona com o pai
                        Parcela= parcela.Parcela,
                        Valor = parcela.Valor,
                        TipoPagamentoId = parcela.TipoPagamentoId,
                        FormaPagamentoId= parcela.FormaPagamentoId,
                        DataPagamento = parcela.DataPagamento,
                        DataVencimento = parcela.DataVencimento,
                        Observacao = parcela.Observacao
                    };

                    _context.Add(subItem);
                }

                await _context.SaveChangesAsync();
            }

            resposta.Dados = await _context.Financ_Receber.Include(f => f.subFinancReceber).ToListAsync();
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

    public async Task<ResponseModel<List<Financ_ReceberModel>>> Delete(int idFinanc_Receber)
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

            resposta.Dados = await _context.Financ_Receber.ToListAsync();
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

    public async Task<ResponseModel<List<Financ_ReceberModel>>> Editar(Financ_ReceberEdicaoDto financ_receberEdicaoDto)
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

            financ_receber.IdOrigem = financ_receberEdicaoDto.IdOrigem;
            financ_receber.NrDocto = financ_receberEdicaoDto.NrDocto;
            financ_receber.DataEmissao = financ_receberEdicaoDto.DataEmissao;
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
            financ_receber.BancoId = financ_receberEdicaoDto.BancoId;

            // Atualiza parcelas existentes
            _context.Update(financ_receber);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Financ_Receber.Include(f => f.subFinancReceber).ToListAsync();
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
            var financ_receber = await _context.Financ_Receber
                .Include(f => f.subFinancReceber) // Inclui as parcelas no resultado
                .ToListAsync();

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

    public async Task<ResponseModel<Financ_ReceberSubModel>> QuitarParcela(int idParcela, decimal valorPago, DateTime dataPagamento)
    {
        ResponseModel<Financ_ReceberSubModel> resposta = new ResponseModel<Financ_ReceberSubModel>();

        try
        {
            var parcela = await _context.Financ_ReceberSub.FirstOrDefaultAsync(p => p.Id == idParcela);

            if (parcela == null)
            {
                resposta.Mensagem = "Parcela não encontrada";
                resposta.Status = false;
                return resposta;
            }

            parcela.Valor = valorPago;
            parcela.DataPagamento = dataPagamento;

            _context.Update(parcela);
            await _context.SaveChangesAsync();

            resposta.Dados = parcela;
            resposta.Mensagem = "Parcela quitada com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<decimal>> CalcularTotalRecebiveis()
    {
        ResponseModel<decimal> resposta = new ResponseModel<decimal>();

        try
        {
            var total = await _context.Financ_Receber
                .Where(f => f.Status == "Em Aberto")
                .SumAsync(f => f.ValorOriginal - f.ValorPago);

            resposta.Dados = (decimal)total;
            resposta.Mensagem = "Total de recebíveis calculado com sucesso";
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