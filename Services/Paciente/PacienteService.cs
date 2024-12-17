
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Helpers;
using WebApiSmartClinic.Dto.Paciente;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Paciente;

public class PacienteService : IPacienteInterface
{
    private readonly AppDbContext _context;
    public PacienteService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseModel<PacienteModel>> BuscarPorId(int idPaciente)
    {
        ResponseModel<PacienteModel> resposta = new ResponseModel<PacienteModel>();
        try
        {
            var paciente = await _context.Paciente.FirstOrDefaultAsync(x => x.Id == idPaciente);
            if (paciente == null)
            {
                resposta.Mensagem = "Nenhum Paciente encontrado";
                return resposta;
            }

            resposta.Dados = paciente;
            resposta.Mensagem = "Paciente Encontrado";
            return resposta;
        }
        catch (Exception ex)
        {

            resposta.Mensagem = "Erro ao buscar Paciente";
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<PacienteModel>>> Criar(PacienteCreateDto pacienteCreateDto)
    {
        ResponseModel<List<PacienteModel>> resposta = new ResponseModel<List<PacienteModel>>();

        try
        {
            var cpfLimpo = Funcoes.RemoverCaracteres(pacienteCreateDto.Cpf);

            // Verificação de duplicidade do CPF no banco
            var cpfExistente = await _context.Paciente
                .AnyAsync(p => p.Cpf == cpfLimpo);

            if (cpfExistente)
            {
                resposta.Mensagem = "CPF já cadastrado, verifique.";
                return resposta;
            }

            // Criação do paciente com dados limpos
            var paciente = new PacienteModel
            {
                Bairro = pacienteCreateDto.Bairro,
                BreveDiagnostico = pacienteCreateDto.BreveDiagnostico,
                Celular = pacienteCreateDto.Celular,
                Cep = pacienteCreateDto.Cep,
                Cidade = pacienteCreateDto.Cidade,
                ComoConheceu = pacienteCreateDto.ComoConheceu,
                Complemento = pacienteCreateDto.Complemento,
                Cpf = cpfLimpo,
                DataNascimento = pacienteCreateDto.DataNascimento,
                Email = pacienteCreateDto.Email,
                Uf = pacienteCreateDto.Uf,
                EstadoCivil = pacienteCreateDto.EstadoCivil,
                Logradouro = pacienteCreateDto.Logradouro,
                Medicamento = pacienteCreateDto.Medicamento,
                ProfissionalId = pacienteCreateDto.ProfissionalId,
                Nome = pacienteCreateDto.Nome,
                Numero = pacienteCreateDto.Numero,
                Pais = pacienteCreateDto.Pais,
                PermitirLembretes = pacienteCreateDto.PermitirLembretes ?? false,
                PreferenciaDeContato = pacienteCreateDto.PreferenciaDeContato,
                Profissao = pacienteCreateDto.Profissao,
                Responsavel = pacienteCreateDto.Responsavel ?? false,
                Rg = Funcoes.RemoverCaracteres(pacienteCreateDto.Rg),
                Sexo = pacienteCreateDto.Sexo,
                Telefone = pacienteCreateDto.Telefone,
                PlanoId = pacienteCreateDto.PlanoId,
                DataCadastro = DateTime.Now,
                ConvenioId = pacienteCreateDto.ConvenioId
            };

            _context.Add(paciente);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Paciente.ToListAsync();
            resposta.Mensagem = "Paciente criado com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }



    public async Task<ResponseModel<List<PacienteModel>>> Delete(int idPaciente)
    {
        ResponseModel<List<PacienteModel>> resposta = new ResponseModel<List<PacienteModel>>();

        try
        {
            var paciente = await _context.Paciente.FirstOrDefaultAsync(x => x.Id == idPaciente);
            if (paciente == null)
            {
                resposta.Mensagem = "Nenhum Paciente encontrado";
                return resposta;
            }

            _context.Remove(paciente);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Paciente.ToListAsync();
            resposta.Mensagem = "Paciente Excluido com sucesso";
            return resposta;

        }
        catch (Exception ex)
        {

            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<PacienteModel>>> Editar(PacienteEdicaoDto pacienteEdicaoDto)
    {
        ResponseModel<List<PacienteModel>> resposta = new ResponseModel<List<PacienteModel>>();

        try
        {
            var paciente = _context.Paciente.FirstOrDefault(x => x.Id == pacienteEdicaoDto.Id);
            if (paciente == null)
            {
                resposta.Mensagem = "Paciente não encontrado";
                return resposta;
            }

            var cpfLimpo = Funcoes.RemoverCaracteres(pacienteEdicaoDto.Cpf);

            // Verificação de duplicidade do CPF no banco
            var cpfExistente = await _context.Paciente
                .AnyAsync(p => p.Cpf == cpfLimpo);

            if (cpfExistente)
            {
                resposta.Mensagem = "CPF já cadastrado, verifique.";
                return resposta;
            }

            paciente.Id = pacienteEdicaoDto.Id;
            paciente.Bairro = pacienteEdicaoDto.Bairro;
            paciente.BreveDiagnostico = pacienteEdicaoDto.BreveDiagnostico;
            paciente.Celular = pacienteEdicaoDto.Celular;
            paciente.Cep = pacienteEdicaoDto.Cep;
            paciente.Cidade = pacienteEdicaoDto.Cidade;
            paciente.ComoConheceu = pacienteEdicaoDto.ComoConheceu;
            paciente.Complemento = pacienteEdicaoDto.Complemento;
            paciente.Cpf = pacienteEdicaoDto.Cpf;
            paciente.DataNascimento = pacienteEdicaoDto.DataNascimento;
            paciente.Email = pacienteEdicaoDto.Email;
            paciente.Uf = pacienteEdicaoDto.Uf;
            paciente.EstadoCivil = pacienteEdicaoDto.EstadoCivil;
            paciente.Logradouro = pacienteEdicaoDto.Logradouro;
            paciente.Medicamento = pacienteEdicaoDto.Medicamento;
            paciente.ProfissionalId = pacienteEdicaoDto.ProfissionalId;
            paciente.Nome = pacienteEdicaoDto.Nome;           
            paciente.Numero = pacienteEdicaoDto.Numero;
            paciente.Pais = pacienteEdicaoDto.Pais;
            paciente.PermitirLembretes = pacienteEdicaoDto.PermitirLembretes;
            paciente.PreferenciaDeContato = pacienteEdicaoDto.PreferenciaDeContato;
            paciente.Profissao = pacienteEdicaoDto.Profissao;
            paciente.Responsavel = pacienteEdicaoDto.Responsavel;
            paciente.Rg = pacienteEdicaoDto.Rg;
            paciente.Sexo = pacienteEdicaoDto.Sexo;
            paciente.Telefone = pacienteEdicaoDto.Telefone;
            paciente.PlanoId = pacienteEdicaoDto.PlanoId;
            paciente.ConvenioId = pacienteEdicaoDto.ConvenioId;

            _context.Update(paciente);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Paciente.ToListAsync();
            resposta.Mensagem = "Paciente Atualizado com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {

            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<PacienteModel>>> Listar()
    {
        ResponseModel<List<PacienteModel>> resposta = new ResponseModel<List<PacienteModel>>();

        try
        {
            var paciente = await _context.Paciente.ToListAsync();

            resposta.Dados = paciente;
            resposta.Mensagem = "Todos os Paciente foram encontrados";
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