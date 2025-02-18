
using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Dto.PlanoConta;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.PlanoConta;

public class PlanoContaService : IPlanoContaInterface
{
    private readonly AppDbContext _context;
    public PlanoContaService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseModel<PlanoContaModel>> BuscarPorId(int idPlanoConta)
    {
        ResponseModel<PlanoContaModel> resposta = new ResponseModel<PlanoContaModel>();
        try
        {
            var planoconta = await _context.PlanoConta.FirstOrDefaultAsync(x => x.Id == idPlanoConta);
            if (planoconta == null)
            {
                resposta.Mensagem = "Nenhum PlanoConta encontrado";
            
                return resposta;
            }

            resposta.Dados = planoconta;
            resposta.Mensagem = "PlanoConta Encontrado";
            
            return resposta;
        }
        catch (Exception ex)
        {

            resposta.Mensagem = "Erro ao buscar PlanoConta";
            resposta.Status = false;
            
            return resposta;
        }
    }

    public async Task<ResponseModel<List<PlanoContaModel>>> Criar(PlanoContaCreateDto planocontaCreateDto, int pageNumber = 1, int pageSize = 10)
    {
        ResponseModel<List<PlanoContaModel>> resposta = new ResponseModel<List<PlanoContaModel>>();

        try
        {
            // Criação do plano de contas principal
            var planoConta = new PlanoContaModel
            {
                Codigo = planocontaCreateDto.Codigo,
                Nome = planocontaCreateDto.Nome,
                Tipo = planocontaCreateDto.Tipo,
            };

            _context.PlanoConta.Add(planoConta);
            await _context.SaveChangesAsync();

            // Adicionando subplanos
            foreach (var subDto in planocontaCreateDto.SubPlanos)
            {
                var subPlano = new PlanoContaSubModel
                {
                    PlanoContaId = planoConta.Id,
                    Codigo = subDto.Codigo,
                    Nome = subDto.Nome,
                    Tipo = subDto.Tipo,
                };

                _context.PlanoContaSub.Add(subPlano);
            }

            await _context.SaveChangesAsync();
            
            var query = _context.PlanoConta
                .Include(x => x.SubPlanos)
                .AsQueryable();

            resposta = await PaginationHelper.PaginateAsync(query, pageNumber, pageSize);
            resposta.Mensagem = "Plano de Contas criado com sucesso.";

            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = $"Erro ao criar Plano de Contas: {ex.Message}";
            resposta.Status = false;
         
            return resposta;
        }
    }

    public async Task<ResponseModel<List<PlanoContaModel>>> Delete(int idPlanoConta, int pageNumber = 1, int pageSize = 10)
    {
        ResponseModel<List<PlanoContaModel>> resposta = new ResponseModel<List<PlanoContaModel>>();

        try
        {
            var planoconta = await _context.PlanoConta.FirstOrDefaultAsync(x => x.Id == idPlanoConta);
            if (planoconta == null)
            {
                resposta.Mensagem = "Nenhum PlanoConta encontrado";
                return resposta;
            }

            _context.Remove(planoconta);
            await _context.SaveChangesAsync();

            var query = _context.PlanoConta
                .Include(x => x.SubPlanos)
                .AsQueryable();

            resposta = await PaginationHelper.PaginateAsync(query, pageNumber, pageSize);
            resposta.Mensagem = "PlanoConta Excluido com sucesso";
            
            return resposta;
        }
        catch (Exception ex)
        {

            resposta.Mensagem = ex.Message;
            resposta.Status = false;
         
            return resposta;
        }
    }

    public async Task<ResponseModel<List<PlanoContaModel>>> Editar(PlanoContaEdicaoDto planocontaEdicaoDto, int pageNumber = 1, int pageSize = 10)
    {
        ResponseModel<List<PlanoContaModel>> resposta = new ResponseModel<List<PlanoContaModel>>();
        try
        {
            var planoConta = await _context.PlanoConta
                .Include(p => p.SubPlanos)
                .FirstOrDefaultAsync(p => p.Id == planocontaEdicaoDto.Id);

            if (planoConta == null)
            {
                resposta.Mensagem = "Plano de Contas não encontrado.";
                resposta.Status = false;
               
                return resposta;
            }

            // Atualizando os dados do plano principal
            planoConta.Codigo = planocontaEdicaoDto.Codigo;
            planoConta.Nome = planocontaEdicaoDto.Nome;
            planoConta.Tipo = planocontaEdicaoDto.Tipo;

            // Atualizando subplanos
            foreach (var subDto in planocontaEdicaoDto.SubPlanos)
            {
                var subPlano = planoConta.SubPlanos.FirstOrDefault(s => s.Id == subDto.Id);

                if (subPlano != null)
                {
                    // Atualizar subplano existente
                    subPlano.Codigo = subDto.Codigo;
                    subPlano.Nome = subDto.Nome;
                    subPlano.Tipo = subDto.Tipo;
                }
                else
                {
                    // Adicionar novo subplano
                    var novoSubPlano = new PlanoContaSubModel
                    {
                        PlanoContaId = planoConta.Id,
                        Codigo = subDto.Codigo,
                        Nome = subDto.Nome,
                        Tipo = subDto.Tipo,
                    };

                    planoConta.SubPlanos.Add(novoSubPlano);
                }
            }

            await _context.SaveChangesAsync();

            var query = _context.PlanoConta
                .Include(x => x.SubPlanos)
                .AsQueryable();

            resposta = await PaginationHelper.PaginateAsync(query, pageNumber, pageSize);
            resposta.Mensagem = "Plano de Contas atualizado com sucesso.";
            
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = $"Erro ao atualizar Plano de Contas: {ex.Message}";
            resposta.Status = false;
          
            return resposta;
        }
    }

    public async Task<ResponseModel<List<PlanoContaModel>>> Listar(int pageNumber = 1, int pageSize = 10, string? codigoFiltro = null, string? nomeFiltro = null, Tipo? tipoFiltro = null, bool? inativoFiltro = null, bool paginar = true)
    {
        ResponseModel<List<PlanoContaModel>> resposta = new ResponseModel<List<PlanoContaModel>>();

        try
        {
            // Criacaoo da query inicial
            var query = _context.PlanoConta
                .Include(p => p.SubPlanos) // Inclui as subcontas relacionadas
                .AsQueryable();

            // Aplicacao de filtros
            query = query.Where(p =>
                (string.IsNullOrEmpty(codigoFiltro) || p.Codigo.Contains(codigoFiltro)) &&
                (string.IsNullOrEmpty(nomeFiltro) || p.Nome.Contains(nomeFiltro)) &&
                (!tipoFiltro.HasValue || p.Tipo.ToString().Contains(p.Tipo.ToString())) &&
                (!inativoFiltro.HasValue || p.Inativo == inativoFiltro)
            );

            // Ordenacao padrao (cuidado ao mudar a ordenacao pq pode afetar/dificultar a visualizacao de pais e filhos)
            query = query.OrderBy(p => p.Codigo);

            resposta = paginar ? await PaginationHelper.PaginateAsync(query, pageNumber, pageSize) : new ResponseModel<List<PlanoContaModel>> { Dados = await query.ToListAsync() };
            resposta.Mensagem = "Planos de Contas listados com sucesso.";
         
            return resposta;
        }
        catch (Exception e)
        {
            resposta.Mensagem = e.Message;
            resposta.Status = false;
            
            return resposta;
        }
    }
}