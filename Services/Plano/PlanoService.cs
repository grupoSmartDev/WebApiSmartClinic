using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensions.Msal;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Dto.Plano;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Plano;

public class PlanoService : IPlanoInterface
{
    private readonly AppDbContext _context;
    public PlanoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseModel<PlanoModel>> BuscarPorId(int idPlano)
    {
        ResponseModel<PlanoModel> resposta = new ResponseModel<PlanoModel>();
        
        try
        {
            var plano = await _context.Plano.FirstOrDefaultAsync(x => x.Id == idPlano);
            if (plano == null)
            {
                resposta.Mensagem = "Nenhum Plano encontrado";
                return resposta;
            }

            resposta.Dados = plano;
            resposta.Mensagem = "Plano Encontrado";
            
            return resposta;
        }
        catch (Exception ex)
        {

            resposta.Mensagem = "Erro ao buscar Plano";
            resposta.Status = false;
            
            return resposta;
        }
    }

    public async Task<ResponseModel<List<PlanoModel>>> Criar(PlanoCreateDto planoCreateDto, int pageNumber = 1, int pageSize = 10)
    {
        ResponseModel<List<PlanoModel>> resposta = new ResponseModel<List<PlanoModel>>();

        try
        {
            var plano = new PlanoModel();

            plano.Descricao = planoCreateDto.Descricao;
            plano.TempoMinutos = planoCreateDto.TempoMinutos;
            plano.CentroCustoId = planoCreateDto.CentroCustoId;
            plano.DiasSemana = planoCreateDto.DiasSemana;
            plano.ValorBimestral = planoCreateDto.ValorBimestral;
            plano.ValorMensal = planoCreateDto.ValorMensal;
            plano.ValorTrimestral = planoCreateDto.ValorTrimestral;
            plano.ValorQuadrimestral = planoCreateDto.ValorQuadrimestral;
            plano.ValorSemestral = planoCreateDto.ValorSemestral;
            plano.ValorAnual = planoCreateDto.ValorAnual;
            plano.DataInicio = planoCreateDto.DataInicio;
            plano.DataFim = planoCreateDto.DataFim;
            plano.Ativo = planoCreateDto.Ativo;
            plano.PacienteId = planoCreateDto.PacienteId;
            plano.FinanceiroId = planoCreateDto.FinanceiroId;
            plano.TipoMes = planoCreateDto.TipoMes;

            

            _context.Add(plano);
            await _context.SaveChangesAsync();

            var query = _context.Plano.AsQueryable();

            resposta.Dados = (await PaginationHelper.PaginateAsync(query, pageNumber, pageSize)).Dados;
            resposta.Mensagem = "Plano criado com sucesso";
            
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            
            return resposta;
        }
    }

    public async Task<ResponseModel<List<PlanoModel>>> Delete(int idPlano, int pageNumber = 1, int pageSize = 10)
    {
        ResponseModel<List<PlanoModel>> resposta = new ResponseModel<List<PlanoModel>>();

        try
        {
            var plano = await _context.Plano.FirstOrDefaultAsync(x => x.Id == idPlano);
            if (plano == null)
            {
                resposta.Mensagem = "Nenhum Plano encontrado";
                return resposta;
            }

            _context.Remove(plano);
            await _context.SaveChangesAsync();

            var query = _context.Plano.AsQueryable();

            resposta.Dados = (await PaginationHelper.PaginateAsync(query, pageNumber, pageSize)).Dados;
            resposta.Mensagem = "Plano Excluido com sucesso";
            
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            
            return resposta;
        }
    }

    public async Task<ResponseModel<List<PlanoModel>>> Editar(PlanoEdicaoDto planoEdicaoDto, int pageNumber = 1, int pageSize = 10)
    {
        ResponseModel<List<PlanoModel>> resposta = new ResponseModel<List<PlanoModel>>();

        try
        {
            var plano = _context.Plano.FirstOrDefault(x => x.Id == planoEdicaoDto.Id);
            if (plano == null)
            {
                resposta.Mensagem = "Plano não encontrado";
                return resposta;
            }

            plano.Descricao = planoEdicaoDto.Descricao;
            plano.TempoMinutos = planoEdicaoDto.TempoMinutos;
            plano.CentroCustoId = planoEdicaoDto.CentroCustoId;
            plano.DiasSemana = planoEdicaoDto.DiasSemana;
            plano.ValorBimestral = planoEdicaoDto.ValorBimestral;
            plano.ValorMensal = planoEdicaoDto.ValorMensal;
            plano.ValorTrimestral = planoEdicaoDto.ValorTrimestral;
            plano.ValorQuadrimestral = planoEdicaoDto.ValorQuadrimestral;
            plano.ValorSemestral = planoEdicaoDto.ValorSemestral;
            plano.ValorAnual = planoEdicaoDto.ValorAnual;
            plano.DataInicio = planoEdicaoDto.DataInicio;
            plano.DataFim = planoEdicaoDto.DataFim;
            plano.Ativo = planoEdicaoDto.Ativo;
            plano.PacienteId = planoEdicaoDto.PacienteId;
            plano.FinanceiroId = planoEdicaoDto.FinanceiroId;
            plano.TipoMes = planoEdicaoDto.TipoMes;



            _context.Update(plano);
            await _context.SaveChangesAsync();

            var query = _context.Plano.AsQueryable();

            resposta.Dados = (await PaginationHelper.PaginateAsync(query, pageNumber, pageSize)).Dados;
            resposta.Mensagem = "Plano Atualizado com sucesso";
            
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            
            return resposta;
        }
    }

    public async Task<ResponseModel<List<PlanoModel>>> Listar(int pageNumber = 1, int pageSize = 10, int? codigoFiltro = null, string? descricaoFiltro = null, bool paginar = true)
    {
        ResponseModel<List<PlanoModel>> resposta = new ResponseModel<List<PlanoModel>>();

        try
        {
            var query = _context.Plano.AsQueryable();

            query = query.Where(x =>
                (!codigoFiltro.HasValue || x.Id == codigoFiltro) &&
                (string.IsNullOrEmpty(descricaoFiltro) || x.Descricao == descricaoFiltro)
            );

            query.OrderBy(x => x.Id);

            resposta.Dados = paginar ? (await PaginationHelper.PaginateAsync(query, pageNumber, pageSize)).Dados : await query.ToListAsync();
            resposta.Mensagem = "Planos listados com sucesso";

            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = "Erro ao listar planos: " + ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

}