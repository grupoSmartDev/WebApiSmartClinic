
using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Dto.Categoria;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Categoria;

public class CategoriaService : ICategoriaInterface
{
    private readonly AppDbContext _context;
    public CategoriaService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseModel<CategoriaModel>> BuscarPorId(int idCategoria)
    {
        ResponseModel<CategoriaModel> resposta = new ResponseModel<CategoriaModel>();
        try
        {
            var categoria = await _context.Categoria.FirstOrDefaultAsync(x => x.Id == idCategoria);
            if (categoria == null)
            {
                resposta.Mensagem = "Nenhum Categoria encontrado";
                return resposta;
            }

            resposta.Dados = categoria;
            resposta.Mensagem = "Categoria Encontrado";
            return resposta;
        }
        catch (Exception ex)
        {

            resposta.Mensagem = "Erro ao buscar Categoria";
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<CategoriaModel>>> Criar(CategoriaCreateDto categoriaCreateDto, int pageNumber = 1, int pageSize = 10)
    {
        ResponseModel<List<CategoriaModel>> resposta = new ResponseModel<List<CategoriaModel>>();

        try
        {
            var categoria = new CategoriaModel();

            categoria.Nome = categoriaCreateDto.Nome;

            _context.Add(categoria);
            await _context.SaveChangesAsync();

            var query = _context.Categoria.AsQueryable();

            resposta = await PaginationHelper.PaginateAsync(query, pageNumber, pageSize);
            resposta.Mensagem = "Categoria criado com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }

    }

    public async Task<ResponseModel<List<CategoriaModel>>> Delete(int idCategoria, int pageNumber = 1, int pageSize = 10)
    {
        ResponseModel<List<CategoriaModel>> resposta = new ResponseModel<List<CategoriaModel>>();
        try
        {
            var categoria = await _context.Categoria.FirstOrDefaultAsync(x => x.Id == idCategoria);
            if (categoria == null)
            {
                resposta.Mensagem = "Nenhuma Categoria encontrada";
                return resposta;
            }

            if (categoria.IsSystemDefault)
            {
                resposta.Mensagem = "Não é possível excluir uma categoria padrão do sistema";
                resposta.Status = false;
                return resposta;
            }

            // Soft Delete - apenas marca como inativo
            categoria.Ativo = false;
            _context.Update(categoria);
            await _context.SaveChangesAsync();

            var query = _context.Categoria.Where(x => x.Ativo).AsQueryable();
            resposta = await PaginationHelper.PaginateAsync(query, pageNumber, pageSize);
            resposta.Mensagem = "Categoria excluída com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<CategoriaModel>>> Editar(CategoriaEdicaoDto categoriaEdicaoDto, int pageNumber = 1, int pageSize = 10)
    {
        ResponseModel<List<CategoriaModel>> resposta = new ResponseModel<List<CategoriaModel>>();

        try
        {
            var categoria = _context.Categoria.FirstOrDefault(x => x.Id == categoriaEdicaoDto.Id);
            if (categoria == null)
            {
                resposta.Mensagem = "Categoria não encontrado";
                return resposta;
            }

            categoria.Id = categoriaEdicaoDto.Id;
            categoria.Nome = categoriaEdicaoDto.Nome;

            _context.Update(categoria);
            await _context.SaveChangesAsync();

            var query = _context.Categoria.AsQueryable();

            resposta = await PaginationHelper.PaginateAsync(query, pageNumber, pageSize);
            resposta.Mensagem = "Categoria Atualizado com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {

            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<CategoriaModel>>> Listar(int pageNumber = 1, int pageSize = 10, int? idFiltro = null, string? descricaoFiltro = null, bool paginar = true)
    {
        ResponseModel<List<CategoriaModel>> resposta = new ResponseModel<List<CategoriaModel>>();

        try
        {
            var query = _context.Categoria.AsQueryable();

          
            if (idFiltro.HasValue)
                query = query.Where(x => x.Id == idFiltro);

            if (!string.IsNullOrEmpty(descricaoFiltro))
                query = query.Where(x => x.Nome.ToLower().Contains(descricaoFiltro));

            query.OrderBy(x => x.Id);

            resposta = paginar ? await PaginationHelper.PaginateAsync(query, pageNumber, pageSize) : new ResponseModel<List<CategoriaModel>> { Dados = await query.ToListAsync() };
            resposta.Mensagem = "Todos as categorias foram encontradas";
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