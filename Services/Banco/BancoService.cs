
using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Dto.Banco;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Banco;

public class BancoService : IBancoInterface
{
    private readonly AppDbContext _context;
    public BancoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseModel<BancoModel>> BuscarPorId(int idBanco)
    {
        ResponseModel<BancoModel> resposta = new ResponseModel<BancoModel>();
        try
        {
            var banco = await _context.Banco.FirstOrDefaultAsync(x => x.Id == idBanco);
            if (banco == null)
            {
                resposta.Mensagem = "Nenhum Banco encontrado";
                return resposta;
            }

            resposta.Dados = banco;
            resposta.Mensagem = "Banco Encontrado";
            return resposta;
        }
        catch (Exception ex)
        {

            resposta.Mensagem = "Erro ao buscar Banco";
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<BancoModel>>> Criar(BancoCreateDto bancoCreateDto, int pageNumber = 1, int pageSize = 10)
    {
        ResponseModel<List<BancoModel>> resposta = new ResponseModel<List<BancoModel>>();

        try
        {
            var banco = new BancoModel();

            banco.NomeBanco = bancoCreateDto.NomeBanco;
            banco.Codigo = bancoCreateDto.Codigo;
            banco.Agencia = bancoCreateDto.Agencia;
            banco.NumeroConta = bancoCreateDto.NumeroConta;
            banco.TipoConta = bancoCreateDto.TipoConta;
            banco.NomeTitular = bancoCreateDto.NomeTitular;
            banco.DocumentoTitular = bancoCreateDto.DocumentoTitular;
            banco.SaldoInicial = bancoCreateDto.SaldoInicial;
            banco.Ativo = bancoCreateDto.Ativo;
            banco.CodigoConvenio = bancoCreateDto.CodigoConvenio;
            banco.Carteira = bancoCreateDto.Carteira;
            banco.VariacaoCarteira = bancoCreateDto.VariacaoCarteira;
            banco.CodigoBeneficiario = bancoCreateDto.CodigoBeneficiario;
            banco.NumeroContrato = bancoCreateDto.NumeroContrato;
            banco.CodigoTransmissao = bancoCreateDto.CodigoTransmissao;

            _context.Add(banco);
            await _context.SaveChangesAsync();

            var query = _context.Banco.AsQueryable();

            resposta = await PaginationHelper.PaginateAsync(query, pageNumber, pageSize);
            resposta.Mensagem = "Banco criado com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }

    }

    public async Task<ResponseModel<List<BancoModel>>> Delete(int idBanco, int pageNumber = 1, int pageSize = 10)
    {
        ResponseModel<List<BancoModel>> resposta = new ResponseModel<List<BancoModel>>();

        try
        {
            var banco = await _context.Banco.FirstOrDefaultAsync(x => x.Id == idBanco);
            if (banco == null)
            {
                resposta.Mensagem = "Nenhum Banco encontrado";
                return resposta;
            }

            _context.Remove(banco);
            await _context.SaveChangesAsync();

            var query = _context.Banco.AsQueryable();

            resposta = await PaginationHelper.PaginateAsync(query, pageNumber, pageSize);
            resposta.Mensagem = "Banco Excluido com sucesso";
            return resposta;

        }
        catch (Exception ex)
        {

            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<BancoModel>>> Editar(BancoEdicaoDto bancoEdicaoDto, int pageNumber = 1, int pageSize = 10)
    {
        ResponseModel<List<BancoModel>> resposta = new ResponseModel<List<BancoModel>>();

        try
        {
            var banco = _context.Banco.FirstOrDefault(x => x.Id == bancoEdicaoDto.Id);
            if (banco == null)
            {
                resposta.Mensagem = "Banco não encontrado";
                return resposta;
            }

            banco.Id = bancoEdicaoDto.Id;
            banco.NomeBanco = bancoEdicaoDto.NomeBanco;
            banco.Codigo = bancoEdicaoDto.Codigo;
            banco.Agencia = bancoEdicaoDto.Agencia;
            banco.NumeroConta = bancoEdicaoDto.NumeroConta;
            banco.TipoConta = bancoEdicaoDto.TipoConta;
            banco.NomeTitular = bancoEdicaoDto.NomeTitular;
            banco.DocumentoTitular = bancoEdicaoDto.DocumentoTitular;
            banco.SaldoInicial = bancoEdicaoDto.SaldoInicial;
            banco.Ativo = bancoEdicaoDto.Ativo;
            banco.CodigoConvenio = bancoEdicaoDto.CodigoConvenio;
            banco.Carteira = bancoEdicaoDto.Carteira;
            banco.VariacaoCarteira = bancoEdicaoDto.VariacaoCarteira;
            banco.CodigoBeneficiario = bancoEdicaoDto.CodigoBeneficiario;
            banco.NumeroContrato = bancoEdicaoDto.NumeroContrato;
            banco.CodigoTransmissao = bancoEdicaoDto.CodigoTransmissao;

            _context.Update(banco);
            await _context.SaveChangesAsync();

            var query = _context.Banco.AsQueryable();

            resposta = await PaginationHelper.PaginateAsync(query, pageNumber, pageSize);
            resposta.Mensagem = "Banco Atualizado com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {

            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<BancoModel>>> Listar(int pageNumber = 1, int pageSize = 10, int? codigoFiltro = null, string? nomeBancoFiltro = null, string? nomeTitularFiltro = null, string? documentoTitularFiltro = null, bool paginar = true)
    {
        ResponseModel<List<BancoModel>> resposta = new ResponseModel<List<BancoModel>>();

        try
        {
            var query = _context.Banco.AsQueryable();

            query = query.Where(x =>
                (!codigoFiltro.HasValue || x.Id == codigoFiltro) &&
                (string.IsNullOrEmpty(nomeBancoFiltro) || x.NomeBanco == nomeBancoFiltro) &&
                (string.IsNullOrEmpty(nomeTitularFiltro) || x.NomeTitular == nomeTitularFiltro) &&
                (string.IsNullOrEmpty(documentoTitularFiltro) || x.DocumentoTitular == documentoTitularFiltro)
            );

            query.OrderBy(x => x.Id);

            resposta = paginar ? await PaginationHelper.PaginateAsync(query, pageNumber, pageSize) : new ResponseModel<List<BancoModel>> { Dados = await query.ToListAsync() };
            resposta.Mensagem = "Todos os Banco foram encontrados";
            
            return resposta;
        }
        catch (Exception e)
        {
            resposta.Mensagem = e.Message;
            resposta.Status = false;
            
            return resposta;
        }
    }

    public async Task<ResponseModel<BancoModel>> DebitarSaldo(int idBanco, decimal valor)
    {
        var resposta = new ResponseModel<BancoModel>();

        var banco = await _context.Banco.FirstOrDefaultAsync(b => b.Id == idBanco);
        if (banco == null || !banco.Ativo)
        {
            resposta.Mensagem = "Banco não encontrado ou inativo.";
            return resposta;
        }

        if (banco.SaldoInicial < valor)
        {
            resposta.Mensagem = "Saldo insuficiente.";
            resposta.Status = false;
            return resposta;
        }

        banco.SaldoInicial -= valor;
        _context.Update(banco);

        // Registra o histórico da transação de débito
        await RegistrarHistoricoTransacao(idBanco, valor, "Débito");

        await _context.SaveChangesAsync();

        resposta.Dados = banco;
        resposta.Mensagem = "Saldo debitado com sucesso.";
        return resposta;
    }

    public async Task<ResponseModel<BancoModel>> CreditarSaldo(int idBanco, decimal valor)
    {
        var resposta = new ResponseModel<BancoModel>();

        var banco = await _context.Banco.FirstOrDefaultAsync(b => b.Id == idBanco);
        if (banco == null || !banco.Ativo)
        {
            resposta.Mensagem = "Banco não encontrado ou inativo.";
            return resposta;
        }

        
        banco.SaldoInicial += valor;
        _context.Update(banco);

        // Registra o histórico da transação de crédito
        await RegistrarHistoricoTransacao(idBanco, valor, "Crédito");

        await _context.SaveChangesAsync();

        resposta.Dados = banco;
        resposta.Mensagem = "Saldo creditado com sucesso.";
        return resposta;
    }

    public async Task RegistrarHistoricoTransacao(int idBanco, decimal valor, string tipoTransacao)
    {
        var transacao = new HistoricoTransacaoModel
        {
            BancoId = idBanco,
            Valor = valor,
            TipoTransacao = tipoTransacao,
            DataTransacao = DateTime.Now
        };

        _context.HistoricoTransacao.Add(transacao);
        await _context.SaveChangesAsync();
    }

    public async Task<ResponseModel<List<HistoricoTransacaoModel>>> ObterHistoricoTransacoes(int bancoId)
    {
        var resposta = new ResponseModel<List<HistoricoTransacaoModel>>();

        try
        {
            var transacoes = await _context.HistoricoTransacao
                .Where(t => t.BancoId == bancoId)
                .OrderByDescending(t => t.DataTransacao)
                .ToListAsync();

            if (!transacoes.Any())
            {
                resposta.Mensagem = "Nenhuma transação encontrada para o banco especificado.";
                resposta.Status = false;
                return resposta;
            }

            resposta.Dados = transacoes;
            resposta.Mensagem = "Histórico de transações encontrado com sucesso.";
            resposta.Status = true;
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = $"Erro ao obter histórico de transações: {ex.Message}";
            resposta.Status = false;
            return resposta;
        }
    }

    internal Task DebitarSaldo(int? bancoId, decimal? valorPago)
    {
        throw new NotImplementedException();
    }
}