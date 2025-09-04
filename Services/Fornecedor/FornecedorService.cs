
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.ConstrainedExecution;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Dto.Fornecedor;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Fornecedor;

public class FornecedorService : IFornecedorInterface
{
    private readonly AppDbContext _context;
    public FornecedorService(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<ResponseModel<FornecedorModel>> BuscarPorId(int idFornecedor)
    {
        ResponseModel<FornecedorModel> resposta = new ResponseModel<FornecedorModel>();
        try
        {
            var fornecedor = await _context.Fornecedor.FirstOrDefaultAsync(x => x.Id == idFornecedor);
            if (fornecedor == null)
            {
                resposta.Mensagem = "Nenhum fornecedor encontrado";
                return resposta;
            }

            resposta.Dados = fornecedor;
            resposta.Mensagem = "Fornecedor encontrado";
            return resposta;
        }
        catch (Exception ex)
        {

            resposta.Mensagem = "Erro ao buscar fornecedor";
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<FornecedorModel>>> Criar(FornecedorCreateDto fornecedorCreateDto, int pageNumber = 1, int pageSize = 10)
    {
        ResponseModel<List<FornecedorModel>> resposta = new ResponseModel<List<FornecedorModel>>();
        try
        {
            // Validação para verificar se já existe fornecedor com o CPF ou CNPJ informado
            bool fornecedorExiste = false;
            string mensagemDuplicata = "";

            if (!string.IsNullOrWhiteSpace(fornecedorCreateDto.CPF))
            {
                var fornecedorComCPF = await _context.Fornecedor
                    .FirstOrDefaultAsync(f => f.CPF == fornecedorCreateDto.CPF && f.Ativo);
                if (fornecedorComCPF != null)
                {
                    fornecedorExiste = true;
                    mensagemDuplicata = $"Já existe um fornecedor ativo cadastrado com o CPF: {fornecedorCreateDto.CPF}";
                }
            }

            if (!fornecedorExiste && !string.IsNullOrWhiteSpace(fornecedorCreateDto.CNPJ))
            {
                var fornecedorComCNPJ = await _context.Fornecedor
                    .FirstOrDefaultAsync(f => f.CNPJ == fornecedorCreateDto.CNPJ && f.Ativo);
                if (fornecedorComCNPJ != null)
                {
                    fornecedorExiste = true;
                    mensagemDuplicata = $"Já existe um fornecedor ativo cadastrado com o CNPJ: {fornecedorCreateDto.CNPJ}";
                }
            }

            // Se encontrou duplicata, retorna erro
            if (fornecedorExiste)
            {
                resposta.Mensagem = mensagemDuplicata;
                resposta.Status = false;
                return resposta;
            }

            // Criação do fornecedor
            var fornecedor = new FornecedorModel();

            // Dados básicos
            fornecedor.Razao = fornecedorCreateDto.Razao;
            fornecedor.Fantasia = fornecedorCreateDto.Fantasia;
            fornecedor.Nome = fornecedorCreateDto.Nome;
            fornecedor.Tipo = fornecedorCreateDto.Tipo;
            fornecedor.EstadoCivil = fornecedorCreateDto.EstadoCivil;
            fornecedor.Sexo = fornecedorCreateDto.Sexo;
            fornecedor.DataNascimento = fornecedorCreateDto.DataNascimento;

            // Documentos
            fornecedor.IE = fornecedorCreateDto.IE;
            fornecedor.IM = fornecedorCreateDto.IM;
            fornecedor.CPF = fornecedorCreateDto.CPF;
            fornecedor.CNPJ = fornecedorCreateDto.CNPJ;

            // Endereço
            fornecedor.Pais = fornecedorCreateDto.Pais;
            fornecedor.UF = fornecedorCreateDto.UF;
            fornecedor.Cidade = fornecedorCreateDto.Cidade;
            fornecedor.Bairro = fornecedorCreateDto.Bairro;
            fornecedor.Complemento = fornecedorCreateDto.Complemento;
            fornecedor.Logradouro = fornecedorCreateDto.Logradouro;
            fornecedor.NrLogradouro = fornecedorCreateDto.NrLogradouro;
            fornecedor.CEP = fornecedorCreateDto.CEP;

            // Contatos
            fornecedor.Celular = fornecedorCreateDto.Celular;
            fornecedor.TelefoneFixo = fornecedorCreateDto.TelefoneFixo;
            fornecedor.Email = fornecedorCreateDto.Email;

            // Dados bancários
            fornecedor.Banco = fornecedorCreateDto.Banco;
            fornecedor.Agencia = fornecedorCreateDto.Agencia;
            fornecedor.Conta = fornecedorCreateDto.Conta;
            fornecedor.TipoPIX = fornecedorCreateDto.TipoPIX;
            fornecedor.ChavePIX = fornecedorCreateDto.ChavePIX;

            // Informações específicas da área da saúde
            fornecedor.CRF = fornecedorCreateDto.CRF;
            fornecedor.ANVISA = fornecedorCreateDto.ANVISA;
            fornecedor.CategoriaFornecedor = fornecedorCreateDto.CategoriaFornecedor;
            fornecedor.EspecialidadeFornecimento = fornecedorCreateDto.EspecialidadeFornecimento;

            // Representante
            fornecedor.Representante = fornecedorCreateDto.Representante;
            fornecedor.TelefoneRepresentante = fornecedorCreateDto.TelefoneRepresentante;
            fornecedor.EmailRepresentante = fornecedorCreateDto.EmailRepresentante;

            // Observações
            fornecedor.Observacao = fornecedorCreateDto.Observacao;

            // Auditoria e controle
            fornecedor.Ativo = true; // Sempre ativo ao criar
            fornecedor.DataAlteracao = DateTime.UtcNow; // Data de criação
                                                        // fornecedor.UsuarioCriacao = "usuarioLogado"; // Se tiver controle de usuário

            _context.Add(fornecedor);
            await _context.SaveChangesAsync();

            // Buscar apenas fornecedores ativos para a paginação
            var query = _context.Fornecedor.Where(f => f.Ativo == true).AsQueryable();
            resposta = await PaginationHelper.PaginateAsync(query, pageNumber, pageSize);
            resposta.Mensagem = "Fornecedor criado com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<FornecedorModel>>> Delete(int idFornecedor, int pageNumber = 1, int pageSize = 10)
    {
        ResponseModel<List<FornecedorModel>> resposta = new ResponseModel<List<FornecedorModel>>();
        try
        {
            var fornecedor = await _context.Fornecedor
                .Include(f => f.Financ_Pagar)
                .FirstOrDefaultAsync(x => x.Id == idFornecedor);

            if (fornecedor == null)
            {
                resposta.Mensagem = "Nenhum fornecedor encontrado";
                return resposta;
            }

            // Verificar se tem títulos a pagar vinculados
            if (fornecedor.Financ_Pagar != null && fornecedor.Financ_Pagar.Any())
            {
                resposta.Mensagem = "Não é possível inativar o fornecedor pois existem títulos a pagar vinculados a ele";
                resposta.Status = false;
                return resposta;
            }

            
            fornecedor.Ativo = false;
            fornecedor.DataAlteracao = DateTime.UtcNow;
            // fornecedor.UsuarioAlteracao = "usuarioLogado"; // Se tiver controle de usuário

            _context.Update(fornecedor);
            await _context.SaveChangesAsync();

            // Buscar apenas fornecedores ativos para a paginação
            var query = _context.Fornecedor.Where(f => f.Ativo == true).AsQueryable();
            resposta = await PaginationHelper.PaginateAsync(query, pageNumber, pageSize);
            resposta.Mensagem = "Fornecedor inativado com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<FornecedorModel>>> Editar(FornecedorEdicaoDto fornecedorEdicaoDto, int pageNumber = 1, int pageSize = 10)
    {
        ResponseModel<List<FornecedorModel>> resposta = new ResponseModel<List<FornecedorModel>>();
        try
        {
            var fornecedor = await _context.Fornecedor.FirstOrDefaultAsync(x => x.Id == fornecedorEdicaoDto.Id);
            if (fornecedor == null)
            {
                resposta.Mensagem = "Fornecedor não encontrado";
                resposta.Status = false;
                return resposta;
            }

            // Dados básicos
            fornecedor.Id = fornecedorEdicaoDto.Id;
            fornecedor.Razao = fornecedorEdicaoDto.Razao;
            fornecedor.Fantasia = fornecedorEdicaoDto.Fantasia;
            fornecedor.Nome = fornecedorEdicaoDto.Nome;
            fornecedor.Tipo = fornecedorEdicaoDto.Tipo;
            fornecedor.EstadoCivil = fornecedorEdicaoDto.EstadoCivil;
            fornecedor.Sexo = fornecedorEdicaoDto.Sexo;
            fornecedor.DataNascimento = fornecedorEdicaoDto.DataNascimento;

            // Documentos
            fornecedor.IE = fornecedorEdicaoDto.IE;
            fornecedor.IM = fornecedorEdicaoDto.IM;
            fornecedor.CPF = fornecedorEdicaoDto.CPF;
            fornecedor.CNPJ = fornecedorEdicaoDto.CNPJ;

            // Endereço
            fornecedor.Pais = fornecedorEdicaoDto.Pais;
            fornecedor.UF = fornecedorEdicaoDto.UF;
            fornecedor.Cidade = fornecedorEdicaoDto.Cidade;
            fornecedor.Bairro = fornecedorEdicaoDto.Bairro;
            fornecedor.Complemento = fornecedorEdicaoDto.Complemento;
            fornecedor.Logradouro = fornecedorEdicaoDto.Logradouro;
            fornecedor.NrLogradouro = fornecedorEdicaoDto.NrLogradouro;
            fornecedor.CEP = fornecedorEdicaoDto.CEP;

            // Contatos
            fornecedor.Celular = fornecedorEdicaoDto.Celular;
            fornecedor.TelefoneFixo = fornecedorEdicaoDto.TelefoneFixo;
            fornecedor.Email = fornecedorEdicaoDto.Email;

            // Dados bancários
            fornecedor.Banco = fornecedorEdicaoDto.Banco;
            fornecedor.Agencia = fornecedorEdicaoDto.Agencia;
            fornecedor.Conta = fornecedorEdicaoDto.Conta;
            fornecedor.TipoPIX = fornecedorEdicaoDto.TipoPIX;
            fornecedor.ChavePIX = fornecedorEdicaoDto.ChavePIX;

            // Informações específicas da área da saúde
            fornecedor.CRF = fornecedorEdicaoDto.CRF;
            fornecedor.ANVISA = fornecedorEdicaoDto.ANVISA;
            fornecedor.CategoriaFornecedor = fornecedorEdicaoDto.CategoriaFornecedor;
            fornecedor.EspecialidadeFornecimento = fornecedorEdicaoDto.EspecialidadeFornecimento;

            // Representante
            fornecedor.Representante = fornecedorEdicaoDto.Representante;
            fornecedor.TelefoneRepresentante = fornecedorEdicaoDto.TelefoneRepresentante;
            fornecedor.EmailRepresentante = fornecedorEdicaoDto.EmailRepresentante;

            // Observações
            fornecedor.Observacao = fornecedorEdicaoDto.Observacao;

            // Auditoria
            fornecedor.DataAlteracao = DateTime.UtcNow;
            // fornecedor.UsuarioAlteracao = "usuarioLogado"; // Se tiver controle de usuário

            _context.Update(fornecedor);
            await _context.SaveChangesAsync();

            // Buscar apenas fornecedores ativos para a paginação
            var query = _context.Fornecedor.Where(f => f.Ativo == true).AsQueryable();
            resposta = await PaginationHelper.PaginateAsync(query, pageNumber, pageSize);
            resposta.Mensagem = "Fornecedor atualizado com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<FornecedorModel>>> Listar(int pageNumber = 1, int pageSize = 10, int? codigoFiltro = null, string? nomeFiltro = null, string? cpfFiltro = null, string? cnpjFiltro = null, string? celularFiltro = null, bool paginar = true)
    {
        ResponseModel<List<FornecedorModel>> resposta = new ResponseModel<List<FornecedorModel>>();

        try
        {
            var query = _context.Fornecedor.AsQueryable();

            // Aplicar filtros
            query = query.Where(x =>
                (!codigoFiltro.HasValue || x.Id == codigoFiltro.Value) &&
                (string.IsNullOrEmpty(nomeFiltro) || x.Razao.ToLower().Contains(nomeFiltro.ToLower()) || x.Fantasia.ToLower().Contains(nomeFiltro.ToLower()) || x.Nome.ToLower().Contains(nomeFiltro.ToLower())) &&
                (string.IsNullOrEmpty(cpfFiltro) || x.CPF.Contains(cpfFiltro)) &&
                (string.IsNullOrEmpty(cnpjFiltro) || x.CNPJ.Contains(cnpjFiltro)) &&
                (string.IsNullOrEmpty(celularFiltro) || x.Celular.Contains(celularFiltro))
            );

            // Ordenação padrão
            query = query.OrderBy(x => x.Id);

            // Paginação opcional
            resposta = paginar ? await PaginationHelper.PaginateAsync(query, pageNumber, pageSize) : new ResponseModel<List<FornecedorModel>> { Dados = await query.ToListAsync() };
            resposta.Mensagem = "Todos os fornecedores foram encontrados";
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