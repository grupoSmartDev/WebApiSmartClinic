
using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Dto.Convenio;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Convenio;

public class ConvenioService : IConvenioInterface
{
    private readonly AppDbContext _context;
    public ConvenioService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseModel<ConvenioModel>> BuscarPorId(int idConvenio)
    {
        ResponseModel<ConvenioModel> resposta = new ResponseModel<ConvenioModel>();
        try
        {
            var convenio = await _context.Convenio.FirstOrDefaultAsync(x => x.Id == idConvenio);
            if (convenio == null)
            {
                resposta.Mensagem = "Nenhum Convenio encontrado";
                return resposta;
            }

            resposta.Dados = convenio;
            resposta.Mensagem = "Convenio Encontrado";
           
            return resposta;
        }
        catch (Exception ex)
        {

            resposta.Mensagem = "Erro ao buscar Convenio";
            resposta.Status = false;
            
            return resposta;
        }
    }

    public async Task<ResponseModel<List<ConvenioModel>>> Criar(ConvenioCreateDto convenioCreateDto, int pageNumber = 1, int pageSize = 10)
    {
        ResponseModel<List<ConvenioModel>> resposta = new ResponseModel<List<ConvenioModel>>();

        try
        {
            var convenio = new ConvenioModel();

            convenio.Nome = convenioCreateDto.Nome;
            convenio.RegistroAvs = convenioCreateDto.RegistroAvs;
            convenio.PeriodoCarencia = convenioCreateDto.PeriodoCarencia;
            convenio.Telefone = convenioCreateDto.Telefone;
            convenio.Email  = convenioCreateDto.Email;
            convenio.Ativo = convenioCreateDto.Ativo;

            _context.Add(convenio);
            await _context.SaveChangesAsync();

            var query = _context.Convenio.AsQueryable();

            resposta.Dados = (await PaginationHelper.PaginateAsync(query, pageNumber, pageSize)).Dados;
            resposta.Mensagem = "Convenio criado com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }

    }

    public async Task<ResponseModel<List<ConvenioModel>>> Delete(int idConvenio, int pageNumber = 1, int pageSize = 10)
    {
        ResponseModel<List<ConvenioModel>> resposta = new ResponseModel<List<ConvenioModel>>();

        try
        {
            var convenio = await _context.Convenio.FirstOrDefaultAsync(x => x.Id == idConvenio);
            if (convenio == null)
            {
                resposta.Mensagem = "Nenhum Convenio encontrado";
                return resposta;
            }

            _context.Remove(convenio);
            await _context.SaveChangesAsync();

            var query = _context.Convenio.AsQueryable();

            resposta.Dados = (await PaginationHelper.PaginateAsync(query, pageNumber, pageSize)).Dados;
            resposta.Mensagem = "Convenio Excluido com sucesso";
            
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            
            return resposta;
        }
    }

    public async Task<ResponseModel<List<ConvenioModel>>> Editar(ConvenioEdicaoDto convenioEdicaoDto, int pageNumber = 1, int pageSize = 10)
    {
        ResponseModel<List<ConvenioModel>> resposta = new ResponseModel<List<ConvenioModel>>();

        try
        {
            var convenio = _context.Convenio.FirstOrDefault(x => x.Id == convenioEdicaoDto.Id);
            if (convenio == null)
            {
                resposta.Mensagem = "Convenio não encontrado";
                return resposta;
            }
            
            convenio.Id = convenioEdicaoDto.Id;
            convenio.Nome = convenioEdicaoDto.Nome;
            convenio.RegistroAvs = convenioEdicaoDto.RegistroAvs;
            convenio.PeriodoCarencia = convenioEdicaoDto.PeriodoCarencia;
            convenio.Telefone = convenioEdicaoDto.Telefone;
            convenio.Email = convenioEdicaoDto.Email;
            convenio.Ativo = convenioEdicaoDto.Ativo;

            _context.Update(convenio);
            await _context.SaveChangesAsync();

            var query = _context.Convenio.AsQueryable();

            resposta.Dados = (await PaginationHelper.PaginateAsync(query, pageNumber, pageSize)).Dados;
            resposta.Mensagem = "Convenio Atualizado com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {

            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<ConvenioModel>>> Listar(int pageNumber = 1, int pageSize = 10, int? codigoFiltro = null, string? nomeFiltro = null, string? telefoneFiltro = null, string? registroAvsFiltro = null, bool paginar = true)
    {
        ResponseModel<List<ConvenioModel>> resposta = new ResponseModel<List<ConvenioModel>>();

        try
        {
            var query = _context.Convenio.AsQueryable();

            query = query.Where(x =>
                (!codigoFiltro.HasValue || x.Id == codigoFiltro) &&
                (string.IsNullOrEmpty(nomeFiltro) || x.Nome == nomeFiltro) &&
                (string.IsNullOrEmpty(telefoneFiltro) || x.Telefone == telefoneFiltro) &&
                (string.IsNullOrEmpty(registroAvsFiltro) || x.RegistroAvs == registroAvsFiltro)
            );
                
            query = query.OrderBy(x => x.Id);

            resposta.Dados = paginar ? (await PaginationHelper.PaginateAsync(query, pageNumber, pageSize)).Dados : await query.ToListAsync();
            resposta.Mensagem = "Todos os Convenio foram encontrados";
            
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