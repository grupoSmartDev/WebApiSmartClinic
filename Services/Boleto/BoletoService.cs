
using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Dto.Boleto;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Boleto;

public class BoletoService : IBoletoInterface
{
    private readonly AppDbContext _context;
    public BoletoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseModel<BoletoModel>> BuscarPorId(int idBoleto)
    {
        ResponseModel<BoletoModel> resposta = new ResponseModel<BoletoModel>();
        try
        {
            var boleto = await _context.Boleto.FirstOrDefaultAsync(x => x.Id == idBoleto);
            if (boleto == null)
            {
                resposta.Mensagem = "Nenhum Boleto encontrado";
                return resposta;
            }

            resposta.Dados = boleto;
            resposta.Mensagem = "Boleto Encontrado";
            return resposta;
        }
        catch (Exception ex)
        {

            resposta.Mensagem = "Erro ao buscar Boleto";
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<BoletoModel>>> Criar(BoletoCreateDto boletoCreateDto)
    {
        ResponseModel<List<BoletoModel>> resposta = new ResponseModel<List<BoletoModel>>();

        try
        {
            var boleto = new BoletoModel();
            
            boleto.NossoNumero = boletoCreateDto.NossoNumero;
            boleto.NumeroDocumento = boletoCreateDto.NumeroDocumento;
            boleto.Valor = boletoCreateDto.Valor;
            boleto.DataVencimento = boletoCreateDto.DataVencimento;
            boleto.Juros = boletoCreateDto.Juros;
            boleto.Multa = boletoCreateDto.Multa;
            boleto.NomeSacado = boletoCreateDto.NomeSacado;
            boleto.DocumentoSacado = boletoCreateDto.DocumentoSacado;
            boleto.BancoId = boletoCreateDto.BancoId;
            boleto.Pago = boletoCreateDto.Pago;
            boleto.DataPagamento = boletoCreateDto.DataPagamento;
            boleto.CodigoDeBarras = boletoCreateDto.CodigoDeBarras;
            boleto.LinhaDigitavel = boletoCreateDto.LinhaDigitavel;

            _context.Add(boleto);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Boleto.ToListAsync();
            resposta.Mensagem = "Boleto criado com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }

    }

    public async Task<ResponseModel<List<BoletoModel>>> Delete(int idBoleto)
    {
        ResponseModel<List<BoletoModel>> resposta = new ResponseModel<List<BoletoModel>>();

        try
        {
            var boleto = await _context.Boleto.FirstOrDefaultAsync(x => x.Id == idBoleto);
            if (boleto == null)
            {
                resposta.Mensagem = "Nenhum Boleto encontrado";
                return resposta;
            }

            _context.Remove(boleto);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Boleto.ToListAsync();
            resposta.Mensagem = "Boleto Excluido com sucesso";
            return resposta;

        }
        catch (Exception ex)
        {

            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<BoletoModel>>> Editar(BoletoEdicaoDto boletoEdicaoDto)
    {
        ResponseModel<List<BoletoModel>> resposta = new ResponseModel<List<BoletoModel>>();

        try
        {
            var boleto = _context.Boleto.FirstOrDefault(x => x.Id == boletoEdicaoDto.Id);
            if (boleto == null)
            {
                resposta.Mensagem = "Boleto não encontrado";
                return resposta;
            }

            boleto.Id = boletoEdicaoDto.Id;
            boleto.NossoNumero = boletoEdicaoDto.NossoNumero;
            boleto.NumeroDocumento = boletoEdicaoDto.NumeroDocumento;
            boleto.Valor = boletoEdicaoDto.Valor;
            boleto.DataVencimento = boletoEdicaoDto.DataVencimento;
            boleto.Juros = boletoEdicaoDto.Juros;
            boleto.Multa = boletoEdicaoDto.Multa;
            boleto.NomeSacado = boletoEdicaoDto.NomeSacado;
            boleto.DocumentoSacado = boletoEdicaoDto.DocumentoSacado;
            boleto.BancoId = boletoEdicaoDto.BancoId;
            boleto.Pago = boletoEdicaoDto.Pago;
            boleto.DataPagamento = boletoEdicaoDto.DataPagamento;
            boleto.CodigoDeBarras = boletoEdicaoDto.CodigoDeBarras;
            boleto.LinhaDigitavel = boletoEdicaoDto.LinhaDigitavel;

            _context.Update(boleto);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Boleto.ToListAsync();
            resposta.Mensagem = "Boleto Atualizado com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {

            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<BoletoModel>>> Listar()
    {
        ResponseModel<List<BoletoModel>> resposta = new ResponseModel<List<BoletoModel>>();

        try
        {
            var boleto = await _context.Boleto.ToListAsync();

            resposta.Dados = boleto;
            resposta.Mensagem = "Todos os Boleto foram encontrados";
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