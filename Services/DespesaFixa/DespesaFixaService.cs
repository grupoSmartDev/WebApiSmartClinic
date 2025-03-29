using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Dto.DespesaFixa;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.DespesaFixa;

public class DespesaFixaService : IDespesaFixaInterface
{
    private readonly AppDbContext _context;
    public DespesaFixaService(AppDbContext context)
    {
        _context = context;
    }
    public async Task<ResponseModel<DespesaFixaModel>> BuscarPorId(int idDespesa)
    {
        ResponseModel<DespesaFixaModel> resposta = new ResponseModel<DespesaFixaModel>();
        try
        {
            var despesa = await _context.Despesas
                .Include(d => d.Fornecedor)
                .Include(d => d.PlanoConta)
                .Include(d => d.CentroCusto)
                .FirstOrDefaultAsync(x => x.Id == idDespesa);

            if (despesa == null)
            {
                resposta.Mensagem = "Nenhuma despesa encontrada";
                return resposta;
            }

            resposta.Dados = despesa;
            resposta.Mensagem = "Despesa encontrada";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = "Erro ao buscar despesa: " + ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<DespesaFixaModel>>> Criar(DespesaFixaCreateDto despesaCreateDto)
    {
        ResponseModel<List<DespesaFixaModel>> resposta = new ResponseModel<List<DespesaFixaModel>>();
        try
        {
            var despesa = new DespesaFixaModel
            {
                Descricao = despesaCreateDto.Descricao,
                Valor = despesaCreateDto.Valor,
                DiaVencimento = despesaCreateDto.DiaVencimento,
                DataInicio = despesaCreateDto.DataInicio,
                DataFim = despesaCreateDto.DataFim,
                Ativo = despesaCreateDto.Ativo,
                Categoria = despesaCreateDto.Categoria,
                Frequencia = despesaCreateDto.Frequencia,
                CentroCustoId = despesaCreateDto.CentroCustoId,
                PlanoContaId = despesaCreateDto.PlanoContaId,
                FornecedorId = despesaCreateDto.FornecedorId
            };

            // Validações adicionais se necessário
            if (despesa.DiaVencimento < 1 || despesa.DiaVencimento > 31)
            {
                resposta.Mensagem = "O dia de vencimento deve estar entre 1 e 31";
                resposta.Status = false;
                return resposta;
            }

            // Verifica se fornecedor existe, se fornecedorId for informado
            if (despesa.FornecedorId.HasValue)
            {
                var fornecedor = await _context.Fornecedor.FindAsync(despesa.FornecedorId.Value);
                if (fornecedor == null)
                {
                    resposta.Mensagem = "Fornecedor não encontrado";
                    resposta.Status = false;
                    return resposta;
                }
            }

            // Verifica se plano de contas existe, se planoContaId for informado
            if (despesa.PlanoContaId.HasValue)
            {
                var planoConta = await _context.PlanoConta.FindAsync(despesa.PlanoContaId.Value);
                if (planoConta == null)
                {
                    resposta.Mensagem = "Plano de Contas não encontrado";
                    resposta.Status = false;
                    return resposta;
                }
            }

            // Verifica se centro de custo existe, se centroCustoId for informado
            if (despesa.CentroCustoId.HasValue)
            {
                var centroCusto = await _context.CentroCusto.FindAsync(despesa.CentroCustoId.Value);
                if (centroCusto == null)
                {
                    resposta.Mensagem = "Centro de Custo não encontrado";
                    resposta.Status = false;
                    return resposta;
                }
            }

            // Adiciona e salva no banco de dados
            await _context.Despesas.AddAsync(despesa);
            await _context.SaveChangesAsync();

            // Retorna a lista atualizada de despesas
            resposta.Dados = await _context.Despesas.ToListAsync();
            resposta.Mensagem = "Despesa fixa criada com sucesso";
            resposta.TotalCount = resposta.Dados.Count;
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = "Erro ao criar uma despesa: " + ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<DespesaFixaModel>>> Delete(int idDespesa)
    {
        ResponseModel<List<DespesaFixaModel>> resposta = new ResponseModel<List<DespesaFixaModel>>();
        try
        {
            var despesa = await _context.Despesas.FirstOrDefaultAsync(x => x.Id == idDespesa);
            if (despesa == null)
            {
                resposta.Mensagem = "Nenhuma despesa encontrada";
                return resposta;
            }

            _context.Remove(despesa);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Despesas.ToListAsync();
            resposta.Mensagem = "Despesa excluida com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<DespesaFixaModel>>> Editar(DespesaFixaEdicaoDto despesaEdicaoDto)
    {
        ResponseModel<List<DespesaFixaModel>> resposta = new ResponseModel<List<DespesaFixaModel>>();
        try
        {
            // Busca a despesa no banco de dados
            var despesa = await _context.Despesas.FirstOrDefaultAsync(x => x.Id == despesaEdicaoDto.Id);
            if (despesa == null)
            {
                resposta.Mensagem = "Despesa não encontrada";
                resposta.Status = false;
                return resposta;
            }

            // Atualiza as propriedades da despesa
            despesa.Descricao = despesaEdicaoDto.Descricao;
            despesa.Valor = despesaEdicaoDto.Valor;
            despesa.DiaVencimento = despesaEdicaoDto.DiaVencimento;
            despesa.DataInicio = despesaEdicaoDto.DataInicio;
            despesa.DataFim = despesaEdicaoDto.DataFim;
            despesa.Ativo = despesaEdicaoDto.Ativo;
            despesa.Categoria = despesaEdicaoDto.Categoria;
            despesa.Frequencia = despesaEdicaoDto.Frequencia;
            despesa.CentroCustoId = despesaEdicaoDto.CentroCustoId;
            despesa.PlanoContaId = despesaEdicaoDto.PlanoContaId;
            despesa.FornecedorId = despesaEdicaoDto.FornecedorId;

            // Validações adicionais se necessário
            if (despesa.DiaVencimento < 1 || despesa.DiaVencimento > 31)
            {
                resposta.Mensagem = "O dia de vencimento deve estar entre 1 e 31";
                resposta.Status = false;
                return resposta;
            }

            // Verifica se fornecedor existe, se fornecedorId for informado
            if (despesa.FornecedorId.HasValue)
            {
                var fornecedor = await _context.Fornecedor.FindAsync(despesa.FornecedorId.Value);
                if (fornecedor == null)
                {
                    resposta.Mensagem = "Fornecedor não encontrado";
                    resposta.Status = false;
                    return resposta;
                }
            }

            // Verifica se plano de contas existe, se planoContaId for informado
            if (despesa.PlanoContaId.HasValue)
            {
                var planoConta = await _context.PlanoConta.FindAsync(despesa.PlanoContaId.Value);
                if (planoConta == null)
                {
                    resposta.Mensagem = "Plano de Contas não encontrado";
                    resposta.Status = false;
                    return resposta;
                }
            }

            // Verifica se centro de custo existe, se centroCustoId for informado
            if (despesa.CentroCustoId.HasValue)
            {
                var centroCusto = await _context.CentroCusto.FindAsync(despesa.CentroCustoId.Value);
                if (centroCusto == null)
                {
                    resposta.Mensagem = "Centro de Custo não encontrado";
                    resposta.Status = false;
                    return resposta;
                }
            }

            // Marca a entidade como modificada e salva as alterações
            _context.Despesas.Update(despesa);
            await _context.SaveChangesAsync();

            // Retorna a lista atualizada de despesas
            resposta.Dados = await _context.Despesas.ToListAsync();
            resposta.Mensagem = "Despesa fixa atualizada com sucesso";
            resposta.TotalCount = resposta.Dados.Count;
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = "Erro ao editar a despesa: " + ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<DespesaFixaModel>>> Listar()
    {
        ResponseModel<List<DespesaFixaModel>> resposta = new ResponseModel<List<DespesaFixaModel>>();
        try
        {
            var despesas = await _context.Despesas
                .Include(d => d.Fornecedor)
                .Include(d => d.PlanoConta)
                .Include(d => d.CentroCusto)
                .ToListAsync();

            resposta.Dados = despesas;
            resposta.TotalCount = despesas.Count;
            resposta.Mensagem = despesas.Count > 0 ? "Todas as despesas foram encontradas" : "Nenhuma despesa encontrada";
            return resposta;
        }
        catch (Exception e)
        {
            resposta.Mensagem = e.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<Financ_PagarModel>>> GerarLancamentosDoMes(DateTime dataReferencia)
    {
        ResponseModel<List<Financ_PagarModel>> resposta = new ResponseModel<List<Financ_PagarModel>>();

        // Usar uma transação explícita para garantir que tudo seja salvo ou nada seja salvo
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            // Obtém todas as despesas fixas ativas
            var despesasAtivas = await _context.Despesas
                .Where(d => d.Ativo == true &&
                       (d.DataFim == null || d.DataFim >= dataReferencia) &&
                       d.DataInicio <= dataReferencia)
                .ToListAsync();

            // Valida o último dia do mês para tratar corretamente datas como 31 em fevereiro
            int ultimoDiaDoMes = DateTime.DaysInMonth(dataReferencia.Year, dataReferencia.Month);

            // Cria uma lista para armazenar os IDs das despesas que devem gerar lançamentos neste mês
            var despesasIdsParaGerar = new List<int>();

            // Verifica quais despesas devem gerar lançamentos neste mês com base na frequência
            foreach (var despesa in despesasAtivas)
            {
                if (DeveGerarLancamentoNoMes(despesa, dataReferencia))
                {
                    despesasIdsParaGerar.Add(despesa.Id);
                }
            }

            // Se não houver despesas para gerar, retorna imediatamente
            if (!despesasIdsParaGerar.Any())
            {
                resposta.Mensagem = "Nenhuma despesa fixa encontrada para o período selecionado";
                resposta.Dados = new List<Financ_PagarModel>();
                return resposta;
            }

            // Busca lançamentos existentes para evitar duplicações
            // Agora verifica nos subitens (Financ_PagarSub) pois é aí que fica o DespesaFixaId
            var lancamentosExistentes = await _context.Financ_PagarSub
            .Where(s => despesasIdsParaGerar.Contains(s.DespesaFixaId ?? 0) &&
                    s.DataVencimento != null && // Garante que não é nulo
                    s.DataVencimento.Value.Year == dataReferencia.Year && // Usa .Value para acessar
                    s.DataVencimento.Value.Month == dataReferencia.Month)
            .Select(s => s.DespesaFixaId)
            .Distinct()
            .ToListAsync();

            // Filtra apenas as despesas que ainda não têm lançamentos
            var despesasParaGerar = despesasAtivas
                .Where(d => despesasIdsParaGerar.Contains(d.Id) && !lancamentosExistentes.Contains(d.Id))
                .ToList();

            // Lista para armazenar os lançamentos gerados
            var lancamentosGerados = new List<Financ_PagarModel>();

            // Gera os lançamentos para as despesas filtradas
            foreach (var despesa in despesasParaGerar)
            {
                // Ajusta o dia de vencimento caso seja maior que o último dia do mês
                int diaVencimento = Math.Min(despesa.DiaVencimento, ultimoDiaDoMes);

                // Cria o lançamento financeiro PAI (cabeçalho)
                var novoLancamento = new Financ_PagarModel
                {
                    Descricao = despesa.Descricao,
                    ValorOriginal = despesa.Valor,
                    Valor = despesa.Valor, // Mesmo valor no cabeçalho
                    DataEmissao = DateTime.Now,
                    // DespesaFixaId foi removido do model pai
                    FornecedorId = despesa.FornecedorId,
                    PlanoContaId = despesa.PlanoContaId,
                    CentroCustoId = despesa.CentroCustoId,
                    Status = "Pendente",
                    NrDocto = null,
                    Classificacao = "DESPESA FIXA",
                    Parcela = 1, // Mesmo sendo uma única parcela, mantemos consistência
                    IdOrigem = despesa.Id, // Referência à despesa fixa que originou
                    subFinancPagar = new List<Financ_PagarSubModel>() // Inicializa a lista de subitens
                };

                // Adiciona o lançamento PAI ao contexto
                _context.Financ_Pagar.Add(novoLancamento);
                await _context.SaveChangesAsync(); // Precisa salvar para obter o ID gerado

                // Cria a parcela (FILHO) com a data de vencimento e DespesaFixaId
                var subItem = new Financ_PagarSubModel
                {
                    financPagarId = novoLancamento.Id, // Relaciona com o pai
                    Parcela = 1, // Como é despesa fixa, normalmente tem apenas uma parcela
                    Valor = despesa.Valor,
                    DataVencimento = new DateTime(dataReferencia.Year, dataReferencia.Month, diaVencimento),
                    DespesaFixaId = despesa.Id, // AQUI está o ID da despesa fixa no subitem
                    TipoPagamentoId = null, // Defina valores padrão conforme necessário
                    FormaPagamentoId = null, // Defina valores padrão conforme necessário
                    DataPagamento = null, // Ainda não foi pago
                    Desconto = 0,
                    Juros = 0,
                    Multa = 0,
                    Observacao = $"Gerado automaticamente a partir da despesa fixa #{despesa.Id}"
                };

                // Adiciona o subitem ao contexto
                _context.Financ_PagarSub.Add(subItem);

                // Também adiciona o subitem à coleção do lançamento pai para a resposta
                novoLancamento.subFinancPagar.Add(subItem);

                // Adiciona à lista de lançamentos gerados para retorno
                lancamentosGerados.Add(novoLancamento);
            }

            // Salva todos os subitens
            await _context.SaveChangesAsync();

            // Confirma a transação
            await transaction.CommitAsync();

            // Prepara a resposta
            resposta.Dados = lancamentosGerados;
            resposta.TotalCount = lancamentosGerados.Count;

            // Mensagem informativa sobre o resultado
            var mensagem = new List<string>();
            if (lancamentosGerados.Count > 0)
            {
                mensagem.Add($"Foram gerados {lancamentosGerados.Count} lançamentos financeiros");
            }
            else
            {
                mensagem.Add("Nenhum lançamento financeiro foi gerado");
            }

            if (lancamentosExistentes.Count > 0)
            {
                mensagem.Add($"{lancamentosExistentes.Count} lançamentos já existiam para o período");
            }

            resposta.Mensagem = string.Join(". ", mensagem);
            resposta.Status = true;

            return resposta;
        }
        catch (Exception ex)
        {
            // Em caso de erro, desfaz a transação
            await transaction.RollbackAsync();

            resposta.Mensagem = "Erro ao gerar lançamentos: " + ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    // Método auxiliar para determinar se deve gerar um lançamento para uma despesa fixa
    private bool DeveGerarLancamentoNoMes(DespesaFixaModel despesa, DateTime dataReferencia)
    {
        // Calcula o número de meses desde a data de início
        int mesesDesdeInicio = ((dataReferencia.Year - despesa.DataInicio.Year) * 12) +
                               (dataReferencia.Month - despesa.DataInicio.Month);

        // Verifica se o mês atual corresponde à frequência da despesa
        switch (despesa.Frequencia)
        {
            case 1:
                return true;

            case 2:
                // Verifica se o mês está na sequência bimestral desde o início
                return mesesDesdeInicio % 2 == 0;

            case 3:
                // Verifica se o mês está na sequência trimestral desde o início
                return mesesDesdeInicio % 3 == 0;

            case 6:
                // Verifica se o mês está na sequência semestral desde o início
                return mesesDesdeInicio % 6 == 0;

            case 12:
                // Verifica se o mês está na sequência anual desde o início
                return mesesDesdeInicio % 12 == 0;

            default:
                return false;
        }
    }
}

  