
using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Dto.Profissao;
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

    public async Task<ResponseModel<List<ProfissaoModel>>> Criar(ProfissaoCreateDto profissaoCreateDto)
    {
        ResponseModel<List<ProfissaoModel>> resposta = new ResponseModel<List<ProfissaoModel>>();

        try
        {
            var profissao = new ProfissaoModel();

            profissao.Descricao = profissaoCreateDto.Descricao;

            _context.Add(profissao);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Profissao.ToListAsync();
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

    public async Task<ResponseModel<List<ProfissaoModel>>> Delete(int idProfissao)
    {
        ResponseModel<List<ProfissaoModel>> resposta = new ResponseModel<List<ProfissaoModel>>();

        try
        {
            var profissao = await _context.Profissao.FirstOrDefaultAsync(x => x.Id == idProfissao);
            if (profissao == null)
            {
                resposta.Mensagem = "Nenhum Profissao encontrado";
                return resposta;
            }

            _context.Remove(profissao);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Profissao.ToListAsync();
            resposta.Mensagem = "Profissao Excluido com sucesso";
            
            return resposta;

        }
        catch (Exception ex)
        {

            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            
            return resposta;
        }
    }

    public async Task<ResponseModel<List<ProfissaoModel>>> Editar(ProfissaoEdicaoDto profissaoEdicaoDto)
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

            resposta.Dados = await _context.Profissao.ToListAsync();
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

    public async Task<ResponseModel<List<ProfissaoModel>>> Listar()
    {
        ResponseModel<List<ProfissaoModel>> resposta = new ResponseModel<List<ProfissaoModel>>();

        try
        {
            var profissao = await _context.Profissao.ToListAsync();

            resposta.Dados = profissao;
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