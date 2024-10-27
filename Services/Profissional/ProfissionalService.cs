
using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Dto.Profissional;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Profissional;

public class ProfissionalService : IProfissionalInterface
{
    private readonly AppDbContext _context;
    public ProfissionalService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseModel<ProfissionalModel>> BuscarPorId(int idProfissional)
    {
        ResponseModel<ProfissionalModel> resposta = new ResponseModel<ProfissionalModel>();
        try
        {
            var profissional = await _context.Profissional.FirstOrDefaultAsync(x => x.Id == idProfissional);
            if (profissional == null)
            {
                resposta.Mensagem = "Nenhum Profissional encontrado";
                return resposta;
            }

            resposta.Dados = profissional;
            resposta.Mensagem = "Profissional Encontrado";
            return resposta;
        }
        catch (Exception ex)
        {

            resposta.Mensagem = "Erro ao buscar Profissional";
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<ProfissionalModel>>> Criar(ProfissionalCreateDto profissionalCreateDto)
    {
        ResponseModel<List<ProfissionalModel>> resposta = new ResponseModel<List<ProfissionalModel>>();

        try
        {
            var profissional = new ProfissionalModel();

            // Atualizar para o código de acordo com o necessário
            //profissional.Profissional = profissionalCreateDto.Profissional;

            profissional.Email = profissionalCreateDto.Email;
            profissional.Nome = profissionalCreateDto.Nome;
            profissional.Cpf = profissionalCreateDto.Cpf;
            profissional.Celular = profissionalCreateDto.Celular;
            profissional.Sexo = profissionalCreateDto.Sexo;
            profissional.ConselhoId = profissionalCreateDto.ConselhoId;
            profissional.RegistroConselho = profissionalCreateDto.RegistroConselho;
            profissional.UfConselho = profissionalCreateDto.UfConselho;
            profissional.ProfissaoId = profissionalCreateDto.ProfissaoId;
            profissional.Cbo = profissionalCreateDto.Cbo;
            profissional.Rqe = profissionalCreateDto.Rqe;
            profissional.Cnes = profissionalCreateDto.Cnes;


            _context.Add(profissional);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Profissional.ToListAsync();
            resposta.Mensagem = "Profissional criado com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }

    }

    public async Task<ResponseModel<List<ProfissionalModel>>> Delete(int idProfissional)
    {
        ResponseModel<List<ProfissionalModel>> resposta = new ResponseModel<List<ProfissionalModel>>();

        try
        {
            var profissional = await _context.Profissional.FirstOrDefaultAsync(x => x.Id == idProfissional);
            if (profissional == null)
            {
                resposta.Mensagem = "Nenhum Profissional encontrado";
                return resposta;
            }

            _context.Remove(profissional);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Profissional.ToListAsync();
            resposta.Mensagem = "Profissional Excluido com sucesso";
            return resposta;

        }
        catch (Exception ex)
        {

            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<ProfissionalModel>>> Editar(ProfissionalEdicaoDto profissionalEdicaoDto)
    {
        ResponseModel<List<ProfissionalModel>> resposta = new ResponseModel<List<ProfissionalModel>>();

        try
        {
            var profissional = _context.Profissional.FirstOrDefault(x => x.Id == profissionalEdicaoDto.Id);
            if (profissional == null)
            {
                resposta.Mensagem = "Profissional não encontrado";
                return resposta;
            }

            profissional.Id = profissionalEdicaoDto.Id;
            profissional.Email = profissionalEdicaoDto.Email;
            profissional.Nome = profissionalEdicaoDto.Nome;
            profissional.Cpf = profissionalEdicaoDto.Cpf;
            profissional.Celular = profissionalEdicaoDto.Celular;
            profissional.Sexo = profissionalEdicaoDto.Sexo;
            profissional.ConselhoId = profissionalEdicaoDto.ConselhoId;
            profissional.RegistroConselho = profissionalEdicaoDto.RegistroConselho;
            profissional.UfConselho = profissionalEdicaoDto.UfConselho;
            profissional.ProfissaoId = profissionalEdicaoDto.ProfissaoId;
            profissional.Cbo = profissionalEdicaoDto.Cbo;
            profissional.Rqe = profissionalEdicaoDto.Rqe;
            profissional.Cnes = profissionalEdicaoDto.Cnes;

            _context.Update(profissional);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Profissional.ToListAsync();
            resposta.Mensagem = "Profissional Atualizado com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {

            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<ProfissionalModel>>> Listar()
    {
        ResponseModel<List<ProfissionalModel>> resposta = new ResponseModel<List<ProfissionalModel>>();

        try
        {
            var profissional = await _context.Profissional.ToListAsync();

            resposta.Dados = profissional;
            resposta.Mensagem = "Todos os Profissional foram encontrados";
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