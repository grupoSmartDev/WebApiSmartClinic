
using Microsoft.EntityFrameworkCore;
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
    
    public async Task<ResponseModel<FornecedorModel>> BuscarFornecedorPorId(int idFornecedor)
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

    public async Task<ResponseModel<List<FornecedorModel>>> CriarFornecedor(FornecedorCreateDto fornecedorCreateDto)
    {
        ResponseModel<List<FornecedorModel>> resposta = new ResponseModel<List<FornecedorModel>>();

        try
        {
            var fornecedor = new FornecedorModel();

            fornecedor.Razao = fornecedorCreateDto.Razao;
            fornecedor.Fantasia = fornecedorCreateDto.Fantasia;
            fornecedor.Tipo = fornecedorCreateDto.Tipo;
            fornecedor.EstadoCivil = fornecedorCreateDto.EstadoCivil;
            fornecedor.Sexo = fornecedorCreateDto.Sexo;
            fornecedor.IE = fornecedorCreateDto.IE;
            fornecedor.IM = fornecedorCreateDto.IM;
            fornecedor.CPF = fornecedorCreateDto.CPF;
            fornecedor.CNPJ = fornecedorCreateDto.CNPJ;
            fornecedor.Pais = fornecedorCreateDto.Pais;
            fornecedor.UF = fornecedorCreateDto.UF;
            fornecedor.Cidade = fornecedorCreateDto.Cidade;
            fornecedor.Bairro = fornecedorCreateDto.Bairro;
            fornecedor.Complemento = fornecedorCreateDto.Complemento;
            fornecedor.Logradouro = fornecedorCreateDto.Logradouro;
            fornecedor.NrLogradouro = fornecedorCreateDto.NrLogradouro;
            fornecedor.CEP = fornecedorCreateDto.CEP;
            fornecedor.Celular = fornecedorCreateDto.Celular;
            fornecedor.TelefoneFixo = fornecedorCreateDto.TelefoneFixo;
            fornecedor.Banco = fornecedorCreateDto.Banco;
            fornecedor.Agencia = fornecedorCreateDto.Agencia;
            fornecedor.Conta = fornecedorCreateDto.Conta;
            fornecedor.TipoPIX = fornecedorCreateDto.TipoPIX;
            fornecedor.ChavePIX = fornecedorCreateDto.ChavePIX;
            fornecedor.Email = fornecedorCreateDto.Email;
            fornecedor.Observacao = fornecedorCreateDto.Observacao;
            fornecedor.nome = fornecedorCreateDto.Nome;

            _context.Add(fornecedor);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Fornecedor.ToListAsync();
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

    public async Task<ResponseModel<List<FornecedorModel>>> DeleteFornecedor(int idFornecedor)
    {
        ResponseModel<List<FornecedorModel>> resposta = new ResponseModel<List<FornecedorModel>>();

        try
        {
            var fornecedor = await _context.Fornecedor.FirstOrDefaultAsync(x => x.Id == idFornecedor);
            if (fornecedor == null)
            {
                resposta.Mensagem = "Nenhum fornecedor encontrado";
                return resposta;
            }

            _context.Remove(fornecedor);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Fornecedor.ToListAsync();
            resposta.Mensagem = "Fornecedor excluído com sucesso";
            return resposta;

        }
        catch (Exception ex)
        {

            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<FornecedorModel>>> EditarFornecedor(FornecedorEdicaoDto fornecedorEdicaoDto)
    {
        ResponseModel<List<FornecedorModel>> resposta = new ResponseModel<List<FornecedorModel>>();

        try
        {
            var fornecedor = _context.Fornecedor.FirstOrDefault(x => x.Id == fornecedorEdicaoDto.Id);
            if (fornecedor == null)
            {
                resposta.Mensagem = "Fornecedor não encontrado";
                return resposta;
            }

            fornecedor.Id = fornecedorEdicaoDto.Id;
            fornecedor.Razao = fornecedorEdicaoDto.Razao;
            fornecedor.Fantasia = fornecedorEdicaoDto.Fantasia;
            fornecedor.Tipo = fornecedorEdicaoDto.Tipo;
            fornecedor.EstadoCivil = fornecedorEdicaoDto.EstadoCivil;
            fornecedor.Sexo = fornecedorEdicaoDto.Sexo;
            fornecedor.IE = fornecedorEdicaoDto.IE;
            fornecedor.IM = fornecedorEdicaoDto.IM;
            fornecedor.CPF = fornecedorEdicaoDto.CPF;
            fornecedor.CNPJ = fornecedorEdicaoDto.CNPJ;
            fornecedor.Pais = fornecedorEdicaoDto.Pais;
            fornecedor.UF = fornecedorEdicaoDto.UF;
            fornecedor.Cidade = fornecedorEdicaoDto.Cidade;
            fornecedor.Bairro = fornecedorEdicaoDto.Bairro;
            fornecedor.Complemento = fornecedorEdicaoDto.Complemento;
            fornecedor.Logradouro = fornecedorEdicaoDto.Logradouro;
            fornecedor.NrLogradouro = fornecedorEdicaoDto.NrLogradouro;
            fornecedor.CEP = fornecedorEdicaoDto.CEP;
            fornecedor.Celular = fornecedorEdicaoDto.Celular;
            fornecedor.TelefoneFixo = fornecedorEdicaoDto.TelefoneFixo;
            fornecedor.Banco = fornecedorEdicaoDto.Banco;
            fornecedor.Agencia = fornecedorEdicaoDto.Agencia;
            fornecedor.Conta = fornecedorEdicaoDto.Conta;
            fornecedor.TipoPIX = fornecedorEdicaoDto.TipoPIX;
            fornecedor.ChavePIX = fornecedorEdicaoDto.ChavePIX;
            fornecedor.Email = fornecedorEdicaoDto.Email;
            fornecedor.Observacao = fornecedorEdicaoDto.Observacao;
            fornecedor.nome = fornecedorEdicaoDto.Nome;

            _context.Update(fornecedor);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Fornecedor.ToListAsync();
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

    public async Task<ResponseModel<List<FornecedorModel>>> ListarFornecedor()
    {
        ResponseModel<List<FornecedorModel>> resposta = new ResponseModel<List<FornecedorModel>>();

        try
        {
            var fornecedor = await _context.Fornecedor.ToListAsync();

            resposta.Dados = fornecedor;
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