using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services;

public interface IComissaoService
{
    Task<ResponseModel<List<ComissaoCalculadaModel>>> CalcularComissoes(DateTime dataInicio, DateTime dataFim, int? profissionalId = null);
    Task<ResponseModel<List<ComissaoCalculadaModel>>> ListarComissoes(DateTime dataInicio, DateTime dataFim, StatusComissao? status = null, int? profissionalId = null);
    Task<ResponseModel<string>> DarBaixaComissoes(List<int> idsComissoes);
    Task<ResponseModel<ComissaoResumoDto>> ObterResumoComissoes(DateTime dataInicio, DateTime dataFim, int? profissionalId = null);
}

public class ComissaoService : IComissaoService
{
    private readonly AppDbContext _context;

    public ComissaoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseModel<List<ComissaoCalculadaModel>>> CalcularComissoes(DateTime dataInicio, DateTime dataFim, int? profissionalId = null)
    {
        var resposta = new ResponseModel<List<ComissaoCalculadaModel>>();

        try
        {
            // Buscar agendamentos realizados no período que ainda não foram calculados
            var query = _context.Agenda
                .Include(a => a.Profissional)
                .Include(a => a.Paciente)
                .ThenInclude(p => p.Plano)
                .Where(a => 
                           a.Data >= dataInicio &&
                           a.Data <= dataFim &&
                           a.StatusId == 4 && // Apenas agendamentos realizados
                           !_context.Comissoes.Any(c => c.AgendamentoId == a.Id && c.Ativo)); // Não calculados ainda

            if (profissionalId.HasValue)
            {
                query = query.Where(a => a.ProfissionalId == profissionalId.Value);
            }

            var agendamentos = await query.ToListAsync();
            var comissoesCalculadas = new List<ComissaoCalculadaModel>();

            foreach (var agendamento in agendamentos)
            {
                // Verificar se o profissional tem configuração de comissão
                if (agendamento.Profissional.ValorComissao <= 0)
                    continue;

                var valorBase = 0.00M; // Valor do plano como base
                switch (agendamento.Paciente.Plano?.TipoMes)
                {
                    case "m":
                        valorBase = (decimal)(agendamento.Paciente.Plano?.ValorMensal);
                        break;
                    case "b":
                        valorBase = (decimal)(agendamento.Paciente.Plano?.ValorBimestral);
                        break;
                    case "t":
                        valorBase = (decimal)(agendamento.Paciente.Plano?.ValorTrimestral);
                        break;
                    case "q":
                        valorBase = (decimal)(agendamento.Paciente.Plano?.ValorQuadrimestral);
                        break;
                    case "s":
                        valorBase = (decimal)(agendamento.Paciente.Plano?.ValorSemestral);
                        break;
                    case "a":
                        valorBase = (decimal)(agendamento.Paciente.Plano?.ValorAnual);
                        break;
                    default:
                        valorBase = 0.00M;
                        break;
                }

                var comissao = new ComissaoCalculadaModel
                {
                    ProfissionalId = (int)agendamento.ProfissionalId,
                    AgendamentoId = agendamento.Id,
                    DataAgendamento = (DateTime)agendamento.Data,
                    TipoComissaoUtilizado = agendamento.Profissional.TipoComissao,
                    PercentualOuValor = agendamento.Profissional.ValorComissao,
                    ValorBase = (decimal)valorBase,
                    NomePaciente = agendamento.Paciente.Nome,
                    NomePlano = agendamento.Paciente.Plano?.Descricao,
                    DataCalculo = DateTime.UtcNow
                };

                var dataAgTratada = DateTime.SpecifyKind(comissao.DataAgendamento, DateTimeKind.Utc);
                var dataCalcTratada = DateTime.SpecifyKind(comissao.DataCalculo, DateTimeKind.Utc);

                comissao.DataAgendamento = dataAgTratada;
                comissao.DataCalculo = dataCalcTratada;


                // Calcular valor da comissão
                if (agendamento.Profissional.TipoComissao == "P")
                {
                    comissao.ValorComissao = valorBase * (agendamento.Profissional.ValorComissao / 100);
                }
                else
                {
                    comissao.ValorComissao = agendamento.Profissional.ValorComissao;
                }

                comissoesCalculadas.Add(comissao);
            }

            // Salvar comissões calculadas
            if (comissoesCalculadas.Any())
            {
                await _context.Comissoes.AddRangeAsync(comissoesCalculadas);
                await _context.SaveChangesAsync();
            }

            resposta.Dados = comissoesCalculadas;
            resposta.Mensagem = $"{comissoesCalculadas.Count} comissões calculadas com sucesso";

            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<ComissaoCalculadaModel>>> ListarComissoes(DateTime dataInicio, DateTime dataFim, StatusComissao? status = null, int? profissionalId = null)
    {
        var resposta = new ResponseModel<List<ComissaoCalculadaModel>>();

        try
        {
            var query = _context.Comissoes
                .Include(c => c.Profissional)
                .Include(c => c.Agendamento)
                .Where(c => c.Ativo &&
                           c.DataAgendamento >= dataInicio &&
                           c.DataAgendamento <= dataFim);

            if (status.HasValue)
            {
                query = query.Where(c => c.Status == status.Value);
            }

            if (profissionalId.HasValue)
            {
                query = query.Where(c => c.ProfissionalId == profissionalId.Value);
            }

            var comissoes = await query
                .OrderByDescending(c => c.DataAgendamento)
                .ToListAsync();

            resposta.Dados = comissoes;
            resposta.Mensagem = $"{comissoes.Count} comissões encontradas";

            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<string>> DarBaixaComissoes(List<int> idsComissoes)
    {
        var resposta = new ResponseModel<string>();

        try
        {
            var comissoes = await _context.Comissoes
                .Where(c => idsComissoes.Contains(c.Id) && c.Status == StatusComissao.Pendente)
                .ToListAsync();

            foreach (var comissao in comissoes)
            {
                comissao.Status = StatusComissao.Pago;
                comissao.DataPagamento = DateTime.UtcNow;
                // comissao.UsuarioPagamento = usuarioLogado; // Se tiver controle de usuário
            }

            await _context.SaveChangesAsync();

            resposta.Dados = $"{comissoes.Count} comissões pagas com sucesso";
            resposta.Mensagem = "Baixa realizada com sucesso";

            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<ComissaoResumoDto>> ObterResumoComissoes(DateTime dataInicio, DateTime dataFim, int? profissionalId = null)
    {
        var resposta = new ResponseModel<ComissaoResumoDto>();

        try
        {
            var query = _context.Comissoes
                .Where(c => c.Ativo &&
                           c.DataAgendamento >= dataInicio &&
                           c.DataAgendamento <= dataFim);

            if (profissionalId.HasValue)
            {
                query = query.Where(c => c.ProfissionalId == profissionalId.Value);
            }

            var resumo = new ComissaoResumoDto
            {
                TotalPendente = await query.Where(c => c.Status == StatusComissao.Pendente).SumAsync(c => c.ValorComissao),
                TotalPago = await query.Where(c => c.Status == StatusComissao.Pago).SumAsync(c => c.ValorComissao),
                QuantidadePendente = await query.CountAsync(c => c.Status == StatusComissao.Pendente),
                QuantidadePago = await query.CountAsync(c => c.Status == StatusComissao.Pago)
            };

            resumo.TotalGeral = resumo.TotalPendente + resumo.TotalPago;
            resumo.QuantidadeTotal = resumo.QuantidadePendente + resumo.QuantidadePago;

            resposta.Dados = resumo;
            resposta.Mensagem = "Resumo obtido com sucesso";

            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }
}

// DTO para resumo
public class ComissaoResumoDto
{
    public decimal TotalGeral { get; set; }
    public decimal TotalPendente { get; set; }
    public decimal TotalPago { get; set; }
    public int QuantidadeTotal { get; set; }
    public int QuantidadePendente { get; set; }
    public int QuantidadePago { get; set; }
}