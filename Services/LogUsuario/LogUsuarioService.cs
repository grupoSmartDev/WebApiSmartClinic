
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

    public async Task<ResponseModel<List<LogUsuarioModel>>> Listar(int pageNumber = 1, int pageSize = 10, int? codigoFiltro = null, string? descricaoFiltro = null, string? rotinaFiltro = null, int? idMovimentacaoFiltro = null,
        string? usuarioFiltro = null, DateTime? dataMovimentacaoFiltro = null, bool paginar = true)
    {
        ResponseModel<List<LogUsuarioModel>> resposta = new ResponseModel<List<LogUsuarioModel>>();

        try
        {
            var query = _context.LogUsuario
                .Include(l => l.Usuario) // Inclui relacionamento com o usuário
                .AsQueryable();

            // Aplicação dos filtros
            query = query.Where(l =>
                (!codigoFiltro.HasValue || l.Id == codigoFiltro) &&
                (!string.IsNullOrEmpty(descricaoFiltro) || l.Descricao.Contains(descricaoFiltro)) &&
                (!string.IsNullOrEmpty(rotinaFiltro) || l.Rotina.Contains(rotinaFiltro)) &&
                (!idMovimentacaoFiltro.HasValue || l.IdMovimentacao == idMovimentacaoFiltro) &&
                (!string.IsNullOrEmpty(usuarioFiltro) || l.Usuario.Nome.Contains(usuarioFiltro)) &&
                (!dataMovimentacaoFiltro.HasValue || l.DataMovimentacao.Value.Date == dataMovimentacaoFiltro.Value.Date)
            );

            // Ordenação padrão por DataMovimentacao (ou por ID caso seja necessário)
            query = query.OrderByDescending(l => l.DataMovimentacao);

            // Paginação
            resposta.Dados = paginar ? (await PaginationHelper.PaginateAsync(query, pageNumber, pageSize)).Dados : await query.ToListAsync();
            resposta.Mensagem = "Registros de log encontrados com sucesso";
        }
        catch (Exception e)
        {
            resposta.Mensagem = e.Message;
            resposta.Status = false;
        }

        return resposta;
    }

}