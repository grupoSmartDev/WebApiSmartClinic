using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Helpers;
using WebApiSmartClinic.Dto.Paciente;
using WebApiSmartClinic.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
            var paciente = await _context.Paciente
                .Include(p => p.Evolucoes)
                    .ThenInclude(e => e.Exercicios)
                .Include(p => p.Evolucoes)
                    .ThenInclude(e => e.Atividades)
                .FirstOrDefaultAsync(x => x.Id == idPaciente);

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

    public async Task<ResponseModel<List<PacienteModel>>> Criar(PacienteCreateDto pacienteCreateDto, int pageNumber = 1, int pageSize = 10)
    {
        ResponseModel<List<PacienteModel>> resposta = new ResponseModel<List<PacienteModel>>();

        try
        {
            var cpfLimpo = Funcoes.RemoverCaracteres(pacienteCreateDto.Cpf);

            var cpfExistente = await _context.Paciente
                .AnyAsync(p => p.Cpf == cpfLimpo);

            if (cpfExistente)
            {
                resposta.Mensagem = "CPF já cadastrado, verifique.";
                return resposta;
            }

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
                ConvenioId = pacienteCreateDto.ConvenioId,
                Evolucoes = new List<EvolucaoModel>()
            };

            if (pacienteCreateDto.Evolucoes != null && pacienteCreateDto.Evolucoes.Any())
            {
                foreach (var evolucaoDto in pacienteCreateDto.Evolucoes)
                {
                    var evolucao = new EvolucaoModel
                    {
                        Observacao = evolucaoDto.Observacao,
                        DataEvolucao = evolucaoDto.DataEvolucao,
                        PacienteId = paciente.Id,
                        Exercicios = evolucaoDto.Exercicios?.Select(e => new ExercicioModel
                        {
                            Obs = e.Obs,
                            Peso = (int)e.Peso,
                            Repeticoes = (int)e.Repeticoes,
                            Series = (int)e.Series,
                            Tempo = (int)e.Tempo,
                            Descricao = e.Descricao
                        }).ToList(),
                        Atividades = evolucaoDto.Atividades?.Select(a => new AtividadeModel
                        {
                            Tempo = a.Tempo,
                            Titulo = a.Titulo,
                            Descricao = a.Descricao
                        }).ToList()
                    };
                    paciente.Evolucoes.Add(evolucao);
                }
            }

            _context.Add(paciente);
            await _context.SaveChangesAsync();

            var query = _context.Paciente
                .Include(p => p.Evolucoes)
                    .ThenInclude(e => e.Exercicios)
                .Include(p => p.Evolucoes)
                    .ThenInclude(e => e.Atividades)
                .AsQueryable();

            resposta.Dados = (await PaginationHelper.PaginateAsync(query, pageNumber, pageSize)).Dados;
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

    public async Task<ResponseModel<List<PacienteModel>>> Delete(int idPaciente, int pageNumber = 1, int pageSize = 10)
    {
        ResponseModel<List<PacienteModel>> resposta = new ResponseModel<List<PacienteModel>>();

        try
        {
            var paciente = await _context.Paciente
                .Include(p => p.Evolucoes)
                    .ThenInclude(e => e.Exercicios)
                .Include(p => p.Evolucoes)
                    .ThenInclude(e => e.Atividades)
                .FirstOrDefaultAsync(x => x.Id == idPaciente);

            if (paciente == null)
            {
                resposta.Mensagem = "Nenhum Paciente encontrado";
                return resposta;
            }

            _context.Remove(paciente);
            await _context.SaveChangesAsync();

            var query = _context.Paciente
                .Include(p => p.Evolucoes)
                    .ThenInclude(e => e.Exercicios)
                .Include(p => p.Evolucoes)
                    .ThenInclude(e => e.Atividades)
                .AsQueryable();

            resposta.Dados = (await PaginationHelper.PaginateAsync(query, pageNumber, pageSize)).Dados;
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

    public async Task<ResponseModel<List<PacienteModel>>> Editar(PacienteEdicaoDto pacienteEdicaoDto, int pageNumber = 1, int pageSize = 10)
    {
        ResponseModel<List<PacienteModel>> resposta = new ResponseModel<List<PacienteModel>>();

        try
        {
            var paciente = await _context.Paciente
                .Include(p => p.Evolucoes)
                    .ThenInclude(e => e.Exercicios)
                .Include(p => p.Evolucoes)
                    .ThenInclude(e => e.Atividades)
                .FirstOrDefaultAsync(x => x.Id == pacienteEdicaoDto.Id);

            if (paciente == null)
            {
                resposta.Mensagem = "Paciente não encontrado";
                return resposta;
            }

            var cpfLimpo = Funcoes.RemoverCaracteres(pacienteEdicaoDto.Cpf);

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

            // Atualizar evolucoes se existirem no DTO
            if (pacienteEdicaoDto.Evolucoes != null)
            {
                // Limpar evolucoes existentes
                paciente.Evolucoes.Clear();

                // Adicionar novas evolucoes
                foreach (var evolucaoDto in pacienteEdicaoDto.Evolucoes)
                {
                    var evolucao = new EvolucaoModel
                    {
                        Observacao = evolucaoDto.Observacao,
                        DataEvolucao = evolucaoDto.DataEvolucao,
                        PacienteId = paciente.Id,
                        Exercicios = evolucaoDto.Exercicios?.Select(e => new ExercicioModel
                        {
                            Obs = e.Obs,
                            Peso = e.Peso,
                            Repeticoes = e.Repeticoes,
                            Series = e.Series,
                            Tempo = e.Tempo,
                            Descricao = e.Descricao
                        }).ToList(),
                        Atividades = evolucaoDto.Atividades?.Select(a => new AtividadeModel
                        {
                            Tempo = a.Tempo,
                            Titulo = a.Titulo,
                            Descricao = a.Descricao
                        }).ToList()
                    };
                    paciente.Evolucoes.Add(evolucao);
                }
            }

            _context.Update(paciente);
            await _context.SaveChangesAsync();

            var query = _context.Paciente
                    .Include(p => p.Evolucoes)
                    .ThenInclude(e => e.Exercicios)
                    .Include(p => p.Evolucoes)
                    .ThenInclude(e => e.Atividades)
                    .AsQueryable();

            resposta.Dados = (await PaginationHelper.PaginateAsync(query, pageNumber, pageSize)).Dados;
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

    public async Task<ResponseModel<List<PacienteModel>>> Listar(int pageNumber = 1, int pageSize = 10, int? codigoFiltro = null, string? nomeFiltro = null, string? cpfFiltro = null, string? celularFiltro = null, bool paginar = true)
    {
        ResponseModel<List<PacienteModel>> resposta = new ResponseModel<List<PacienteModel>>();

        try
        {
            var query = _context.Paciente
                .Include(p => p.Evolucoes)
                    .ThenInclude(e => e.Exercicios)
                .Include(p => p.Evolucoes)
                    .ThenInclude(e => e.Atividades)
                .AsQueryable();

            query = query.Where(x =>
                (!codigoFiltro.HasValue || x.Id == codigoFiltro) &&
                (string.IsNullOrEmpty(nomeFiltro) || x.Nome == nomeFiltro) &&
                (string.IsNullOrEmpty(cpfFiltro) || x.Cpf == cpfFiltro) &&
                (string.IsNullOrEmpty(celularFiltro) || x.Celular == celularFiltro)
            );

            query = query.OrderBy(x => x.Id);

            resposta.Dados = paginar ? (await PaginationHelper.PaginateAsync(query, pageNumber, pageSize)).Dados : await query.ToListAsync();
            resposta.Mensagem = "Todos os Pacientes foram encontrados";
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