
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Data;
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
            var paciente = new PacienteModel();

            paciente.Bairro = pacienteCreateDto.Bairro;
            paciente.BreveDiagnostico = pacienteCreateDto.BreveDiagnostico;
            paciente.Celular = pacienteCreateDto.Celular;
            paciente.Cep = pacienteCreateDto.Cep;
            paciente.Cidade = pacienteCreateDto.Cidade;
            paciente.ComoConheceu = pacienteCreateDto.ComoConheceu;
            paciente.Complemento = pacienteCreateDto.Complemento;
            paciente.Cpf = pacienteCreateDto.Cpf;
            paciente.DataNascimento = pacienteCreateDto.DataNascimento;
            paciente.Email = pacienteCreateDto.Email;
            paciente.Uf = pacienteCreateDto.Uf;
            paciente.EstadoCivil = pacienteCreateDto.EstadoCivil;
            paciente.Logradouro = pacienteCreateDto.Logradouro;
            paciente.Medicamento = pacienteCreateDto.Medicamento;
            paciente.ProfissionalId = pacienteCreateDto.ProfissionalId;
            paciente.Nome = pacienteCreateDto.Nome;
            paciente.Numero = pacienteCreateDto.Numero;
            paciente.Pais = pacienteCreateDto.Pais;
            paciente.PermitirLembretes = (bool)pacienteCreateDto.PermitirLembretes;
            paciente.PreferenciaDeContato = pacienteCreateDto.PreferenciaDeContato;
            paciente.Profissao = pacienteCreateDto.Profissao;
            paciente.Responsavel = (bool)pacienteCreateDto.Responsavel;
            paciente.Rg = pacienteCreateDto.Rg;
            paciente.Sexo = pacienteCreateDto.Sexo;
            paciente.Telefone = pacienteCreateDto.Telefone;
            paciente.PlanoId = pacienteCreateDto.PlanoId;
            paciente.DataCadastro = DateTime.Now;
            paciente.ConvenioId = pacienteCreateDto.ConvenioId;

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