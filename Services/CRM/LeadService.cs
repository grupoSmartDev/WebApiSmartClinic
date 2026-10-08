using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Dto.CRM;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.CRM;

public class LeadService : ILeadInterface
{
    private readonly AppDbContext _context;
    public LeadService(AppDbContext context)
    {
        _context = context;
    }

    private IQueryable<LeadModel> QueryBase() =>
        _context.Leads
            .Include(l => l.EtapaFunil)
            .Include(l => l.Campanha)
            .Include(l => l.Profissional);

    private static IQueryable<LeadModel> AplicarFiltros(IQueryable<LeadModel> query, int? etapaId, int? profissionalId, string? canalOrigem, string? search)
    {
        if (etapaId.HasValue && etapaId.Value > 0)
            query = query.Where(l => l.EtapaFunilId == etapaId.Value);

        if (profissionalId.HasValue && profissionalId.Value > 0)
            query = query.Where(l => l.ProfissionalId == profissionalId.Value);

        if (!string.IsNullOrWhiteSpace(canalOrigem))
            query = query.Where(l => l.CanalOrigem == canalOrigem);

        if (!string.IsNullOrWhiteSpace(search))
        {
            var termo = search.Trim().ToLower();
            query = query.Where(l =>
                l.Nome.ToLower().Contains(termo) ||
                l.Telefone.Contains(termo) ||
                (l.Email != null && l.Email.ToLower().Contains(termo)));
        }

        return query;
    }

    private async Task<ResponseModel<List<LeadModel>>> ListarPaginado(int pageNumber, int pageSize) =>
        await PaginationHelper.PaginateAsync(QueryBase().OrderByDescending(l => l.DataCriacao), pageNumber, pageSize);

    /// <summary>
    /// Aplica as regras de troca de etapa no lead (Ganho/Perdido/DataUltimaInteracao).
    /// Retorna mensagem de erro ou null se ok. Não salva.
    /// </summary>
    private static string? AplicarEtapa(LeadModel lead, EtapaFunilModel etapa, string? motivoPerdaId)
    {
        if (etapa.Id == EtapaFunilModel.IdPerdido)
        {
            var motivo = string.IsNullOrWhiteSpace(motivoPerdaId) ? lead.MotivoPerdaId : motivoPerdaId;
            if (string.IsNullOrWhiteSpace(motivo))
                return "Informe o motivo da perda para mover o lead para a etapa \"Perdido\".";
            lead.MotivoPerdaId = motivo;
        }
        else
        {
            lead.MotivoPerdaId = null;
        }

        // Ganho apenas sinaliza: a criação do Paciente é feita explicitamente em ConverterEmPaciente
        lead.IsGanho = etapa.Id == EtapaFunilModel.IdGanho;

        if (lead.EtapaFunilId != etapa.Id)
            lead.DataUltimaInteracao = DateTime.UtcNow;

        lead.EtapaFunilId = etapa.Id;
        return null;
    }

    private async Task<string?> ValidarVinculos(int? campanhaId, int? profissionalId)
    {
        if (campanhaId.HasValue && campanhaId.Value > 0 && !await _context.Campanhas.AnyAsync(c => c.Id == campanhaId.Value))
            return "Campanha não encontrada.";

        if (profissionalId.HasValue && profissionalId.Value > 0 && !await _context.Profissional.AnyAsync(p => p.Id == profissionalId.Value))
            return "Profissional não encontrado.";

        return null;
    }

