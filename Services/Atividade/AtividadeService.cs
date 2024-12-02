
using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Dto.Atividade;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Atividade;

public class AtividadeService : IAtividadeInterface
{
    private readonly AppDbContext _context;
    public AtividadeService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseModel<AtividadeModel>> BuscarPorId(int idAtividade)
    {
        ResponseModel<AtividadeModel> resposta = new ResponseModel<AtividadeModel>();
        try
        {
            var atividade = await _context.Atividade.FirstOrDefaultAsync(x => x.Id == idAtividade);
            if (atividade == null)
            {
                resposta.Mensagem = "Nenhum Atividade encontrado";
                return resposta;
            }

            resposta.Dados = atividade;
            resposta.Mensagem = "Atividade Encontrado";
            return resposta;
        }
        catch (Exception ex)
        {

            resposta.Mensagem = "Erro ao buscar Atividade";
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<AtividadeModel>>> Criar(AtividadeCreateDto atividadeCreateDto)
    {
        ResponseModel<List<AtividadeModel>> resposta = new ResponseModel<List<AtividadeModel>>();

        try
        {
            var atividade = new AtividadeModel();

            atividade.Descricao = atividadeCreateDto.Descricao;
            atividade.EvolucaoId = atividadeCreateDto.EvolucaoId;

            _context.Add(atividade);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Atividade.ToListAsync();
            resposta.Mensagem = "Atividade criado com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }

    }

    public async Task<ResponseModel<List<AtividadeModel>>> Delete(int idAtividade)
    {
        ResponseModel<List<AtividadeModel>> resposta = new ResponseModel<List<AtividadeModel>>();

        try
        {
            var atividade = await _context.Atividade.FirstOrDefaultAsync(x => x.Id == idAtividade);
            if (atividade == null)
            {
                resposta.Mensagem = "Nenhum Atividade encontrado";
                return resposta;
            }

            _context.Remove(atividade);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Atividade.ToListAsync();
            resposta.Mensagem = "Atividade Excluido com sucesso";
            return resposta;

        }
        catch (Exception ex)
        {

            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<AtividadeModel>>> Editar(AtividadeEdicaoDto atividadeEdicaoDto)
    {
        ResponseModel<List<AtividadeModel>> resposta = new ResponseModel<List<AtividadeModel>>();

        try
        {
            var atividade = _context.Atividade.FirstOrDefault(x => x.Id == atividadeEdicaoDto.Id);
            if (atividade == null)
            {
                resposta.Mensagem = "Atividade não encontrado";
                return resposta;
            }

            atividade.Id = atividadeEdicaoDto.Id;
            atividade.Descricao = atividadeEdicaoDto.Descricao;
            atividade.EvolucaoId = atividadeEdicaoDto.EvolucaoId;

            _context.Update(atividade);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Atividade.ToListAsync();
            resposta.Mensagem = "Atividade Atualizado com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {

            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<AtividadeModel>>> Listar()
    {
        ResponseModel<List<AtividadeModel>> resposta = new ResponseModel<List<AtividadeModel>>();

        try
        {
            var atividade = await _context.Atividade.ToListAsync();

            resposta.Dados = atividade;
            resposta.Mensagem = "Todos os Atividade foram encontrados";
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