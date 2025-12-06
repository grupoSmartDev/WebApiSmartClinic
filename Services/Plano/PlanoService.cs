using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Dto.Agenda;
using WebApiSmartClinic.Dto.Financ_Receber;
using WebApiSmartClinic.Dto.Plano;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Services.Agenda;

namespace WebApiSmartClinic.Services.Plano;

public class PlanoService : IPlanoInterface
{
    private readonly AppDbContext _context;
    private readonly AgendaService _agendaService;
    public PlanoService(AppDbContext context, AgendaService agendaService)
    {
        _context = context;
        _agendaService = agendaService;
    }

    public async Task<ResponseModel<PlanoModel>> BuscarPorId(int idPlano)
    {
        ResponseModel<PlanoModel> resposta = new ResponseModel<PlanoModel>();
        
        try
        {
            var plano = await _context.Plano.FirstOrDefaultAsync(x => x.Id == idPlano);
            if (plano == null)
            {
                resposta.Mensagem = "Nenhum Plano encontrado";
                return resposta;
            }

            resposta.Dados = plano;
            resposta.Mensagem = "Plano Encontrado";
            
            return resposta;
        }
        catch (Exception ex)
        {

            resposta.Mensagem = "Erro ao buscar Plano";
            resposta.Status = false;
            
            return resposta;
        }
    }

    public async Task<ResponseModel<List<PlanoModel>>> Criar(PlanoCreateDto planoCreateDto, int pageNumber = 1, int pageSize = 10)
    {
        ResponseModel<List<PlanoModel>> resposta = new ResponseModel<List<PlanoModel>>();

        try
        {
            var plano = new PlanoModel();

            plano.Descricao = planoCreateDto.Descricao;
            plano.TempoMinutos = planoCreateDto.TempoMinutos;
            plano.CentroCustoId = planoCreateDto.CentroCustoId;
            plano.DiasSemana = planoCreateDto.DiasSemana;
            plano.ValorBimestral = planoCreateDto.ValorBimestral;
            plano.ValorMensal = planoCreateDto.ValorMensal;
            plano.ValorTrimestral = planoCreateDto.ValorTrimestral;
            plano.ValorQuadrimestral = planoCreateDto.ValorQuadrimestral;
            plano.ValorSemestral = planoCreateDto.ValorSemestral;
            plano.ValorAnual = planoCreateDto.ValorAnual;
            plano.DataInicio = planoCreateDto.DataInicio;
            plano.DataFim = planoCreateDto.DataFim;
            plano.Ativo = planoCreateDto.Ativo;
            plano.PacienteId = planoCreateDto.PacienteId;
            plano.FinanceiroId = planoCreateDto.FinanceiroId;
            plano.TipoMes = planoCreateDto.TipoMes;

            

            _context.Add(plano);
            await _context.SaveChangesAsync();

            var query = _context.Plano.AsQueryable();

            resposta = await PaginationHelper.PaginateAsync(query, pageNumber, pageSize);
            resposta.Mensagem = "Plano criado com sucesso";
            
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            
            return resposta;
        }
    }

