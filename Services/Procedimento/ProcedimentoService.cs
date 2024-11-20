
using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Dto.Paciente;
using WebApiSmartClinic.Dto.Procedimento;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Procedimento;

public class ProcedimentoService : IProcedimentoInterface
{
    private readonly AppDbContext _context;
    public ProcedimentoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseModel<ProcedimentoModel>> BuscarPorId(int idProcedimento)
    {
        ResponseModel<ProcedimentoModel> resposta = new ResponseModel<ProcedimentoModel>();
        try
        {
            var procedimento = await _context.Procedimento.FirstOrDefaultAsync(x => x.Id == idProcedimento);
            if (procedimento == null)
            {
                resposta.Mensagem = "Nenhum Procedimento encontrado";
            
                return resposta;
            }

            resposta.Dados = procedimento;
            resposta.Mensagem = "Procedimento Encontrado";
            
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            
            return resposta;
        }
    }

    public async Task<ResponseModel<List<ProcedimentoModel>>> Criar(ProcedimentoCreateDto procedimentoCreateDto, int pageNumber = 1, int pageSize = 10)
    {
        ResponseModel<List<ProcedimentoModel>> resposta = new ResponseModel<List<ProcedimentoModel>>();

        try
        {
            var procedimento = new ProcedimentoModel();

            procedimento.Nome = procedimentoCreateDto.Nome;
            procedimento.Descricao = procedimentoCreateDto.Descricao;
            procedimento.Preco = procedimentoCreateDto.Preco;
            procedimento.Duracao = procedimentoCreateDto.Duracao;
            procedimento.CategoriaId = procedimentoCreateDto.CategoriaId;
            procedimento.Ativo = procedimentoCreateDto.Ativo;
            procedimento.MateriaisNecessarios = procedimentoCreateDto.MateriaisNecessarios;
            procedimento.PercentualComissao = procedimentoCreateDto.PercentualComissao;

            // Verificar depois a possíbilidade de fazer apenas uma verificação se é diferente de nulo e não haver necessidade de toda hora bater no banco pra ver se é válido
            var categoriaExiste = await _context.Categoria.AnyAsync(c => c.Id == procedimentoCreateDto.CategoriaId);
            if (categoriaExiste)
            {
                procedimento.CategoriaId = procedimentoCreateDto.CategoriaId;
            }

            _context.Add(procedimento);
            await _context.SaveChangesAsync();

            // Query do banco de dados
            var query = _context.Procedimento.AsQueryable();

            // Paginação usando o helper
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

    public async Task<ResponseModel<List<ProcedimentoModel>>> Delete(int idProcedimento, int pageNumber = 1, int pageSize = 10)
    {
        ResponseModel<List<ProcedimentoModel>> resposta = new ResponseModel<List<ProcedimentoModel>>();

        try
        {
            var procedimento = await _context.Procedimento.FirstOrDefaultAsync(x => x.Id == idProcedimento);
            if (procedimento == null)
            {
                resposta.Mensagem = "Nenhum Procedimento encontrado";
               
                return resposta;
            }

            _context.Remove(procedimento);
            await _context.SaveChangesAsync();

            // Query do banco de dados
            var query = _context.Procedimento.AsQueryable();

            // Paginação usando o helper
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

    public async Task<ResponseModel<List<ProcedimentoModel>>> Editar(ProcedimentoEdicaoDto procedimentoEdicaoDto, int pageNumber = 1, int pageSize = 10)
    {
        ResponseModel<List<ProcedimentoModel>> resposta = new ResponseModel<List<ProcedimentoModel>>();

        try
        {
            var procedimento = _context.Procedimento.FirstOrDefault(x => x.Id == procedimentoEdicaoDto.Id);
            if (procedimento == null)
            {
                resposta.Mensagem = "Procedimento não encontrado";
                
                return resposta;
            }

            procedimento.Id = procedimentoEdicaoDto.Id;
            procedimento.Nome = procedimentoEdicaoDto.Nome;
            procedimento.Descricao = procedimentoEdicaoDto.Descricao;
            procedimento.Preco = procedimentoEdicaoDto.Preco;
            procedimento.Duracao = procedimentoEdicaoDto.Duracao;
            procedimento.CategoriaId = procedimentoEdicaoDto.CategoriaId;
            procedimento.Ativo = procedimentoEdicaoDto.Ativo;
            procedimento.MateriaisNecessarios = procedimentoEdicaoDto.MateriaisNecessarios;
            procedimento.PercentualComissao = procedimentoEdicaoDto.PercentualComissao;

            // Verificar depois a possíbilidade de fazer apenas uma verificação se é diferente de nulo e não haver necessidade de toda hora bater no banco pra ver se é válido
            var categoriaExiste = await _context.Categoria.AnyAsync(c => c.Id == procedimentoEdicaoDto.CategoriaId);
            if (categoriaExiste)
            {
                procedimento.CategoriaId = procedimentoEdicaoDto.CategoriaId;
            }

            _context.Update(procedimento);
            await _context.SaveChangesAsync();

            // Query do banco de dados
            var query = _context.Procedimento.AsQueryable();

            // Paginação usando o helper
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

    public async Task<ResponseModel<List<ProcedimentoModel>>> Listar(int pageNumber = 1, int pageSize = 10, int? codigo = null, string? nome = null)
    {
        ResponseModel<List<ProcedimentoModel>> resposta = new ResponseModel<List<ProcedimentoModel>>();

        try
        {
            // Query do banco de dados
            var query = _context.Procedimento.AsQueryable();

            // Filtros
            if (codigo.HasValue)
            {
                query.Where(x => x.Id == codigo.Value);
            }

            if (!string.IsNullOrEmpty(nome))
            {
                query.Where(x => x.Nome.Contains(nome));
            }

            query = query.OrderBy(x => x.Id);

            // Paginação usando o helper
            resposta = await PaginationHelper.PaginateAsync(query, pageNumber, pageSize);

            return resposta;
        }
        catch (Exception ex)
        {
            return new ResponseModel<List<ProcedimentoModel>>
            {
                Status = false,
                Mensagem = ex.Message
            };
        }
    }
}