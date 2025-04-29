using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Dto.Status;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Status;

public class StatusService : IStatusInterface
{
    private readonly AppDbContext _context;
    public StatusService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseModel<StatusModel>> BuscarStatusPorId(int idStatus)
    {
        ResponseModel<StatusModel> resposta = new ResponseModel<StatusModel>();
        try
        {
            var status = await _context.Status.FirstOrDefaultAsync(x => x.Id == idStatus);
            if (status == null)
            {
                resposta.Mensagem = "Nenhum Status encontrado";
                return resposta;
            }

            resposta.Dados = status;
            resposta.Mensagem = "Status Encontrado";
            return resposta;
        }
        catch (Exception ex)
        {

            resposta.Mensagem = "Erro ao buscar Status";
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<StatusModel>>> CriarStatus(StatusCreateDto statusCreateDto)
    {
        ResponseModel<List<StatusModel>> resposta = new ResponseModel<List<StatusModel>>();

        try
        {
            var status = new StatusModel();
            if (statusCreateDto == null)
            {
                resposta.Mensagem = "Erro ao Criar um Status";
                return resposta;
            }

            status.Status = statusCreateDto.Status;
            status.Legenda = statusCreateDto.Legenda;
            status.Cor = statusCreateDto.Cor;

            _context.AddAsync(status);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Status.ToListAsync();
            resposta.Mensagem = "Status criado com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }

    }

    public async Task<ResponseModel<List<StatusModel>>> DeleteStatus(int idStatus)
    {
        ResponseModel<List<StatusModel>> resposta = new ResponseModel<List<StatusModel>>();

        try
        {
            resposta.Status = false; 

            var status = await _context.Status.FirstOrDefaultAsync(x => x.Id == idStatus);
            if (status == null)
            {
                resposta.Mensagem = "Nenhum Status encontrado";
                return resposta;
            }

            if (!status.IsSystemDefault)
            {
                _context.Remove(status);
                await _context.SaveChangesAsync();
                resposta.Status = true;
            }

           
            resposta.Dados = await _context.Status.ToListAsync();
            resposta.Mensagem = status.IsSystemDefault ?  "Status não pode ser excluído." : "Status Excluido com sucesso";
            return resposta;

        }
        catch (Exception ex)
        {

            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<StatusModel>>> EditarStatus(StatusEdicaoDto statusEdicaoDto)
    {
        ResponseModel<List<StatusModel>> resposta = new ResponseModel<List<StatusModel>>();

        try
        {
            var status = _context.Status.FirstOrDefault(x => x.Id == statusEdicaoDto.Id);
            if (status == null)
            {
                resposta.Mensagem = "Status não encontrado";
                return resposta;
            }

            status.Legenda = statusEdicaoDto.Legenda;
            status.Status = statusEdicaoDto.Status;
            status.Cor = statusEdicaoDto.Cor;

            _context.Update(status);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Status.ToListAsync();
            resposta.Mensagem = "Status Atualizado com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {

            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    [HttpGet("Listar")]
    public async Task<ResponseModel<List<StatusModel>>> Listar(string status, string cor,int page = 1, int pageSize = 10)
    {
        ResponseModel<List<StatusModel>> resposta = new ResponseModel<List<StatusModel>>();

        try
        {
            // Query base
            var query = _context.Status.AsQueryable();

            // Filtro por status
            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(x => x.Status.Contains(status));
            }

            // Filtro por cor
            if (!string.IsNullOrEmpty(cor))
            {
                query = query.Where(x => x.Cor.Contains(cor));
            }

            query = query.OrderBy(x => x.Id);

            // Contar total para paginação
            int totalItens = await query.CountAsync();

            // Paginação
            var statusPaginado = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Montando a resposta
            resposta.Dados = statusPaginado;
            resposta.Mensagem = "Dados filtrados com sucesso.";
            resposta.Status = true;
            resposta.TotalCount = totalItens; // Campo total opcional para paginação
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
