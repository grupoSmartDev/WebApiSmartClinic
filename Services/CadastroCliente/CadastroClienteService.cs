
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Dto.CadastroCliente;
using WebApiSmartClinic.Dto.User;
using WebApiSmartClinic.Helpers;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Services.ConnectionsService;

namespace WebApiSmartClinic.Services.CadastroCliente;

public class CadastroClienteService : ICadastroClienteInterface
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly AppDbContext _context;
    private readonly AppSettings _appSettings;
    private const long TamanhoMaximoFotoPerfilEmBytes = 10L * 1024 * 1024; // 10 MB
    private readonly IConnectionsService _connectionsService;
    private readonly IConnectionStringProvider _connectionStringProvider;

    public CadastroClienteService(SignInManager<User> signInManager, UserManager<User> userManager, AppDbContext identityContext, IOptions<AppSettings> appSettings, IConnectionsService connectionsService, IConnectionStringProvider connectionStringProvider)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _context = identityContext;
        _appSettings = appSettings.Value;
        _connectionsService = connectionsService;
        _connectionStringProvider = connectionStringProvider;
    }

    //private readonly AppDbContext _context;
    //public CadastroClienteService(AppDbContext context)
    //{
    //    _context = context;
    //}

    public async Task<ResponseModel<CadastroClienteModel>> BuscarPorId(int idCadastroCliente)
    {
        ResponseModel<CadastroClienteModel> resposta = new ResponseModel<CadastroClienteModel>();
        try
        {
            var cadastrocliente = await _context.CadastroCliente.FirstOrDefaultAsync(x => x.Id == idCadastroCliente);
            if (cadastrocliente == null)
            {
                resposta.Mensagem = "Nenhum CadastroCliente encontrado";
                return resposta;
            }

            resposta.Dados = cadastrocliente;
            resposta.Mensagem = "CadastroCliente Encontrado";
            return resposta;
        }
        catch (Exception ex)
        {

            resposta.Mensagem = "Erro ao buscar CadastroCliente";
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<CadastroClienteModel>>> Criar(CadastroClienteCreateDto cadastroclienteCreateDto)
    {
        ResponseModel<List<CadastroClienteModel>> resposta = new ResponseModel<List<CadastroClienteModel>>();

        try
        {
            // 1) VALIDAÇÃO BÁSICA E PRÉ-CADASTRO:
            // Exemplo: Verificar se já existe um cadastro com o mesmo Email ou CPF (ou CNPJ).
            bool jaExisteCadastro = await _context.CadastroCliente
                .AnyAsync(c => c.Email == cadastroclienteCreateDto.Email
                            || c.TitularCPF == cadastroclienteCreateDto.TitularCPF);

            if (jaExisteCadastro)
            {
                resposta.Mensagem = "Já existe um cliente cadastrado com este e-mail ou CPF.";
                return resposta;
            }

            // 2) DEFINIR O PLANO E SUAS RESTRIÇÕES:
            // Você pode ter uma tabela ou enum que defina as restrições de cada plano.
            // Aqui simplificamos com if/else, mas você pode buscar do banco, por exemplo:
            int qtdeEmpresaPermitida;
            int qtdeUsuariosPermitidaPorEmpresa;

            switch (cadastroclienteCreateDto.PlanoEscolhido.ToLower())
            {
                case "1":
                    qtdeEmpresaPermitida = 1;
                    qtdeUsuariosPermitidaPorEmpresa = 3;
                    break;
                case "2":
                    qtdeEmpresaPermitida = 3;
                    qtdeUsuariosPermitidaPorEmpresa = 5;
                    break;
                case "3":
                    qtdeEmpresaPermitida = 999999;
                    qtdeUsuariosPermitidaPorEmpresa = 999999;
                    break;
                default:
                    resposta.Mensagem = "Plano inválido. Escolha entre: Basic, Plus ou Premium.";
                    return resposta;
            }

            // 3) DEFINIÇÃO DE DATAS E PERÍODO DE TESTE (7 dias):
            // Se estiver em período de teste, definimos DataInicioTeste e DataFim como +7 dias.
            // É comum colocar DataInicio = "hoje" (UTC) e DataFim = null caso seja uma assinatura mensal.
            var dataAtualUtc = DateTime.UtcNow;

            var novoCadastro = new CadastroClienteModel
            {
                Nome = cadastroclienteCreateDto.Nome,
                Sobrenome = cadastroclienteCreateDto.Sobrenome,
                Email = cadastroclienteCreateDto.Email,
                Celular = cadastroclienteCreateDto.Celular,
                TitularCPF = cadastroclienteCreateDto.TitularCPF,
                CNPJEmpresaMatriz = cadastroclienteCreateDto.CNPJEmpresaMatriz,
                Especialidade = cadastroclienteCreateDto.Especialidade,
                PlanoEscolhido = cadastroclienteCreateDto.PlanoEscolhido,

                // Define a forma de pagamento escolhida
                TipoPagamentoId = cadastroclienteCreateDto.TipoPagamentoId,

                // Definição de licenças permitidas com base no plano
                QtdeLicencaEmpresaPermitida = qtdeEmpresaPermitida,
                QtdeLicencaUsuarioPermitida = qtdeUsuariosPermitidaPorEmpresa,

                // Como está no início, nenhuma licença foi utilizada ainda
                QtdeLicencaEmpresaUtilizada = 0,
                QtdeLicencaUsuarioUtilizada = 0,

                // Caso você já queira iniciar a contagem de datas do plano:
                DataInicio = dataAtualUtc,
                PeriodoTeste = true,
                DataInicioTeste = dataAtualUtc,
                DataFim = dataAtualUtc.AddDays(7), // período de teste de 7 dias
                Ativo = true, // inicia ativo durante o período de teste
            };

            // 4) CRIAR O CADASTRO (CLIENTE) NO BANCO VIA EF
            _context.CadastroCliente.Add(novoCadastro);
            await _context.SaveChangesAsync();

            // 5) CRIAR O USUÁRIO MASTER RELACIONADO
            // Idealmente, você teria uma tabela de Usuários do sistema.
            // Nesse passo, você registra o "usuário master" com as credenciais iniciais.

            // ****---- CONTINUAR DAQUIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIII
            
            //.Add(usuarioMaster);
            await _context.SaveChangesAsync();

            // 6) MONTANDO A RESPOSTA
            resposta.Dados.Add(novoCadastro);
            resposta.Status = true;
            resposta.Mensagem = "Cadastro criado com sucesso. Usuário Master foi definido!";
            return resposta;
        }
        catch (Exception ex)
        {
            // 7) TRATAMENTO DE ERROS
            resposta.Mensagem = $"Ocorreu um erro ao criar o cadastro: {ex.Message}";
            return resposta;
        }

        //try
        //{
        //    var cadastrocliente = new CadastroClienteModel();

        //    if (cadastrocliente.PeriodoTeste && cadastrocliente.DataInicio > cadastrocliente.DataInicioTeste.Value.AddDays(7))
        //    {

        //    }

        //    // Atualizar para o código de acordo com o necessário
        //    if (!cadastrocliente.PeriodoTeste)
        //    {
        //        cadastrocliente.DataInicio = cadastroclienteCreateDto.DataInicio;
        //        cadastrocliente.DataFim = cadastroclienteCreateDto.DataFim;
        //    }
        //    else
        //    {
        //        cadastrocliente.DataInicioTeste = cadastroclienteCreateDto.DataInicioTeste;
        //    }
        //    if (cadastrocliente.)
        //    {

        //    }
        //    cadastrocliente.PlanoEscolhido = cadastroclienteCreateDto.PlanoEscolhido;

        //    _context.Add(cadastrocliente);
        //    await _context.SaveChangesAsync();

        //    resposta.Dados = await _context.CadastroCliente.ToListAsync();
        //    resposta.Mensagem = "Cadastro realizado com sucesso";
        //    return resposta;
        //}
        //catch (Exception ex)
        //{
        //    resposta.Mensagem = ex.Message;
        //    resposta.Status = false;
        //    return resposta;
        //}

    }

    public async Task<ResponseModel<List<CadastroClienteModel>>> Delete(int idCadastroCliente)
    {
        ResponseModel<List<CadastroClienteModel>> resposta = new ResponseModel<List<CadastroClienteModel>>();

        try
        {
            var cadastrocliente = await _context.CadastroCliente.FirstOrDefaultAsync(x => x.Id == idCadastroCliente);
            if (cadastrocliente == null)
            {
                resposta.Mensagem = "Nenhum CadastroCliente encontrado";
                return resposta;
            }

            _context.Remove(cadastrocliente);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.CadastroCliente.ToListAsync();
            resposta.Mensagem = "CadastroCliente Excluido com sucesso";
            return resposta;

        }
        catch (Exception ex)
        {

            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<CadastroClienteModel>>> Editar(CadastroClienteEdicaoDto cadastroclienteEdicaoDto)
    {
        ResponseModel<List<CadastroClienteModel>> resposta = new ResponseModel<List<CadastroClienteModel>>();

        try
        {
            var cadastrocliente = _context.CadastroCliente.FirstOrDefault(x => x.Id == cadastroclienteEdicaoDto.Id);
            if (cadastrocliente == null)
            {
                resposta.Mensagem = "CadastroCliente não encontrado";
                return resposta;
            }

            // Atualizar para o código de acordo com o necessário
            //cadastrocliente.CadastroCliente = cadastroclienteEdicaoDto.CadastroCliente;

            _context.Update(cadastrocliente);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.CadastroCliente.ToListAsync();
            resposta.Mensagem = "CadastroCliente Atualizado com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {

            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<CadastroClienteModel>>> Listar()
    {
        ResponseModel<List<CadastroClienteModel>> resposta = new ResponseModel<List<CadastroClienteModel>>();

        try
        {
            var cadastrocliente = await _context.CadastroCliente.ToListAsync();

            resposta.Dados = cadastrocliente;
            resposta.Mensagem = "Todos os CadastroCliente foram encontrados";
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