    public async Task<ResponseModel<List<PlanoModel>>> Delete(int idPlano, int pageNumber = 1, int pageSize = 10)
    {
        ResponseModel<List<PlanoModel>> resposta = new ResponseModel<List<PlanoModel>>();
        try
        {
            var plano = await _context.Plano
                .Include(p => p.Paciente) // Assumindo que existe navegação para pacientes
                .FirstOrDefaultAsync(x => x.Id == idPlano);

            if (plano == null)
            {
                resposta.Mensagem = "Nenhum Plano encontrado";
                resposta.Status = false;
                return resposta;
            }

            // Verificar se há pacientes ativos vinculados ao plano
            if (plano.Paciente != null && plano.Paciente.Plano.Ativo) // Assumindo que paciente tem propriedade Ativo
            {
                resposta.Mensagem = "Não é possível inativar o plano pois existem pacientes ativos vinculados a ele";
                resposta.Status = false;
                return resposta;
            }

            // Soft delete - marcar como inativo
            plano.Ativo = false;
      
            _context.Update(plano);
            await _context.SaveChangesAsync();

            // Buscar apenas planos ativos para a paginação
            var query = _context.Plano.Where(p => p.Ativo == true).AsQueryable();
            resposta = await PaginationHelper.PaginateAsync(query, pageNumber, pageSize);
            resposta.Mensagem = "Esse processo não irá excluir o registro e sim inativá-lo. Plano inativado com sucesso";

            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<PlanoModel>>> Editar(PlanoEdicaoDto planoEdicaoDto, int pageNumber = 1, int pageSize = 10)
    {
        ResponseModel<List<PlanoModel>> resposta = new ResponseModel<List<PlanoModel>>();

        try
        {
            var plano = _context.Plano.FirstOrDefault(x => x.Id == planoEdicaoDto.Id);
            if (plano == null)
            {
                resposta.Mensagem = "Plano não encontrado";
                return resposta;
            }

            plano.Descricao = planoEdicaoDto.Descricao;
            plano.TempoMinutos = planoEdicaoDto.TempoMinutos;
            plano.CentroCustoId = planoEdicaoDto.CentroCustoId;
            plano.DiasSemana = planoEdicaoDto.DiasSemana;
            plano.ValorBimestral = planoEdicaoDto.ValorBimestral;
            plano.ValorMensal = planoEdicaoDto.ValorMensal;
            plano.ValorTrimestral = planoEdicaoDto.ValorTrimestral;
            plano.ValorQuadrimestral = planoEdicaoDto.ValorQuadrimestral;
            plano.ValorSemestral = planoEdicaoDto.ValorSemestral;
            plano.ValorAnual = planoEdicaoDto.ValorAnual;
            plano.DataInicio = planoEdicaoDto.DataInicio;
            plano.DataFim = planoEdicaoDto.DataFim;
            plano.Ativo = planoEdicaoDto.Ativo;
            plano.PacienteId = planoEdicaoDto.PacienteId;
            plano.FinanceiroId = planoEdicaoDto.FinanceiroId;
            plano.TipoMes = planoEdicaoDto.TipoMes;



            _context.Update(plano);
            await _context.SaveChangesAsync();

            var query = _context.Plano.AsQueryable();

            resposta = await PaginationHelper.PaginateAsync(query, pageNumber, pageSize);
            resposta.Mensagem = "Plano Atualizado com sucesso";
            
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            
            return resposta;
        }
    }

    public async Task<ResponseModel<List<PlanoModel>>> Listar(int pageNumber = 1, int pageSize = 10, int? codigoFiltro = null, string? descricaoFiltro = null, bool paginar = true, bool paraPaciente = false)
    {
        ResponseModel<List<PlanoModel>> resposta = new ResponseModel<List<PlanoModel>>();

        try
        {
            var query = _context.Plano.AsQueryable();

            if (paraPaciente)
            {
               query = query.Where(p => p.Ativo == false);
            }

            query = query.Where(x =>
                (!codigoFiltro.HasValue || x.Id == codigoFiltro) &&
                (string.IsNullOrEmpty(descricaoFiltro) || x.Descricao == descricaoFiltro)
            );

            query.OrderBy(x => x.Id);

            resposta = paginar ? await PaginationHelper.PaginateAsync(query, pageNumber, pageSize) : new ResponseModel<List<PlanoModel>> { Dados = await query.ToListAsync() };
            resposta.Mensagem = "Planos listados com sucesso";

            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = "Erro ao listar planos: " + ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<PlanoModel>>> InativarPlanoPaciente(PlanoEdicaoDto planoEditarDto,int pageNumber = 1, int pageSize = 10)
    {
        ResponseModel<List<PlanoModel>> resposta = new ResponseModel<List<PlanoModel>>();

        try
        {
            var plano = await _context.Plano
                .FirstOrDefaultAsync(p => p.Id == planoEditarDto.Id);

            if (plano == null)
            {
                resposta.Mensagem = "Plano não encontrado";
                resposta.Status = false;
                return resposta;
            }

            // Verifica se já está inativo
            if (!plano.Ativo)
            {
                resposta.Mensagem = "Plano já está inativo";
                resposta.Status = false;
                return resposta;
            }

            plano.Ativo = false;
            plano.DataFim = DateTime.Now;

            _context.Update(plano);
            await _context.SaveChangesAsync();

            
            resposta.Status = true;


            var query = _context.Plano.AsQueryable();

            resposta = await PaginationHelper.PaginateAsync(query, pageNumber, pageSize);
            resposta.Mensagem = "Plano desativado com sucesso!";

            return resposta;
  
        }
        catch (Exception ex)
        {
            resposta.Mensagem = $"Erro ao desativar plano: {ex.Message}";
            resposta.Status = false;
            return resposta;
        }
    }

    

    public async Task<ResponseModel<PlanoModel>> PlanoParaPaciente(PlanoCreateDto planoCreateDto)
    {
        ResponseModel<PlanoModel> resposta = new ResponseModel<PlanoModel>();

        try
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // ========== VALIDAÇÕES INICIAIS ==========
                var paciente = await _context.Paciente
                    .Include(p => p.Plano) // ✅ Include adicionado
                    .FirstOrDefaultAsync(p => p.Id == planoCreateDto.PacienteId);

                if (paciente == null)
                {
                    resposta.Mensagem = "Paciente não encontrado";
                    resposta.Status = false;
                    return resposta;
                }

                // Valida datas
                if (planoCreateDto.DataInicio > planoCreateDto.DataFim)
                {
                    resposta.Mensagem = "Data de início não pode ser maior que data fim";
                    resposta.Status = false;
                    return resposta;
                }

                // Valida TipoMes
                if (string.IsNullOrEmpty(planoCreateDto.TipoMes) ||
                    (planoCreateDto.TipoMes != "b" && planoCreateDto.TipoMes != "t" && planoCreateDto.TipoMes != "m" && planoCreateDto.TipoMes != "q" && planoCreateDto.TipoMes != "s" && planoCreateDto.TipoMes != "a"))
                {
                    resposta.Mensagem = "Tipo Mes inválido";
                    resposta.Status = false;
                    return resposta;
                }

                // Valida profissionais e salas se houver agendamentos
                if (planoCreateDto.Agendamento?.DiasRecorrencia != null &&
                    planoCreateDto.Agendamento.DiasRecorrencia.Any())
                {
                    var validacao = await ValidarProfissionaisESalas(planoCreateDto.Agendamento.DiasRecorrencia);
                    if (!validacao.Status)
                    {
                        return new ResponseModel<PlanoModel>
                        {
                            Mensagem = validacao.Mensagem,
                            Status = false
                        };
                    }
                }

                // ========== INATIVAR PLANO ANTERIOR ==========
                if (paciente.Plano != null && paciente.Plano.Ativo)
                {
                    await FinalizarPlanoAnterior(paciente);
                }

                // ========== CRIAR NOVO PLANO ==========
                var plano = new PlanoModel
                {
                    Descricao = planoCreateDto.Descricao,
                    TempoMinutos = planoCreateDto.TempoMinutos,
                    CentroCustoId = planoCreateDto.CentroCustoId,
                    DiasSemana = planoCreateDto.DiasSemana,
                    ValorBimestral = planoCreateDto.ValorBimestral,
                    ValorMensal = planoCreateDto.ValorMensal,
                    ValorTrimestral = planoCreateDto.ValorTrimestral,
                    ValorQuadrimestral = planoCreateDto.ValorQuadrimestral,
                    ValorSemestral = planoCreateDto.ValorSemestral,
                    ValorAnual = planoCreateDto.ValorAnual,
                    DataInicio = planoCreateDto.DataInicio,
                    DataFim = planoCreateDto.DataFim,
                    Ativo = planoCreateDto.Ativo,
                    PacienteId = paciente.Id,
                    TipoMes = planoCreateDto.TipoMes
                };

                // ========== CRIAR FINANCEIRO SE NECESSÁRIO ==========
                if (planoCreateDto.Financeiro != null)
                {
                    var financeiroId = await CriarFinanceiro(planoCreateDto.Financeiro, plano, paciente);
                    plano.FinanceiroId = financeiroId;
                }

                _context.Add(plano);
                await _context.SaveChangesAsync();

                // ========== CRIAR AGENDAMENTOS SE NECESSÁRIO ==========
                if (planoCreateDto.Agendamento != null &&
                    planoCreateDto.Agendamento.DiasRecorrencia != null &&
                    planoCreateDto.Agendamento.DiasRecorrencia.Any())
                {
                    await CriarAgendamentosDoPlano(plano, paciente, planoCreateDto.Agendamento);
                }

                // ========== VINCULAR PLANO AO PACIENTE ==========
                paciente.PlanoId = plano.Id;
                _context.Update(paciente);

                // ✅ SALVAR TUDO DE UMA VEZ
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                resposta.Dados = plano;
                resposta.Mensagem = "Plano vinculado ao paciente com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                resposta.Mensagem = $"Erro ao vincular plano ao paciente: {ex.InnerException?.Message ?? ex.Message}";
                resposta.Status = false;
                return resposta;
            }
        }
        catch (Exception ex)
        {
            resposta.Mensagem = $"Erro ao vincular plano ao paciente: {ex.Message}";
            resposta.Status = false;
            return resposta;
        }
    }

    // ========== MÉTODOS AUXILIARES ==========

    private async Task<ResponseModel<bool>> ValidarProfissionaisESalas(List<DiaRecorrenciaDto> diasRecorrencia)
    {
        var resposta = new ResponseModel<bool>();

        // Valida profissionais
        var profissionaisIds = diasRecorrencia
            .Select(d => d.ProfissionalId)
            .Where(id => id.HasValue)
            .Select(id => id.Value)
            .Distinct()
            .ToList();

        if (profissionaisIds.Any())
        {
            var profExistem = await _context.Profissional
                .Where(p => profissionaisIds.Contains(p.Id))
                .CountAsync();

            if (profExistem != profissionaisIds.Count)
            {
                resposta.Mensagem = "Um ou mais profissionais não existem";
                resposta.Status = false;
                return resposta;
            }
        }

        // Valida salas
        var salasIds = diasRecorrencia
            .Select(d => d.SalaId)
            .Where(id => id.HasValue)
            .Select(id => id.Value)
            .Distinct()
            .ToList();

        if (salasIds.Any())
        {
            var salasExistem = await _context.Sala
                .Where(s => salasIds.Contains(s.Id))
                .CountAsync();

            if (salasExistem != salasIds.Count)
            {
                resposta.Mensagem = "Uma ou mais salas não existem";
                resposta.Status = false;
                return resposta;
            }
        }

        resposta.Status = true;
        resposta.Dados = true;
        return resposta;
    }

    private async Task FinalizarPlanoAnterior(PacienteModel paciente)
    {
        var planoAnterior = paciente.Plano;
        var agora = DateTime.UtcNow;

        // Conta quantas sessões foram utilizadas
        var sessoesUtilizadas = await _context.Agenda
            .Where(a => a.PacienteId == paciente.Id &&
                        a.StatusId == 4 && // Concluído
                        a.Data >= planoAnterior.DataInicio &&
                        a.Data <= agora)
            .CountAsync();

        // Calcula quantas sessões eram previstas
        var sessoesPrevistas = CalcularSessoesContratadas(planoAnterior);

        // Calcula o valor que foi pago
        var valorPago = CalcularValorPago(planoAnterior);

        // Cria histórico do plano anterior
        var historico = new PacientePlanoHistoricoModel
        {
            PacienteId = paciente.Id,
            PlanoId = planoAnterior.Id,
            DataInicio = planoAnterior.DataInicio ?? agora,
            DataFim = agora,
            AulasContratadas = sessoesPrevistas,
            AulasUtilizadas = sessoesUtilizadas,
            ValorPago = valorPago,
            Status = StatusPlano.Finalizado,
            MotivoFinalizacao = "Novo plano contratado",
            Observacoes = $"Plano finalizado automaticamente. Utilizou {sessoesUtilizadas} de {sessoesPrevistas} sessões."
        };

        _context.PacientePlanoHistoricos.Add(historico);

        // Inativa o plano anterior
        planoAnterior.Ativo = false;
        planoAnterior.DataFim = agora;
        _context.Update(planoAnterior);
    }

    private int CalcularSessoesContratadas(PlanoModel plano)
    {
        // Calcula baseado no tipo de plano e dias da semana
        if (!plano.DataInicio.HasValue || !plano.DataFim.HasValue)
            return 0;

        var diasEntreDatas = (plano.DataFim.Value - plano.DataInicio.Value).Days;
        var semanas = diasEntreDatas / 7;

        return semanas * plano.DiasSemana;
    }

    private decimal CalcularValorPago(PlanoModel plano)
    {
        // Retorna o valor baseado no tipo de plano
        return plano.TipoMes switch
        {
            "M" => plano.ValorMensal ?? 0,
            "B" => plano.ValorBimestral ?? 0,
            "T" => plano.ValorTrimestral ?? 0,
            "Q" => plano.ValorQuadrimestral ?? 0,
            "S" => plano.ValorSemestral ?? 0,
            "A" => plano.ValorAnual ?? 0,
            _ => 0
        };
    }

    private async Task<int> CriarFinanceiro(
        Financ_ReceberCreateDto financeiroDto,
        PlanoModel plano,
        PacienteModel paciente)
    {
        var financ_receber = new Financ_ReceberModel
        {
            IdOrigem = financeiroDto.IdOrigem ?? 0,
            NrDocto = financeiroDto.NrDocto ?? 0,
            DataEmissao = plano.DataInicio,
            ValorOriginal = financeiroDto.Valor,
            ValorPago = financeiroDto.ValorPago,
            Valor = financeiroDto.Valor,
            Status = financeiroDto.Status,
            NotaFiscal = financeiroDto.NotaFiscal,
            Descricao = $"{plano.Descricao} - {paciente.Nome}",
            Parcela = financeiroDto.Parcela,
            Classificacao = financeiroDto.Classificacao,
            Observacao = $"{plano.Descricao} - Criado pelo Gerenciamento do paciente",
            FornecedorId = financeiroDto.FornecedorId,
            CentroCustoId = financeiroDto.CentroCustoId,
            PacienteId = paciente.Id,
            TipoPagamentoId = financeiroDto.TipoPagamentoId ?? 1,
            BancoId = financeiroDto.BancoId == 0 ? null : financeiroDto.BancoId,
            subFinancReceber = new List<Financ_ReceberSubModel>()
        };

        // Adiciona as parcelas
        if (financeiroDto.subFinancReceber != null && financeiroDto.subFinancReceber.Any())
        {
            foreach (var parcela in financeiroDto.subFinancReceber)
            {
                var subItem = new Financ_ReceberSubModel
                {
                    Parcela = parcela.Parcela,
                    Valor = parcela.Valor,
                    ValorPago = 0,
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

        _context.Add(financ_receber);
        await _context.SaveChangesAsync(); // Precisa salvar aqui pra pegar o ID

        return financ_receber.Id;
    }

    private async Task CriarAgendamentosDoPlano(
        PlanoModel plano,
        PacienteModel paciente,
        AgendaCreateDto agendamentoDto)
    {
        var agendamentos = new List<AgendaModel>();
        DateTime dataAtual = plano.DataInicio ?? DateTime.UtcNow;
        DateTime dataFim = plano.DataFim ?? DateTime.UtcNow;

        // Processa cada dia entre a data inicial e final
        while (dataAtual <= dataFim)
        {
            // Verifica se o dia atual corresponde a algum dia de recorrência configurado
            foreach (var diaSemana in agendamentoDto.DiasRecorrencia)
            {
                if ((int)diaSemana.DiaSemana == (int)dataAtual.DayOfWeek)
                {
                    // Valida e converte horários
                    if (!TimeSpan.TryParse(diaSemana.HoraInicio, out TimeSpan horaInicio))
                    {
                        throw new Exception($"Formato de hora inválido para HoraInicio do dia {diaSemana.DiaSemana}");
                    }

                    if (!TimeSpan.TryParse(diaSemana.HoraFim, out TimeSpan horaFim))
                    {
                        throw new Exception($"Formato de hora inválido para HoraFim do dia {diaSemana.DiaSemana}");
                    }

                    // Cria o agendamento
                    var agenda = new AgendaModel
                    {
                        Titulo = $"Atendimento - {paciente.Nome}",
                        Data = DateTime.SpecifyKind(dataAtual, DateTimeKind.Unspecified),
                        HoraInicio = horaInicio,
                        HoraFim = horaFim,
                        PacienteId = paciente.Id,
                        ProfissionalId = diaSemana.ProfissionalId,
                        SalaId = diaSemana.SalaId,
                        Convenio = agendamentoDto.Convenio,
                        Valor = 0, // Valor zero pois está no plano
                        FormaPagamento = "Plano",
                        Pago = true, // Já está pago pelo plano
                        PacoteId = null,
                        LembreteSms = true,
                        LembreteWhatsapp = true,
                        LembreteEmail = true,
                        StatusId = 1, // Status inicial (Agendado)
                        IntegracaoGmail = true,
                        StatusFinal = false,
                        Avulso = false
                    };

                    agendamentos.Add(agenda);

                    // Só criar um agendamento por dia
                    break;
                }
            }

            // Avança para o próximo dia
            dataAtual = dataAtual.AddDays(1);
        }

        // ✅ Adiciona todos os agendamentos de uma vez
        if (agendamentos.Any())
        {
            await _context.Agenda.AddRangeAsync(agendamentos);
        }
    }

    public async Task<ResponseModel<PlanoModel>> RenovarPlano(PlanoRenovacaoDto renovacaoDto)
    {
        ResponseModel<PlanoModel> resposta = new ResponseModel<PlanoModel>();

        try
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Buscar o plano atual
                var planoAtual = await _context.Plano
                    .Include(p => p.Paciente)
                    .FirstOrDefaultAsync(p => p.Id == renovacaoDto.PlanoId);

                if (planoAtual == null)
                {
                    resposta.Mensagem = "Plano não encontrado";
                    resposta.Status = false;
                    return resposta;
                }

                var paciente = planoAtual.Paciente;
                if (paciente == null)
                {
                    resposta.Mensagem = "Paciente não encontrado";
                    resposta.Status = false;
                    return resposta;
                }

                // Inativar o plano atual
                planoAtual.Ativo = false;
                planoAtual.DataFim = DateTime.Now;

                // Criar o novo plano com base no atual
                var novoPlano = new PlanoModel
                {
                    Descricao = renovacaoDto.Descricao ?? planoAtual.Descricao,
                    TempoMinutos = planoAtual.TempoMinutos,
                    CentroCustoId = planoAtual.CentroCustoId,
                    DiasSemana = planoAtual.DiasSemana,
                    ValorBimestral = planoAtual.ValorBimestral,
                    ValorMensal = planoAtual.ValorMensal,
                    ValorTrimestral = planoAtual.ValorTrimestral,
                    ValorQuadrimestral = planoAtual.ValorQuadrimestral,
                    ValorSemestral = planoAtual.ValorSemestral,
                    ValorAnual = planoAtual.ValorAnual,
                    DataInicio = renovacaoDto.DataInicio,
                    DataFim = renovacaoDto.DataFim,
                    Ativo = true,
                    PacienteId = paciente.Id,
                    TipoMes = renovacaoDto.TipoMes
                };

                // Gerar financeiro se solicitado
                if (renovacaoDto.GerarFinanceiro && renovacaoDto.Financeiro != null)
                {
                    var financ_receber = new Financ_ReceberModel
                    {
                        IdOrigem = 0,
                        NrDocto = 0,
                        DataEmissao = renovacaoDto.DataInicio,
                        ValorOriginal = renovacaoDto.Financeiro.Valor,
                        ValorPago = 0,
                        Valor = renovacaoDto.Financeiro.Valor,
                        Status = "Pendente",
                        NotaFiscal = "",
                        Descricao = $"Renovação de Plano - {paciente.Nome}",
                        Parcela = renovacaoDto.Financeiro.Parcela,
                        Classificacao = "Receita",
                        Observacao = renovacaoDto.Financeiro.Observacao ?? "Renovação de plano",
                        CentroCustoId = renovacaoDto.Financeiro.CentroCustoId,
                        TipoPagamentoId = (int)renovacaoDto.Financeiro.TipoPagamentoId,
                        PacienteId = paciente.Id,
                        BancoId = null,
                        subFinancReceber = new List<Financ_ReceberSubModel>()
                    };

                    _context.Add(financ_receber);
                    await _context.SaveChangesAsync();

                    // Adicionar parcelas
                    if (renovacaoDto.Financeiro.subFinancReceber != null && renovacaoDto.Financeiro.subFinancReceber.Any())
                    {
                        foreach (var parcela in renovacaoDto.Financeiro.subFinancReceber)
                        {
                            var subItem = new Financ_ReceberSubModel
                            {
                                financReceberId = financ_receber.Id,
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

                    novoPlano.FinanceiroId = financ_receber.Id;
                }

                _context.Add(novoPlano);
                await _context.SaveChangesAsync();

                // Gerar agendamentos se solicitado
                if (renovacaoDto.GerarAgendamento && renovacaoDto.Agendamento != null)
                {
                    List<AgendaModel> agendamentos = new List<AgendaModel>();

                    // Configurar dados do agendamento
                    DateTime? dataAtual = renovacaoDto.DataInicio;
                    DateTime? dataFim = renovacaoDto.DataFim;

                    // Verificar se há dias de recorrência configurados
                    if (renovacaoDto.Agendamento.DiasRecorrencia != null && renovacaoDto.Agendamento.DiasRecorrencia.Count > 0)
                    {
                        // Processar cada dia entre a data inicial e final
                        while (dataAtual <= dataFim)
                        {
                            // Verificar se o dia atual corresponde a algum dia de recorrência configurado
                            foreach (var diaSemana in renovacaoDto.Agendamento.DiasRecorrencia)
                            {
                                if ((int)diaSemana.DiaSemana == (int)dataAtual.Value.DayOfWeek)
                                {
                                    // Criar agendamento para este dia
                                    var agenda = new AgendaModel
                                    {
                                        Titulo = $"Atendimento - {paciente.Nome}",
                                        Data = dataAtual,
                                        PacienteId = paciente.Id,
                                        ProfissionalId = diaSemana.ProfissionalId,
                                        SalaId = diaSemana.SalaId,
                                        Convenio = renovacaoDto.Agendamento.Convenio,
                                        Valor = 0, // Valor zero pois está no plano
                                        FormaPagamento = "Plano",
                                        Pago = true, // Já está pago pelo plano
                                        PacoteId = null,
                                        LembreteSms = true,
                                        LembreteWhatsapp = true,
                                        LembreteEmail = true,
                                        StatusId = 1, // Status inicial
                                        IntegracaoGmail = true,
                                        StatusFinal = false,
                                        Avulso = false, // Não é avulso pois faz parte do plano
                                    };

                                    // Processar horários específicos para este dia da semana
                                    if (TimeSpan.TryParse(diaSemana.HoraInicio, out TimeSpan horaInicio))
                                    {
                                        agenda.HoraInicio = horaInicio;
                                    }
                                    else
                                    {
                                        throw new Exception($"Formato de hora inválido para HoraInicio do dia {diaSemana.DiaSemana}");
                                    }

                                    if (TimeSpan.TryParse(diaSemana.HoraFim, out TimeSpan horaFim))
                                    {
                                        agenda.HoraFim = horaFim;
                                    }
                                    else
                                    {
                                        throw new Exception($"Formato de hora inválido para HoraFim do dia {diaSemana.DiaSemana}");
                                    }

                                    _context.Agenda.Add(agenda);
                                    agendamentos.Add(agenda);

                                    // Só criar um agendamento por dia
                                    break;
                                }
                            }

                            // Avançar para o próximo dia
                            dataAtual = dataAtual.Value.AddDays(1);
                        }

                        // Salvar todos os agendamentos criados
                        await _context.SaveChangesAsync();
                    }
                }

                // Atualizar o plano do paciente
                paciente.PlanoId = novoPlano.Id;
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                resposta.Dados = novoPlano;
                resposta.Mensagem = "Plano renovado com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                resposta.Mensagem = $"Erro ao renovar plano: {ex.InnerException?.Message ?? ex.Message}";
                resposta.Status = false;
                return resposta;
            }
        }
        catch (Exception ex)
        {
            resposta.Mensagem = $"Erro ao renovar plano: {ex.Message}";
            resposta.Status = false;
            return resposta;
        }
    }
}