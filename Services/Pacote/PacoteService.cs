using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Dto.Pacote;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Pacote
{
    public class PacoteService : IPacoteInterface
    {
        private readonly AppDbContext _context;

        public PacoteService(AppDbContext context)
        {
            _context = context;
        }

        // ==================== PACOTE (TEMPLATE) ====================

        public async Task<ResponseModel<List<PacoteModel>>> Listar(int pageNumber = 1, int pageSize = 10, string? descricaoFiltro = null, bool paginar = true)
        {
            ResponseModel<List<PacoteModel>> resposta = new ResponseModel<List<PacoteModel>>();
            try
            {
                var query = _context.Pacotes
                    .Include(p => p.Procedimento)
                    .Include(p => p.CentroCusto)
                    .AsQueryable();

                if (!string.IsNullOrEmpty(descricaoFiltro))
                    query = query.Where(p => p.Descricao.Contains(descricaoFiltro));

                resposta.Dados = paginar
                    ? (await PaginationHelper.PaginateAsync(query, pageNumber, pageSize)).Dados
                    : await query.ToListAsync();

                resposta.Mensagem = "Pacotes listados com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<PacoteModel>> BuscarPorId(int idPacote)
        {
            ResponseModel<PacoteModel> resposta = new ResponseModel<PacoteModel>();
            try
            {
                var pacote = await _context.Pacotes
                    .Include(p => p.Procedimento)
                    .Include(p => p.CentroCusto)
                    .FirstOrDefaultAsync(x => x.Id == idPacote);

                if (pacote == null)
                {
                    resposta.Mensagem = "Pacote não encontrado";
                    resposta.Status = false;
                    return resposta;
                }

                resposta.Dados = pacote;
                resposta.Mensagem = "Pacote encontrado";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<PacoteModel>>> Criar(PacoteCreateDto pacoteCreateDto, int pageNumber = 1, int pageSize = 10)
        {
            ResponseModel<List<PacoteModel>> resposta = new ResponseModel<List<PacoteModel>>();
            try
            {
                var pacote = new PacoteModel
                {
                    Descricao = pacoteCreateDto.Descricao,
                    ProcedimentoId = pacoteCreateDto.ProcedimentoId,
                    QuantidadeSessoes = pacoteCreateDto.QuantidadeSessoes,
                    Valor = pacoteCreateDto.Valor,
                    CentroCustoId = pacoteCreateDto.CentroCustoId,
                    Observacao = pacoteCreateDto.Observacao
                };

                _context.Pacotes.Add(pacote);
                await _context.SaveChangesAsync();

                var query = _context.Pacotes
                    .Include(p => p.Procedimento)
                    .Include(p => p.CentroCusto)
                    .AsQueryable();

                resposta = await PaginationHelper.PaginateAsync(query, pageNumber, pageSize);
                resposta.Mensagem = "Pacote criado com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<PacoteModel>>> Editar(PacoteEdicaoDto pacoteEdicaoDto, int pageNumber = 1, int pageSize = 10)
        {
            ResponseModel<List<PacoteModel>> resposta = new ResponseModel<List<PacoteModel>>();
            try
            {
                var pacote = await _context.Pacotes.FirstOrDefaultAsync(x => x.Id == pacoteEdicaoDto.Id);

                if (pacote == null)
                {
                    resposta.Mensagem = "Pacote não encontrado";
                    resposta.Status = false;
                    return resposta;
                }

                pacote.Descricao = pacoteEdicaoDto.Descricao;
                pacote.ProcedimentoId = pacoteEdicaoDto.ProcedimentoId;
                pacote.QuantidadeSessoes = pacoteEdicaoDto.QuantidadeSessoes;
                pacote.Valor = pacoteEdicaoDto.Valor;
                pacote.CentroCustoId = pacoteEdicaoDto.CentroCustoId;
                pacote.Observacao = pacoteEdicaoDto.Observacao;
                pacote.Ativo = pacoteEdicaoDto.Ativo;

                _context.Pacotes.Update(pacote);
                await _context.SaveChangesAsync();

                var query = _context.Pacotes
                    .Include(p => p.Procedimento)
                    .Include(p => p.CentroCusto)
                    .AsQueryable();

                resposta = await PaginationHelper.PaginateAsync(query, pageNumber, pageSize);
                resposta.Mensagem = "Pacote editado com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<PacoteModel>>> Delete(int idPacote, int pageNumber = 1, int pageSize = 10)
        {
            ResponseModel<List<PacoteModel>> resposta = new ResponseModel<List<PacoteModel>>();
            try
            {
                var pacote = await _context.Pacotes.FirstOrDefaultAsync(x => x.Id == idPacote);

                if (pacote == null)
                {
                    resposta.Mensagem = "Pacote não encontrado";
                    resposta.Status = false;
                    return resposta;
                }

                pacote.Ativo = false;
                _context.Pacotes.Update(pacote);
                await _context.SaveChangesAsync();

                var query = _context.Pacotes
                    .Include(p => p.Procedimento)
                    .Include(p => p.CentroCusto)
                    .Where(p => p.Ativo)
                    .AsQueryable();

                resposta = await PaginationHelper.PaginateAsync(query, pageNumber, pageSize);
                resposta.Mensagem = "Pacote inativado com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        // ==================== VENDA DE PACOTE ====================

        public async Task<ResponseModel<PacotePacienteModel>> VenderPacote(PacoteVendaDto pacoteVendaDto)
        {
            ResponseModel<PacotePacienteModel> resposta = new ResponseModel<PacotePacienteModel>();

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var pacote = await _context.Pacotes.FirstOrDefaultAsync(p => p.Id == pacoteVendaDto.PacoteId);
                    if (pacote == null)
                    {
                        resposta.Mensagem = "Pacote não encontrado";
                        resposta.Status = false;
                        return resposta;
                    }

                    var paciente = await _context.Paciente.FirstOrDefaultAsync(p => p.Id == pacoteVendaDto.PacienteId);
                    if (paciente == null)
                    {
                        resposta.Mensagem = "Paciente não encontrado";
                        resposta.Status = false;
                        return resposta;
                    }

                    int? financeiroId = null;

                    if (pacoteVendaDto.Financeiro != null)
                    {
                        var financ_receber = new Financ_ReceberModel
                        {
                            IdOrigem = 0,
                            NrDocto = 0,
                            DataEmissao = DateTime.UtcNow,
                            ValorOriginal = pacoteVendaDto.Financeiro.Valor,
                            ValorPago = 0,
                            Valor = pacoteVendaDto.Financeiro.Valor,
                            Status = "Em Aberto",
                            NotaFiscal = "",
                            Descricao = $"Venda de Pacote - {pacote.Descricao} - {paciente.Nome}",
                            Parcela = pacoteVendaDto.Financeiro.Parcela,
                            Classificacao = "Receita",
                            Observacao = pacoteVendaDto.Financeiro.Observacao ?? "Venda de pacote",
                            CentroCustoId = pacoteVendaDto.Financeiro.CentroCustoId ?? pacote.CentroCustoId,
                            TipoPagamentoId = pacoteVendaDto.Financeiro.TipoPagamentoId,
                            PacienteId = paciente.Id,
                            BancoId = null,
                            subFinancReceber = new List<Financ_ReceberSubModel>()
                        };

                        _context.Financ_Receber.Add(financ_receber);
                        await _context.SaveChangesAsync();

                        if (pacoteVendaDto.Financeiro.subFinancReceber != null && pacoteVendaDto.Financeiro.subFinancReceber.Any())
                        {
                            foreach (var parcela in pacoteVendaDto.Financeiro.subFinancReceber)
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
                        }

                        financeiroId = financ_receber.Id;
                    }

                    var pacotePaciente = new PacotePacienteModel
                    {
                        PacoteId = pacote.Id,
                        PacienteId = paciente.Id,
                        FinanceiroId = financeiroId,
                        QuantidadeTotal = pacote.QuantidadeSessoes,
                        QuantidadeUsada = 0,
                        DataCompra = DateTime.UtcNow,
                        Status = "Ativo",
                        Observacao = pacoteVendaDto.Observacao
                    };

                    _context.PacotesPacientes.Add(pacotePaciente);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    pacotePaciente = await _context.PacotesPacientes
                        .Include(pp => pp.Pacote)
                        .Include(pp => pp.Paciente)
                        .Include(pp => pp.Financeiro)
                        .FirstOrDefaultAsync(pp => pp.Id == pacotePaciente.Id);

                    resposta.Dados = pacotePaciente;
                    resposta.Mensagem = "Pacote vendido com sucesso";
                    return resposta;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    resposta.Mensagem = $"Erro ao vender pacote: {ex.Message}";
                    resposta.Status = false;
                    return resposta;
                }
            }
        }

        // ==================== PACOTE DO PACIENTE ====================

        public async Task<ResponseModel<List<PacotePacienteModel>>> ListarPacotesPaciente(int pacienteId)
        {
            ResponseModel<List<PacotePacienteModel>> resposta = new ResponseModel<List<PacotePacienteModel>>();
            try
            {
                var pacotesPaciente = await _context.PacotesPacientes
                    .Include(pp => pp.Pacote)
                        .ThenInclude(p => p.Procedimento)
                    .Include(pp => pp.Paciente)
                    .Include(pp => pp.Financeiro)
                    .Include(pp => pp.Usos)
                    .Where(pp => pp.PacienteId == pacienteId && pp.Ativo)
                    .ToListAsync();

                resposta.Dados = pacotesPaciente;
                resposta.Mensagem = "Pacotes do paciente listados com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<PacotePacienteModel>> BuscarPacotePacientePorId(int idPacotePaciente)
        {
            ResponseModel<PacotePacienteModel> resposta = new ResponseModel<PacotePacienteModel>();
            try
            {
                var pacotePaciente = await _context.PacotesPacientes
                    .Include(pp => pp.Pacote)
                        .ThenInclude(p => p.Procedimento)
                    .Include(pp => pp.Paciente)
                    .Include(pp => pp.Financeiro)
                    .Include(pp => pp.Usos)
                    .FirstOrDefaultAsync(pp => pp.Id == idPacotePaciente);

                if (pacotePaciente == null)
                {
                    resposta.Mensagem = "Pacote do paciente não encontrado";
                    resposta.Status = false;
                    return resposta;
                }

                resposta.Dados = pacotePaciente;
                resposta.Mensagem = "Pacote do paciente encontrado";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        // ==================== USO DO PACOTE ====================

        public async Task<ResponseModel<PacoteUsoModel>> ConsumirSessao(PacoteUsoDto pacoteUsoDto)
        {
            ResponseModel<PacoteUsoModel> resposta = new ResponseModel<PacoteUsoModel>();

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var pacotePaciente = await _context.PacotesPacientes
                        .FirstOrDefaultAsync(pp => pp.Id == pacoteUsoDto.PacotePacienteId);

                    if (pacotePaciente == null)
                    {
                        resposta.Mensagem = "Pacote do paciente não encontrado";
                        resposta.Status = false;
                        return resposta;
                    }

                    if (pacotePaciente.QuantidadeDisponivel <= 0)
                    {
                        resposta.Mensagem = "Pacote não possui sessões disponíveis";
                        resposta.Status = false;
                        return resposta;
                    }

                    var agenda = await _context.Agenda.FirstOrDefaultAsync(a => a.Id == pacoteUsoDto.AgendaId);
                    if (agenda == null)
                    {
                        resposta.Mensagem = "Agendamento não encontrado";
                        resposta.Status = false;
                        return resposta;
                    }

                    var uso = new PacoteUsoModel
                    {
                        PacotePacienteId = pacotePaciente.Id,
                        AgendaId = agenda.Id,
                        PacienteUtilizadoId = pacoteUsoDto.PacienteUtilizadoId,
                        DataUso = DateTime.UtcNow,
                        Observacao = pacoteUsoDto.Observacao
                    };

                    _context.PacotesUsos.Add(uso);
                    pacotePaciente.QuantidadeUsada++;

                    if (pacotePaciente.QuantidadeDisponivel == 0)
                    {
                        pacotePaciente.Status = "Esgotado";
                    }

                    _context.PacotesPacientes.Update(pacotePaciente);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    uso = await _context.PacotesUsos
                        .Include(u => u.PacotePaciente)
                        .Include(u => u.Agenda)
                        .Include(u => u.PacienteUtilizado)
                        .FirstOrDefaultAsync(u => u.Id == uso.Id);

                    resposta.Dados = uso;
                    resposta.Mensagem = "Sessão consumida com sucesso";
                    return resposta;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    resposta.Mensagem = $"Erro ao consumir sessão: {ex.Message}";
                    resposta.Status = false;
                    return resposta;
                }
            }
        }

        public async Task<ResponseModel<List<PacoteUsoModel>>> ListarHistoricoUso(int pacotePacienteId)
        {
            ResponseModel<List<PacoteUsoModel>> resposta = new ResponseModel<List<PacoteUsoModel>>();
            try
            {
                var historico = await _context.PacotesUsos
                    .Include(u => u.Agenda)
                    .Include(u => u.PacienteUtilizado)
                    .Where(u => u.PacotePacienteId == pacotePacienteId && u.Ativo)
                    .OrderByDescending(u => u.DataUso)
                    .ToListAsync();

                resposta.Dados = historico;
                resposta.Mensagem = "Histórico de uso listado com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<string>> EstornarUso(int idUso)
        {
            ResponseModel<string> resposta = new ResponseModel<string>();

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var uso = await _context.PacotesUsos
                        .Include(u => u.PacotePaciente)
                        .FirstOrDefaultAsync(u => u.Id == idUso);

                    if (uso == null)
                    {
                        resposta.Mensagem = "Uso não encontrado";
                        resposta.Status = false;
                        return resposta;
                    }

                    uso.PacotePaciente.QuantidadeUsada--;

                    if (uso.PacotePaciente.Status == "Esgotado")
                    {
                        uso.PacotePaciente.Status = "Ativo";
                    }

                    uso.Ativo = false;

                    _context.PacotesUsos.Update(uso);
                    _context.PacotesPacientes.Update(uso.PacotePaciente);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    resposta.Mensagem = "Uso estornado com sucesso";
                    return resposta;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    resposta.Mensagem = $"Erro ao estornar uso: {ex.Message}";
                    resposta.Status = false;
                    return resposta;
                }
            }
        }
    }
}