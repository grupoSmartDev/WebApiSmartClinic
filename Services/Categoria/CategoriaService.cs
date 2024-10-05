
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

    public async Task<ResponseModel<List<CategoriaModel>>> Criar(CategoriaCreateDto categoriaCreateDto)
    {
        ResponseModel<List<CategoriaModel>> resposta = new ResponseModel<List<CategoriaModel>>();

        try
        {
            var categoria = new CategoriaModel();

            categoria.Nome = categoriaCreateDto.Nome;

            _context.Add(categoria);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Categoria.ToListAsync();
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

    public async Task<ResponseModel<List<CategoriaModel>>> Delete(int idCategoria)
    {
        ResponseModel<List<CategoriaModel>> resposta = new ResponseModel<List<CategoriaModel>>();

        try
        {
            var categoria = await _context.Categoria.FirstOrDefaultAsync(x => x.Id == idCategoria);
            if (categoria == null)
            {
                resposta.Mensagem = "Nenhum Categoria encontrado";
                return resposta;
            }

            _context.Remove(categoria);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Categoria.ToListAsync();
            resposta.Mensagem = "Categoria Excluido com sucesso";
            return resposta;

        }
        catch (Exception ex)
        {

            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<CategoriaModel>>> Editar(CategoriaEdicaoDto categoriaEdicaoDto)
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

            resposta.Dados = await _context.Categoria.ToListAsync();
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

    public async Task<ResponseModel<List<CategoriaModel>>> Listar()
    {
        ResponseModel<List<CategoriaModel>> resposta = new ResponseModel<List<CategoriaModel>>();

        try
        {
            var categoria = await _context.Categoria.ToListAsync();

            resposta.Dados = categoria;
            resposta.Mensagem = "Todos os Categoria foram encontrados";
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