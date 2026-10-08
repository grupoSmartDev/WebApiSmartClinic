using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Dto.CRM;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.CRM;

public class EtapaFunilService : IEtapaFunilInterface
{
    private readonly AppDbContext _context;
    public EtapaFunilService(AppDbContext context)
    {
        _context = context;
    }

    // Filtro global já traz fixas (EmpresaId = 0) + customizadas da empresa, apenas ativas
    private async Task<List<EtapaFunilModel>> ListarOrdenado() =>
        await _context.EtapasFunil.AsNoTracking()
            .OrderBy(x => x.Ordem).ThenBy(x => x.Id)
            .ToListAsync();

    public async Task<ResponseModel<List<EtapaFunilModel>>> Listar()
    {
        var resposta = new ResponseModel<List<EtapaFunilModel>>();
        try
        {
            resposta.Dados = await ListarOrdenado();
            resposta.TotalCount = resposta.Dados.Count;
            resposta.Mensagem = "Etapas do funil listadas com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<EtapaFunilModel>> BuscarPorId(int id)
    {
        var resposta = new ResponseModel<EtapaFunilModel>();
        try
        {
            var etapa = await _context.EtapasFunil.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (etapa == null)
            {
                resposta.Mensagem = "Etapa não encontrada";
                resposta.Status = false;
                return resposta;
            }

            resposta.Dados = etapa;
            resposta.Mensagem = "Etapa encontrada";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<EtapaFunilModel>>> Criar(EtapaFunilCreateDto dto)
    {
        var resposta = new ResponseModel<List<EtapaFunilModel>>();
        try
        {
            if (string.IsNullOrWhiteSpace(dto.Nome))
            {
                resposta.Mensagem = "O nome da etapa é obrigatório.";
                resposta.Status = false;
                return resposta;
            }

            var ordem = dto.Ordem;
            if (!ordem.HasValue || ordem.Value <= 0)
            {
                var ultimaOrdem = await _context.EtapasFunil.MaxAsync(x => (int?)x.Ordem) ?? 0;
                ordem = ultimaOrdem + 1;
            }

            var etapa = new EtapaFunilModel
            {
                Nome = dto.Nome.Trim(),
                Cor = dto.Cor,
                ScriptSugerido = dto.ScriptSugerido,
                Ordem = ordem.Value,
                IsFixa = false
            };

            _context.EtapasFunil.Add(etapa);
            await _context.SaveChangesAsync();

            resposta.Dados = await ListarOrdenado();
            resposta.Mensagem = "Etapa criada com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<EtapaFunilModel>>> Editar(EtapaFunilEdicaoDto dto)
    {
        var resposta = new ResponseModel<List<EtapaFunilModel>>();
        try
        {
            var etapa = await _context.EtapasFunil.FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (etapa == null)
            {
                resposta.Mensagem = "Etapa não encontrada";
                resposta.Status = false;
                return resposta;
            }

            if (etapa.IsFixa)
            {
                // Etapas fixas: só Cor e ScriptSugerido são editáveis. Nome e Ordem são ignorados
                // mesmo se vierem no DTO. ATENÇÃO: fixas são globais (EmpresaId = 0), então cor e
                // script alterados valem para todas as empresas do mesmo banco.
                etapa.Cor = dto.Cor;
                etapa.ScriptSugerido = dto.ScriptSugerido;
                _context.EtapasFunil.Update(etapa);
                await _context.SaveChangesAsync();
                resposta.Mensagem = "Etapa atualizada com sucesso!";
                // Filtro global do AppDbContext já traz fixas (EmpresaId = 0) + etapas da empresa atual
                resposta.Dados = await ListarOrdenado();
                return resposta;
            }

            if (string.IsNullOrWhiteSpace(dto.Nome))
            {
                resposta.Mensagem = "O nome da etapa é obrigatório.";
                resposta.Status = false;
                return resposta;
            }

            etapa.Nome = dto.Nome.Trim();
            etapa.Cor = dto.Cor;
            etapa.ScriptSugerido = dto.ScriptSugerido;
            if (dto.Ordem.HasValue && dto.Ordem.Value > 0)
                etapa.Ordem = dto.Ordem.Value;

            _context.EtapasFunil.Update(etapa);
            await _context.SaveChangesAsync();

            resposta.Dados = await ListarOrdenado();
            resposta.Mensagem = "Etapa atualizada com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<EtapaFunilModel>>> Delete(int id)
    {
        var resposta = new ResponseModel<List<EtapaFunilModel>>();
        try
        {
            var etapa = await _context.EtapasFunil.FirstOrDefaultAsync(x => x.Id == id);
            if (etapa == null)
            {
                resposta.Mensagem = "Etapa não encontrada";
                resposta.Status = false;
                return resposta;
            }

            if (etapa.IsFixa)
            {
                resposta.Mensagem = "Etapas fixas do funil não podem ser excluídas.";
                resposta.Status = false;
                return resposta;
            }

            var temLeads = await _context.Leads.AnyAsync(l => l.EtapaFunilId == id);
            if (temLeads)
            {
                resposta.Mensagem = "Não é possível excluir a etapa pois existem leads vinculados a ela. Mova os leads para outra etapa antes.";
                resposta.Status = false;
                return resposta;
            }

            _context.EtapasFunil.Remove(etapa); // soft delete (IEntidadeAuditavel)
            await _context.SaveChangesAsync();

            resposta.Dados = await ListarOrdenado();
            resposta.Mensagem = "Etapa excluída com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<EtapaFunilModel>>> ReordenarEtapas(List<EtapaFunilOrdemDto> ordens)
    {
        var resposta = new ResponseModel<List<EtapaFunilModel>>();
        try
        {
            if (ordens == null || ordens.Count == 0)
            {
                resposta.Mensagem = "Nenhuma etapa informada para reordenar.";
                resposta.Status = false;
                return resposta;
            }

            var ids = ordens.Select(o => o.Id).Distinct().ToList();

            // Fixas são globais (compartilhadas entre empresas) — uma empresa não pode alterar a ordem delas.
            // Se vierem na lista (o front costuma mandar todas as colunas), são simplesmente ignoradas.
            var etapas = await _context.EtapasFunil
                .Where(x => ids.Contains(x.Id) && !x.IsFixa)
                .ToListAsync();

            foreach (var etapa in etapas)
                etapa.Ordem = ordens.Last(o => o.Id == etapa.Id).Ordem;

            await _context.SaveChangesAsync();

            resposta.Dados = await ListarOrdenado();
            resposta.Mensagem = "Etapas reordenadas com sucesso";
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
