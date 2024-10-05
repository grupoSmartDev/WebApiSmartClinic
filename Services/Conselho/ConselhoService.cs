
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

    public async Task<ResponseModel<List<ConselhoModel>>> Criar(ConselhoCreateDto conselhoCreateDto)
    {
        ResponseModel<List<ConselhoModel>> resposta = new ResponseModel<List<ConselhoModel>>();

        try
        {
            var conselho = new ConselhoModel();

            conselho.Nome = conselhoCreateDto.Nome;

            _context.Add(conselho);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Conselho.ToListAsync();
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

    public async Task<ResponseModel<List<ConselhoModel>>> Delete(int idConselho)
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

            resposta.Dados = await _context.Conselho.ToListAsync();
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

    public async Task<ResponseModel<List<ConselhoModel>>> Editar(ConselhoEdicaoDto conselhoEdicaoDto)
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

            _context.Update(conselho);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Conselho.ToListAsync();
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

    public async Task<ResponseModel<List<ConselhoModel>>> Listar()
    {
        ResponseModel<List<ConselhoModel>> resposta = new ResponseModel<List<ConselhoModel>>();

        try
        {
            var conselho = await _context.Conselho.ToListAsync();

            resposta.Dados = conselho;
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