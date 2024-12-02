
using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Dto.Evolucao;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Evolucao;

public class EvolucaoService : IEvolucaoInterface
{
    private readonly AppDbContext _context;
    public EvolucaoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseModel<EvolucaoModel>> BuscarPorId(int idEvolucao)
    {
        ResponseModel<EvolucaoModel> resposta = new ResponseModel<EvolucaoModel>();
        try
        {
            var evolucao = await _context.Evolucoes.FirstOrDefaultAsync(x => x.Id == idEvolucao);
            if (evolucao == null)
            {
                resposta.Mensagem = "Nenhum Evolucao encontrado";
                return resposta;
            }
            
            resposta.Dados = evolucao;

            resposta.Mensagem = "Evolucao Encontrado";
            return resposta;
        }
        catch (Exception ex)
        {

            resposta.Mensagem = "Erro ao buscar Evolucao";
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<EvolucaoModel>>> Criar(EvolucaoCreateDto evolucaoCreateDto)
    {
        ResponseModel<List<EvolucaoModel>> resposta = new ResponseModel<List<EvolucaoModel>>();

        try
        {
            var evolucao = new EvolucaoModel();

            evolucao.Observacao = evolucaoCreateDto.Observacao;
            evolucao.DataEvolucao = evolucaoCreateDto.DataEvolucao;
            evolucao.PacienteId = evolucaoCreateDto.PacienteId;
            evolucao.Exercicios = evolucaoCreateDto.Exercicios;
            evolucao.Atividades = evolucaoCreateDto.Atividades;

            _context.Add(evolucao);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Evolucoes.ToListAsync();
            resposta.Mensagem = "Evolucao criado com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }

    }

    public async Task<ResponseModel<List<EvolucaoModel>>> Delete(int idEvolucao)
    {
        ResponseModel<List<EvolucaoModel>> resposta = new ResponseModel<List<EvolucaoModel>>();

        try
        {
            var evolucao = await _context.Evolucoes.FirstOrDefaultAsync(x => x.Id == idEvolucao);
            if (evolucao == null)
            {
                resposta.Mensagem = "Nenhum Evolucao encontrado";
                return resposta;
            }

            _context.Remove(evolucao);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Evolucoes.ToListAsync();
            resposta.Mensagem = "Evolucao Excluido com sucesso";
            return resposta;

        }
        catch (Exception ex)
        {

            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<EvolucaoModel>>> Editar(EvolucaoEdicaoDto evolucaoEdicaoDto)
    {
        ResponseModel<List<EvolucaoModel>> resposta = new ResponseModel<List<EvolucaoModel>>();

        try
        {
            var evolucao = _context.Evolucoes.FirstOrDefault(x => x.Id == evolucaoEdicaoDto.Id);
            if (evolucao == null)
            {
                resposta.Mensagem = "Evolucao não encontrado";
                return resposta;
            }

            evolucao.Observacao = evolucaoEdicaoDto.Observacao;
            evolucao.DataEvolucao = evolucaoEdicaoDto.DataEvolucao;
            evolucao.PacienteId = evolucaoEdicaoDto.PacienteId;
            evolucao.Exercicios = evolucaoEdicaoDto.Exercicios;
            evolucao.Atividades = evolucaoEdicaoDto.Atividades;

            _context.Update(evolucao);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Evolucoes.ToListAsync();
            resposta.Mensagem = "Evolucao Atualizado com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {

            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<EvolucaoModel>>> Listar()
    {
        ResponseModel<List<EvolucaoModel>> resposta = new ResponseModel<List<EvolucaoModel>>();

        try
        {
            var evolucao = await _context.Evolucoes.ToListAsync();

            resposta.Dados = evolucao;
            resposta.Mensagem = "Todos os Evolucao foram encontrados";
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