
using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Dto.HistoricoTransacao;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.HistoricoTransacao;

public class HistoricoTransacaoService : IHistoricoTransacaoInterface
{
    private readonly AppDbContext _context;
    public HistoricoTransacaoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseModel<HistoricoTransacaoModel>> BuscarPorId(int idHistoricoTransacao)
    {
        ResponseModel<HistoricoTransacaoModel> resposta = new ResponseModel<HistoricoTransacaoModel>();
        try
        {
            var históricotransacao = await _context.HistoricoTransacao.FirstOrDefaultAsync(x => x.Id == idHistoricoTransacao);
            if (históricotransacao == null)
            {
                resposta.Mensagem = "Nenhum histórico de transação encontrado";
                return resposta;
            }

            resposta.Dados = históricotransacao;
            resposta.Mensagem = "Histórico de transação encontrado";
            return resposta;
        }
        catch (Exception ex)
        {

            resposta.Mensagem = "Erro ao buscar histórico de transação";
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<HistoricoTransacaoModel>>> Criar(HistoricoTransacaoCreateDto históricotransacaoCreateDto, int pageNumber = 1, int pageSize = 10, bool paginar = true)
    {
        ResponseModel<List<HistoricoTransacaoModel>> resposta = new ResponseModel<List<HistoricoTransacaoModel>>();

        try
        {
            var históricotransacao = new HistoricoTransacaoModel();

            _context.Add(históricotransacao);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.HistoricoTransacao.ToListAsync();
            resposta.Mensagem = "Histórico de transação criado com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }

    }

    public async Task<ResponseModel<List<HistoricoTransacaoModel>>> Delete(int idHistoricoTransacao, int pageNumber = 1, int pageSize = 10, bool paginar = true)
    {
        ResponseModel<List<HistoricoTransacaoModel>> resposta = new ResponseModel<List<HistoricoTransacaoModel>>();

        try
        {
            var históricotransacao = await _context.HistoricoTransacao.FirstOrDefaultAsync(x => x.Id == idHistoricoTransacao);
            if (históricotransacao == null)
            {
                resposta.Mensagem = "Nenhum histórico de transação encontrado";
                return resposta;
            }

            _context.Remove(históricotransacao);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.HistoricoTransacao.ToListAsync();
            resposta.Mensagem = "Histórico de transação excluido com sucesso";
            return resposta;

        }
        catch (Exception ex)
        {

            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<HistoricoTransacaoModel>>> Editar(HistoricoTransacaoEdicaoDto históricotransacaoEdicaoDto, int pageNumber = 1, int pageSize = 10, bool paginar = true)
    {
        ResponseModel<List<HistoricoTransacaoModel>> resposta = new ResponseModel<List<HistoricoTransacaoModel>>();

        try
        {
            var históricotransacao = _context.HistoricoTransacao.FirstOrDefault(x => x.Id == históricotransacaoEdicaoDto.Id);
            if (históricotransacao == null)
            {
                resposta.Mensagem = "Histórico de transação não encontrado";
                return resposta;
            }

            _context.Update(históricotransacao);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.HistoricoTransacao.ToListAsync();
            resposta.Mensagem = "Histórico de transação atualizado com sucesso";
            
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            
            return resposta;
        }
    }

    public async Task<ResponseModel<List<HistoricoTransacaoModel>>> Listar(int pageNumber = 1, int pageSize = 10, int? codigoFiltro = null, string? bancoFiltro = null, DateTime? dataTransacaoFiltro = null, string? tipoTransacaoFiltro = null,
        string? descricaoFiltro = null, string? referenciaFiltro = null, string? usuarioFiltro = null, bool paginar = true)
    {
        ResponseModel<List<HistoricoTransacaoModel>> resposta = new ResponseModel<List<HistoricoTransacaoModel>>();

        try
        {
            var query = _context.HistoricoTransacao
                .Include(h => h.Banco) // Inclui o relacionamento com o BancoModel
                .Include(h => h.Usuario) // Inclui o relacionamento com UsuarioModel (se necessário)
                .AsQueryable();

            // Aplicação dos filtros
            query = query.Where(h =>
                (!codigoFiltro.HasValue || h.Id == codigoFiltro) &&
                (!string.IsNullOrEmpty(bancoFiltro) || h.Banco.NomeBanco.Contains(bancoFiltro)) &&
                (!dataTransacaoFiltro.HasValue || h.DataTransacao.Value.Date == dataTransacaoFiltro.Value.Date) &&
                (!string.IsNullOrEmpty(tipoTransacaoFiltro) || h.TipoTransacao == tipoTransacaoFiltro) &&
                (!string.IsNullOrEmpty(descricaoFiltro) || h.Descricao.Contains(descricaoFiltro)) &&
                (!string.IsNullOrEmpty(referenciaFiltro) || h.Referencia.Contains(referenciaFiltro)) &&
                (!string.IsNullOrEmpty(usuarioFiltro) || h.Usuario.Nome.Contains(usuarioFiltro))
            );

            // Ordenação padrão por DataTransacao (ou por ID caso seja necessário)
            query = query.OrderByDescending(h => h.DataTransacao);

            // Paginação
            resposta = paginar ? await PaginationHelper.PaginateAsync(query, pageNumber, pageSize) : new ResponseModel<List<HistoricoTransacaoModel>> { Dados = await query.ToListAsync() };
            resposta.Mensagem = "Histórico de transações encontrado com sucesso";
        }
        catch (Exception e)
        {
            resposta.Mensagem = e.Message;
            resposta.Status = false;
        }

        return resposta;
    }
}