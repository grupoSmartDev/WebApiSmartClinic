
using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Dto.Conselho;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Conselho;

public class ConselhoService : IConselhoInterface
{
    private readonly AppDbContext _context;
    public ConselhoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseModel<ConselhoModel>> BuscarPorId(int idConselho)
    {
        ResponseModel<ConselhoModel> resposta = new ResponseModel<ConselhoModel>();
        try
        {
            var conselho = await _context.Conselho.FirstOrDefaultAsync(x => x.Id == idConselho);
            if (conselho == null)
            {
                resposta.Mensagem = "Nenhum Conselho encontrado";
                return resposta;
            }

            resposta.Dados = conselho;
            resposta.Mensagem = "Conselho encontrado";
            return resposta;
        }
        catch (Exception ex)
        {

            resposta.Mensagem = "Erro ao buscar Conselho";
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<ConselhoModel>>> Criar(ConselhoCreateDto conselhoCreateDto, int pageNumber = 1, int pageSize = 10)
    {
        ResponseModel<List<ConselhoModel>> resposta = new ResponseModel<List<ConselhoModel>>();

        try
        {
            var conselho = new ConselhoModel();

            conselho.Nome = conselhoCreateDto.Nome;
            conselho.Sigla = conselhoCreateDto.Sigla;

            _context.Add(conselho);
            await _context.SaveChangesAsync();

            var query = _context.Conselho.AsQueryable();

            resposta = await PaginationHelper.PaginateAsync(query, pageNumber, pageSize);
            resposta.Mensagem = "Conselho criado com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }

    }

    public async Task<ResponseModel<List<ConselhoModel>>> Delete(int idConselho, int pageNumber = 1, int pageSize = 10)
    {
        ResponseModel<List<ConselhoModel>> resposta = new ResponseModel<List<ConselhoModel>>();

        try
        {
            var conselho = await _context.Conselho.FirstOrDefaultAsync(x => x.Id == idConselho);
            if (conselho == null)
            {
                resposta.Mensagem = "Nenhum Conselho encontrado";
                return resposta;
            }

            _context.Remove(conselho);
            await _context.SaveChangesAsync();

            var query = _context.Conselho.AsQueryable();

            resposta = await PaginationHelper.PaginateAsync(query, pageNumber, pageSize);
            resposta.Mensagem = "Conselho Excluido com sucesso";
            return resposta;

        }
        catch (Exception ex)
        {

            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<ConselhoModel>>> Editar(ConselhoEdicaoDto conselhoEdicaoDto, int pageNumber = 1, int pageSize = 10)
    {
        ResponseModel<List<ConselhoModel>> resposta = new ResponseModel<List<ConselhoModel>>();

        try
        {
            var conselho = _context.Conselho.FirstOrDefault(x => x.Id == conselhoEdicaoDto.Id);
            if (conselho == null)
            {
                resposta.Mensagem = "Conselho não encontrado";
                return resposta;
            }

            conselho.Id = conselhoEdicaoDto.Id;
            conselho.Nome = conselhoEdicaoDto.Nome;
            conselho.Sigla= conselhoEdicaoDto.Sigla;

            _context.Update(conselho);
            await _context.SaveChangesAsync();

            var query = _context.Conselho.AsQueryable();

            resposta = await PaginationHelper.PaginateAsync(query, pageNumber, pageSize);
            resposta.Mensagem = "Conselho Atualizado com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {

            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<ConselhoModel>>> Listar(int pageNumber = 1, int pageSize = 10, int? codigoFiltro = null, string? nomeFiltro = null, string? siglaFiltro = null, bool paginar = true)
    {
        ResponseModel<List<ConselhoModel>> resposta = new ResponseModel<List<ConselhoModel>>();

        try
        {
            var query = _context.Conselho.AsQueryable();

            query = query.Where(x =>
                (!codigoFiltro.HasValue || x.Id == codigoFiltro) &&
                (string.IsNullOrEmpty(nomeFiltro) || x.Nome == nomeFiltro) &&
                (string.IsNullOrEmpty(siglaFiltro) || x.Sigla == siglaFiltro)
            );

            query = query.OrderBy(x => x.Id);

            resposta = paginar ? await PaginationHelper.PaginateAsync(query, pageNumber, pageSize) : new ResponseModel<List<ConselhoModel>> { Dados = await query.ToListAsync() };
            resposta.Mensagem = "Todos os Conselho foram encontrados";
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