
using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Dto.Profissao;
using WebApiSmartClinic.Dto.Profissional;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Profissao;

public class ProfissaoService : IProfissaoInterface
{
    private readonly AppDbContext _context;
    public ProfissaoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseModel<ProfissaoModel>> BuscarPorId(int idProfissao)
    {
        ResponseModel<ProfissaoModel> resposta = new ResponseModel<ProfissaoModel>();
        try
        {
            var profissao = await _context.Profissao.FirstOrDefaultAsync(x => x.Id == idProfissao);
            if (profissao == null)
            {
                resposta.Mensagem = "Nenhum Profissao encontrado";
                return resposta;
            }

            resposta.Dados = profissao;
            resposta.Mensagem = "Profissao Encontrado";
            
            return resposta;
        }
        catch (Exception ex)
        {

            resposta.Mensagem = "Erro ao buscar Profissao";
            resposta.Status = false;
            
            return resposta;
        }
    }

    public async Task<ResponseModel<List<ProfissaoModel>>> Criar(ProfissaoCreateDto profissaoCreateDto, int pageNumber = 1, int pageSize = 10)
    {
        ResponseModel<List<ProfissaoModel>> resposta = new ResponseModel<List<ProfissaoModel>>();

        try
        {
            var profissao = new ProfissaoModel();

            profissao.Descricao = profissaoCreateDto.Descricao;

            _context.Add(profissao);
            await _context.SaveChangesAsync();

            var query = _context.Profissao.AsQueryable();

            resposta = await PaginationHelper.PaginateAsync(query, pageNumber, pageSize);
            resposta.Mensagem = "Profissao criado com sucesso";
            
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            
            return resposta;
        }
    }

    public async Task<ResponseModel<List<ProfissaoModel>>> Delete(int idProfissao, int pageNumber = 1, int pageSize = 10)
    {
        ResponseModel<List<ProfissaoModel>> resposta = new ResponseModel<List<ProfissaoModel>>();

        try
        {
            resposta.Status = false;

            var profissao = await _context.Profissao.FirstOrDefaultAsync(x => x.Id == idProfissao);
            if (profissao == null)
            {
                resposta.Mensagem = "Nenhuma Profissão encontrada.";
                return resposta;
            }

            var profissional = await _context.Profissional.FirstOrDefaultAsync(x => x.ProfissaoId == idProfissao);
            if (profissional == null)
            {
                profissao.Ativo = false;

                _context.Update(profissao);
                await _context.SaveChangesAsync();
                resposta.Status = true;
                resposta.Mensagem = "Profissão inativa com sucesso.";
            }
            else resposta.Mensagem = "Profissional vinculado, remova a profissão do profissional antes de inativar.";

            
            var query = _context.Profissao.AsQueryable();

            resposta = await PaginationHelper.PaginateAsync(query, pageNumber, pageSize);
            
            
            return resposta;

        }
        catch (Exception ex)
        {

            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            
            return resposta;
        }
    }

    public async Task<ResponseModel<List<ProfissaoModel>>> Editar(ProfissaoEdicaoDto profissaoEdicaoDto, int pageNumber = 1, int pageSize = 10)
    {
        ResponseModel<List<ProfissaoModel>> resposta = new ResponseModel<List<ProfissaoModel>>();

        try
        {
            var profissao = _context.Profissao.FirstOrDefault(x => x.Id == profissaoEdicaoDto.Id);
            if (profissao == null)
            {
                resposta.Mensagem = "Profissao não encontrado";
                
                return resposta;
            }
            
            profissao.Id = profissaoEdicaoDto.Id;
            profissao.Descricao = profissaoEdicaoDto.Descricao;

            _context.Update(profissao);
            await _context.SaveChangesAsync();

            var query = _context.Profissao.AsQueryable();

            resposta = await PaginationHelper.PaginateAsync(query, pageNumber, pageSize);
            resposta.Mensagem = "Profissao Atualizado com sucesso";
            
            return resposta;
        }
        catch (Exception ex)
        {

            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            
            return resposta;
        }
    }

    public async Task<ResponseModel<List<ProfissaoModel>>> Ativar(ProfissaoEdicaoDto dto, int pageNumber = 1, int pageSize = 10)
    {
        ResponseModel<List<ProfissaoModel>> resposta = new ResponseModel<List<ProfissaoModel>>();

        try
        {
            var profissao = _context.Profissao.FirstOrDefault(x => x.Id == dto.Id);
            if (profissao == null)
            {
                resposta.Mensagem = "Profissão não encontrado";
                return resposta;
            }

            if (profissao.Ativo == true)
            {
                resposta.Mensagem = "Profissão já esta atívo";
                return resposta;
            }

            profissao.Ativo = true;

            _context.Update(profissao);
            await _context.SaveChangesAsync();
            var query = _context.Profissao.AsQueryable();

            resposta = await PaginationHelper.PaginateAsync(query, pageNumber, pageSize);
            resposta.Mensagem = "Profissão Ativo com sucesso";
            return resposta;

        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<ProfissaoModel>>> Listar(int pageNumber = 1, int pageSize = 10, int? codigoFiltro = null, string? descricaoFiltro = null, bool paginar = true)
    {
        ResponseModel<List<ProfissaoModel>> resposta = new ResponseModel<List<ProfissaoModel>>();

        try
        {
            var query = _context.Profissao.AsQueryable();
          

            if (!string.IsNullOrEmpty(descricaoFiltro))
                query = query.Where(p => p.Descricao.ToLower().Contains(descricaoFiltro.ToLower()));


            // Ordenação padrão
            query = query.OrderBy(x => x.Id);

            // Paginação opcional
            resposta = paginar ? await PaginationHelper.PaginateAsync(query, pageNumber, pageSize) : new ResponseModel<List<ProfissaoModel>> { Dados = await query.ToListAsync() };
            resposta.Mensagem = "Todos os Profissao foram encontrados";

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