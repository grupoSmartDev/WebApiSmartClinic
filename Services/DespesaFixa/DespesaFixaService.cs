using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Dto.DespesaFixa;
using WebApiSmartClinic.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

    public async Task<ResponseModel<List<DespesaFixaModel>>> Criars(DespesaFixaCreateDto despesaCreateDto)
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
                Frequencia = despesaCreateDto.Frequencia,
                CentroCustoId = despesaCreateDto.CentroCustoId,
                PlanoContaId = despesaCreateDto.PlanoContaId,
                FornecedorId = despesaCreateDto.FornecedorId
            };

            if (despesa.DiaVencimento < 1 || despesa.DiaVencimento > 31)
            {
                resposta.Mensagem = "O dia de vencimento deve estar entre 1 e 31";
                resposta.Status = false;
                return resposta;
            }

            // Verificações de integridade
            if (despesa.FornecedorId.HasValue && await _context.Fornecedor.FindAsync(despesa.FornecedorId.Value) is null)
            {
                resposta.Mensagem = "Fornecedor não encontrado";
                resposta.Status = false;
                return resposta;
            }

            if (despesa.PlanoContaId.HasValue && await _context.PlanoConta.FindAsync(despesa.PlanoContaId.Value) is null)
            {
                resposta.Mensagem = "Plano de Contas não encontrado";
                resposta.Status = false;
                return resposta;
            }

            if (despesa.CentroCustoId.HasValue && await _context.CentroCusto.FindAsync(despesa.CentroCustoId.Value) is null)
            {
                resposta.Mensagem = "Centro de Custo não encontrado";
                resposta.Status = false;
                return resposta;
            }

            await _context.Despesas.AddAsync(despesa);
            await _context.SaveChangesAsync();

            // Após salvar, gerar os 12 lançamentos
            List<Financ_PagarModel> listaPagar = new();

            for (int i = 0; i < 12; i++)
            {
                if (!despesa.DataInicio.HasValue)
                {
                    resposta.Mensagem = "Data de início é obrigatória";
                    resposta.Status = false;
                    return resposta;
                }

                var dataVencimento = new DateTime(
                    despesa.DataInicio.Value.Year,
                    despesa.DataInicio.Value.Month,
                    1
                ).AddMonths(i);

                int dia = despesa.DiaVencimento > DateTime.DaysInMonth(dataVencimento.Year, dataVencimento.Month)
                    ? DateTime.DaysInMonth(dataVencimento.Year, dataVencimento.Month)
                    : despesa.DiaVencimento;

                var dataFinal = new DateTime(dataVencimento.Year, dataVencimento.Month, dia);

                var pagar = new Financ_PagarModel
                {
                    Descricao = despesa.Descricao,
                    DataEmissao = DateTime.UtcNow,
                    ValorOriginal = despesa.Valor,
                    Valor = despesa.Valor,
                    Status = "Pendente",
                    FornecedorId = despesa.FornecedorId,
                    PlanoContaId = despesa.PlanoContaId,
                    CentroCustoId = despesa.CentroCustoId,
                    TipoPagamentoId = despesa.TipoPagamentoId,
                    subFinancPagar = new List<Financ_PagarSubModel>()
                };

                var sub = new Financ_PagarSubModel
                {
                    Parcela = 1,
                    Valor = despesa.Valor,
                    DataVencimento = dataFinal,
                    FormaPagamentoId = despesa.FormaPagamentoId
                };

                pagar.subFinancPagar.Add(sub);
                listaPagar.Add(pagar);
            }

            await _context.Financ_Pagar.AddRangeAsync(listaPagar);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Despesas.ToListAsync();
            resposta.Mensagem = "Despesa fixa criada e lançamentos financeiros gerados com sucesso";
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

    public async Task<ResponseModel<List<DespesaFixaModel>>> Editars(DespesaFixaEdicaoDto despesaEdicaoDto)
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


    public async Task<ResponseModel<List<DespesaFixaModel>>> Listar(int pageNumber = 1, int pageSize = 10, int? idFiltro = null, string? descricaoFiltro = null, string? vencimentoFiltro = null, string? centroCustoFiltro = null,
        string? planoContaFiltro = null, bool paginar = true)
    {
        ResponseModel<List<DespesaFixaModel>> resposta = new ResponseModel<List<DespesaFixaModel>>();
        try
        {
            var query =  _context.Despesas
                .Include(d => d.Fornecedor)
                .Include(d => d.PlanoConta)
                .Include(d => d.CentroCusto)
                .Include(f => f.FinancPagar)
                    .ThenInclude(f => f.subFinancPagar)
            .AsQueryable();

            if (idFiltro.HasValue)
                query = query.Where(i => i.Id == idFiltro.Value);

            if (!string.IsNullOrEmpty(descricaoFiltro))
                query = query.Where(p => p.Descricao.Contains(descricaoFiltro));



            if (!string.IsNullOrEmpty(centroCustoFiltro))
                query = query.Where(p => p.CentroCustoId == Convert.ToInt64(centroCustoFiltro));


            if (!string.IsNullOrEmpty(planoContaFiltro))
                query = query.Where(p => p.PlanoContaId == Convert.ToInt64(planoContaFiltro));

            query = query.OrderBy(x => x.Id);

            resposta.Dados = paginar ? (await PaginationHelper.PaginateAsync(query, pageNumber, pageSize)).Dados : await query.ToListAsync();
            resposta.Mensagem = "Consulta finalizada";
      
            return resposta;
        }
        catch (Exception e)
        {
            resposta.Mensagem = e.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<DespesaFixaModel>>> Criar(DespesaFixaCreateDto despesaCreateDto)
    {
        ResponseModel<List<DespesaFixaModel>> resposta = new ResponseModel<List<DespesaFixaModel>>();

        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // Validações iniciais
            var validacao = await ValidarDespesaFixa(despesaCreateDto);
            if (!validacao.IsValid)
            {
                resposta.Mensagem = validacao.ErrorMessage;
                resposta.Status = false;
                return resposta;
            }

            var despesa = new DespesaFixaModel
            {
                Descricao = despesaCreateDto.Descricao,
                Valor = despesaCreateDto.Valor,
                DiaVencimento = despesaCreateDto.DiaVencimento,
                DataInicio = despesaCreateDto.DataInicio,
                DataFim = despesaCreateDto.DataFim,
                Ativo = despesaCreateDto.Ativo,
                Frequencia = despesaCreateDto.Frequencia,
                CentroCustoId = despesaCreateDto.CentroCustoId,
                PlanoContaId = despesaCreateDto.PlanoContaId,
                FornecedorId = despesaCreateDto.FornecedorId,
                TipoPagamentoId = despesaCreateDto.TipoPagamentoId,
                FormaPagamentoId = despesaCreateDto.FormaPagamentoId
            };

            await _context.Despesas.AddAsync(despesa);
            await _context.SaveChangesAsync();

            // Gerar lançamentos baseado na frequência e período
            var lancamentos = await GerarLancamentosFinanceiros(despesa);

            if (lancamentos.Any())
            {
                await _context.Financ_Pagar.AddRangeAsync(lancamentos);
                await _context.SaveChangesAsync();
            }

            await transaction.CommitAsync();

            resposta.Dados = await _context.Despesas.ToListAsync();
            resposta.Mensagem = $"Despesa fixa criada com {lancamentos.Count} lançamentos financeiros gerados";
            resposta.TotalCount = resposta.Dados.Count;
            resposta.Status = true;

            return resposta;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            resposta.Mensagem = "Erro ao criar despesa: " + ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    // Método para validações
    private async Task<(bool IsValid, string ErrorMessage)> ValidarDespesaFixa(DespesaFixaCreateDto dto)
    {
        if (dto.DiaVencimento < 1 || dto.DiaVencimento > 31)
            return (false, "O dia de vencimento deve estar entre 1 e 31");

        if (!dto.DataInicio.HasValue)
            return (false, "Data de início é obrigatória");

        if (dto.FornecedorId.HasValue && await _context.Fornecedor.FindAsync(dto.FornecedorId.Value) is null)
            return (false, "Fornecedor não encontrado");

        if (dto.PlanoContaId.HasValue && await _context.PlanoConta.FindAsync(dto.PlanoContaId.Value) is null)
            return (false, "Plano de Contas não encontrado");

        if (dto.CentroCustoId.HasValue && await _context.CentroCusto.FindAsync(dto.CentroCustoId.Value) is null)
            return (false, "Centro de Custo não encontrado");

        return (true, string.Empty);
    }

    // Método para gerar lançamentos baseado na frequência
    private async Task<List<Financ_PagarModel>> GerarLancamentosFinanceiros(DespesaFixaModel despesa)
    {
        var listaPagar = new List<Financ_PagarModel>();

        if (!despesa.DataInicio.HasValue)
            return listaPagar;

        // Calcular quantas parcelas baseado na frequência e período
        var quantidadeParcelas = CalcularQuantidadeParcelas(despesa);

        for (int i = 0; i < quantidadeParcelas; i++)
        {
            var dataVencimento = CalcularDataVencimento(despesa.DataInicio.Value, despesa.DiaVencimento, i, despesa.Frequencia);

            // Verificar se ainda está dentro do período (se DataFim foi informada)
            if (despesa.DataFim.HasValue && dataVencimento > despesa.DataFim.Value)
                break;

            var pagar = new Financ_PagarModel
            {
                Descricao = $"{despesa.Descricao} - {dataVencimento:MM/yyyy}",
                DataEmissao = DateTime.UtcNow,
                ValorOriginal = despesa.Valor,
                Valor = despesa.Valor,
                Status = "Pendente",
                FornecedorId = despesa.FornecedorId,
                PlanoContaId = despesa.PlanoContaId,
                CentroCustoId = despesa.CentroCustoId,
                TipoPagamentoId = despesa.TipoPagamentoId,
                DespesaFixaId = despesa.Id, // Link com a despesa fixa
                subFinancPagar = new List<Financ_PagarSubModel>()
            };

            var sub = new Financ_PagarSubModel
            {
                Parcela = 1,
                Valor = despesa.Valor,
                DataVencimento = dataVencimento,
                FormaPagamentoId = despesa.FormaPagamentoId
            };

            pagar.subFinancPagar.Add(sub);
            listaPagar.Add(pagar);
        }

        return listaPagar;
    }

    // Calcular quantidade de parcelas baseado na frequência
    private int CalcularQuantidadeParcelas(DespesaFixaModel despesa)
    {
        if (despesa.DataFim.HasValue && despesa.DataInicio.HasValue)
        {
            return despesa.Frequencia switch
            {
                1 => ((despesa.DataFim.Value.Year - despesa.DataInicio.Value.Year) * 12) +
                           (despesa.DataFim.Value.Month - despesa.DataInicio.Value.Month) + 1,
                3 => (int)Math.Ceiling(((despesa.DataFim.Value - despesa.DataInicio.Value).Days / 90.0)),
                6 => (int)Math.Ceiling(((despesa.DataFim.Value - despesa.DataInicio.Value).Days / 180.0)),
                12 => despesa.DataFim.Value.Year - despesa.DataInicio.Value.Year + 1,
                _ => 12 // Default mensal por 12 meses
            };
        }

        return 12; // Default se não informar data fim
    }

    // Calcular data de vencimento baseado na frequência
    private DateTime CalcularDataVencimento(DateTime dataInicio, int diaVencimento, int indice, int frequencia = 12)
    {
        var dataBase = frequencia switch
        {
            1 => dataInicio.AddMonths(indice),
            3 => dataInicio.AddMonths(indice * 3),
            6 => dataInicio.AddMonths(indice * 6),
            12 => dataInicio.AddYears(indice),
            _ => dataInicio.AddMonths(indice) // Default mensal
        };

        // Ajustar o dia, considerando os dias do mês
        int diaFinal = Math.Min(diaVencimento, DateTime.DaysInMonth(dataBase.Year, dataBase.Month));

        return new DateTime(dataBase.Year, dataBase.Month, diaFinal);
    }

    // Método para atualizar lançamentos quando alterar a despesa fixa
    public async Task AtualizarLancamentosFuturos(int despesaFixaId, DespesaFixaModel despesaAtualizada)
    {
        // Remove lançamentos futuros não pagos
        var lancamentosFuturos = await _context.Financ_Pagar
            .Where(x => x.DespesaFixaId == despesaFixaId &&
                       x.Status == "Pendente" &&
                       x.subFinancPagar.Any(s => s.DataVencimento > DateTime.Now))
            .ToListAsync();

        _context.Financ_Pagar.RemoveRange(lancamentosFuturos);

        // Regera os lançamentos com os novos dados
        var novosLancamentos = await GerarLancamentosFinanceiros(despesaAtualizada);

        // Filtra apenas os futuros
        var lancamentosFuturosNovos = novosLancamentos
            .Where(x => x.subFinancPagar.Any(s => s.DataVencimento > DateTime.Now))
            .ToList();

        await _context.Financ_Pagar.AddRangeAsync(lancamentosFuturosNovos);
        await _context.SaveChangesAsync();
    }

    public async Task<ResponseModel<List<DespesaFixaModel>>> Editar(DespesaFixaEdicaoDto despesaUpdateDto)
    {
        ResponseModel<List<DespesaFixaModel>> resposta = new ResponseModel<List<DespesaFixaModel>>();

        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // Buscar a despesa fixa existente
            var despesaExistente = await _context.Despesas
                .Include(d => d.FinancPagar)
                .ThenInclude(f => f.subFinancPagar)
                .FirstOrDefaultAsync(d => d.Id == despesaUpdateDto.Id);

            if (despesaExistente == null)
            {
                resposta.Mensagem = "Despesa fixa não encontrada";
                resposta.Status = false;
                return resposta;
            }

            // Validações
            var validacao = await ValidarDespesaFixaEdicao(despesaUpdateDto, despesaExistente);
            if (!validacao.IsValid)
            {
                resposta.Mensagem = validacao.ErrorMessage;
                resposta.Status = false;
                return resposta;
            }

            // Verificar se houve mudanças que afetam lançamentos futuros
            bool afetaLancamentosFuturos = VerificarMudancasSignificativas(despesaExistente, despesaUpdateDto);

            // Atualizar dados da despesa fixa
            AtualizarDadosDespesa(despesaExistente, despesaUpdateDto);

            await _context.SaveChangesAsync();

            // Se mudanças afetam lançamentos futuros, reprocessar
            if (afetaLancamentosFuturos)
            {
                await ReprocessarLancamentosFuturos(despesaExistente);
            }

            await transaction.CommitAsync();

            // Retornar lista atualizada de todas as despesas fixas
            var todasDespesas = await _context.Despesas.ToListAsync();

            resposta.Dados = todasDespesas;
            resposta.Mensagem = "Despesa fixa atualizada com sucesso";
            resposta.TotalCount = todasDespesas.Count;
            resposta.Status = true;

            return resposta;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            resposta.Mensagem = "Erro ao editar despesa: " + ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    // Validações específicas para edição
    private async Task<(bool IsValid, string ErrorMessage)> ValidarDespesaFixaEdicao(
        DespesaFixaEdicaoDto dto,
        DespesaFixaModel despesaExistente)
    {
        if (dto.DiaVencimento < 1 || dto.DiaVencimento > 31)
            return (false, "O dia de vencimento deve estar entre 1 e 31");

        if (!dto.DataInicio.HasValue)
            return (false, "Data de início é obrigatória");

        // Validar se a nova data de início não conflita com pagamentos já realizados
        var pagamentosRealizados = await _context.Financ_Pagar
            .Where(x => x.DespesaFixaId == despesaExistente.Id && x.Status == "Pago")
            .Include(x => x.subFinancPagar)
            .ToListAsync();

        if (pagamentosRealizados.Any())
        {
            var primeiroPagamento = pagamentosRealizados
                .SelectMany(x => x.subFinancPagar)
                .Min(x => x.DataVencimento);

            if (dto.DataInicio.Value > primeiroPagamento)
            {
                return (false, "Não é possível alterar a data de início para depois de pagamentos já realizados");
            }
        }

        // Validações de integridade referencial
        if (dto.FornecedorId.HasValue && await _context.Fornecedor.FindAsync(dto.FornecedorId.Value) is null)
            return (false, "Fornecedor não encontrado");

        if (dto.PlanoContaId.HasValue && await _context.PlanoConta.FindAsync(dto.PlanoContaId.Value) is null)
            return (false, "Plano de Contas não encontrado");

        if (dto.CentroCustoId.HasValue && await _context.CentroCusto.FindAsync(dto.CentroCustoId.Value) is null)
            return (false, "Centro de Custo não encontrado");

        return (true, string.Empty);
    }

    // Verificar se as mudanças afetam lançamentos futuros
    private bool VerificarMudancasSignificativas(DespesaFixaModel existente, DespesaFixaEdicaoDto dto)
    {
        return existente.Valor != dto.Valor ||
               existente.DiaVencimento != dto.DiaVencimento ||
               existente.DataInicio != dto.DataInicio ||
               existente.DataFim != dto.DataFim ||
               existente.Frequencia != dto.Frequencia ||
               existente.FormaPagamentoId != dto.FormaPagamentoId ||
               existente.TipoPagamentoId != dto.TipoPagamentoId ||
               existente.FornecedorId != dto.FornecedorId ||
               existente.PlanoContaId != dto.PlanoContaId ||
               existente.CentroCustoId != dto.CentroCustoId;
    }

    // Atualizar dados da despesa fixa
    private void AtualizarDadosDespesa(DespesaFixaModel existente, DespesaFixaEdicaoDto dto)
    {
        existente.Descricao = dto.Descricao;
        existente.Valor = dto.Valor;
        existente.DiaVencimento = dto.DiaVencimento;
        existente.DataInicio = dto.DataInicio;
        existente.DataFim = dto.DataFim;
        existente.Ativo = dto.Ativo;
        existente.Frequencia = dto.Frequencia;
        existente.CentroCustoId = dto.CentroCustoId;
        existente.PlanoContaId = dto.PlanoContaId;
        existente.FornecedorId = dto.FornecedorId;
        existente.TipoPagamentoId = dto.TipoPagamentoId;
        existente.FormaPagamentoId = dto.FormaPagamentoId;
        existente.DataAlteracao = DateTime.UtcNow;
    }

    // Reprocessar lançamentos futuros
    private async Task ReprocessarLancamentosFuturos(DespesaFixaModel despesa)
    {
        var dataAtual = DateTime.Now.Date;

        // Buscar lançamentos futuros não pagos
        var lancamentosFuturos = await _context.Financ_Pagar
            .Where(x => x.DespesaFixaId == despesa.Id)
            .Include(x => x.subFinancPagar)
            .Where(x => x.subFinancPagar.Any(s => s.DataVencimento > dataAtual) &&
                       (x.Status == "Pendente" || x.Status == "Vencido"))
            .ToListAsync();

        // Separar lançamentos: alguns podem ter sub-parcelas pagas e outras não
        var lancamentosParaRemover = new List<Financ_PagarModel>();
        var lancamentosParaAtualizar = new List<Financ_PagarModel>();

        foreach (var lancamento in lancamentosFuturos)
        {
            var subParcelas = lancamento.subFinancPagar.ToList();
            var subParcelasPagas = subParcelas.Where(s => s.DataPagamento.HasValue).ToList();
            var subParcelasPendentes = subParcelas.Where(s => !s.DataPagamento.HasValue && s.DataVencimento > dataAtual).ToList();

            if (subParcelasPagas.Any() && subParcelasPendentes.Any())
            {
                // Remover apenas sub-parcelas pendentes futuras
                foreach (var subPendente in subParcelasPendentes)
                {
                    _context.Entry(subPendente).State = EntityState.Deleted;
                }
                lancamentosParaAtualizar.Add(lancamento);
            }
            else if (!subParcelasPagas.Any())
            {
                // Remover lançamento completo se não tem nada pago
                lancamentosParaRemover.Add(lancamento);
            }
        }

        // Remover lançamentos completamente futuros
        if (lancamentosParaRemover.Any())
        {
            _context.Financ_Pagar.RemoveRange(lancamentosParaRemover);
        }

        await _context.SaveChangesAsync();

        // Gerar novos lançamentos futuros
        var novosLancamentos = await GerarLancamentosFuturos(despesa, dataAtual);

        if (novosLancamentos.Any())
        {
            await _context.Financ_Pagar.AddRangeAsync(novosLancamentos);
            await _context.SaveChangesAsync();
        }
    }

    // Gerar apenas lançamentos futuros
    private async Task<List<Financ_PagarModel>> GerarLancamentosFuturos(DespesaFixaModel despesa, DateTime apartirDe)
    {
        var listaPagar = new List<Financ_PagarModel>();

        if (!despesa.DataInicio.HasValue)
            return listaPagar;

        // Calcular quantas parcelas baseado na frequência e período
        var quantidadeParcelas = CalcularQuantidadeParcelas(despesa);

        // Calcular quantos lançamentos já deveriam ter sido gerados até a data atual
        var mesesDecorridos = ((apartirDe.Year - despesa.DataInicio.Value.Year) * 12) +
                             (apartirDe.Month - despesa.DataInicio.Value.Month);

        var indiceInicio = Math.Max(0, mesesDecorridos);

        for (int i = indiceInicio; i < quantidadeParcelas; i++)
        {
            var dataVencimento = CalcularDataVencimento(despesa.DataInicio.Value, despesa.DiaVencimento, i, despesa.Frequencia);

            // Só gerar lançamentos futuros
            if (dataVencimento <= apartirDe)
                continue;

            // Verificar se ainda está dentro do período (se DataFim foi informada)
            if (despesa.DataFim.HasValue && dataVencimento > despesa.DataFim.Value)
                break;

            // Verificar se já existe lançamento para esta data
            var jaExiste = await _context.Financ_Pagar
                .AnyAsync(x => x.DespesaFixaId == despesa.Id &&
                              x.subFinancPagar.Any(s => s.DataVencimento.Value.Date == dataVencimento.Date));

            if (jaExiste)
                continue;

            var pagar = new Financ_PagarModel
            {
                Descricao = $"{despesa.Descricao} - {dataVencimento:MM/yyyy}",
                DataEmissao = DateTime.UtcNow,
                ValorOriginal = despesa.Valor,
                Valor = despesa.Valor,
                Status = "Pendente",
                FornecedorId = despesa.FornecedorId,
                PlanoContaId = despesa.PlanoContaId,
                CentroCustoId = despesa.CentroCustoId,
                TipoPagamentoId = despesa.TipoPagamentoId,
                DespesaFixaId = despesa.Id,
                subFinancPagar = new List<Financ_PagarSubModel>()
            };

            var sub = new Financ_PagarSubModel
            {
                Parcela = 1,
                Valor = despesa.Valor,
                DataVencimento = dataVencimento,
                FormaPagamentoId = despesa.FormaPagamentoId
            };

            pagar.subFinancPagar.Add(sub);
            listaPagar.Add(pagar);
        }

        return listaPagar;
    }

    // Método para inativar despesa fixa (soft delete)
    public async Task<ResponseModel<bool>> Inativar(int id)
    {
        ResponseModel<bool> resposta = new ResponseModel<bool>();

        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var despesa = await _context.Despesas.FindAsync(id);

            if (despesa == null)
            {
                resposta.Mensagem = "Despesa fixa não encontrada";
                resposta.Status = false;
                return resposta;
            }

            // Verificar se existem lançamentos pagos
            var temPagamentos = await _context.Financ_Pagar
                .AnyAsync(x => x.DespesaFixaId == id && x.Status == "Pago");

            if (temPagamentos)
            {
                // Se tem pagamentos, apenas inativar e cancelar lançamentos futuros
                despesa.Ativo = false;
                despesa.DataFim = DateTime.Now.Date;

                // Cancelar lançamentos futuros pendentes
                var lancamentosFuturos = await _context.Financ_Pagar
                    .Where(x => x.DespesaFixaId == id &&
                               x.Status == "Pendente" &&
                               x.subFinancPagar.Any(s => s.DataVencimento > DateTime.Now))
                    .ToListAsync();

                foreach (var lancamento in lancamentosFuturos)
                {
                    lancamento.Status = "Cancelado";
                }
            }
            else
            {
                // Se não tem pagamentos, pode remover tudo
                var lancamentos = await _context.Financ_Pagar
                    .Where(x => x.DespesaFixaId == id)
                    .ToListAsync();

                _context.Financ_Pagar.RemoveRange(lancamentos);
                _context.Despesas.Remove(despesa);
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            resposta.Dados = true;
            resposta.Mensagem = "Despesa fixa inativada com sucesso";
            resposta.Status = true;

            return resposta;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            resposta.Mensagem = "Erro ao inativar despesa: " + ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }
}

  