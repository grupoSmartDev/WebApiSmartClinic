using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Dto.Agenda;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Agenda;

public class AgendaService : IAgendaInterface
{
    private readonly AppDbContext _context;

    public AgendaService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseModel<AgendaModel>> BuscarPorId(int idAgenda)
    {
        ResponseModel<AgendaModel> resposta = new ResponseModel<AgendaModel>();
        try
        {
            var agenda = await _context.Agenda.FirstOrDefaultAsync(x => x.Id == idAgenda);
            if (agenda == null)
            {
                resposta.Mensagem = "Nenhum Agenda encontrado";
                return resposta;
            }

            resposta.Dados = agenda;
            resposta.Mensagem = "Agenda Encontrado";
            return resposta;
        }
        catch (Exception ex)
        {

            resposta.Mensagem = "Erro ao buscar Agenda";
            resposta.Status = false;
            return resposta;
        }
    }

    // MODIFICADO: Método Criar agora consome pacote quando status = Concluído
    public async Task<ResponseModel<List<AgendaModel>>> Criar(AgendaCreateDto agendaCreateDto)
    {
       ResponseModel<List<AgendaModel>> resposta = new ResponseModel<List<AgendaModel>>();

        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                List<AgendaModel> agendamentos = new List<AgendaModel>();

                DateTime? dataAtual = agendaCreateDto.Data;
                DateTime? dataFim = agendaCreateDto.DiasRecorrencia != null && agendaCreateDto.DiasRecorrencia.Count > 0 ? agendaCreateDto.DataFimRecorrencia : dataAtual;

                while (dataAtual <= dataFim)
                {

                    bool criarAgendamento = false;

                    if (agendaCreateDto.DiasRecorrencia == null)
                    {
                        // Se não tem recorrência, cria apenas um agendamento na data original
                        criarAgendamento = true;
                    }
                    else
                    {
                        // Verificando se algum dos dias selecionados corresponde ao dia atual
                        foreach (var diaSemana in agendaCreateDto.DiasRecorrencia)
                        {
                            // Verifica se o dia da semana selecionado corresponde ao dia atual
                            if ((int)diaSemana.DiaSemana == (int)dataAtual.Value.DayOfWeek)
                            {
                                criarAgendamento = true;
                                break;
                            }
                        }
                    }

                    if (criarAgendamento)
                    {
                        var agenda = new AgendaModel
                        {
                            Titulo = agendaCreateDto.Titulo,
                            Data = dataAtual,
                            PacienteId = agendaCreateDto.PacienteId,
                            ProfissionalId = agendaCreateDto.ProfissionalId,
                            SalaId = agendaCreateDto.SalaId,
                            Convenio = agendaCreateDto.Convenio,
                            Valor = agendaCreateDto.Valor,
                            FormaPagamento = "teste",
                            Pago = agendaCreateDto.Pago,
                            PacoteId = agendaCreateDto.PacoteId,
                            LembreteSms = agendaCreateDto.LembreteSms,
                            LembreteWhatsapp = agendaCreateDto.LembreteWhatsapp,
                            LembreteEmail = agendaCreateDto.LembreteEmail,
                            StatusId = agendaCreateDto.StatusId,
                            IntegracaoGmail = agendaCreateDto.IntegracaoGmail,
                            StatusFinal = agendaCreateDto.StatusFinal,
                            Avulso = agendaCreateDto.Avulso,
                        };

                        // Processamento das horas
                        if (TimeSpan.TryParse(agendaCreateDto.HoraInicio, out TimeSpan horaInicio))
                        {
                            agenda.HoraInicio = horaInicio;
                        }
                        else
                        {
                            throw new Exception("Formato de hora inválido para HoraInicio");
                        }

                        if (TimeSpan.TryParse(agendaCreateDto.HoraFim, out TimeSpan horaFim))
                        {
                            agenda.HoraFim = horaFim;
                        }
                        else
                        {
                            throw new Exception("Formato de hora inválido para HoraFim");
                        }

                        if (agendaCreateDto.FinancReceber != null)
                        {
                            var financ_receber = new Financ_ReceberModel
                            {
                                IdOrigem = agendaCreateDto.FinancReceber.IdOrigem ?? 0,
                                NrDocto = agendaCreateDto.FinancReceber.NrDocto ?? 0,
                                DataEmissao = agendaCreateDto.FinancReceber.DataEmissao,
                                ValorOriginal = agendaCreateDto.FinancReceber.ValorOriginal,
                                ValorPago = agendaCreateDto.FinancReceber.ValorPago,
                                Valor = agendaCreateDto.FinancReceber.Valor,
                                Status = agendaCreateDto.FinancReceber.Status,
                                NotaFiscal = agendaCreateDto.FinancReceber.NotaFiscal,
                                Descricao = agendaCreateDto.FinancReceber.Descricao,
                                Parcela = agendaCreateDto.FinancReceber.Parcela,
                                Classificacao = agendaCreateDto.FinancReceber.Classificacao,
                                Observacao = agendaCreateDto.FinancReceber.Observacao,
                                FornecedorId = agendaCreateDto.FinancReceber.FornecedorId,
                                CentroCustoId = agendaCreateDto.FinancReceber.CentroCustoId,
                                PacienteId = agendaCreateDto.FinancReceber.PacienteId,
                                BancoId = agendaCreateDto.FinancReceber.BancoId,
                                TipoPagamentoId = (int)agendaCreateDto.FinancReceber.TipoPagamentoId,
                                subFinancReceber = new List<Financ_ReceberSubModel>()
                            };

                            _context.Financ_Receber.Add(financ_receber);
                            await _context.SaveChangesAsync();

                            foreach (var parcela in agendaCreateDto.FinancReceber.subFinancReceber)
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
                                    DataVencimento = dataAtual,
                                    Observacao = parcela.Observacao
                                };

                                financ_receber.subFinancReceber.Add(subItem);
                            }

                            await _context.SaveChangesAsync();
                            agenda.FinancReceberId = financ_receber.Id;
                        }

                        _context.Agenda.Add(agenda);
                        await _context.SaveChangesAsync(); // IMPORTANTE: Salvar antes de consumir pacote

                        // ============== NOVO BLOCO: CONSUMO DE PACOTE ==============
                        // Se tem pacote E status é "Concluído" (4), consumir sessão
                        if (agenda.PacoteId.HasValue && agenda.StatusId == 4)
                        {
                            // Buscar o pacote do paciente
                            var pacotePaciente = await _context.PacotesPacientes
                                .FirstOrDefaultAsync(pp => pp.Id == agenda.PacoteId.Value);

                            if (pacotePaciente == null)
                            {
                                await transaction.RollbackAsync();
                                resposta.Mensagem = "Pacote do paciente não encontrado";
                                resposta.Status = false;
                                return resposta;
                            }

                            if (pacotePaciente.QuantidadeDisponivel <= 0)
                            {
                                await transaction.RollbackAsync();
                                resposta.Mensagem = "Pacote não possui sessões disponíveis";
                                resposta.Status = false;
                                return resposta;
                            }

                            // Criar registro de uso
                            var uso = new PacoteUsoModel
                            {
                                PacotePacienteId = pacotePaciente.Id,
                                AgendaId = agenda.Id,
                                PacienteUtilizadoId = agenda.PacienteId.Value,
                                DataUso = DateTime.UtcNow,
                                Observacao = $"Consumo automático - Agendamento #{agenda.Id} criado como concluído"
                            };

                            _context.PacotesUsos.Add(uso);
                            pacotePaciente.QuantidadeUsada++;

                            if (pacotePaciente.QuantidadeDisponivel == 0)
                            {
                                pacotePaciente.Status = "Esgotado";
                            }

                            _context.PacotesPacientes.Update(pacotePaciente);
                        }
                        // ============================================================

                        agendamentos.Add(agenda);
                    }

