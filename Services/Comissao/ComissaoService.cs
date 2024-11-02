
using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Dto.Comissao;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Comissao;

public class ComissaoService : IComissaoInterface
{
    private readonly AppDbContext _context;
    public ComissaoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseModel<ComissaoModel>> BuscarPorId(int idComissao)
    {
        ResponseModel<ComissaoModel> resposta = new ResponseModel<ComissaoModel>();
        try
        {
            var comissao = await _context.Comissao.FirstOrDefaultAsync(x => x.Id == idComissao);
            if (comissao == null)
            {
                resposta.Mensagem = "Nenhum Comissao encontrado";
                return resposta;
            }

            resposta.Dados = comissao;
            resposta.Mensagem = "Comissao Encontrado";
            return resposta;
        }
        catch (Exception ex)
        {

            resposta.Mensagem = "Erro ao buscar Comissao";
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<ComissaoModel>>> Criar(ComissaoCreateDto comissaoCreateDto)
    {
        ResponseModel<List<ComissaoModel>> resposta = new ResponseModel<List<ComissaoModel>>();

        try
        {
            var comissao = new ComissaoModel
            {
                ProfissionalId = comissaoCreateDto.ProfissionalId,
                ProcedimentoId = comissaoCreateDto.ProcedimentoId,
                DataAtendimento = comissaoCreateDto.DataAtendimento,
                ValorProcedimento = comissaoCreateDto.ValorProcedimento,
                Pago = false // Inicia como não pago
            };

            // Define o percentual de comissão padrão, por exemplo, 20%
            decimal percentualComissao = await ObterPercentualComissao(comissaoCreateDto.ProcedimentoId);

            // Calcula o valor da comissão
            decimal valorComissao = comissao.ValorProcedimento * (percentualComissao / 100);

            comissao.PercentualComissao = percentualComissao;
            comissao.ValorComissao = valorComissao;

            _context.Comissao.Add(comissao);
            await _context.SaveChangesAsync();

            // Opcional: para retornar apenas a comissão recém-criada
            resposta.Dados = await _context.Comissao.ToListAsync();

            resposta.Mensagem = "Comissão criada com sucesso";
            resposta.Status = true;
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<decimal> ObterPercentualComissao(int procedimentoId)
    {
        var procedimento = await _context.Procedimento
            .FirstOrDefaultAsync(p => p.Id == procedimentoId);

        if (procedimento == null)
        {
            throw new Exception("Procedimento não encontrado.");
        }

        // Retorna o percentual de comissão configurado no procedimento
        // Se o valor for zero, podemos retornar um valor padrão (ex.: 20%)
        return procedimento.PercentualComissao > 0 ? procedimento.PercentualComissao : 20;
    }

    public async Task<ResponseModel<List<ComissaoModel>>> ObterComissoesPendentes(int profissionalId)
    {
        try
        {
            var comissoesPendentes = await _context.Comissao
                .Where(c => c.ProfissionalId == profissionalId && !c.Pago)
                .ToListAsync();

            return new ResponseModel<List<ComissaoModel>>
            {
                Dados = comissoesPendentes,
                Mensagem = "Comissões pendentes obtidas com sucesso.",
                Status = true
            };
        }
        catch (Exception ex)
        {
            return new ResponseModel<List<ComissaoModel>>
            {
                Mensagem = ex.Message,
                Status = false
            };
        }
    }

    public async Task<ResponseModel<ComissaoModel>> PagarComissao(int comissaoId)
    {
        ResponseModel<ComissaoModel> resposta = new ResponseModel<ComissaoModel>();
       
        try
        {
            var comissao = await _context.Comissao.FindAsync(comissaoId);
            
            if (comissao == null || comissao.Pago)
            {
                resposta.Mensagem = "Comissão já paga ou não encontrada.";
                resposta.Status = false;
               
                return resposta;
            }
            
            comissao.Pago = true;
            comissao.DataPagamento = DateTime.Now;

            _context.Comissao.Update(comissao);
            await _context.SaveChangesAsync();
           
            resposta.Dados = comissao;
            resposta.Mensagem = "Comissão paga com sucesso.";
            resposta.Status = true;
           
            return resposta;
        }
        catch (Exception ex)
        {

            resposta.Mensagem = ex.Message;
            resposta.Status = false;
           
            return resposta;
        }
    }

    public async Task<ResponseModel<List<ComissaoModel>>> Delete(int idComissao)
    {
        ResponseModel<List<ComissaoModel>> resposta = new ResponseModel<List<ComissaoModel>>();

        try
        {
            var comissao = await _context.Comissao.FirstOrDefaultAsync(x => x.Id == idComissao);
            if (comissao == null)
            {
                resposta.Mensagem = "Nenhum Comissao encontrado";
                return resposta;
            }

            _context.Remove(comissao);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Comissao.ToListAsync();
            resposta.Mensagem = "Comissao Excluido com sucesso";
            return resposta;

        }
        catch (Exception ex)
        {

            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<ComissaoModel>>> Editar(ComissaoEdicaoDto comissaoEdicaoDto)
    {
        ResponseModel<List<ComissaoModel>> resposta = new ResponseModel<List<ComissaoModel>>();

        try
        {
            var comissao = _context.Comissao.FirstOrDefault(x => x.Id == comissaoEdicaoDto.Id);
            if (comissao == null)
            {
                resposta.Mensagem = "Comissao não encontrado";
                return resposta;
            }

            comissao.ProfissionalId = comissaoEdicaoDto.ProfissionalId;
            comissao.ProcedimentoId = comissaoEdicaoDto.ProcedimentoId;
            comissao.DataAtendimento = comissaoEdicaoDto.DataAtendimento;
            comissao.ValorProcedimento = comissaoEdicaoDto.ValorProcedimento;
            comissao.Pago = false;

            // Define o percentual de comissão padrão, por exemplo, 20%
            decimal percentualComissao = await ObterPercentualComissao(comissaoEdicaoDto.ProcedimentoId);

            // Calcula o valor da comissão
            decimal valorComissao = comissao.ValorProcedimento * (percentualComissao / 100);

            comissao.PercentualComissao = percentualComissao;
            comissao.ValorComissao = valorComissao;

            _context.Update(comissao);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Comissao.ToListAsync();
            resposta.Mensagem = "Comissao Atualizado com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {

            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<ComissaoModel>>> Listar()
    {
        ResponseModel<List<ComissaoModel>> resposta = new ResponseModel<List<ComissaoModel>>();

        try
        {
            var comissao = await _context.Comissao.ToListAsync();

            resposta.Dados = comissao;
            resposta.Mensagem = "Todos os Comissao foram encontrados";
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