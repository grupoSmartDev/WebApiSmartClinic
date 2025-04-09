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
            var fichaavaliacao = await _context.FichaAvaliacao.FirstOrDefaultAsync(x => x.Id == fichaAvaliacaoEdicaoDto.Id);
            if (fichaavaliacao == null)
            {
                resposta.Mensagem = "Ficha de avaliação não encontrada";
                return resposta;
            }

            fichaavaliacao.PacienteId = fichaAvaliacaoEdicaoDto.PacienteId;
            fichaavaliacao.DataAvaliacao = fichaAvaliacaoEdicaoDto.DataAvaliacao;
            fichaavaliacao.Profissional = fichaAvaliacaoEdicaoDto.Profissional;
            fichaavaliacao.Especialidade = fichaAvaliacaoEdicaoDto.Especialidade;
            fichaavaliacao.Idade = fichaAvaliacaoEdicaoDto.Idade;
            fichaavaliacao.Altura = fichaAvaliacaoEdicaoDto.Altura;
            fichaavaliacao.Peso = fichaAvaliacaoEdicaoDto.Peso;
            fichaavaliacao.Sexo = fichaAvaliacaoEdicaoDto.Sexo;
            fichaavaliacao.ObservacoesGerais = fichaAvaliacaoEdicaoDto.ObservacoesGerais;
            fichaavaliacao.HistoricoDoencas = fichaAvaliacaoEdicaoDto.HistoricoDoencas;
            fichaavaliacao.DoencasPreExistentes = fichaAvaliacaoEdicaoDto.DoencasPreExistentes;
            fichaavaliacao.MedicacaoUsoContinuo = fichaAvaliacaoEdicaoDto.MedicacaoUsoContinuo;
            fichaavaliacao.Medicacao = fichaAvaliacaoEdicaoDto.Medicacao;
            fichaavaliacao.CirurgiasPrevias = fichaAvaliacaoEdicaoDto.CirurgiasPrevias;
            fichaavaliacao.DetalheCirurgias = fichaAvaliacaoEdicaoDto.DetalheCirurgias;
            fichaavaliacao.Alergias = fichaAvaliacaoEdicaoDto.Alergias;
            fichaavaliacao.QueixaPrincipal = fichaAvaliacaoEdicaoDto.QueixaPrincipal;
            fichaavaliacao.ObjetivosDoTratamento = fichaAvaliacaoEdicaoDto.ObjetivosDoTratamento;
            fichaavaliacao.IMC = fichaAvaliacaoEdicaoDto.IMC;
            fichaavaliacao.AvaliacaoPostural = fichaAvaliacaoEdicaoDto.AvaliacaoPostural;
            fichaavaliacao.AmplitudeMovimento = fichaAvaliacaoEdicaoDto.AmplitudeMovimento;
            fichaavaliacao.AssinaturaProfissional = fichaAvaliacaoEdicaoDto.AssinaturaProfissional;
            fichaavaliacao.AssinaturaCliente = fichaAvaliacaoEdicaoDto.AssinaturaCliente;
            fichaavaliacao.HistoriaPregressa = fichaAvaliacaoEdicaoDto.HistoriaPregressa;
            fichaavaliacao.HistoriaAtual = fichaAvaliacaoEdicaoDto.HistoriaAtual;
            fichaavaliacao.TipoDor = fichaAvaliacaoEdicaoDto.TipoDor;
            fichaavaliacao.SinaisVitais = fichaAvaliacaoEdicaoDto.SinaisVitais;
            fichaavaliacao.DoencasCronicas = fichaAvaliacaoEdicaoDto.DoencasCronicas;
            fichaavaliacao.Cirurgia = fichaAvaliacaoEdicaoDto.Cirurgia;
            fichaavaliacao.DoencaNeurodegenerativa = fichaAvaliacaoEdicaoDto.DoencaNeurodegenerativa;
            fichaavaliacao.TratamentosRealizados = fichaAvaliacaoEdicaoDto.TratamentosRealizados;
            fichaavaliacao.AlergiaMedicamentos = fichaAvaliacaoEdicaoDto.AlergiaMedicamentos;
            fichaavaliacao.FrequenciaConsumoAlcool = fichaAvaliacaoEdicaoDto.FrequenciaConsumoAlcool;
            fichaavaliacao.PraticaAtividade = fichaAvaliacaoEdicaoDto.PraticaAtividade;
            fichaavaliacao.Tabagista = fichaAvaliacaoEdicaoDto.Tabagista;
            fichaavaliacao.ProfissionalId = fichaAvaliacaoEdicaoDto.ProfissionalId;

            _context.Update(fichaAvaliacaoEdicaoDto);
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