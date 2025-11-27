
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Dto.Atividade;
using WebApiSmartClinic.Dto.Evolucao;
using WebApiSmartClinic.Dto.Exercicio;
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
                ProfissionalId = evolucaoCreateDto.ProfissionalId,
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
                        Peso = (int?)exercicioDto.Peso,
                        Repeticoes = (int?)exercicioDto.Repeticoes,
                        Series = (int?)exercicioDto.Series,
                        Tempo = (int?)exercicioDto.Tempo,
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
                .Include(p => p.Paciente)
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

    public async Task<ResponseModel<EvolucaoModel>> CriarEvolucaoPaciente(EvolucaoCreateDto evolucaoCreateDto)
    {
        ResponseModel<EvolucaoModel> resposta = new ResponseModel<EvolucaoModel>(); 
        try
        {
            var evolucao = new EvolucaoModel
            {
                Observacao = evolucaoCreateDto.Observacao,
                DataEvolucao = evolucaoCreateDto.DataEvolucao,
                PacienteId = evolucaoCreateDto.PacienteId,
                ProfissionalId = evolucaoCreateDto.ProfissionalId,
                Exercicios = new List<ExercicioModel>(),
                Atividades = new List<AtividadeModel>()
            };

            // ... código dos exercícios e atividades (mantém igual) ...
            // Adicionar exercícios
            if (evolucaoCreateDto.Exercicios != null)
            {
                foreach (var exercicioDto in evolucaoCreateDto.Exercicios)
                {
                    var exercicio = new ExercicioModel
                    {
                        Obs = exercicioDto.Obs,
                        Peso = (int?)exercicioDto.Peso,
                        Repeticoes = (int?)exercicioDto.Repeticoes,
                        Series = (int?)exercicioDto.Series,
                        Tempo = (int?)exercicioDto.Tempo,
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

            
            var evolucaoCompleta = await _context.Evolucoes
                .Include(e => e.Exercicios)
                .Include(e => e.Atividades)
                .Include(p => p.Paciente)
                .FirstOrDefaultAsync(e => e.Id == evolucao.Id);

            resposta.Dados = evolucaoCompleta; // Retorna só o objeto criado
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
            evolucao.ProfissionalId = evolucaoEdicaoDto.ProfissionalId;
          
            if (evolucaoEdicaoDto.Exercicios != null)
            {
                AtualizarListaExercicios(evolucao, (List<ExercicioEdicaoDto>)evolucaoEdicaoDto.Exercicios);
            }

            
            if (evolucaoEdicaoDto.Atividades != null)
            {
                AtualizarListaAtividades(evolucao, (List<AtividadeEdicaoDto>)evolucaoEdicaoDto.Atividades);
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
            resposta.TotalCount = evolucao.Count;
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

    private void AtualizarListaExercicios(EvolucaoModel evolucao, List<ExercicioEdicaoDto> exerciciosDto)
    {
     
        var idsNoDto = exerciciosDto.Where(x => x.Id > 0).Select(x => x.Id).ToList();

        
        var exerciciosParaRemover = evolucao.Exercicios
            .Where(x => !idsNoDto.Contains(x.Id))
            .ToList();

        foreach (var ex in exerciciosParaRemover)
        {
            evolucao.Exercicios.Remove(ex);
        }

        
        foreach (var dto in exerciciosDto)
        {
           
            var exercicioExistente = evolucao.Exercicios
                .FirstOrDefault(x => x.Id == dto.Id && x.Id > 0);

            if (exercicioExistente != null)
            {
                
                exercicioExistente.Obs = dto.Obs;
                exercicioExistente.Peso = dto.Peso;
                exercicioExistente.Repeticoes = dto.Repeticoes;
                exercicioExistente.Series = dto.Series;
                exercicioExistente.Tempo = dto.Tempo;
                exercicioExistente.Descricao = dto.Descricao;
            }
            else
            {
                
                var novoExercicio = new ExercicioModel
                {
                    
                    Obs = dto.Obs,
                    Peso = dto.Peso,
                    Repeticoes = dto.Repeticoes,
                    Series = dto.Series,
                    Tempo = dto.Tempo,
                    Descricao = dto.Descricao
                };
                evolucao.Exercicios.Add(novoExercicio);
            }
        }
    }

    private void AtualizarListaAtividades(EvolucaoModel evolucao, List<AtividadeEdicaoDto> atividadesDto)
    {
        
        var idsNoDto = atividadesDto.Where(x => x.Id > 0).Select(x => x.Id).ToList();

        var paraRemover = evolucao.Atividades
            .Where(x => !idsNoDto.Contains(x.Id))
            .ToList();

        foreach (var atv in paraRemover)
            evolucao.Atividades.Remove(atv);

        foreach (var dto in atividadesDto)
        {
            var existente = evolucao.Atividades.FirstOrDefault(x => x.Id == dto.Id && x.Id > 0);

            if (existente != null)
            {
                existente.Titulo = dto.Titulo;
                existente.Descricao = dto.Descricao;
                existente.Tempo = dto.Tempo;
            }
            else
            {
                evolucao.Atividades.Add(new AtividadeModel
                {
                    Titulo = dto.Titulo,
                    Descricao = dto.Descricao,
                    Tempo = dto.Tempo
                });
            }
        }
    }


}