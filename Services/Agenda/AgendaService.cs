
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Dto.Agenda;
using WebApiSmartClinic.Dto.Financ_Receber;
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

    public async Task<ResponseModel<List<AgendaModel>>> Criar(AgendaCreateDto agendaCreateDto)
    {
        ResponseModel<List<AgendaModel>> resposta = new ResponseModel<List<AgendaModel>>();

        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                List<AgendaModel> agendamentos = new List<AgendaModel>();

                DateTime? dataAtual = agendaCreateDto.Data;
                DateTime? dataFim = agendaCreateDto.DiasRecorrencia != null && agendaCreateDto.DiasRecorrencia.Count > 0  ? agendaCreateDto.DataFimRecorrencia : dataAtual;

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
                                    DataVencimento = parcela.DataVencimento,
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


    public async Task<ResponseModel<List<AgendaModel>>> Delete(int idAgenda)
    {
        ResponseModel<List<AgendaModel>> resposta = new ResponseModel<List<AgendaModel>>();

        try
        {
            var agenda = await _context.Agenda.FirstOrDefaultAsync(x => x.Id == idAgenda);
            if (agenda == null)
            {
                resposta.Mensagem = "Nenhum Agenda encontrado";
                return resposta;
            }

            _context.Remove(agenda);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Agenda.ToListAsync();
            resposta.Mensagem = "Agenda Excluido com sucesso";
            return resposta;

        }
        catch (Exception ex)
        {

            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<AgendaModel>>> Editar(AgendaEdicaoDto agendaEdicaoDto)
    {
        ResponseModel<List<AgendaModel>> resposta = new ResponseModel<List<AgendaModel>>();

        try
        {
            var agenda = await _context.Agenda.FirstOrDefaultAsync(x => x.Id == agendaEdicaoDto.Id);
            if (agenda == null)
            {
                resposta.Mensagem = "Agenda não encontrada";
                resposta.Status = false;
                return resposta;
            }

            agenda.Data = agendaEdicaoDto.Data;
            agenda.HoraInicio = agendaEdicaoDto.HoraInicio;
            agenda.HoraFim = agendaEdicaoDto.HoraFim;
            agenda.PacienteId = agendaEdicaoDto.PacienteId;
            agenda.ProfissionalId = agendaEdicaoDto.ProfissionalId;
            agenda.Convenio = agendaEdicaoDto.Convenio;
            agenda.Valor = agendaEdicaoDto.Valor;
            agenda.FormaPagamento = agendaEdicaoDto.FormaPagamento;
            agenda.Pago = agendaEdicaoDto.Pago;
            agenda.SalaId = agendaEdicaoDto.SalaId;
            agenda.PacoteId = agendaEdicaoDto.PacoteId;
            agenda.LembreteSms = agendaEdicaoDto.LembreteSms;
            agenda.LembreteWhatsapp = agendaEdicaoDto.LembreteWhatsapp;
            agenda.LembreteEmail = agendaEdicaoDto.LembreteEmail;
            agenda.StatusId = agendaEdicaoDto.StatusId;
            agenda.IntegracaoGmail = agendaEdicaoDto.IntegracaoGmail;
            agenda.StatusFinal = agendaEdicaoDto.StatusFinal;
            agenda.Avulso = agendaEdicaoDto.Avulso;

            if (agendaEdicaoDto.FinancReceber != null)
            {
                Financ_ReceberModel financ_receber;

                if (agenda.FinancReceberId.HasValue)
                {
                    // Busca o registro financeiro existente
                    financ_receber = await _context.Financ_Receber.Include(f => f.subFinancReceber)
                        .FirstOrDefaultAsync(f => f.Id == agenda.FinancReceberId);

                    if (financ_receber != null)
                    {
                        // Atualiza os campos do registro financeiro
                        financ_receber.IdOrigem = agendaEdicaoDto.FinancReceber.IdOrigem ?? 0;
                        financ_receber.NrDocto = agendaEdicaoDto.FinancReceber.NrDocto ?? 0;
                        financ_receber.DataEmissao = agendaEdicaoDto.FinancReceber.DataEmissao;
                        financ_receber.ValorOriginal = agendaEdicaoDto.FinancReceber.ValorOriginal;
                        financ_receber.ValorPago = agendaEdicaoDto.FinancReceber.ValorPago;
                        financ_receber.Valor = agendaEdicaoDto.FinancReceber.Valor;
                        financ_receber.Status = agendaEdicaoDto.FinancReceber.Status;
                        financ_receber.NotaFiscal = agendaEdicaoDto.FinancReceber.NotaFiscal;
                        financ_receber.Descricao = agendaEdicaoDto.FinancReceber.Descricao;
                        financ_receber.Parcela = agendaEdicaoDto.FinancReceber.Parcela;
                        financ_receber.Classificacao = agendaEdicaoDto.FinancReceber.Classificacao;
                        financ_receber.Observacao = agendaEdicaoDto.FinancReceber.Observacao;
                        financ_receber.FornecedorId = agendaEdicaoDto.FinancReceber.FornecedorId;
                        financ_receber.CentroCustoId = agendaEdicaoDto.FinancReceber.CentroCustoId;
                        financ_receber.PacienteId = agendaEdicaoDto.FinancReceber.PacienteId;
                        financ_receber.BancoId = agendaEdicaoDto.FinancReceber.BancoId;
                        financ_receber.TipoPagamentoId = agendaEdicaoDto.FinancReceber.TipoPagamentoId;

                        // Remove as parcelas existentes
                        if (financ_receber.subFinancReceber != null)
                        {
                            _context.Financ_ReceberSub.RemoveRange(financ_receber.subFinancReceber);
                        }
                    }
                    else
                    {
                        // Se não encontrou o registro, cria um novo
                        financ_receber = new Financ_ReceberModel
                        {
                            IdOrigem = agendaEdicaoDto.FinancReceber.IdOrigem ?? 0,
                            NrDocto = agendaEdicaoDto.FinancReceber.NrDocto ?? 0,
                            DataEmissao = agendaEdicaoDto.FinancReceber.DataEmissao,
                            ValorOriginal = agendaEdicaoDto.FinancReceber.ValorOriginal,
                            ValorPago = agendaEdicaoDto.FinancReceber.ValorPago,
                            Valor = agendaEdicaoDto.FinancReceber.Valor,
                            Status = agendaEdicaoDto.FinancReceber.Status,
                            NotaFiscal = agendaEdicaoDto.FinancReceber.NotaFiscal,
                            Descricao = agendaEdicaoDto.FinancReceber.Descricao,
                            Parcela = agendaEdicaoDto.FinancReceber.Parcela,
                            Classificacao = agendaEdicaoDto.FinancReceber.Classificacao,
                            Observacao = agendaEdicaoDto.FinancReceber.Observacao,
                            FornecedorId = agendaEdicaoDto.FinancReceber.FornecedorId,
                            CentroCustoId = agendaEdicaoDto.FinancReceber.CentroCustoId,
                            PacienteId = agendaEdicaoDto.FinancReceber.PacienteId,
                            BancoId = agendaEdicaoDto.FinancReceber.BancoId,
                            TipoPagamentoId = agendaEdicaoDto.FinancReceber.TipoPagamentoId,
                            subFinancReceber = new List<Financ_ReceberSubModel>()
                        };

                        _context.Financ_Receber.Add(financ_receber);
                        await _context.SaveChangesAsync();
                    }
                }
                else
                {
                    // Cria novo registro financeiro se não existia antes
                    financ_receber = new Financ_ReceberModel
                    {
                        IdOrigem = agendaEdicaoDto.FinancReceber.IdOrigem ?? 0,
                        NrDocto = agendaEdicaoDto.FinancReceber.NrDocto ?? 0,
                        DataEmissao = agendaEdicaoDto.FinancReceber.DataEmissao,
                        ValorOriginal = agendaEdicaoDto.FinancReceber.ValorOriginal,
                        ValorPago = agendaEdicaoDto.FinancReceber.ValorPago,
                        Valor = agendaEdicaoDto.FinancReceber.Valor,
                        Status = agendaEdicaoDto.FinancReceber.Status,
                        NotaFiscal = agendaEdicaoDto.FinancReceber.NotaFiscal,
                        Descricao = agendaEdicaoDto.FinancReceber.Descricao,
                        Parcela = agendaEdicaoDto.FinancReceber.Parcela,
                        Classificacao = agendaEdicaoDto.FinancReceber.Classificacao,
                        Observacao = agendaEdicaoDto.FinancReceber.Observacao,
                        FornecedorId = agendaEdicaoDto.FinancReceber.FornecedorId,
                        CentroCustoId = agendaEdicaoDto.FinancReceber.CentroCustoId,
                        PacienteId = agendaEdicaoDto.FinancReceber.PacienteId,
                        BancoId = agendaEdicaoDto.FinancReceber.BancoId,
                        TipoPagamentoId = agendaEdicaoDto.FinancReceber.TipoPagamentoId,
                        subFinancReceber = new List<Financ_ReceberSubModel>()
                    };

                    _context.Financ_Receber.Add(financ_receber);
                    await _context.SaveChangesAsync();
                }

                // Adiciona as novas parcelas
                if (agendaEdicaoDto.FinancReceber.subFinancReceber != null)
                {
                    foreach (var parcela in agendaEdicaoDto.FinancReceber.subFinancReceber)
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

                await _context.SaveChangesAsync();
                agenda.FinancReceberId = financ_receber.Id;
            }

            _context.Update(agenda);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Agenda.ToListAsync();
            resposta.Mensagem = "Agenda atualizada com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<AgendaModel>>> Listar()
    {
        ResponseModel<List<AgendaModel>> resposta = new ResponseModel<List<AgendaModel>>();

        try
        {
            var agenda = await _context.Agenda
                .Include(x => x.Paciente)
                .Include(a => a.Profissional)
                .Include(y => y.FinancReceber)
                .ThenInclude(z => z.subFinancReceber)

                .ToListAsync();

            resposta.Dados = agenda;
            resposta.Mensagem = "Todos os Agenda foram encontrados";
            return resposta;


        }
        catch (Exception e)
        {
            resposta.Mensagem = e.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<ContadoresDashboard>>> ObterContadoresDashboard(int? profissionalId, DateTime? dataInicio = null, DateTime? dataFim = null)
    {
        ResponseModel<List<ContadoresDashboard>> resposta = new ResponseModel<List<ContadoresDashboard>>();

        try
        {
            var consultaAgenda = _context.Agenda.AsQueryable();

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
            int agendasFinalizadas = agendas.Count(a => a.StatusFinal);
            int agendasFuturas = agendas.Count(a => a.Status != null && a.Status.Legenda != "Finalizado" && a.Data > DateTime.Now);
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


    public async Task<ResponseModel<List<AgendaModel>>> CriarPeloPlano(AgendaCreateDto agendaCreateDto)
    {
        ResponseModel<List<AgendaModel>> resposta = new ResponseModel<List<AgendaModel>>();

        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                List<AgendaModel> agendamentos = new List<AgendaModel>();

                DateTime? dataAtual = agendaCreateDto.Data;
                DateTime? dataFim = agendaCreateDto.DiasRecorrencia.Count > 0 ? agendaCreateDto.DataFimRecorrencia : dataAtual;

                while (dataAtual <= dataFim)
                {

                    bool criarAgendamento = false;

                    if (agendaCreateDto.DiasRecorrencia.Count == 0)
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
                                    DataVencimento = parcela.DataVencimento,
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

    public async Task<ResponseModel<List<AgendaModel>>> AtualizarStatus(int id, int statusNovo)
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

            if (statusNovo > 8)
            {
                var status = _context.Status.FirstOrDefaultAsync(a => a.Id ==  statusNovo);
                if (status == null)
                {
                    resposta.Mensagem = "Status não encontrado";
                    return resposta;

                }

            }

            agendaAlterada.StatusId = statusNovo;

            _context.Update(agendaAlterada);
            await _context.SaveChangesAsync();

            var listaAtualizada = await _context.Agenda.ToListAsync();

            resposta.Dados =  listaAtualizada;
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
}

public class ContadoresDashboard
{
    public int TotalAgendas { get; set; }
    public int AgendasFinalizadas { get; set; }
    public int AgendasFuturas { get; set; }
    public int PacientesNoPeriodo { get; set; }
}
