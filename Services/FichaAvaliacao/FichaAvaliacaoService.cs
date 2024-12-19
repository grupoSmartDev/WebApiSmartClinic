
using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Dto.FichaAvaliacao;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.FichaAvaliacao;

public class FichaAvaliacaoService : IFichaAvaliacaoInterface
{
    private readonly AppDbContext _context;
    public FichaAvaliacaoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseModel<FichaAvaliacaoModel>> BuscarPorId(int idFichaAvaliacao)
    {
        ResponseModel<FichaAvaliacaoModel> resposta = new ResponseModel<FichaAvaliacaoModel>();
        try
        {
            var fichaavaliacao = await _context.FichaAvaliacao.FirstOrDefaultAsync(x => x.Id == idFichaAvaliacao);
            if (fichaavaliacao == null)
            {
                resposta.Mensagem = "Nenhum FichaAvaliacao encontrado";
                return resposta;
            }

            resposta.Dados = fichaavaliacao;
            resposta.Mensagem = "FichaAvaliacao Encontrado";
            
            return resposta;
        }
        catch (Exception ex)
        {

            resposta.Mensagem = "Erro ao buscar FichaAvaliacao";
            resposta.Status = false;
            
            return resposta;
        }
    }

    public async Task<ResponseModel<List<FichaAvaliacaoModel>>> Criar(FichaAvaliacaoCreateDto fichaavaliacaoCreateDto)
    {
        ResponseModel<List<FichaAvaliacaoModel>> resposta = new ResponseModel<List<FichaAvaliacaoModel>>();

        try
        {
            var fichaavaliacao = new FichaAvaliacaoModel();

            fichaavaliacao.QueixaPrincipal = fichaavaliacaoCreateDto.QueixaPrincipal;
            fichaavaliacao.HistoriaPregressa = fichaavaliacaoCreateDto.HistoriaPregressa;
            fichaavaliacao.HistoriaAtual = fichaavaliacaoCreateDto.HistoriaAtual;
            fichaavaliacao.TipoDor = fichaavaliacaoCreateDto.TipoDor;
            fichaavaliacao.SinaisVitais = fichaavaliacaoCreateDto.SinaisVitais;
            fichaavaliacao.Medicamentos = fichaavaliacaoCreateDto.Medicamentos;
            fichaavaliacao.DoencasCronicas = fichaavaliacaoCreateDto.DoencasCronicas;
            fichaavaliacao.Cirurgia = fichaavaliacaoCreateDto.Cirurgia;
            fichaavaliacao.DoencaNeurodegenerativa = fichaavaliacaoCreateDto.DoencaNeurodegenerativa;
            fichaavaliacao.TratamentosRealizados = fichaavaliacaoCreateDto.TratamentosRealizados;
            fichaavaliacao.AlergiaMedicamentos = fichaavaliacaoCreateDto.AlergiaMedicamentos;
            fichaavaliacao.Tabagista = fichaavaliacaoCreateDto.Tabagista;
            fichaavaliacao.FrequenciaConsumoAlcool = fichaavaliacaoCreateDto.FrequenciaConsumoAlcool;
            fichaavaliacao.PraticaAtividade = fichaavaliacaoCreateDto.PraticaAtividade;
            fichaavaliacao.ObjetivoTratamento = fichaavaliacaoCreateDto.ObjetivoTratamento;

            _context.Add(fichaavaliacao);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.FichaAvaliacao.ToListAsync();
            resposta.Mensagem = "FichaAvaliacao criado com sucesso";
            
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            
            return resposta;
        }
    }

    public async Task<ResponseModel<List<FichaAvaliacaoModel>>> Delete(int idFichaAvaliacao)
    {
        ResponseModel<List<FichaAvaliacaoModel>> resposta = new ResponseModel<List<FichaAvaliacaoModel>>();

        try
        {
            var fichaavaliacao = await _context.FichaAvaliacao.FirstOrDefaultAsync(x => x.Id == idFichaAvaliacao);
            if (fichaavaliacao == null)
            {
                resposta.Mensagem = "Nenhum FichaAvaliacao encontrado";
                return resposta;
            }

            _context.Remove(fichaavaliacao);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.FichaAvaliacao.ToListAsync();
            resposta.Mensagem = "FichaAvaliacao Excluido com sucesso";
         
            return resposta;
        }
        catch (Exception ex)
        {

            resposta.Mensagem = ex.Message;
            resposta.Status = false;
           
            return resposta;
        }
    }

    public async Task<ResponseModel<List<FichaAvaliacaoModel>>> Editar(FichaAvaliacaoEdicaoDto fichaavaliacaoEdicaoDto)
    {
        ResponseModel<List<FichaAvaliacaoModel>> resposta = new ResponseModel<List<FichaAvaliacaoModel>>();

        try
        {
            var fichaavaliacao = _context.FichaAvaliacao.FirstOrDefault(x => x.Id == fichaavaliacaoEdicaoDto.Id);
            if (fichaavaliacao == null)
            {
                resposta.Mensagem = "FichaAvaliacao não encontrado";
                return resposta;
            }

            fichaavaliacao.QueixaPrincipal = fichaavaliacaoEdicaoDto.QueixaPrincipal;
            fichaavaliacao.HistoriaPregressa = fichaavaliacaoEdicaoDto.HistoriaPregressa;
            fichaavaliacao.HistoriaAtual = fichaavaliacaoEdicaoDto.HistoriaAtual;
            fichaavaliacao.TipoDor = fichaavaliacaoEdicaoDto.TipoDor;
            fichaavaliacao.SinaisVitais = fichaavaliacaoEdicaoDto.SinaisVitais;
            fichaavaliacao.Medicamentos = fichaavaliacaoEdicaoDto.Medicamentos;
            fichaavaliacao.DoencasCronicas = fichaavaliacaoEdicaoDto.DoencasCronicas;
            fichaavaliacao.Cirurgia = fichaavaliacaoEdicaoDto.Cirurgia;
            fichaavaliacao.DoencaNeurodegenerativa = fichaavaliacaoEdicaoDto.DoencaNeurodegenerativa;
            fichaavaliacao.TratamentosRealizados = fichaavaliacaoEdicaoDto.TratamentosRealizados;
            fichaavaliacao.AlergiaMedicamentos = fichaavaliacaoEdicaoDto.AlergiaMedicamentos;
            fichaavaliacao.Tabagista = fichaavaliacaoEdicaoDto.Tabagista;
            fichaavaliacao.FrequenciaConsumoAlcool = fichaavaliacaoEdicaoDto.FrequenciaConsumoAlcool;
            fichaavaliacao.PraticaAtividade = fichaavaliacaoEdicaoDto.PraticaAtividade;
            fichaavaliacao.ObjetivoTratamento = fichaavaliacaoEdicaoDto.ObjetivoTratamento;

            _context.Update(fichaavaliacao);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.FichaAvaliacao.ToListAsync();
            resposta.Mensagem = "FichaAvaliacao Atualizado com sucesso";

            return resposta;
        }
        catch (Exception ex)
        {

            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            
            return resposta;
        }
    }

    public async Task<ResponseModel<List<FichaAvaliacaoModel>>> Listar()
    {
        ResponseModel<List<FichaAvaliacaoModel>> resposta = new ResponseModel<List<FichaAvaliacaoModel>>();

        try
        {
            var fichaavaliacao = await _context.FichaAvaliacao.ToListAsync();

            resposta.Dados = fichaavaliacao;
            resposta.Mensagem = "Todos os FichaAvaliacao foram encontrados";
         
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