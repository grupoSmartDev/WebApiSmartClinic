using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Dto.CRM;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.CRM;

public class FollowUpService : IFollowUpInterface
{
    private readonly AppDbContext _context;
    public FollowUpService(AppDbContext context)
    {
        _context = context;
    }

    private async Task<List<FollowUpModel>> ListarDoLead(int leadId) =>
        await _context.FollowUps.AsNoTracking()
            .Include(f => f.EtapaFunil)
            .Where(f => f.LeadId == leadId)
            .OrderByDescending(f => f.DataContato)
            .ThenByDescending(f => f.Id)
            .ToListAsync();

    public async Task<ResponseModel<List<FollowUpModel>>> Listar(int leadId)
    {
        var resposta = new ResponseModel<List<FollowUpModel>>();
        try
        {
            resposta.Dados = await ListarDoLead(leadId);
            resposta.TotalCount = resposta.Dados.Count;
            resposta.Mensagem = "Follow-ups listados com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<FollowUpModel>>> Criar(FollowUpCreateDto dto)
    {
        var resposta = new ResponseModel<List<FollowUpModel>>();
        try
        {
            var lead = await _context.Leads.FirstOrDefaultAsync(l => l.Id == dto.LeadId);
            if (lead == null)
            {
                resposta.Mensagem = "Lead não encontrado";
                resposta.Status = false;
                return resposta;
            }

            var etapaId = dto.EtapaFunilId.HasValue && dto.EtapaFunilId.Value > 0 ? dto.EtapaFunilId.Value : lead.EtapaFunilId;
            if (!await _context.EtapasFunil.AnyAsync(e => e.Id == etapaId))
            {
                resposta.Mensagem = "Etapa do funil não encontrada.";
                resposta.Status = false;
                return resposta;
            }

            var dataContato = CrmHelper.ParaUtc(dto.DataContato) ?? DateTime.UtcNow;

            var followUp = new FollowUpModel
            {
                LeadId = lead.Id,
                EtapaFunilId = etapaId,
                Canal = dto.Canal,
                MensagemEnviada = dto.MensagemEnviada,
                RespostaRecebida = dto.RespostaRecebida,
                ProximaAcaoData = CrmHelper.ParaUtc(dto.ProximaAcaoData),
                ProximaAcaoDescricao = dto.ProximaAcaoDescricao,
                DataContato = dataContato
            };

            _context.FollowUps.Add(followUp);

            // Não regride a última interação se o contato registrado for retroativo
            if (!lead.DataUltimaInteracao.HasValue || dataContato > lead.DataUltimaInteracao.Value)
                lead.DataUltimaInteracao = dataContato;

            // Follow-up + atualização do lead num único SaveChanges (atômico)
            await _context.SaveChangesAsync();

            resposta.Dados = await ListarDoLead(lead.Id);
            resposta.Mensagem = "Follow-up registrado com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<FollowUpModel>>> Editar(FollowUpEdicaoDto dto)
    {
        var resposta = new ResponseModel<List<FollowUpModel>>();
        try
        {
            var followUp = await _context.FollowUps.FirstOrDefaultAsync(f => f.Id == dto.Id);
            if (followUp == null)
            {
                resposta.Mensagem = "Follow-up não encontrado";
                resposta.Status = false;
                return resposta;
            }

            // Concluído = somente leitura (inclui tentar concluir de novo)
            if (followUp.Concluido)
            {
                resposta.Status = false;
                resposta.Mensagem = "Follow-ups concluídos não podem ser editados.";
                return resposta;
            }

            // Somente desfecho, próxima ação e conclusão — MensagemEnviada, Canal, DataContato e Etapa não mudam
            followUp.RespostaRecebida = dto.RespostaRecebida;
            followUp.ProximaAcaoData = CrmHelper.ParaUtc(dto.ProximaAcaoData);
            followUp.ProximaAcaoDescricao = dto.ProximaAcaoDescricao;

            if (dto.Concluido.HasValue)
            {
                followUp.Concluido = dto.Concluido.Value;
                followUp.DataConclusao = dto.Concluido.Value ? DateTime.UtcNow : null;
            }

            await _context.SaveChangesAsync();

            resposta.Dados = await ListarDoLead(followUp.LeadId);
            resposta.Mensagem = followUp.Concluido ? "Follow-up concluído com sucesso" : "Follow-up atualizado com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<FollowUpModel>>> Delete(int id)
    {
        var resposta = new ResponseModel<List<FollowUpModel>>();
        try
        {
            var followUp = await _context.FollowUps.FirstOrDefaultAsync(f => f.Id == id);
            if (followUp == null)
            {
                resposta.Mensagem = "Follow-up não encontrado";
                resposta.Status = false;
                return resposta;
            }

            _context.FollowUps.Remove(followUp); // soft delete (IEntidadeAuditavel)
            await _context.SaveChangesAsync();

            resposta.Dados = await ListarDoLead(followUp.LeadId);
            resposta.Mensagem = "Follow-up excluído com sucesso";
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
