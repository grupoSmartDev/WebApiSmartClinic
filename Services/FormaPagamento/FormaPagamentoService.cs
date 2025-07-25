
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Dto.FormaPagamento;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.FormaPagamento;

public class FormaPagamentoService : IFormaPagamentoInterface
{
    private readonly AppDbContext _context;
    public FormaPagamentoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseModel<FormaPagamentoModel>> BuscarPorId(int idFormaPagamento)
    {
        ResponseModel<FormaPagamentoModel> resposta = new ResponseModel<FormaPagamentoModel>();

        try
        {
            var formapagamento = await _context.FormaPagamento.FirstOrDefaultAsync(x => x.Id == idFormaPagamento);
            if (formapagamento == null)
            {
                resposta.Mensagem = "Nenhuma forma de pagamento encontrada";
                return resposta;
            }

            resposta.Dados = formapagamento;
            resposta.Mensagem = "Forma de pagamento encontrada";

            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = "Erro ao buscar forma de pagamento";
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<FormaPagamentoModel>>> Criar(FormaPagamentoCreateDto formapagamentoCreateDto, int pageNumber = 1, int pageSize = 10, bool paginar = true)
    {
        ResponseModel<List<FormaPagamentoModel>> resposta = new ResponseModel<List<FormaPagamentoModel>>();

        try
        {
            var formapagamento = new FormaPagamentoModel();

            formapagamento.Descricao = formapagamentoCreateDto.Descricao;
            formapagamento.Parcelas = formapagamentoCreateDto.Parcelas;

            _context.Add(formapagamento);
            await _context.SaveChangesAsync();

            var query = _context.FormaPagamento.AsQueryable();

            resposta = await PaginationHelper.PaginateAsync(query, pageNumber, pageSize);
            resposta.Mensagem = "Forma de pagamento criada com sucesso";

            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<FormaPagamentoModel>>> Delete(int idFormaPagamento, int pageNumber = 1, int pageSize = 10, bool paginar = true)
    {
        ResponseModel<List<FormaPagamentoModel>> resposta = new ResponseModel<List<FormaPagamentoModel>>();

        try
        {
            var formapagamento = await _context.FormaPagamento.FirstOrDefaultAsync(x => x.Id == idFormaPagamento);
            if (formapagamento == null)
            {
                resposta.Mensagem = "Nenhuma forma de pagamento encontrada";
                return resposta;
            }

            _context.Remove(formapagamento);
            await _context.SaveChangesAsync();

            var query = _context.FormaPagamento.AsQueryable();

            resposta = await PaginationHelper.PaginateAsync(query, pageNumber, pageSize);
            resposta.Mensagem = "Forma de pagamento excluida com sucesso";

            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<FormaPagamentoModel>>> Editar(FormaPagamentoEdicaoDto formapagamentoEdicaoDto, int pageNumber = 1, int pageSize = 10, bool paginar = true)
    {
        ResponseModel<List<FormaPagamentoModel>> resposta = new ResponseModel<List<FormaPagamentoModel>>();

        try
        {
            var formapagamento = _context.FormaPagamento.FirstOrDefault(x => x.Id == formapagamentoEdicaoDto.Id);
            if (formapagamento == null)
            {
                resposta.Mensagem = "Forma de pagamento não encontrada";
                return resposta;
            }

            formapagamento.Id = formapagamentoEdicaoDto.Id;
            formapagamento.Descricao = formapagamentoEdicaoDto.Descricao;
            formapagamento.Parcelas = formapagamentoEdicaoDto.Parcelas;

            _context.Update(formapagamento);
            await _context.SaveChangesAsync();

            var query = _context.FormaPagamento.AsQueryable();

            resposta = await PaginationHelper.PaginateAsync(query, pageNumber, pageSize);
            resposta.Mensagem = "Forma de pagamento atualizada com sucesso";

            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<FormaPagamentoModel>>> Listar(int pageNumber = 1, int pageSize = 10, int? idFiltro = null, int? parcelasFiltro = null, string? descricaoFiltro = null, bool paginar = true)
    {
        ResponseModel<List<FormaPagamentoModel>> resposta = new ResponseModel<List<FormaPagamentoModel>>();

        try
        {
            var query = _context.FormaPagamento.AsQueryable();

            //query = query.Where(p =>
            //   (!codigoFiltro.HasValue || p.Id == codigoFiltro) &&
            //   (!parcelasFiltro.HasValue || p.Parcelas == parcelasFiltro) &&
            //   (!string.IsNullOrEmpty(descricaoFiltro) || p.Descricao == descricaoFiltro)
            //);
            if (!string.IsNullOrEmpty(descricaoFiltro))
                query = query.Where(p => p.Descricao.Contains(descricaoFiltro));

            if (idFiltro.HasValue)
                query = query.Where(p => p.Id == idFiltro);

            query = query.OrderBy(p => p.Id);

            resposta = paginar ? await PaginationHelper.PaginateAsync(query, pageNumber, pageSize) : new ResponseModel<List<FormaPagamentoModel>> { Dados = await query.ToListAsync() };
            resposta.Mensagem = "Todas as formas de pagamentos foram encontradas";

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