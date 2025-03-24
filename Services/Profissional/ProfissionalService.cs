
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Dto.Agenda;
using WebApiSmartClinic.Dto.Profissional;
using WebApiSmartClinic.Dto.User;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Services.Agenda;
using WebApiSmartClinic.Services.Auth;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebApiSmartClinic.Services.Profissional;

public class ProfissionalService : IProfissionalInterface
{
    private readonly AppDbContext _context;
    private readonly IServiceProvider _serviceProvider;
    public ProfissionalService(AppDbContext context, IServiceProvider serviceProvider)
    {
        _context = context;
        _serviceProvider = serviceProvider;
    }

    public async Task CriarUsuario(UserCreateRequest userCreateRequest, string? userKey = null)
    {
        var authService = _serviceProvider.GetRequiredService<AuthService>();
        await authService.RegisterAsync(userCreateRequest, userKey);
    }

    public async Task<ResponseModel<List<ProfissionalModel>>> Criar(ProfissionalCreateDto profissionalCreateDto, int pageNumber = 1, int pageSize = 10, string? userKey = null)
    {
        ResponseModel<List<ProfissionalModel>> resposta = new ResponseModel<List<ProfissionalModel>>();

        try
        {
            var profissional = new ProfissionalModel();

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
            profissional.TipoPagamento = profissionalCreateDto.TipoPagamento;
            profissional.ChavePix = profissionalCreateDto.ChavePix;
            profissional.BancoNome = profissionalCreateDto.BancoNome;
            profissional.BancoAgencia = profissionalCreateDto.BancoAgencia;
            profissional.BancoConta = profissionalCreateDto.BancoConta;
            profissional.BancoTipoConta = profissionalCreateDto.BancoTipoConta;
            profissional.BancoCpfTitular = profissionalCreateDto.BancoCpfTitular;
            profissional.EhUsuario = profissionalCreateDto.EhUsuario;
            profissional.DataCadastro = profissionalCreateDto.DataCadastro;

            if (profissionalCreateDto.EhUsuario)
            {
                var userCreateRequest = new UserCreateRequest
                {
                    Email = profissionalCreateDto.Email,
                    Password = profissionalCreateDto.Password,
                    AcceptTerms = true,
                    FirstName = profissionalCreateDto.Nome,
                    LastName = profissionalCreateDto.Sobrenome,
                };

                var authService = _serviceProvider.GetRequiredService<AuthService>();
                var result = await authService.RegisterAsync(userCreateRequest, userKey);

                //var result = await _authService.RegisterAsync(userCreateRequest, userKey);
                var successProp = result.GetType().GetProperty("success");
                bool success = successProp != null && (bool)successProp.GetValue(result, null)!;

                if (!success)
                {
                    resposta.Status = false;
                    resposta.Mensagem = "Falha ao criar usuário.";
            
                    return resposta;
                }
            }

            _context.Add(profissional);
            await _context.SaveChangesAsync();

            var query = _context.Profissional.AsQueryable();

            resposta = await PaginationHelper.PaginateAsync(query, pageNumber, pageSize);
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

    public async Task<ResponseModel<List<ProfissionalModel>>> Delete(int idProfissional, int pageNumber = 1, int pageSize = 10)
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
            var query = _context.Profissional.AsQueryable();

            resposta = await PaginationHelper.PaginateAsync(query, pageNumber, pageSize);
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

    public async Task<ResponseModel<List<ProfissionalModel>>> Editar(ProfissionalEdicaoDto profissionalEdicaoDto, int pageNumber = 1, int pageSize = 10)
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
            profissional.TipoPagamento = profissionalEdicaoDto.TipoPagamento;
            profissional.ChavePix = profissionalEdicaoDto.ChavePix;
            profissional.BancoNome = profissionalEdicaoDto.BancoNome;
            profissional.BancoAgencia = profissionalEdicaoDto.BancoAgencia;
            profissional.BancoConta = profissionalEdicaoDto.BancoConta;
            profissional.BancoTipoConta = profissionalEdicaoDto.BancoTipoConta;
            profissional.BancoCpfTitular = profissionalEdicaoDto.BancoCpfTitular;
            profissional.EhUsuario = profissionalEdicaoDto.EhUsuario;
            profissional.DataCadastro = profissionalEdicaoDto.DataCadastro;

            _context.Update(profissional);
            await _context.SaveChangesAsync();
            
            var query = _context.Profissional.AsQueryable();

            resposta = await PaginationHelper.PaginateAsync(query, pageNumber, pageSize);
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

    public async Task<ResponseModel<List<ProfissionalModel>>> Listar(int pageNumber = 1, int pageSize = 10, int? codigoFiltro = null, string? nomeFiltro = null, string? cpfFiltro = null, int? profissaoIdFiltro = null, bool paginar = true)
    {
        ResponseModel<List<ProfissionalModel>> resposta = new ResponseModel<List<ProfissionalModel>>();

        try
        {
            var query = _context.Profissional.AsQueryable();

            // Aplicar filtros
            query = query.Where(x =>
                (!codigoFiltro.HasValue || x.Id == codigoFiltro.Value) &&
                (!profissaoIdFiltro.HasValue || x.ProfissaoId == profissaoIdFiltro.Value) &&
                (string.IsNullOrEmpty(nomeFiltro) || x.Nome.Contains(nomeFiltro)) &&
                (string.IsNullOrEmpty(cpfFiltro) || x.Cpf.Contains(cpfFiltro))
            );

            // Ordenação padrão
            query = query.OrderBy(x => x.Id);

            // Paginação opcional
            resposta = paginar ? await PaginationHelper.PaginateAsync(query, pageNumber, pageSize) : new ResponseModel<List<ProfissionalModel>> { Dados = await query.ToListAsync() };

            resposta.Mensagem = "Profissionais encontrados com sucesso.";
            resposta.Status = true;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = $"Erro ao buscar profissionais: {ex.Message}";
            resposta.Status = false;
        }

        return resposta;
    }
}