                    dataAtual = dataAtual.Value.AddDays(1);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                resposta.Dados = agendamentos;
                resposta.Mensagem = "Agendamentos criados com sucesso!";
                return resposta;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                resposta.Mensagem = $"Erro ao criar agendamento: {ex.Message}";
                resposta.Status = false;
                return resposta;
            }
        };
    }


    public async Task<ResponseModel<List<AgendaModel>>> Delete(int idAgenda)
    {
        ResponseModel<List<AgendaModel>> resposta = new ResponseModel<List<AgendaModel>>();
        try
        {
            var agenda = _context.Agenda.FirstOrDefault(x => x.Id == idAgenda);
            if (agenda == null)
            {
                resposta.Mensagem = "Agendamento não encontrado";
                return resposta;
            }

            _context.Agenda.Remove(agenda);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Agenda.ToListAsync();
            resposta.Mensagem = "Agendamento Removido com sucesso!";
            return resposta;
        }
        catch (Exception ex)
        {

            resposta.Mensagem = "Erro ao buscar Agenda";
            resposta.Status = false;
            return resposta;
        }
    }

    // MODIFICADO: Método Editar agora gerencia consumo/estorno de pacote
    public async Task<ResponseModel<List<AgendaModel>>> Editar(AgendaEdicaoDto agendaEdicaoDto)
    {
        ResponseModel<List<AgendaModel>> resposta = new ResponseModel<List<AgendaModel>>();

        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                var agenda = await _context.Agenda.FirstOrDefaultAsync(x => x.Id == agendaEdicaoDto.Id);
                if (agenda == null)
                {
                    resposta.Mensagem = "Agendamento não encontrado";
                    resposta.Status = false;
                    return resposta;
                }

                // ============== NOVO: Guardar valores anteriores ==============
                var statusAnterior = agenda.StatusId;
                var pacoteIdAnterior = agenda.PacoteId;
                // ==============================================================

                agenda.Titulo = agendaEdicaoDto.Titulo;
                agenda.PacienteId = agendaEdicaoDto.PacienteId;
                agenda.ProfissionalId = agendaEdicaoDto.ProfissionalId;
                agenda.SalaId = agendaEdicaoDto.SalaId;
                agenda.Convenio = agendaEdicaoDto.Convenio;
                agenda.Valor = agendaEdicaoDto.Valor;
                agenda.Pago = agendaEdicaoDto.Pago;
                agenda.PacoteId = agendaEdicaoDto.PacoteId; // IMPORTANTE: Atualizar pacote
                agenda.LembreteSms = agendaEdicaoDto.LembreteSms;
                agenda.LembreteWhatsapp = agendaEdicaoDto.LembreteWhatsapp;
                agenda.LembreteEmail = agendaEdicaoDto.LembreteEmail;
                agenda.StatusId = agendaEdicaoDto.StatusId;
                agenda.IntegracaoGmail = agendaEdicaoDto.IntegracaoGmail;
                agenda.StatusFinal = agendaEdicaoDto.StatusFinal;
                agenda.Avulso = agendaEdicaoDto.Avulso;

              
                    agenda.HoraInicio = agendaEdicaoDto.HoraInicio;
                

                 agenda.HoraFim = agendaEdicaoDto.HoraFim;
                

                _context.Agenda.Update(agenda);
                await _context.SaveChangesAsync();

                // ============== NOVO BLOCO: GERENCIAR CONSUMO/ESTORNO ==============
                // CENÁRIO 1: Status mudou de OUTRO → CONCLUÍDO e tem pacote
                if (statusAnterior != 4 && agenda.StatusId == 4 && agenda.PacoteId.HasValue)
                {
                    // Buscar o pacote do paciente
                    var pacotePaciente = await _context.PacotesPacientes
                        .FirstOrDefaultAsync(pp => pp.Id == agenda.PacoteId.Value);

                    if (pacotePaciente == null)
                    {
                        await transaction.RollbackAsync();
                        resposta.Mensagem = "Pacote do paciente não encontrado";
                        resposta.Status = false;
                        return resposta;
                    }

                    if (pacotePaciente.QuantidadeDisponivel <= 0)
                    {
                        await transaction.RollbackAsync();
                        resposta.Mensagem = "Pacote não possui sessões disponíveis";
                        resposta.Status = false;
                        return resposta;
                    }

                    // Criar registro de uso
                    var uso = new PacoteUsoModel
                    {
                        PacotePacienteId = pacotePaciente.Id,
                        AgendaId = agenda.Id,
                        PacienteUtilizadoId = agenda.PacienteId.Value,
                        DataUso = DateTime.UtcNow,
                        Observacao = "Consumo na edição - Status alterado para Concluído"
                    };

                    _context.PacotesUsos.Add(uso);
                    pacotePaciente.QuantidadeUsada++;

                    if (pacotePaciente.QuantidadeDisponivel == 0)
                    {
                        pacotePaciente.Status = "Esgotado";
                    }

                    _context.PacotesPacientes.Update(pacotePaciente);
                }
                // CENÁRIO 2: Status mudou de CONCLUÍDO → OUTRO e tinha pacote
                else if (statusAnterior == 4 && agenda.StatusId != 4 && pacoteIdAnterior.HasValue)
                {
                    // Buscar o uso relacionado a este agendamento
                    var uso = await _context.PacotesUsos
                        .Include(u => u.PacotePaciente)
                        .Where(u => u.AgendaId == agenda.Id && u.Ativo)
                        .FirstOrDefaultAsync();

                    if (uso != null)
                    {
                        uso.PacotePaciente.QuantidadeUsada--;

                        if (uso.PacotePaciente.Status == "Esgotado")
                        {
                            uso.PacotePaciente.Status = "Ativo";
                        }

                        uso.Ativo = false;

                        _context.PacotesUsos.Update(uso);
                        _context.PacotesPacientes.Update(uso.PacotePaciente);
                    }
                }
                // ==================================================================

                await transaction.CommitAsync();

                var listaAtualizada = await _context.Agenda.ToListAsync();
                resposta.Dados = listaAtualizada;
                resposta.Mensagem = "Agendamento atualizado com sucesso!";
                return resposta;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                resposta.Mensagem = $"Erro ao editar agendamento: {ex.Message}";
                resposta.Status = false;
                return resposta;
            }
        }
    }

    public async Task<ResponseModel<List<AgendaModel>>> Listar()
    {
        ResponseModel<List<AgendaModel>> resposta = new ResponseModel<List<AgendaModel>>();
        try
        {
            resposta.Dados = await _context.Agenda
                .Include(a => a.Paciente)
                .ToListAsync();
            resposta.Mensagem = "Agendamentos listados com sucesso!";
            return resposta;
        }
        catch (Exception ex)
        {

            resposta.Mensagem = "Erro ao buscar Agenda";
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<AgendaModel>>> CriarPeloPlano(AgendaCreateDto agendaCreateDto)
    {
        ResponseModel<List<AgendaModel>> resposta = new ResponseModel<List<AgendaModel>>();

        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                List<AgendaModel> agendamentos = new List<AgendaModel>();

                DateTime? dataAtual = agendaCreateDto.Data;
                DateTime? dataFim = agendaCreateDto.DiasRecorrencia != null && agendaCreateDto.DiasRecorrencia.Count > 0 ? agendaCreateDto.DataFimRecorrencia : dataAtual;

                while (dataAtual <= dataFim)
                {

                    bool criarAgendamento = false;

                    if (agendaCreateDto.DiasRecorrencia == null)
                    {
                        // Se não tem recorrência, cria apenas um agendamento na data original
                        criarAgendamento = true;
                    }
                    else
                    {
                        // Verificando se algum dos dias selecionados corresponde ao dia atual
                        foreach (var diaSemana in agendaCreateDto.DiasRecorrencia)
                        {
                            // Verifica se o dia da semana selecionado corresponde ao dia atual
                            if ((int)diaSemana.DiaSemana == (int)dataAtual.Value.DayOfWeek)
                            {
                                criarAgendamento = true;
                                break;
                            }
                        }
                    }

                    if (criarAgendamento)
                    {
                        var agenda = new AgendaModel
                        {
                            Titulo = agendaCreateDto.Titulo,
                            Data = dataAtual,
                            PacienteId = agendaCreateDto.PacienteId,
                            ProfissionalId = agendaCreateDto.ProfissionalId,
                            SalaId = agendaCreateDto.SalaId,
                            Convenio = agendaCreateDto.Convenio,
                            Valor = agendaCreateDto.Valor,
                            FormaPagamento = "teste",
                            Pago = agendaCreateDto.Pago,
                            PacoteId = agendaCreateDto.PacoteId,
                            LembreteSms = agendaCreateDto.LembreteSms,
                            LembreteWhatsapp = agendaCreateDto.LembreteWhatsapp,
                            LembreteEmail = agendaCreateDto.LembreteEmail,
                            StatusId = agendaCreateDto.StatusId,
                            IntegracaoGmail = agendaCreateDto.IntegracaoGmail,
                            StatusFinal = agendaCreateDto.StatusFinal,
                            Avulso = agendaCreateDto.Avulso,
                        };

                        // Processamento das horas
                        if (TimeSpan.TryParse(agendaCreateDto.HoraInicio, out TimeSpan horaInicio))
                        {
                            agenda.HoraInicio = horaInicio;
                        }
                        else
                        {
                            throw new Exception("Formato de hora inválido para HoraInicio");
                        }

                        if (TimeSpan.TryParse(agendaCreateDto.HoraFim, out TimeSpan horaFim))
                        {
                            agenda.HoraFim = horaFim;
                        }
                        else
                        {
                            throw new Exception("Formato de hora inválido para HoraFim");
                        }

                        if (agendaCreateDto.FinancReceber != null)
                        {
                            var financ_receber = new Financ_ReceberModel
                            {
                                IdOrigem = agendaCreateDto.FinancReceber.IdOrigem ?? 0,
                                NrDocto = agendaCreateDto.FinancReceber.NrDocto ?? 0,
                                DataEmissao = agendaCreateDto.FinancReceber.DataEmissao,
                                ValorOriginal = agendaCreateDto.FinancReceber.ValorOriginal,
                                ValorPago = agendaCreateDto.FinancReceber.ValorPago,
                                Valor = agendaCreateDto.FinancReceber.Valor,
                                Status = agendaCreateDto.FinancReceber.Status,
                                NotaFiscal = agendaCreateDto.FinancReceber.NotaFiscal,
                                Descricao = agendaCreateDto.FinancReceber.Descricao,
                                Parcela = agendaCreateDto.FinancReceber.Parcela,
                                Classificacao = agendaCreateDto.FinancReceber.Classificacao,
                                Observacao = agendaCreateDto.FinancReceber.Observacao,
                                FornecedorId = agendaCreateDto.FinancReceber.FornecedorId,
                                CentroCustoId = agendaCreateDto.FinancReceber.CentroCustoId,
                                PacienteId = agendaCreateDto.FinancReceber.PacienteId,
                                BancoId = agendaCreateDto.FinancReceber.BancoId,
                                TipoPagamentoId = (int)agendaCreateDto.FinancReceber.TipoPagamentoId,
                                subFinancReceber = new List<Financ_ReceberSubModel>()
                            };

                            _context.Financ_Receber.Add(financ_receber);
                            await _context.SaveChangesAsync();

                            foreach (var parcela in agendaCreateDto.FinancReceber.subFinancReceber)
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
                                    DataVencimento = dataAtual,
                                    Observacao = parcela.Observacao
                                };

                                financ_receber.subFinancReceber.Add(subItem);
                            }

                            await _context.SaveChangesAsync();
                            agenda.FinancReceberId = financ_receber.Id;
                        }

                        _context.Agenda.Add(agenda);
                        agendamentos.Add(agenda);
                    }

                    dataAtual = dataAtual.Value.AddDays(1);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                resposta.Dados = agendamentos;
                resposta.Mensagem = "Agendamentos criados com sucesso!";
                return resposta;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                resposta.Mensagem = $"Erro ao criar agendamento: {ex.Message}";
                resposta.Status = false;
                return resposta;
            }
        };
    }

    // MODIFICADO: Método AtualizarStatus agora gerencia consumo/estorno de pacote
    public async Task<ResponseModel<List<AgendaModel>>> AtualizarStatus(int id, int statusNovo)
    {
        ResponseModel<List<AgendaModel>> resposta = new ResponseModel<List<AgendaModel>>();

        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                var agendaAlterada = await _context.Agenda.FirstOrDefaultAsync(a => a.Id == id);
                if (agendaAlterada == null)
                {
                    resposta.Mensagem = "Agendamento não encontrado";
                    resposta.Status = false;
                    return resposta;
                }

                //1 Agendado
                //2 Confirmado
                //3 Em atendimento
                //4 Concluído
                //5 Cancelado pelo paciente
                //6 Cancelado pela clínica
                //7 Remarcado
                //8 Não compareceu

                if (statusNovo > 8)
                {
                    var status = await _context.Status.FirstOrDefaultAsync(a => a.Id == statusNovo);
                    if (status == null)
                    {
                        resposta.Mensagem = "Status não encontrado";
                        resposta.Status = false;
                        return resposta;
                    }
                }

                // ============== NOVO: Guardar status anterior ==============
                var statusAnterior = agendaAlterada.StatusId;
                // ===========================================================

                agendaAlterada.StatusId = statusNovo;

                _context.Update(agendaAlterada);
                await _context.SaveChangesAsync();

                // ============== NOVO BLOCO: GERENCIAR CONSUMO/ESTORNO ==============
                // Se mudou para "Concluído" (4) E tem pacote, consumir
                if (statusAnterior != 4 && statusNovo == 4 && agendaAlterada.PacoteId.HasValue)
                {
                    // Buscar o pacote do paciente
                    var pacotePaciente = await _context.PacotesPacientes
                        .FirstOrDefaultAsync(pp => pp.Id == agendaAlterada.PacoteId.Value);

                    if (pacotePaciente == null)
                    {
                        await transaction.RollbackAsync();
                        resposta.Mensagem = "Pacote do paciente não encontrado";
                        resposta.Status = false;
                        return resposta;
                    }

                    if (pacotePaciente.QuantidadeDisponivel <= 0)
                    {
                        await transaction.RollbackAsync();
                        resposta.Mensagem = "Pacote não possui sessões disponíveis";
                        resposta.Status = false;
                        return resposta;
                    }

                    // Criar registro de uso
                    var uso = new PacoteUsoModel
                    {
                        PacotePacienteId = pacotePaciente.Id,
                        AgendaId = agendaAlterada.Id,
                        PacienteUtilizadoId = agendaAlterada.PacienteId.Value,
                        DataUso = DateTime.UtcNow,
                        Observacao = "Consumo ao marcar como concluído"
                    };

                    _context.PacotesUsos.Add(uso);
                    pacotePaciente.QuantidadeUsada++;

                    if (pacotePaciente.QuantidadeDisponivel == 0)
                    {
                        pacotePaciente.Status = "Esgotado";
                    }

                    _context.PacotesPacientes.Update(pacotePaciente);
                }
                // Se mudou de "Concluído" (4) para outro E tinha pacote, estornar
                else if (statusAnterior == 4 && statusNovo != 4 && agendaAlterada.PacoteId.HasValue)
                {
                    // Buscar o uso relacionado a este agendamento
                    var uso = await _context.PacotesUsos
                        .Include(u => u.PacotePaciente)
                        .Where(u => u.AgendaId == agendaAlterada.Id && u.Ativo)
                        .FirstOrDefaultAsync();

                    if (uso != null)
                    {
                        uso.PacotePaciente.QuantidadeUsada--;

                        if (uso.PacotePaciente.Status == "Esgotado")
                        {
                            uso.PacotePaciente.Status = "Ativo";
                        }

                        uso.Ativo = false;

                        _context.PacotesUsos.Update(uso);
                        _context.PacotesPacientes.Update(uso.PacotePaciente);
                    }
                }
                // ==================================================================

                await transaction.CommitAsync();

                var listaAtualizada = await _context.Agenda.ToListAsync();

                resposta.Dados = listaAtualizada;
                resposta.TotalCount = listaAtualizada.Count();
                resposta.Mensagem = "Agendamento atualizado com sucesso!";
                return resposta;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                resposta.Status = false;
                resposta.Mensagem = ex.Message;
                return resposta;
            }
        }
    }

    public async Task<ResponseModel<List<AgendaModel>>> Reagendar(int id, int statusNovo, DateTime dataNova, string horaInicioNovo, string horaFimNovo)
    {
        ResponseModel<List<AgendaModel>> resposta = new ResponseModel<List<AgendaModel>>();
        try
        {
            var agendaAlterada = _context.Agenda.FirstOrDefault(a => a.Id == id);
            if (agendaAlterada == null)
            {
                resposta.Mensagem = "Agendamento não encontrado";
                return resposta;
            }

            //1 Agendado
            //2 Confirmado
            //3 Em atendimento
            //4 Concluído
            //5 Cancelado pelo paciente
            //6 Cancelado pela clínica
            //7 Remarcado
            //8 Não compareceu

            var consultaStatus = _context.Status.FirstOrDefaultAsync(s => s.Id == statusNovo);

            if (consultaStatus == null)
            {
                resposta.Mensagem = "Status não encontrado";
                return resposta;

            }
            agendaAlterada.Data = dataNova;
            agendaAlterada.StatusId = statusNovo;

            if (TimeSpan.TryParse(horaInicioNovo, out TimeSpan horaInicio))
            {
                agendaAlterada.HoraInicio = horaInicio;
            }
            if (TimeSpan.TryParse(horaFimNovo, out TimeSpan horaFim))
            {
                agendaAlterada.HoraFim = horaFim;
            }


            _context.Update(agendaAlterada);
            await _context.SaveChangesAsync();

            var listaAtualizada = await _context.Agenda.ToListAsync();

            resposta.Dados = listaAtualizada;
            resposta.TotalCount = listaAtualizada.Count();
            resposta.Mensagem = "Agendamento atualizado com sucesso!";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Status = false;
            resposta.Mensagem = ex.Message;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<AgendaModel>>> ListarGeral(int pageNumber = 1, int pageSize = 10, int? idFiltro = null, string? pacienteIdFiltro = null,
        string? profissionalIdFiltro = null, string? statusIdFiltro = null, DateTime? dataFiltroInicio = null, DateTime? dataFiltroFim = null, bool paginar = true)
    {
        ResponseModel<List<AgendaModel>> resposta = new ResponseModel<List<AgendaModel>>();
        try
        {
            var query = _context.Agenda
                .Include(ag => ag.Paciente)
                .Include(fp => fp.FinancReceber)
                .Include(st => st.Status)
                .Include(p => p.Profissional)
                .AsQueryable();

            if (dataFiltroInicio.HasValue)
            {
                dataFiltroInicio = DateTime.SpecifyKind(dataFiltroInicio.Value, DateTimeKind.Utc);
                query = query.Where(p => p.Data >= dataFiltroInicio);
            }

            if (dataFiltroFim.HasValue)
            {

                dataFiltroFim = DateTime.SpecifyKind(dataFiltroFim.Value, DateTimeKind.Utc);
                query = query.Where(p => p.Data <= dataFiltroFim);
            }

            if (!string.IsNullOrEmpty(pacienteIdFiltro))
                query = query.Where(p => p.PacienteId.ToString() == pacienteIdFiltro);

            if (!string.IsNullOrEmpty(profissionalIdFiltro))
                query = query.Where(p => p.ProfissionalId.ToString() == profissionalIdFiltro);

            if (!string.IsNullOrEmpty(statusIdFiltro))
                query = query.Where(p => p.StatusId.ToString() == statusIdFiltro);

            query = query.OrderBy(a => a.Id)
                .ThenBy(a => a.Data);

            // Executa a query com ou sem paginação
            resposta.Dados = paginar
                ? (await PaginationHelper.PaginateAsync(query, pageNumber, pageSize)).Dados
                : await query.ToListAsync();

            resposta.Mensagem = "Todas as agendas foram encontradas";

            return resposta;

        }
        catch (Exception ex)
        {

            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }

    }

    public async Task<ResponseModel<List<ContadoresDashboard>>> ObterContadoresDashboard(int? profissionalId, DateTime? dataInicio = null, DateTime? dataFim = null)
    {
        ResponseModel<List<ContadoresDashboard>> resposta = new ResponseModel<List<ContadoresDashboard>>();

        try
        {
            var consultaAgenda = _context.Agenda.Include(a => a.Status).AsQueryable();

            if (dataInicio.HasValue)
            {
                consultaAgenda = consultaAgenda.Where(a => a.Data >= dataInicio);
            }

            if (dataFim.HasValue)
            {
                consultaAgenda = consultaAgenda.Where(a => a.Data <= dataFim);
            }

            if (profissionalId.HasValue && profissionalId > 0)
            {
                consultaAgenda = consultaAgenda.Where(a => a.ProfissionalId == profissionalId);
            }

            var agendas = await consultaAgenda.ToListAsync();
            var consultaPaciente = _context.Paciente.AsQueryable();

            if (dataInicio.HasValue && dataFim.HasValue)
            {
                consultaPaciente = consultaPaciente.Where(p => p.DataCadastro >= dataInicio && p.DataCadastro <= dataFim);
            }

            int totalAgendas = agendas.Count;
            int agendasFinalizadas = agendas.Count(a => a.Status?.Status == "Concluído");
            int agendasFuturas = agendas.Count(a => a.Status?.Legenda == "Não compareceu");
            int pacientesNoPeriodo = await consultaPaciente.CountAsync();

            var listaContadores = new List<ContadoresDashboard>
            {
                new ContadoresDashboard
                {
                    TotalAgendas = totalAgendas,
                    AgendasFinalizadas = agendasFinalizadas,
                    AgendasFuturas = agendasFuturas,
                    PacientesNoPeriodo = pacientesNoPeriodo
                }
            };

            resposta.Dados = listaContadores;
            resposta.Mensagem = "Contadores calculados com sucesso";
            resposta.Status = true;

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

public class ContadoresDashboard
{
    public int TotalAgendas { get; set; }
    public int AgendasFinalizadas { get; set; }
    public int AgendasFuturas { get; set; }
    public int PacientesNoPeriodo { get; set; }
}