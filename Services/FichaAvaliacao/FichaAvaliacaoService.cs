
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
                .Include(p => p.Paciente)
                .FirstOrDefaultAsync(f => f.PacienteId ==  pacienteId);

            if (fichaAvaliacao == null)
            {
                resposta.Mensagem = "Erro ao encontrar ficha";
                return resposta;
            }

            resposta.Dados = fichaAvaliacao;
            resposta.Mensagem = "Ficha de avaliação encontrada";
            resposta.Status = true;
            return resposta;
        }
        catch (Exception ex)
        {

            resposta.Mensagem = "Erro ao buscar FichaAvaliacao";
            resposta.Status = false;

            return resposta;
        }
    }

    public async Task<ResponseModel<List<FichaAvaliacaoModel>>> Criar(FichaAvaliacaoCreateDto fichaAvaliacaoCreateDto)
    {
        ResponseModel<List<FichaAvaliacaoModel>> resposta = new ResponseModel<List<FichaAvaliacaoModel>>();

        try
        {
            var fichaAvaliacao = new FichaAvaliacaoModel();
            fichaAvaliacao.PacienteId = fichaAvaliacaoCreateDto.PacienteId;
            fichaAvaliacao.DataAvaliacao = fichaAvaliacaoCreateDto.DataAvaliacao;
            fichaAvaliacao.Profissional = fichaAvaliacaoCreateDto.Profissional;
            fichaAvaliacao.Especialidade = fichaAvaliacaoCreateDto.Especialidade;
            fichaAvaliacao.Idade = fichaAvaliacaoCreateDto.Idade;
            fichaAvaliacao.Altura = fichaAvaliacaoCreateDto.Altura;
            fichaAvaliacao.Peso = fichaAvaliacaoCreateDto.Peso;
            fichaAvaliacao.Sexo = fichaAvaliacaoCreateDto.Sexo;
            fichaAvaliacao.ObservacoesGerais = fichaAvaliacaoCreateDto.ObservacoesGerais;
            fichaAvaliacao.HistoricoDoencas = fichaAvaliacaoCreateDto.HistoricoDoencas;
            fichaAvaliacao.DoencasPreExistentes = fichaAvaliacaoCreateDto.DoencasPreExistentes;
            fichaAvaliacao.MedicacaoUsoContinuo = fichaAvaliacaoCreateDto.MedicacaoUsoContinuo;
            fichaAvaliacao.Medicacao = fichaAvaliacaoCreateDto.Medicacao;
            fichaAvaliacao.CirurgiasPrevias = fichaAvaliacaoCreateDto.CirurgiasPrevias;
            fichaAvaliacao.DetalheCirurgias = fichaAvaliacaoCreateDto.DetalheCirurgias;
            fichaAvaliacao.Alergias = fichaAvaliacaoCreateDto.Alergias;
            fichaAvaliacao.QueixaPrincipal = fichaAvaliacaoCreateDto.QueixaPrincipal;
            fichaAvaliacao.ObjetivosDoTratamento = fichaAvaliacaoCreateDto.ObjetivosDoTratamento;
            fichaAvaliacao.Imc = CalcularIMC(fichaAvaliacaoCreateDto.Peso, fichaAvaliacaoCreateDto.Altura);
            fichaAvaliacao.AvaliacaoPostural = fichaAvaliacaoCreateDto.AvaliacaoPostural;
            fichaAvaliacao.AmplitudeMovimento = fichaAvaliacaoCreateDto.AmplitudeMovimento;
            fichaAvaliacao.AssinaturaProfissional = fichaAvaliacaoCreateDto.AssinaturaProfissional;
            fichaAvaliacao.AssinaturaCliente = fichaAvaliacaoCreateDto.AssinaturaCliente;
            fichaAvaliacao.HistoriaPregressa = fichaAvaliacaoCreateDto.HistoriaPregressa;
            fichaAvaliacao.HistoriaAtual = fichaAvaliacaoCreateDto.HistoriaAtual;
            fichaAvaliacao.TipoDor = fichaAvaliacaoCreateDto.TipoDor;
            fichaAvaliacao.SinaisVitais = fichaAvaliacaoCreateDto.SinaisVitais;
            fichaAvaliacao.DoencasCronicas = fichaAvaliacaoCreateDto.DoencasCronicas;
            fichaAvaliacao.Cirurgia = fichaAvaliacaoCreateDto.Cirurgia;
            fichaAvaliacao.DoencaNeurodegenerativa = fichaAvaliacaoCreateDto.DoencaNeurodegenerativa;
            fichaAvaliacao.TratamentosRealizados = fichaAvaliacaoCreateDto.TratamentosRealizados;
            fichaAvaliacao.AlergiaMedicamentos = fichaAvaliacaoCreateDto.AlergiaMedicamentos;
            fichaAvaliacao.FrequenciaConsumoAlcool = fichaAvaliacaoCreateDto.FrequenciaConsumoAlcool;
            fichaAvaliacao.PraticaAtividade = fichaAvaliacaoCreateDto.PraticaAtividade;
            fichaAvaliacao.Tabagista = fichaAvaliacaoCreateDto.Tabagista;
            fichaAvaliacao.ProfissionalId = fichaAvaliacaoCreateDto.ProfissionalId;
           

            _context.Add(fichaAvaliacao);
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

    public async Task<ResponseModel<List<FichaAvaliacaoModel>>> Editar(FichaAvaliacaoEdicaoDto fichaAvaliacaoEdicaoDto)
    {
        ResponseModel<List<FichaAvaliacaoModel>> resposta = new ResponseModel<List<FichaAvaliacaoModel>>();

        try
        {
            var fichaAvaliacao = _context.FichaAvaliacao.FirstOrDefault(x => x.Id == fichaAvaliacaoEdicaoDto.Id);
            if (fichaAvaliacao == null)
            {
                resposta.Mensagem = "FichaAvaliacao não encontrado";
                return resposta;
            }

            fichaAvaliacao.PacienteId = fichaAvaliacaoEdicaoDto.PacienteId;
            fichaAvaliacao.DataAvaliacao = fichaAvaliacaoEdicaoDto.DataAvaliacao;
            fichaAvaliacao.Profissional = fichaAvaliacaoEdicaoDto.Profissional;
            fichaAvaliacao.Especialidade = fichaAvaliacaoEdicaoDto.Especialidade;
            fichaAvaliacao.Idade = fichaAvaliacaoEdicaoDto.Idade;
            fichaAvaliacao.Altura = fichaAvaliacaoEdicaoDto.Altura;
            fichaAvaliacao.Peso = fichaAvaliacaoEdicaoDto.Peso;
            fichaAvaliacao.Sexo = fichaAvaliacaoEdicaoDto.Sexo;
            fichaAvaliacao.ObservacoesGerais = fichaAvaliacaoEdicaoDto.ObservacoesGerais;
            fichaAvaliacao.HistoricoDoencas = fichaAvaliacaoEdicaoDto.HistoricoDoencas;
            fichaAvaliacao.DoencasPreExistentes = fichaAvaliacaoEdicaoDto.DoencasPreExistentes;
            fichaAvaliacao.MedicacaoUsoContinuo = fichaAvaliacaoEdicaoDto.MedicacaoUsoContinuo;
            fichaAvaliacao.Medicacao = fichaAvaliacaoEdicaoDto.Medicacao;
            fichaAvaliacao.CirurgiasPrevias = fichaAvaliacaoEdicaoDto.CirurgiasPrevias;
            fichaAvaliacao.DetalheCirurgias = fichaAvaliacaoEdicaoDto.DetalheCirurgias;
            fichaAvaliacao.Alergias = fichaAvaliacaoEdicaoDto.Alergias;
            fichaAvaliacao.QueixaPrincipal = fichaAvaliacaoEdicaoDto.QueixaPrincipal;
            fichaAvaliacao.ObjetivosDoTratamento = fichaAvaliacaoEdicaoDto.ObjetivosDoTratamento;
            fichaAvaliacao.Imc = CalcularIMC(fichaAvaliacaoEdicaoDto.Peso, fichaAvaliacaoEdicaoDto.Altura);
            fichaAvaliacao.AvaliacaoPostural = fichaAvaliacaoEdicaoDto.AvaliacaoPostural;
            fichaAvaliacao.AmplitudeMovimento = fichaAvaliacaoEdicaoDto.AmplitudeMovimento;
            fichaAvaliacao.AssinaturaProfissional = fichaAvaliacaoEdicaoDto.AssinaturaProfissional;
            fichaAvaliacao.AssinaturaCliente = fichaAvaliacaoEdicaoDto.AssinaturaCliente;
            fichaAvaliacao.HistoriaPregressa = fichaAvaliacaoEdicaoDto.HistoriaPregressa;
            fichaAvaliacao.HistoriaAtual = fichaAvaliacaoEdicaoDto.HistoriaAtual;
            fichaAvaliacao.TipoDor = fichaAvaliacaoEdicaoDto.TipoDor;
            fichaAvaliacao.SinaisVitais = fichaAvaliacaoEdicaoDto.SinaisVitais;
            fichaAvaliacao.DoencasCronicas = fichaAvaliacaoEdicaoDto.DoencasCronicas;
            fichaAvaliacao.Cirurgia = fichaAvaliacaoEdicaoDto.Cirurgia;
            fichaAvaliacao.DoencaNeurodegenerativa = fichaAvaliacaoEdicaoDto.DoencaNeurodegenerativa;
            fichaAvaliacao.TratamentosRealizados = fichaAvaliacaoEdicaoDto.TratamentosRealizados;
            fichaAvaliacao.AlergiaMedicamentos = fichaAvaliacaoEdicaoDto.AlergiaMedicamentos;
            fichaAvaliacao.FrequenciaConsumoAlcool = fichaAvaliacaoEdicaoDto.FrequenciaConsumoAlcool;
            fichaAvaliacao.PraticaAtividade = fichaAvaliacaoEdicaoDto.PraticaAtividade;
            fichaAvaliacao.Tabagista = fichaAvaliacaoEdicaoDto.Tabagista;

            _context.Update(fichaAvaliacao);
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

    private decimal CalcularIMC(decimal peso, decimal altura)
    {
        if (altura <= 0)
            return 0;

        return Math.Round(peso / (altura * altura), 2);
    }
}