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

    public async Task<ResponseModel<List<StatusModel>>> CriarStatus(SalaCreateDto statusCreateDto)
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
            var status = await _context.Status.FirstOrDefaultAsync(x => x.Id == idStatus);
            if (status == null)
            {
                resposta.Mensagem = "Nenhum Status encontrado";
                return resposta;
            }

            _context.Remove(status);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Status.ToListAsync();
            resposta.Mensagem = "Status Excluido com sucesso";
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

    public async Task<ResponseModel<List<StatusModel>>> ListarStatus()
    {
        ResponseModel<List<StatusModel>> resposta = new ResponseModel<List<StatusModel>>();

        try
        {
            var status = await _context.Status.ToListAsync();

            resposta.Dados = status;
            resposta.Mensagem = "Todos os Status foram encontrados";
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
