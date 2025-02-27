
using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Dto.TipoPagamento;
using WebApiSmartClinic.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebApiSmartClinic.Services.TipoPagamento;

public class TipoPagamentoService : ITipoPagamentoInterface
{
    private readonly AppDbContext _context;
    public TipoPagamentoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseModel<TipoPagamentoModel>> BuscarPorId(int idTipoPagamento)
    {
        ResponseModel<TipoPagamentoModel> resposta = new ResponseModel<TipoPagamentoModel>();
        try
        {
            var tipopagamento = await _context.TipoPagamento.FirstOrDefaultAsync(x => x.Id == idTipoPagamento);
            if (tipopagamento == null)
            {
                resposta.Mensagem = "Nenhum tipo de pagamento encontrado";
                return resposta;
            }

            resposta.Dados = tipopagamento;
            resposta.Mensagem = "Tipo de pagamento encontrado";
            return resposta;
        }
        catch (Exception ex)
        {

            resposta.Mensagem = "Erro ao buscar tipo de pagamento";
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<TipoPagamentoModel>>> Criar(TipoPagamentoCreateDto tipopagamentoCreateDto, int pageNumber = 1, int pageSize = 10, bool paginar = true)
    {
        ResponseModel<List<TipoPagamentoModel>> resposta = new ResponseModel<List<TipoPagamentoModel>>();

        try
        {
            var tipopagamento = new TipoPagamentoModel();

            // Atualizar para o código de acordo com o necessário
            tipopagamento.Descricao = tipopagamentoCreateDto.Descricao;

            _context.Add(tipopagamento);
            await _context.SaveChangesAsync();

            var query = _context.TipoPagamento.AsQueryable();

            resposta.Dados = paginar ? (await PaginationHelper.PaginateAsync(query, pageNumber, pageSize)).Dados : await query.ToListAsync();
            resposta.Mensagem = "Tipo de pagamento criado com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }

    }

    public async Task<ResponseModel<List<TipoPagamentoModel>>> Delete(int idTipoPagamento, int pageNumber = 1, int pageSize = 10, bool paginar = true)
    {
        ResponseModel<List<TipoPagamentoModel>> resposta = new ResponseModel<List<TipoPagamentoModel>>();

        try
        {
            var tipopagamento = await _context.TipoPagamento.FirstOrDefaultAsync(x => x.Id == idTipoPagamento);
            if (tipopagamento == null)
            {
                resposta.Mensagem = "Nenhum tipo de pagamento encontrado";
                return resposta;
            }

            _context.Remove(tipopagamento);
            await _context.SaveChangesAsync();

            var query = _context.TipoPagamento.AsQueryable();

            resposta.Dados = paginar ? (await PaginationHelper.PaginateAsync(query, pageNumber, pageSize)).Dados : await query.ToListAsync();
            resposta.Mensagem = "Tipo de pagamento excluído com sucesso";
            return resposta;

        }
        catch (Exception ex)
        {

            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<TipoPagamentoModel>>> Editar(TipoPagamentoEdicaoDto tipopagamentoEdicaoDto, int pageNumber = 1, int pageSize = 10, bool paginar = true)
    {
        ResponseModel<List<TipoPagamentoModel>> resposta = new ResponseModel<List<TipoPagamentoModel>>();

        try
        {
            var tipopagamento = _context.TipoPagamento.FirstOrDefault(x => x.Id == tipopagamentoEdicaoDto.Id);
            if (tipopagamento == null)
            {
                resposta.Mensagem = "Tipo de pagamento não encontrado";
                return resposta;
            }

            // Atualizar para o código de acordo com o necessário
            tipopagamento.Descricao = tipopagamentoEdicaoDto.Descricao;

            _context.Update(tipopagamento);
            await _context.SaveChangesAsync();
            var query = _context.TipoPagamento.AsQueryable();

            resposta.Dados = paginar ? (await PaginationHelper.PaginateAsync(query, pageNumber, pageSize)).Dados : await query.ToListAsync();
            resposta.Mensagem = "Tipo de pagamento atualizado com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {

            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<TipoPagamentoModel>>> Listar(int pageNumber = 1, int pageSize = 10, int? codigoFiltro = null, string? descricaoFiltro = null, bool paginar = true)
    {
        ResponseModel<List<TipoPagamentoModel>> resposta = new ResponseModel<List<TipoPagamentoModel>>();

        try
        {
            var query = _context.TipoPagamento.AsQueryable();

            if (!string.IsNullOrEmpty(descricaoFiltro))
                query = query.Where(p => p.Descricao.Contains(descricaoFiltro));

            if (!string.IsNullOrEmpty(codigoFiltro.ToString()))
                query = query.Where(p => p.Id == codigoFiltro);

            query = query.OrderBy(p => p.Id);

            resposta = paginar ? await PaginationHelper.PaginateAsync(query, pageNumber, pageSize) : new ResponseModel<List<TipoPagamentoModel>> { Dados = await query.ToListAsync() };
            resposta.Mensagem = "Todos os tipos de pagamentos foram encontrados";
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