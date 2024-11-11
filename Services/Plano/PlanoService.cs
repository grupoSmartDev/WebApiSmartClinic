using Microsoft.EntityFrameworkCore;
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

    public async Task<ResponseModel<List<PlanoModel>>> Criar(PlanoCreateDto planoCreateDto)
    {
        ResponseModel<List<PlanoModel>> resposta = new ResponseModel<List<PlanoModel>>();

        try
        {
            var plano = new PlanoModel();

            plano.Descricao = planoCreateDto.Descricao;
            plano.TempoMinutos = planoCreateDto.TempoMinutos;
            plano.CentroCustoId = planoCreateDto.CentroCustoId;
            plano.TipoCobranca = planoCreateDto.TipoCobranca;
            plano.PlanoGratuito = planoCreateDto.PlanoGratuito;

            if (!plano.PlanoGratuito)
            {
                plano.ValorPlano = planoCreateDto.ValorPlano;
            }
            
            plano.SalaId = planoCreateDto.SalaId;
            plano.ProfissionalId = planoCreateDto.ProfissionalId;

            if (plano.PlanoBimestral)
            {
                plano.PlanoBimestral = planoCreateDto.PlanoBimestral;
                plano.ValorMesBimestral = planoCreateDto.ValorMesBimestral;
                plano.ValorTotalBimestral = planoCreateDto.ValorTotalBimestral;
                plano.DescontoMesBimestral = planoCreateDto.DescontoMesBimestral;
            }

            if (plano.PlanoTrimestral)
            {
                plano.PlanoTrimestral = planoCreateDto.PlanoTrimestral;
                plano.ValorMesTrimestral = planoCreateDto.ValorMesTrimestral;
                plano.ValorTotalTrimestral = planoCreateDto.ValorTotalTrimestral;
                plano.DescontoMesTrimestral = planoCreateDto.DescontoMesTrimestral;
            }

            if (plano.PlanoQuadrimestral)
            {
                plano.PlanoQuadrimestral = planoCreateDto.PlanoQuadrimestral;
                plano.ValorMesQuadrimestral = planoCreateDto.ValorMesQuadrimestral;
                plano.ValorTotalQuadrimestral = planoCreateDto.ValorTotalQuadrimestral;
                plano.DescontoMesQuadrimestral = planoCreateDto.DescontoMesQuadrimestral;
            }

            if (plano.PlanoSemestral)
            {
                plano.PlanoSemestral = planoCreateDto.PlanoSemestral;
                plano.ValorMesSemestral = planoCreateDto.ValorMesSemestral;
                plano.ValorTotalSemestral = planoCreateDto.ValorTotalSemestral;
                plano.DescontoMesSemestral = planoCreateDto.DescontoMesSemestral;
            }
            
            if (plano.PlanoAnual)
            {
                plano.PlanoAnual = planoCreateDto.PlanoAnual;
                plano.ValorMesAnual = planoCreateDto.ValorMesAnual;
                plano.ValorTotalAnual = planoCreateDto.ValorTotalAnual;
                plano.DescontoMesAnual = planoCreateDto.DescontoMesAnual;
            }

            _context.Add(plano);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Plano.ToListAsync();
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

    public async Task<ResponseModel<List<PlanoModel>>> Delete(int idPlano)
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

            resposta.Dados = await _context.Plano.ToListAsync();
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

    public async Task<ResponseModel<List<PlanoModel>>> Editar(PlanoEdicaoDto planoEdicaoDto)
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
            plano.TipoCobranca = planoEdicaoDto.TipoCobranca;
            plano.PlanoGratuito = planoEdicaoDto.PlanoGratuito;

            if (!plano.PlanoGratuito)
            {
                plano.ValorPlano = planoEdicaoDto.ValorPlano;
            }

            plano.SalaId = planoEdicaoDto.SalaId;
            plano.ProfissionalId = planoEdicaoDto.ProfissionalId;

            if (plano.PlanoBimestral)
            {
                plano.PlanoBimestral = planoEdicaoDto.PlanoBimestral;
                plano.ValorMesBimestral = planoEdicaoDto.ValorMesBimestral;
                plano.ValorTotalBimestral = planoEdicaoDto.ValorTotalBimestral;
                plano.DescontoMesBimestral = planoEdicaoDto.DescontoMesBimestral;
            }

            if (plano.PlanoTrimestral)
            {
                plano.PlanoTrimestral = planoEdicaoDto.PlanoTrimestral;
                plano.ValorMesTrimestral = planoEdicaoDto.ValorMesTrimestral;
                plano.ValorTotalTrimestral = planoEdicaoDto.ValorTotalTrimestral;
                plano.DescontoMesTrimestral = planoEdicaoDto.DescontoMesTrimestral;
            }

            if (plano.PlanoQuadrimestral)
            {
                plano.PlanoQuadrimestral = planoEdicaoDto.PlanoQuadrimestral;
                plano.ValorMesQuadrimestral = planoEdicaoDto.ValorMesQuadrimestral;
                plano.ValorTotalQuadrimestral = planoEdicaoDto.ValorTotalQuadrimestral;
                plano.DescontoMesQuadrimestral = planoEdicaoDto.DescontoMesQuadrimestral;
            }

            if (plano.PlanoSemestral)
            {
                plano.PlanoSemestral = planoEdicaoDto.PlanoSemestral;
                plano.ValorMesSemestral = planoEdicaoDto.ValorMesSemestral;
                plano.ValorTotalSemestral = planoEdicaoDto.ValorTotalSemestral;
                plano.DescontoMesSemestral = planoEdicaoDto.DescontoMesSemestral;
            }

            if (plano.PlanoAnual)
            {
                plano.PlanoAnual = planoEdicaoDto.PlanoAnual;
                plano.ValorMesAnual = planoEdicaoDto.ValorMesAnual;
                plano.ValorTotalAnual = planoEdicaoDto.ValorTotalAnual;
                plano.DescontoMesAnual = planoEdicaoDto.DescontoMesAnual;
            }

            _context.Update(plano);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Plano.ToListAsync();
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

    public async Task<ResponseModel<List<PlanoModel>>> Listar()
    {
        ResponseModel<List<PlanoModel>> resposta = new ResponseModel<List<PlanoModel>>();

        try
        {
            var plano = await _context.Plano.ToListAsync();

            resposta.Dados = plano;
            resposta.Mensagem = "Todos os Plano foram encontrados";
         
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