using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Dto.Sala;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Services.Sala;

namespace WebApiSmartClinic.Services.Sala;

public class SalaService : ISalaInterface
{
    private readonly AppDbContext _context;
    public SalaService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseModel<SalaModel>> BuscarPorId(int idSala)
    {
        ResponseModel<SalaModel> resposta = new ResponseModel<SalaModel>();
        try
        {
            var sala = await _context.Sala.FirstOrDefaultAsync(x => x.Id == idSala);
            if (sala == null)
            {
                resposta.Mensagem = "Nenhuma Sala encontrada";
                return resposta;
            }

            resposta.Dados = sala;
            resposta.Mensagem = "Sala Encontrada";
            return resposta;
        }
        catch (Exception ex)
        {

            resposta.Mensagem = "Erro ao buscar sala";
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<SalaModel>>> Criar(SalaCreateDto salaCreateDto, int pageNumber = 1, int pageSize = 10)
    {
        ResponseModel<List<SalaModel>> resposta = new ResponseModel<List<SalaModel>>();

        try
        {
            var sala = new SalaModel();
            if (salaCreateDto == null)
            {
                resposta.Mensagem = "Erro ao Criar um Status";
                return resposta;
            }

            sala.local = salaCreateDto.local;
            sala.Capacidade = salaCreateDto.Capacidade;
            sala.Observacao = salaCreateDto.Observacao;
            sala.Status = salaCreateDto.Status;
            sala.Tipo = salaCreateDto.Tipo;
            sala.HorarioFincionamento = salaCreateDto.HorarioFincionamento;
            sala.Nome = salaCreateDto.Nome;

            _context.AddAsync(sala);
            await _context.SaveChangesAsync();

            var query = _context.Sala.AsQueryable();

            resposta.Dados = (await PaginationHelper.PaginateAsync(query, pageNumber, pageSize)).Dados;
            resposta.Mensagem = "Sala criado com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }

    }

    public async Task<ResponseModel<List<SalaModel>>> Delete(int idSala, int pageNumber = 1, int pageSize = 10)
    {
        ResponseModel<List<SalaModel>> resposta = new ResponseModel<List<SalaModel>>();

        try
        {
            var sala = await _context.Sala.FirstOrDefaultAsync(x => x.Id == idSala);
            if (sala == null)
            {
                resposta.Mensagem = "Nenhum Sala encontrado";
                return resposta;
            }

            _context.Remove(sala);
            await _context.SaveChangesAsync();

            var query = _context.Sala.AsQueryable();

            resposta.Dados = (await PaginationHelper.PaginateAsync(query, pageNumber, pageSize)).Dados;
            resposta.Mensagem = "Sala Excluido com sucesso";
            return resposta;

        }
        catch (Exception ex)
        {

            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<SalaModel>>> Editar(SalaEdicaoDto salaEdicaoDto, int pageNumber = 1, int pageSize = 10)
    {
        ResponseModel<List<SalaModel>> resposta = new ResponseModel<List<SalaModel>>();

        try
        {
            var sala = _context.Sala.FirstOrDefault(x => x.Id == salaEdicaoDto.Id);
            if (sala == null)
            {
                resposta.Mensagem = "Sala não encontrado";
                return resposta;
            }

            sala.local = salaEdicaoDto.local;
            sala.Capacidade = salaEdicaoDto.Capacidade;
            sala.Observacao = salaEdicaoDto.Observacao;
            sala.Status = salaEdicaoDto.Status;
            sala.Tipo = salaEdicaoDto.Tipo;
            sala.HorarioFincionamento = salaEdicaoDto.HorarioFincionamento;
            sala.Nome = salaEdicaoDto.Nome;

            _context.Update(sala);
            await _context.SaveChangesAsync();

            var query = _context.Sala.AsQueryable();
            
            resposta.Dados = (await PaginationHelper.PaginateAsync(query, pageNumber, pageSize)).Dados;
            resposta.Mensagem = "Sala Atualizado com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {

            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<SalaModel>>> Listar(int pageNumber = 1, int pageSize = 10, int? codigoFiltro = null, string? nomeFiltro = null, string? localFiltro = null, int? capacidadeFiltro = null, bool paginar = true)
    {
        ResponseModel<List<SalaModel>> resposta = new ResponseModel<List<SalaModel>>();

        try
        {
            var query = _context.Sala.AsQueryable();
            query = query.Where(x => 
                (!codigoFiltro.HasValue || x.Id == codigoFiltro) &&
                (!capacidadeFiltro.HasValue || x.Capacidade == capacidadeFiltro) &&
                (string.IsNullOrEmpty(nomeFiltro) || x.Nome == nomeFiltro) &&            
                (string.IsNullOrEmpty(localFiltro) || x.local == localFiltro)
            );

            query = query.OrderBy(x => x.Id);

            resposta.Dados = paginar ? (await PaginationHelper.PaginateAsync(query, pageNumber, pageSize)).Dados : await query.ToListAsync();
            resposta.Mensagem = "Todos os Sala foram encontrados";
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