    public async Task<ResponseModel<List<LeadModel>>> Listar(int pageNumber = 1, int pageSize = 10, int? etapaId = null, int? profissionalId = null, string? canalOrigem = null, string? search = null, bool paginar = true)
    {
        var resposta = new ResponseModel<List<LeadModel>>();
        try
        {
            var query = AplicarFiltros(QueryBase(), etapaId, profissionalId, canalOrigem, search)
                .OrderByDescending(l => l.DataUltimaInteracao ?? l.DataCriacao);

            resposta = paginar
                ? await PaginationHelper.PaginateAsync(query, pageNumber, pageSize)
                : new ResponseModel<List<LeadModel>> { Dados = await query.AsNoTracking().ToListAsync() };
            resposta.Mensagem = "Leads listados com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<KanbanColunaDto>>> ListarKanban(int? profissionalId = null, string? canalOrigem = null, string? search = null)
    {
        var resposta = new ResponseModel<List<KanbanColunaDto>>();
        try
        {
            var etapas = await _context.EtapasFunil.AsNoTracking()
                .OrderBy(e => e.Ordem).ThenBy(e => e.Id)
                .ToListAsync();

            var leads = await AplicarFiltros(
                    _context.Leads.AsNoTracking().Include(l => l.Campanha).Include(l => l.Profissional),
                    null, profissionalId, canalOrigem, search)
                .OrderByDescending(l => l.DataUltimaInteracao ?? l.DataCriacao)
                .ToListAsync();

            var leadsPorEtapa = leads.GroupBy(l => l.EtapaFunilId).ToDictionary(g => g.Key, g => g.ToList());

            resposta.Dados = etapas.Select(e =>
            {
                var leadsEtapa = leadsPorEtapa.TryGetValue(e.Id, out var lista) ? lista : new List<LeadModel>();
                return new KanbanColunaDto
                {
                    EtapaFunilId = e.Id,
                    Nome = e.Nome,
                    Ordem = e.Ordem,
                    Cor = e.Cor,
                    ScriptSugerido = e.ScriptSugerido,
                    IsFixa = e.IsFixa,
                    Total = leadsEtapa.Count,
                    Leads = leadsEtapa
                };
            }).ToList();

            resposta.TotalCount = leads.Count;
            resposta.Mensagem = "Kanban carregado com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<LeadModel>> BuscarPorId(int id)
    {
        var resposta = new ResponseModel<LeadModel>();
        try
        {
            var lead = await QueryBase()
                .Include(l => l.FollowUps!.OrderByDescending(f => f.DataContato))
                    .ThenInclude(f => f.EtapaFunil)
                .AsNoTracking()
                .FirstOrDefaultAsync(l => l.Id == id);

            if (lead == null)
            {
                resposta.Mensagem = "Lead não encontrado";
                resposta.Status = false;
                return resposta;
            }

            resposta.Dados = lead;
            resposta.Mensagem = "Lead encontrado";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<LeadModel>>> Criar(LeadCreateDto dto, int pageNumber = 1, int pageSize = 10)
    {
        var resposta = new ResponseModel<List<LeadModel>>();
        try
        {
            if (string.IsNullOrWhiteSpace(dto.Nome) || string.IsNullOrWhiteSpace(dto.Telefone))
            {
                resposta.Mensagem = "Nome e telefone são obrigatórios.";
                resposta.Status = false;
                return resposta;
            }

            var etapaId = dto.EtapaFunilId.HasValue && dto.EtapaFunilId.Value > 0 ? dto.EtapaFunilId.Value : EtapaFunilModel.IdNovoLead;
            var etapa = await _context.EtapasFunil.AsNoTracking().FirstOrDefaultAsync(e => e.Id == etapaId);
            if (etapa == null)
            {
                resposta.Mensagem = "Etapa do funil não encontrada.";
                resposta.Status = false;
                return resposta;
            }

            if (etapa.Id == EtapaFunilModel.IdPerdido)
            {
                resposta.Mensagem = "Não é possível criar um lead diretamente na etapa \"Perdido\".";
                resposta.Status = false;
                return resposta;
            }

            var erroVinculo = await ValidarVinculos(dto.CampanhaId, dto.ProfissionalId);
            if (erroVinculo != null)
            {
                resposta.Mensagem = erroVinculo;
                resposta.Status = false;
                return resposta;
            }

            var lead = new LeadModel
            {
                Nome = dto.Nome.Trim(),
                Telefone = dto.Telefone.Trim(),
                Email = dto.Email,
                CanalOrigem = dto.CanalOrigem,
                CampanhaId = dto.CampanhaId > 0 ? dto.CampanhaId : null,
                FacebookLeadId = dto.FacebookLeadId,
                EtapaFunilId = etapa.Id,
                IsGanho = etapa.Id == EtapaFunilModel.IdGanho,
                ProfissionalId = dto.ProfissionalId > 0 ? dto.ProfissionalId : null,
                Observacao = dto.Observacao,
                DataUltimaInteracao = DateTime.UtcNow
            };

            _context.Leads.Add(lead);
            await _context.SaveChangesAsync();

            resposta = await ListarPaginado(pageNumber, pageSize);
            resposta.Mensagem = "Lead criado com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<LeadModel>>> Editar(LeadEdicaoDto dto, int pageNumber = 1, int pageSize = 10)
    {
        var resposta = new ResponseModel<List<LeadModel>>();
        try
        {
            var lead = await _context.Leads.FirstOrDefaultAsync(l => l.Id == dto.Id);
            if (lead == null)
            {
                resposta.Mensagem = "Lead não encontrado";
                resposta.Status = false;
                return resposta;
            }

            if (string.IsNullOrWhiteSpace(dto.Nome) || string.IsNullOrWhiteSpace(dto.Telefone))
            {
                resposta.Mensagem = "Nome e telefone são obrigatórios.";
                resposta.Status = false;
                return resposta;
            }

            var etapaId = dto.EtapaFunilId > 0 ? dto.EtapaFunilId : lead.EtapaFunilId;
            var etapa = await _context.EtapasFunil.AsNoTracking().FirstOrDefaultAsync(e => e.Id == etapaId);
            if (etapa == null)
            {
                resposta.Mensagem = "Etapa do funil não encontrada.";
                resposta.Status = false;
                return resposta;
            }

            var erroVinculo = await ValidarVinculos(dto.CampanhaId, dto.ProfissionalId);
            if (erroVinculo != null)
            {
                resposta.Mensagem = erroVinculo;
                resposta.Status = false;
                return resposta;
            }

            var erroEtapa = AplicarEtapa(lead, etapa, dto.MotivoPerdaId);
            if (erroEtapa != null)
            {
                resposta.Mensagem = erroEtapa;
                resposta.Status = false;
                return resposta;
            }

            lead.Nome = dto.Nome.Trim();
            lead.Telefone = dto.Telefone.Trim();
            lead.Email = dto.Email;
            lead.CanalOrigem = dto.CanalOrigem;
            lead.CampanhaId = dto.CampanhaId > 0 ? dto.CampanhaId : null;
            lead.ProfissionalId = dto.ProfissionalId > 0 ? dto.ProfissionalId : null;
            lead.Observacao = dto.Observacao;

            _context.Leads.Update(lead);
            await _context.SaveChangesAsync();

            resposta = await ListarPaginado(pageNumber, pageSize);
            resposta.Mensagem = "Lead atualizado com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<LeadModel>> MoverEtapa(LeadMoverEtapaDto dto)
    {
        var resposta = new ResponseModel<LeadModel>();
        try
        {
            var lead = await _context.Leads.FirstOrDefaultAsync(l => l.Id == dto.LeadId);
            if (lead == null)
            {
                resposta.Mensagem = "Lead não encontrado";
                resposta.Status = false;
                return resposta;
            }

            var etapa = await _context.EtapasFunil.AsNoTracking().FirstOrDefaultAsync(e => e.Id == dto.NovaEtapaFunilId);
            if (etapa == null)
            {
                resposta.Mensagem = "Etapa do funil não encontrada.";
                resposta.Status = false;
                return resposta;
            }

            var erroEtapa = AplicarEtapa(lead, etapa, dto.MotivoPerdaId);
            if (erroEtapa != null)
            {
                resposta.Mensagem = erroEtapa;
                resposta.Status = false;
                return resposta;
            }

            // Mover etapa também conta como interação, mesmo que a etapa seja a mesma
            lead.DataUltimaInteracao = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            resposta.Dados = await QueryBase().AsNoTracking().FirstOrDefaultAsync(l => l.Id == lead.Id);
            resposta.Mensagem = lead.IsGanho
                ? "Lead movido para \"Ganho\". Converta-o em paciente quando desejar."
                : "Lead movido de etapa com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<LeadModel>>> Delete(int id, int pageNumber = 1, int pageSize = 10)
    {
        var resposta = new ResponseModel<List<LeadModel>>();
        try
        {
            var lead = await _context.Leads.FirstOrDefaultAsync(l => l.Id == id);
            if (lead == null)
            {
                resposta.Mensagem = "Lead não encontrado";
                resposta.Status = false;
                return resposta;
            }

            _context.Leads.Remove(lead); // soft delete (IEntidadeAuditavel)
            await _context.SaveChangesAsync();

            resposta = await ListarPaginado(pageNumber, pageSize);
            resposta.Mensagem = "Lead excluído com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<LeadModel>> ConverterEmPaciente(int leadId)
    {
        var resposta = new ResponseModel<LeadModel>();
        try
        {
            var lead = await _context.Leads.FirstOrDefaultAsync(l => l.Id == leadId);
            if (lead == null)
            {
                resposta.Mensagem = "Lead não encontrado";
                resposta.Status = false;
                return resposta;
            }

            if (lead.PacienteId.HasValue)
            {
                resposta.Mensagem = "Este lead já foi convertido em paciente.";
                resposta.Status = false;
                return resposta;
            }

            if (!lead.IsGanho || lead.EtapaFunilId != EtapaFunilModel.IdGanho)
            {
                resposta.Mensagem = "Apenas leads na etapa \"Ganho\" podem ser convertidos em paciente.";
                resposta.Status = false;
                return resposta;
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var paciente = new PacienteModel
                {
                    Nome = lead.Nome,
                    Celular = lead.Telefone,
                    Email = lead.Email,
                    ComoConheceu = lead.CanalOrigem,
                    ProfissionalId = lead.ProfissionalId,
                    // Coluna Cpf é NOT NULL e o lead não tem CPF — deve ser completado no cadastro do paciente
                    Cpf = string.Empty
                };

                _context.Paciente.Add(paciente);
                await _context.SaveChangesAsync();

                lead.PacienteId = paciente.Id;
                lead.DataUltimaInteracao = DateTime.UtcNow;
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }

            resposta.Dados = await QueryBase().AsNoTracking().FirstOrDefaultAsync(l => l.Id == lead.Id);
            resposta.Mensagem = "Lead convertido em paciente com sucesso. Complete o cadastro do paciente (CPF e demais dados).";
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
