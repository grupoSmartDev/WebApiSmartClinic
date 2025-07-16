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
            var plano = await _context.Plano.FirstOrDefaultAsync(x => x.Id == idPlano);
            if (plano == null)
            {
                resposta.Mensagem = "Nenhum Plano encontrado";
                return resposta;
            }

            _context.Remove(plano);
            await _context.SaveChangesAsync();

            var query = _context.Plano.AsQueryable();

            resposta = await PaginationHelper.PaginateAsync(query, pageNumber, pageSize);
            resposta.Mensagem = "Plano Excluido com sucesso";
            
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
                var paciente = await _context.Paciente
                    .FirstOrDefaultAsync(p => p.Id == planoCreateDto.PacienteId);

                if (paciente == null)
                {
                    resposta.Mensagem = "Paciente não encontrado";
                    resposta.Status = false;
                    return resposta;
                }

                if (paciente.Plano != null && paciente.Plano.Ativo)
                {
                    paciente.Plano.Ativo = false;
                    paciente.Plano.DataFim = DateTime.Now;
                }

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
                    FinanceiroId = planoCreateDto.FinanceiroId,
                    TipoMes = planoCreateDto.TipoMes
                };

                // Criar financeiro se necessário
                if (planoCreateDto.Financeiro != null)
                {
                    var financ_receber = new Financ_ReceberModel
                    {
                        IdOrigem = planoCreateDto.Financeiro.IdOrigem ?? 0,
                        NrDocto = planoCreateDto.Financeiro.NrDocto ?? 0,
                        DataEmissao = planoCreateDto.DataInicio,
                        ValorOriginal = planoCreateDto.Financeiro.Valor,
                        ValorPago = planoCreateDto.Financeiro.ValorPago,
                        Valor = planoCreateDto.Financeiro.Valor,
                        Status = planoCreateDto.Financeiro.Status,
                        NotaFiscal = planoCreateDto.Financeiro.NotaFiscal,
                        Descricao = planoCreateDto.Descricao + $" {paciente.Nome} ",
                        Parcela = planoCreateDto.Financeiro.Parcela,
                        Classificacao = planoCreateDto.Financeiro.Classificacao,
                        Observacao = planoCreateDto.Descricao + " - Criado pelo Gerenciamento do paciente",
                        FornecedorId = planoCreateDto.Financeiro.FornecedorId,
                        CentroCustoId = planoCreateDto.Financeiro.CentroCustoId,
                        PacienteId = planoCreateDto.PacienteId,
                        TipoPagamentoId = (int)planoCreateDto.Financeiro.TipoPagamentoId,
                        BancoId = planoCreateDto.Financeiro.BancoId == 0 ? null : planoCreateDto.Financeiro.BancoId,
                        subFinancReceber = new List<Financ_ReceberSubModel>()
                    };

                    _context.Add(financ_receber);
                    await _context.SaveChangesAsync();

                    // Adicionando subitens (filhos)
                    if (planoCreateDto.Financeiro.subFinancReceber != null && planoCreateDto.Financeiro.subFinancReceber.Any())
                    {
                        foreach (var parcela in planoCreateDto.Financeiro.subFinancReceber)
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

                    plano.FinanceiroId = financ_receber.Id;
                }

                _context.Add(plano);
                await _context.SaveChangesAsync();

                // Criar agendamentos se necessário
                if (planoCreateDto.Agendamento != null)
                {
                    List<AgendaModel> agendamentos = new List<AgendaModel>();

                    // Configurar dados do agendamento
                    DateTime? dataAtual = planoCreateDto.DataInicio;
                    DateTime? dataFim = planoCreateDto.DataFim;

                    // Verificar se há dias de recorrência configurados
                    if (planoCreateDto.Agendamento.DiasRecorrencia != null && planoCreateDto.Agendamento.DiasRecorrencia.Count > 0)
                    {
                        // Processar cada dia entre a data inicial e final
                        while (dataAtual <= dataFim)
                        {
                            // Verificar se o dia atual corresponde a algum dia de recorrência configurado
                            foreach (var diaSemana in planoCreateDto.Agendamento.DiasRecorrencia)
                            {
                                if ((int)diaSemana.DiaSemana == (int)dataAtual.Value.DayOfWeek)
                                {
                                    // Criar agendamento para este dia
                                    var agenda = new AgendaModel
                                    {
                                        Titulo = $"Atendimento - {paciente.Nome}",
                                        Data = dataAtual,
                                        PacienteId = paciente.Id,
                                        ProfissionalId = diaSemana.ProfissionalId, // Usar o profissional configurado para este dia
                                        SalaId = diaSemana.SalaId, // Usar a sala configurada para este dia
                                        Convenio = planoCreateDto.Agendamento.Convenio,
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

                // Vincular o plano ao paciente
                paciente.PlanoId = plano.Id;
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