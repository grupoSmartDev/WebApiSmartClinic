
using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Dto.Agenda;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Agenda;

public class AgendaService : IAgendaInterface
{
    private readonly AppDbContext _context;
    public AgendaService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseModel<AgendaModel>> BuscarPorId(int idAgenda)
    {
        ResponseModel<AgendaModel> resposta = new ResponseModel<AgendaModel>();
        try
        {
            var agenda = await _context.Agenda.FirstOrDefaultAsync(x => x.Id == idAgenda);
            if (agenda == null)
            {
                resposta.Mensagem = "Nenhum Agenda encontrado";
                return resposta;
            }

            resposta.Dados = agenda;
            resposta.Mensagem = "Agenda Encontrado";
            return resposta;
        }
        catch (Exception ex)
        {

            resposta.Mensagem = "Erro ao buscar Agenda";
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<AgendaModel>>> Criar(AgendaCreateDto agendaCreateDto)
    {
        ResponseModel<List<AgendaModel>> resposta = new ResponseModel<List<AgendaModel>>();

        try
        {
            var agenda = new AgendaModel();

            agenda.Data = agendaCreateDto.Data;
            agenda.HoraInicio = agendaCreateDto.HoraInicio;
            agenda.HoraFim = agendaCreateDto.HoraFim;
            agenda.PacienteId = agendaCreateDto.PacienteId;
            agenda.ProfissionalId = agendaCreateDto.ProfissionalId;
            agenda.Convenio = agendaCreateDto.Convenio;
            agenda.Valor = agendaCreateDto.Valor;
            agenda.FormaPagamento = agendaCreateDto.FormaPagamento;
            agenda.Pago = agendaCreateDto.Pago;
            agenda.FinanceiroId = agendaCreateDto.FinanceiroId;
            agenda.SalaId = agendaCreateDto.SalaId;
            agenda.PacoteId = agendaCreateDto.PacoteId;
            agenda.LembreteSms = agendaCreateDto.LembreteSms;
            agenda.LembreteWhatsapp = agendaCreateDto.LembreteWhatsapp;
            agenda.LembreteEmail = agendaCreateDto.LembreteEmail;
            agenda.Status = agendaCreateDto.Status;
            agenda.CorStatus = agendaCreateDto.CorStatus;
            agenda.IntegracaoGmail = agendaCreateDto.IntegracaoGmail;

            _context.Add(agenda);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Agenda.ToListAsync();
            resposta.Mensagem = "Agenda criado com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }

    }

    public async Task<ResponseModel<List<AgendaModel>>> Delete(int idAgenda)
    {
        ResponseModel<List<AgendaModel>> resposta = new ResponseModel<List<AgendaModel>>();

        try
        {
            var agenda = await _context.Agenda.FirstOrDefaultAsync(x => x.Id == idAgenda);
            if (agenda == null)
            {
                resposta.Mensagem = "Nenhum Agenda encontrado";
                return resposta;
            }

            _context.Remove(agenda);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Agenda.ToListAsync();
            resposta.Mensagem = "Agenda Excluido com sucesso";
            return resposta;

        }
        catch (Exception ex)
        {

            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<AgendaModel>>> Editar(AgendaEdicaoDto agendaEdicaoDto)
    {
        ResponseModel<List<AgendaModel>> resposta = new ResponseModel<List<AgendaModel>>();

        try
        {
            var agenda = _context.Agenda.FirstOrDefault(x => x.Id == agendaEdicaoDto.Id);
            if (agenda == null)
            {
                resposta.Mensagem = "Agenda não encontrado";
                return resposta;
            }

            agenda.Id = agendaEdicaoDto.Id;
            agenda.Data = agendaEdicaoDto.Data;
            agenda.HoraInicio = agendaEdicaoDto.HoraInicio;
            agenda.HoraFim = agendaEdicaoDto.HoraFim;
            agenda.PacienteId = agendaEdicaoDto.PacienteId;
            agenda.ProfissionalId = agendaEdicaoDto.ProfissionalId;
            agenda.Convenio = agendaEdicaoDto.Convenio;
            agenda.Valor = agendaEdicaoDto.Valor;
            agenda.FormaPagamento = agendaEdicaoDto.FormaPagamento;
            agenda.Pago = agendaEdicaoDto.Pago;
            agenda.FinanceiroId = agendaEdicaoDto.FinanceiroId;
            agenda.SalaId = agendaEdicaoDto.SalaId;
            agenda.PacoteId = agendaEdicaoDto.PacoteId;
            agenda.LembreteSms = agendaEdicaoDto.LembreteSms;
            agenda.LembreteWhatsapp = agendaEdicaoDto.LembreteWhatsapp;
            agenda.LembreteEmail = agendaEdicaoDto.LembreteEmail;
            agenda.Status = agendaEdicaoDto.Status;
            agenda.CorStatus = agendaEdicaoDto.CorStatus;
            agenda.IntegracaoGmail = agendaEdicaoDto.IntegracaoGmail;

            _context.Update(agenda);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Agenda.ToListAsync();
            resposta.Mensagem = "Agenda Atualizado com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {

            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<AgendaModel>>> Listar()
    {
        ResponseModel<List<AgendaModel>> resposta = new ResponseModel<List<AgendaModel>>();

        try
        {
            var agenda = await _context.Agenda.ToListAsync();

            resposta.Dados = agenda;
            resposta.Mensagem = "Todos os Agenda foram encontrados";
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