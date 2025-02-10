
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

    public async Task<ResponseModel<FichaAvaliacaoModel>> BuscarPorIdPaciente(int pacienteId)
    {
        ResponseModel<FichaAvaliacaoModel> resposta = new ResponseModel<FichaAvaliacaoModel>();
        try
        {
            var fichaAvaliacao = await _context.FichaAvaliacao
                .Include(f => f.Profissional)  // Inclui os dados do Profissional
                .Include(f => f.Paciente)
                .FirstOrDefaultAsync(x => x.PacienteId == pacienteId);
            if (fichaAvaliacao == null)
            {
                resposta.Mensagem = "Nenhuma Ficha de Avaliacao encontrado";
                return resposta;
            }

            resposta.Dados = fichaAvaliacao;
            resposta.Mensagem = "Ficha de Avaliacao Encontrado";
            return resposta;
        }
        catch (Exception ex)
        {

            resposta.Mensagem = "Erro ao buscar Ficha de Avaliacao";
            resposta.Status = false;

            return resposta;
        }
    }

    public async Task<ResponseModel<List<FichaAvaliacaoModel>>> Criar(FichaAvaliacaoCreateDto fichaavaliacaoCreateDto)
    {
        ResponseModel<List<FichaAvaliacaoModel>> resposta = new ResponseModel<List<FichaAvaliacaoModel>>();
        try
        {
            var fichaavaliacao = new FichaAvaliacaoModel
            {
                PacienteId = fichaavaliacaoCreateDto.PacienteId,
                DataAvaliacao = fichaavaliacaoCreateDto.DataAvaliacao,
                Profissional = fichaavaliacaoCreateDto.Profissional,
                Especialidade = fichaavaliacaoCreateDto.Especialidade,
                Idade = fichaavaliacaoCreateDto.Idade,
                Altura = fichaavaliacaoCreateDto.Altura,
                Peso = fichaavaliacaoCreateDto.Peso,
                Sexo = fichaavaliacaoCreateDto.Sexo,
                ObservacoesGerais = fichaavaliacaoCreateDto.ObservacoesGerais,
                HistoricoDoencas = fichaavaliacaoCreateDto.HistoricoDoencas,
                DoencasPreExistentes = fichaavaliacaoCreateDto.DoencasPreExistentes,
                MedicacaoUsoContinuo = fichaavaliacaoCreateDto.MedicacaoUsoContinuo,
                Medicacao = fichaavaliacaoCreateDto.Medicacao,
                CirurgiasPrevias = fichaavaliacaoCreateDto.CirurgiasPrevias,
                DetalheCirurgias = fichaavaliacaoCreateDto.DetalheCirurgias,
                Alergias = fichaavaliacaoCreateDto.Alergias,
                QueixaPrincipal = fichaavaliacaoCreateDto.QueixaPrincipal,
                ObjetivosDoTratamento = fichaavaliacaoCreateDto.ObjetivosDoTratamento,
                IMC = fichaavaliacaoCreateDto.IMC,
                AvaliacaoPostural = fichaavaliacaoCreateDto.AvaliacaoPostural,
                AmplitudeMovimento = fichaavaliacaoCreateDto.AmplitudeMovimento,
                AssinaturaProfissional = fichaavaliacaoCreateDto.AssinaturaProfissional,
                AssinaturaCliente = fichaavaliacaoCreateDto.AssinaturaCliente,
                HistoriaPregressa = fichaavaliacaoCreateDto.HistoriaPregressa,
                HistoriaAtual = fichaavaliacaoCreateDto.HistoriaAtual,
                TipoDor = fichaavaliacaoCreateDto.TipoDor,
                SinaisVitais = fichaavaliacaoCreateDto.SinaisVitais,
                DoencasCronicas = fichaavaliacaoCreateDto.DoencasCronicas,
                Cirurgia = fichaavaliacaoCreateDto.Cirurgia,
                DoencaNeurodegenerativa = fichaavaliacaoCreateDto.DoencaNeurodegenerativa,
                TratamentosRealizados = fichaavaliacaoCreateDto.TratamentosRealizados,
                AlergiaMedicamentos = fichaavaliacaoCreateDto.AlergiaMedicamentos,
                FrequenciaConsumoAlcool = fichaavaliacaoCreateDto.FrequenciaConsumoAlcool,
                PraticaAtividade = fichaavaliacaoCreateDto.PraticaAtividade,
                Tabagista = fichaavaliacaoCreateDto.Tabagista,
                ProfissionalId = fichaavaliacaoCreateDto.ProfissionalId
            };

            _context.Add(fichaavaliacao);
            await _context.SaveChangesAsync();
            resposta.Dados = await _context.FichaAvaliacao.ToListAsync();
            resposta.Mensagem = "Ficha de avaliação criada com sucesso";

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
                resposta.Mensagem = "Nenhum Ficha de avaliação  encontrado";
                return resposta;
            }

            _context.Remove(fichaavaliacao);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.FichaAvaliacao.ToListAsync();
            resposta.Mensagem = "Ficha de avaliação  Excluido com sucesso";
         
            return resposta;
        }
        catch (Exception ex)
        {

            resposta.Mensagem = ex.Message;
            resposta.Status = false;
           
            return resposta;
        }
    }

    public async Task<ResponseModel<List<FichaAvaliacaoModel>>> Editar(FichaAvaliacaoEdicaoDto fichaAvaliacaoEdicaoDto)
    {
        ResponseModel<List<FichaAvaliacaoModel>> resposta = new ResponseModel<List<FichaAvaliacaoModel>>();
        try
        {
            var fichaavaliacao = await _context.FichaAvaliacao.FirstOrDefaultAsync(x => x.Id == fichaavaliacaoEdicaoDto.Id);
            if (fichaavaliacao == null)
            {
                resposta.Mensagem = "Ficha de avaliação não encontrada";
                return resposta;
            }

            fichaavaliacao.PacienteId = fichaavaliacaoEdicaoDto.PacienteId;
            fichaavaliacao.DataAvaliacao = fichaavaliacaoEdicaoDto.DataAvaliacao;
            fichaavaliacao.Profissional = fichaavaliacaoEdicaoDto.Profissional;
            fichaavaliacao.Especialidade = fichaavaliacaoEdicaoDto.Especialidade;
            fichaavaliacao.Idade = fichaavaliacaoEdicaoDto.Idade;
            fichaavaliacao.Altura = fichaavaliacaoEdicaoDto.Altura;
            fichaavaliacao.Peso = fichaavaliacaoEdicaoDto.Peso;
            fichaavaliacao.Sexo = fichaavaliacaoEdicaoDto.Sexo;
            fichaavaliacao.ObservacoesGerais = fichaavaliacaoEdicaoDto.ObservacoesGerais;
            fichaavaliacao.HistoricoDoencas = fichaavaliacaoEdicaoDto.HistoricoDoencas;
            fichaavaliacao.DoencasPreExistentes = fichaavaliacaoEdicaoDto.DoencasPreExistentes;
            fichaavaliacao.MedicacaoUsoContinuo = fichaavaliacaoEdicaoDto.MedicacaoUsoContinuo;
            fichaavaliacao.Medicacao = fichaavaliacaoEdicaoDto.Medicacao;
            fichaavaliacao.CirurgiasPrevias = fichaavaliacaoEdicaoDto.CirurgiasPrevias;
            fichaavaliacao.DetalheCirurgias = fichaavaliacaoEdicaoDto.DetalheCirurgias;
            fichaavaliacao.Alergias = fichaavaliacaoEdicaoDto.Alergias;
            fichaavaliacao.QueixaPrincipal = fichaavaliacaoEdicaoDto.QueixaPrincipal;
            fichaavaliacao.ObjetivosDoTratamento = fichaavaliacaoEdicaoDto.ObjetivosDoTratamento;
            fichaavaliacao.IMC = fichaavaliacaoEdicaoDto.IMC;
            fichaavaliacao.AvaliacaoPostural = fichaavaliacaoEdicaoDto.AvaliacaoPostural;
            fichaavaliacao.AmplitudeMovimento = fichaavaliacaoEdicaoDto.AmplitudeMovimento;
            fichaavaliacao.AssinaturaProfissional = fichaavaliacaoEdicaoDto.AssinaturaProfissional;
            fichaavaliacao.AssinaturaCliente = fichaavaliacaoEdicaoDto.AssinaturaCliente;
            fichaavaliacao.HistoriaPregressa = fichaavaliacaoEdicaoDto.HistoriaPregressa;
            fichaavaliacao.HistoriaAtual = fichaavaliacaoEdicaoDto.HistoriaAtual;
            fichaavaliacao.TipoDor = fichaavaliacaoEdicaoDto.TipoDor;
            fichaavaliacao.SinaisVitais = fichaavaliacaoEdicaoDto.SinaisVitais;
            fichaavaliacao.DoencasCronicas = fichaavaliacaoEdicaoDto.DoencasCronicas;
            fichaavaliacao.Cirurgia = fichaavaliacaoEdicaoDto.Cirurgia;
            fichaavaliacao.DoencaNeurodegenerativa = fichaavaliacaoEdicaoDto.DoencaNeurodegenerativa;
            fichaavaliacao.TratamentosRealizados = fichaavaliacaoEdicaoDto.TratamentosRealizados;
            fichaavaliacao.AlergiaMedicamentos = fichaavaliacaoEdicaoDto.AlergiaMedicamentos;
            fichaavaliacao.FrequenciaConsumoAlcool = fichaavaliacaoEdicaoDto.FrequenciaConsumoAlcool;
            fichaavaliacao.PraticaAtividade = fichaavaliacaoEdicaoDto.PraticaAtividade;
            fichaavaliacao.Tabagista = fichaavaliacaoEdicaoDto.Tabagista;
            fichaavaliacao.ProfissionalId = fichaavaliacaoEdicaoDto.ProfissionalId;

            _context.Update(fichaAvaliacao);
            await _context.SaveChangesAsync();
            resposta.Dados = await _context.FichaAvaliacao.ToListAsync();
            resposta.Mensagem = "Ficha de Avaliação atualizada com sucesso";
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
            resposta.Mensagem = "Todas as fichas de avaliação foram encontrados";
         
            return resposta;
        }
        catch (Exception e)
        {
            resposta.Mensagem = e.Message;
            resposta.Status = false;
            
            return resposta;
        }
    }

    private decimal CalcularIMC(decimal peso, decimal altura)
    {
        if (altura <= 0)
            return 0;

        return Math.Round(peso / (altura * altura), 2);
    }
}