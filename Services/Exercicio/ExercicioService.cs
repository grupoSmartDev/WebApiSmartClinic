
using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Dto.Exercicio;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Exercicio;

public class ExercicioService : IExercicioInterface
{
    private readonly AppDbContext _context;
    public ExercicioService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseModel<ExercicioModel>> BuscarPorId(int idExercicio)
    {
        ResponseModel<ExercicioModel> resposta = new ResponseModel<ExercicioModel>();
        try
        {
            var exercicio = await _context.Exercicio.FirstOrDefaultAsync(x => x.Id == idExercicio);
            if (exercicio == null)
            {
                resposta.Mensagem = "Nenhum Exercicio encontrado";
                return resposta;
            }

            resposta.Dados = exercicio;
            resposta.Mensagem = "Exercicio Encontrado";
            return resposta;
        }
        catch (Exception ex)
        {

            resposta.Mensagem = "Erro ao buscar Exercicio";
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<ExercicioModel>>> Criar(ExercicioCreateDto exercicioCreateDto)
    {
        ResponseModel<List<ExercicioModel>> resposta = new ResponseModel<List<ExercicioModel>>();

        try
        {
            var exercicio = new ExercicioModel();
            
            exercicio.Descricao =  exercicioCreateDto.Descricao;
            exercicio.Peso = exercicioCreateDto.Peso;
            exercicio.Obs =  exercicioCreateDto.Obs;
            exercicio.Tempo = exercicioCreateDto.Tempo;
            exercicio.Repeticoes =  exercicioCreateDto.Repeticoes;
            exercicio.Series =  exercicioCreateDto.Series;
            exercicio.EvolucaoId = exercicioCreateDto.EvolucaoId;

            _context.Add(exercicio);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Exercicio.ToListAsync();
            resposta.Mensagem = "Exercicio criado com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }

    }

    public async Task<ResponseModel<List<ExercicioModel>>> Delete(int idExercicio)
    {
        ResponseModel<List<ExercicioModel>> resposta = new ResponseModel<List<ExercicioModel>>();

        try
        {
            var exercicio = await _context.Exercicio.FirstOrDefaultAsync(x => x.Id == idExercicio);
            if (exercicio == null)
            {
                resposta.Mensagem = "Nenhum Exercicio encontrado";
                return resposta;
            }

            _context.Remove(exercicio);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Exercicio.ToListAsync();
            resposta.Mensagem = "Exercicio Excluido com sucesso";
            return resposta;

        }
        catch (Exception ex)
        {

            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<ExercicioModel>>> Editar(ExercicioEdicaoDto exercicioEdicaoDto)
    {
        ResponseModel<List<ExercicioModel>> resposta = new ResponseModel<List<ExercicioModel>>();

        try
        {
            var exercicio = _context.Exercicio.FirstOrDefault(x => x.Id == exercicioEdicaoDto.Id);
            if (exercicio == null)
            {
                resposta.Mensagem = "Exercicio não encontrado";
                return resposta;
            }

            exercicio.Id = exercicioEdicaoDto.Id;
            exercicio.Descricao = exercicioEdicaoDto.Descricao;
            exercicio.Peso = exercicioEdicaoDto.Peso;
            exercicio.Obs = exercicioEdicaoDto.Obs;
            exercicio.Tempo = exercicioEdicaoDto.Tempo;
            exercicio.Repeticoes = exercicioEdicaoDto.Repeticoes;
            exercicio.Series = exercicioEdicaoDto.Series;
            exercicio.EvolucaoId = exercicioEdicaoDto.EvolucaoId;

            _context.Update(exercicio);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Exercicio.ToListAsync();
            resposta.Mensagem = "Exercicio Atualizado com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {

            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<ExercicioModel>>> Listar()
    {
        ResponseModel<List<ExercicioModel>> resposta = new ResponseModel<List<ExercicioModel>>();

        try
        {
            var exercicio = await _context.Exercicio.ToListAsync();

            resposta.Dados = exercicio;
            resposta.Mensagem = "Todos os Exercicio foram encontrados";
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