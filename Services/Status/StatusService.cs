using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Status;

public class StatusService : IStatusInterface
{
    private readonly AppDbContext _context;
    public StatusService(AppDbContext context)
    {
        _context = context;
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
