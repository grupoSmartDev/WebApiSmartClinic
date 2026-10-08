using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Dto.CRM;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.CRM;

public class CampanhaService : ICampanhaInterface
{
    private readonly AppDbContext _context;
    public CampanhaService(AppDbContext context)
    {
        _context = context;
    }

    private async Task<List<CampanhaModel>> ListarOrdenado() =>
        await _context.Campanhas.AsNoTracking().OrderBy(x => x.Nome).ToListAsync();

    public async Task<ResponseModel<List<CampanhaModel>>> Listar()
    {
        var resposta = new ResponseModel<List<CampanhaModel>>();
        try
        {
            resposta.Dados = await ListarOrdenado();
            resposta.TotalCount = resposta.Dados.Count;
            resposta.Mensagem = "Campanhas listadas com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<CampanhaModel>> BuscarPorId(int id)
    {
        var resposta = new ResponseModel<CampanhaModel>();
        try
        {
            var campanha = await _context.Campanhas.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (campanha == null)
            {
                resposta.Mensagem = "Campanha não encontrada";
                resposta.Status = false;
                return resposta;
            }

            resposta.Dados = campanha;
            resposta.Mensagem = "Campanha encontrada";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<CampanhaModel>>> Criar(CampanhaCreateDto dto)
    {
        var resposta = new ResponseModel<List<CampanhaModel>>();
        try
        {
            if (string.IsNullOrWhiteSpace(dto.Nome))
            {
                resposta.Mensagem = "O nome da campanha é obrigatório.";
                resposta.Status = false;
                return resposta;
            }

            var campanha = new CampanhaModel
            {
                Nome = dto.Nome.Trim(),
                Plataforma = dto.Plataforma,
                IdExterno = dto.IdExterno
            };

            _context.Campanhas.Add(campanha);
            await _context.SaveChangesAsync();

            resposta.Dados = await ListarOrdenado();
            resposta.Mensagem = "Campanha criada com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<CampanhaModel>>> Editar(CampanhaEdicaoDto dto)
    {
        var resposta = new ResponseModel<List<CampanhaModel>>();
        try
        {
            var campanha = await _context.Campanhas.FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (campanha == null)
            {
                resposta.Mensagem = "Campanha não encontrada";
                resposta.Status = false;
                return resposta;
            }

            if (string.IsNullOrWhiteSpace(dto.Nome))
            {
                resposta.Mensagem = "O nome da campanha é obrigatório.";
                resposta.Status = false;
                return resposta;
            }

            campanha.Nome = dto.Nome.Trim();
            campanha.Plataforma = dto.Plataforma;
            campanha.IdExterno = dto.IdExterno;

            _context.Campanhas.Update(campanha);
            await _context.SaveChangesAsync();

            resposta.Dados = await ListarOrdenado();
            resposta.Mensagem = "Campanha atualizada com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<CampanhaModel>>> Delete(int id)
    {
        var resposta = new ResponseModel<List<CampanhaModel>>();
        try
        {
            var campanha = await _context.Campanhas.FirstOrDefaultAsync(x => x.Id == id);
            if (campanha == null)
            {
                resposta.Mensagem = "Campanha não encontrada";
                resposta.Status = false;
                return resposta;
            }

            // Soft delete: leads que apontam para a campanha mantêm o histórico de origem
            _context.Campanhas.Remove(campanha);
            await _context.SaveChangesAsync();

            resposta.Dados = await ListarOrdenado();
            resposta.Mensagem = "Campanha excluída com sucesso";
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
