
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
            var evolucao = new EvolucaoModel
            {
                Observacao = evolucaoCreateDto.Observacao,
                DataEvolucao = evolucaoCreateDto.DataEvolucao,
                PacienteId = evolucaoCreateDto.PacienteId,
                Exercicios = new List<ExercicioModel>(),
                Atividades = new List<AtividadeModel>()
            };

            // Adicionar exercícios
            if (evolucaoCreateDto.Exercicios != null)
            {
                foreach (var exercicioDto in evolucaoCreateDto.Exercicios)
                {
                    var exercicio = new ExercicioModel
                    {
                        Obs = exercicioDto.Obs,
                        Peso = (int)exercicioDto.Peso,
                        Repeticoes = (int)exercicioDto.Repeticoes,
                        Series = (int)exercicioDto.Series,
                        Tempo = (int)exercicioDto.Tempo,
                        Descricao = exercicioDto.Descricao
                    };
                    evolucao.Exercicios.Add(exercicio);
                }
            }

            // Adicionar atividades
            if (evolucaoCreateDto.Atividades != null)
            {
                foreach (var atividadeDto in evolucaoCreateDto.Atividades)
                {
                    var atividade = new AtividadeModel
                    {
                        Tempo = atividadeDto.Tempo,
                        Titulo = atividadeDto.Titulo,
                        Descricao = atividadeDto.Descricao
                    };
                    evolucao.Atividades.Add(atividade);
                }
            }

            _context.Add(evolucao);
            await _context.SaveChangesAsync();

            // Retornar a lista atualizada incluindo os relacionamentos
            resposta.Dados = await _context.Evolucoes
                .Include(e => e.Exercicios)
                .Include(e => e.Atividades)
                .ToListAsync();

            resposta.Mensagem = "Evolução criada com sucesso";
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
            var evolucao = await _context.Evolucoes
                .Include(e => e.Exercicios)
                .Include(e => e.Atividades)
                .FirstOrDefaultAsync(x => x.Id == evolucaoEdicaoDto.Id);

            if (evolucao == null)
            {
                resposta.Mensagem = "Evolucao não encontrada";
                return resposta;
            }

            evolucao.Observacao = evolucaoEdicaoDto.Observacao;
            evolucao.DataEvolucao = evolucaoEdicaoDto.DataEvolucao;
            evolucao.PacienteId = evolucaoEdicaoDto.PacienteId;

            // Limpar as listas existentes
            if (evolucao.Exercicios != null)
            {
                evolucao.Exercicios.Clear();
            }
            else
            {
                evolucao.Exercicios = new List<ExercicioModel>();
            }

            if (evolucao.Atividades != null)
            {
                evolucao.Atividades.Clear();
            }
            else
            {
                evolucao.Atividades = new List<AtividadeModel>();
            }

            // Adicionar novos exercícios
            if (evolucaoEdicaoDto.Exercicios != null)
            {
                foreach (var exercicioDto in evolucaoEdicaoDto.Exercicios)
                {
                    var exercicio = new ExercicioModel
                    {
                        Obs = exercicioDto.Obs,
                        Peso = exercicioDto.Peso,
                        Repeticoes = exercicioDto.Repeticoes,
                        Series = exercicioDto.Series,
                        Tempo = exercicioDto.Tempo,
                        Descricao = exercicioDto.Descricao
                        // Adicione outras propriedades necessárias
                    };
                    evolucao.Exercicios.Add(exercicio);
                }
            }

            // Adicionar novas atividades
            if (evolucaoEdicaoDto.Atividades != null)
            {
                foreach (var atividadeDto in evolucaoEdicaoDto.Atividades)
                {
                    var atividade = new AtividadeModel
                    {
                       Tempo = atividadeDto.Tempo,
                       Titulo = atividadeDto.Titulo,
                       Descricao = atividadeDto.Descricao
                    };
                    evolucao.Atividades.Add(atividade);
                }
            }

            _context.Update(evolucao);
            await _context.SaveChangesAsync();

            // Carregar a lista atualizada incluindo os relacionamentos
            resposta.Dados = await _context.Evolucoes
                .Include(e => e.Exercicios)
                .Include(e => e.Atividades)
                .ToListAsync();
            resposta.Mensagem = "Evolucao atualizada com sucesso";
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