
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.IdentityModel.Tokens;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Dto.Agenda;
using WebApiSmartClinic.Dto.Profissional;
using WebApiSmartClinic.Dto.User;
using WebApiSmartClinic.Helpers;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Services.Agenda;
using WebApiSmartClinic.Services.Auth;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebApiSmartClinic.Services.Profissional;

public class ProfissionalService : IProfissionalInterface
{
    private readonly AppDbContext _context;
    private readonly IServiceProvider _serviceProvider;
    private readonly DataConnectionContext _contextDataConnection;
    public ProfissionalService(AppDbContext context, IServiceProvider serviceProvider, DataConnectionContext contextDataConnection)
    {
        _context = context;
        _serviceProvider = serviceProvider;
        _contextDataConnection = contextDataConnection;
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

            var consultaProfissional = _context.Profissional.Where(x => x.Cpf == profissionalCreateDto.Cpf).ToArray();

            if(!consultaProfissional.Any())
            {
                var queryPesquisa = _context.Profissional.AsQueryable();

                resposta = await PaginationHelper.PaginateAsync(queryPesquisa, pageNumber, pageSize);
                resposta.Mensagem = "CPF já cadastrado, verifique novamente.";
                resposta.Status = false;

                return resposta;
            }

            var profissional = new ProfissionalModel();

            profissional.Email = profissionalCreateDto.Email;
            profissional.Nome = profissionalCreateDto.Nome;
            profissional.Sobrenome = profissionalCreateDto.Sobrenome;
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
            profissional.TipoComissao = profissionalCreateDto.TipoComissao;
            profissional.ValorComissao = profissionalCreateDto.ValorComissao;

            if (profissionalCreateDto.EhUsuario == true)
            { 
                var userCreateRequest = new UserCreateRequest
                {
                    Email = profissionalCreateDto.Email,
                    Password = "Admin@123",
                    ConfirmPassword = "Admin@123",
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

            profissional.Ativo = false;

            _context.Update(profissional);
            await _context.SaveChangesAsync();
            var query = _context.Profissional.AsQueryable();

            resposta = await PaginationHelper.PaginateAsync(query, pageNumber, pageSize);
            resposta.Mensagem = "Profissional Inativado com sucesso";
            return resposta;

        }
        catch (Exception ex)
        {

            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<ProfissionalModel>>> AtivarProfissional(ProfissionalEdicaoDto dto, int pageNumber = 1, int pageSize = 10, string? userKey = null )
    {
        ResponseModel<List<ProfissionalModel>> resposta = new ResponseModel<List<ProfissionalModel>>();

        try
        {
            var profissional = _context.Profissional.FirstOrDefault(x => x.Id == dto.Id);
            if (profissional == null)
            {
                resposta.Mensagem = "Profissional não encontrado";
                return resposta;
            }

            if (profissional.Ativo == true) {
                resposta.Mensagem = "Profissional já esta atívo";
                return resposta;
            }

            profissional.Ativo = true;

            _context.Update(profissional);
            await _context.SaveChangesAsync();
            var query = _context.Profissional.AsQueryable();

            resposta = await PaginationHelper.PaginateAsync(query, pageNumber, pageSize);
            resposta.Mensagem = "Profissional Ativo com sucesso";
            return resposta;

        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<ProfissionalModel>>> Editar(ProfissionalEdicaoDto profissionalEdicaoDto, int pageNumber = 1, int pageSize = 10, string? userKey = null)
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
            profissional.Sobrenome = profissionalEdicaoDto.Sobrenome;
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
            profissional.TipoComissao = profissionalEdicaoDto.TipoComissao;
            profissional.ValorComissao = profissionalEdicaoDto.ValorComissao;

            if (profissional.EhUsuario == true)
            {
                var existe = await _context.Usuario.AnyAsync(c => c.Email == profissionalEdicaoDto.Email);

                if (!existe)
                {
                    var userCreateRequest = new UserCreateRequest
                    {
                        Email = profissionalEdicaoDto.Email,
                        Password = "Admin@123",
                        ConfirmPassword = "Admin@123",
                        AcceptTerms = true,
                        FirstName = profissionalEdicaoDto.Nome,
                        LastName = profissionalEdicaoDto.Sobrenome,
                    };

                    var authService = _serviceProvider.GetRequiredService<AuthService>();
                    var result = await authService.RegisterAsync(userCreateRequest, userKey);
                    var successProp = result.GetType().GetProperty("success");
                    
                    bool success = successProp != null && (bool)successProp.GetValue(result, null)!;

                    if (!success)
                    {
                        resposta.Status = false;
                        resposta.Mensagem = "Falha ao criar usuário.";

                        //return resposta;
                    }
                }
            }

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

            if (!string.IsNullOrEmpty(codigoFiltro.ToString()))
                query = query.Where(p => p.Id == codigoFiltro);

            if (!string.IsNullOrEmpty(profissaoIdFiltro.ToString()))
                query = query.Where(p => p.ProfissaoId == profissaoIdFiltro);

            if (!string.IsNullOrEmpty(nomeFiltro))
                query = query.Where(p => p.Nome.ToLower().Contains(nomeFiltro.ToLower()));

            if (!string.IsNullOrEmpty(cpfFiltro))
                query = query.Where(p => p.Cpf.Trim().ToLower().Contains(cpfFiltro.Trim()));

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