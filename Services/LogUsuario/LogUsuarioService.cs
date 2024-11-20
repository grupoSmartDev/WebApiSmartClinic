
using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.LogUsuario;

public class LogUsuarioService : ILogUsuarioInterface
{
    private readonly AppDbContext _context;
    public LogUsuarioService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseModel<LogUsuarioModel>> BuscarPorId(int idLogUsuario)
    {
        ResponseModel<LogUsuarioModel> resposta = new ResponseModel<LogUsuarioModel>();
        try
        {
            var logusuario = await _context.LogUsuario.FirstOrDefaultAsync(x => x.Id == idLogUsuario);
            if (logusuario == null)
            {
                resposta.Mensagem = "Nenhum registro encontrado";
                
                return resposta;
            }

            resposta.Dados = logusuario;
            resposta.Mensagem = "Registro encontrado";
            
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            
            return resposta;
        }
    }

    public async Task<ResponseModel<List<LogUsuarioModel>>> Inserir(int id, string descricao, string rotina, int usuarioId)
    {
        ResponseModel<List<LogUsuarioModel>> resposta = new ResponseModel<List<LogUsuarioModel>>();

        try
        {
            var logusuario = new LogUsuarioModel();

            logusuario.IdMovimentacao = id;
            logusuario.Descricao = descricao;
            logusuario.Rotina = rotina;
            logusuario.UsuarioId = usuarioId;
            logusuario.DataMovimentacao = DateTime.Now;

            _context.Add(logusuario);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.LogUsuario.ToListAsync();
            resposta.Mensagem = "Log inserido com sucesso";
           
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<LogUsuarioModel>>> Listar()
    {
        ResponseModel<List<LogUsuarioModel>> resposta = new ResponseModel<List<LogUsuarioModel>>();

        try
        {
            var logusuario = await _context.LogUsuario.ToListAsync();

            resposta.Dados = logusuario;
            resposta.Mensagem = "Todos os registros foram encontrados";
            
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