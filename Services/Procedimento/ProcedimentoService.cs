
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

            resposta.Mensagem = "Erro ao buscar Procedimento";
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<ProcedimentoModel>>> Criar(ProcedimentoCreateDto procedimentoCreateDto)
    {
        ResponseModel<List<ProcedimentoModel>> resposta = new ResponseModel<List<ProcedimentoModel>>();

        try
        {
            var procedimento = new ProcedimentoModel();

            procedimento.Nome = procedimentoCreateDto.Nome;
            procedimento.Descricao = procedimentoCreateDto.Descricao;
            procedimento.Preco = procedimentoCreateDto.Preco;
            procedimento.Duracao = procedimentoCreateDto.Duracao;
            procedimento.Categoria = procedimentoCreateDto.Categoria;
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

            resposta.Dados = await _context.Procedimento.ToListAsync();
            resposta.Mensagem = "Procedimento criado com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }

    }

    public async Task<ResponseModel<List<ProcedimentoModel>>> Delete(int idProcedimento)
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

            resposta.Dados = await _context.Procedimento.ToListAsync();
            resposta.Mensagem = "Procedimento Excluido com sucesso";

            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<ProcedimentoModel>>> Editar(ProcedimentoEdicaoDto procedimentoEdicaoDto)
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
            procedimento.Categoria = procedimentoEdicaoDto.Categoria;
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

            resposta.Dados = await _context.Procedimento.ToListAsync();
            resposta.Mensagem = "Procedimento Atualizado com sucesso";
            
            return resposta;
        }
        catch (Exception ex)
        {

            resposta.Mensagem = ex.Message;
            resposta.Status = false;
           
            return resposta;
        }
    }

    public async Task<ResponseModel<List<ProcedimentoModel>>> Listar()
    {
        ResponseModel<List<ProcedimentoModel>> resposta = new ResponseModel<List<ProcedimentoModel>>();

        try
        {
            var procedimento = await _context.Procedimento.ToListAsync();

            resposta.Dados = procedimento;
            resposta.Mensagem = "Todos os Procedimento foram encontrados";
            
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