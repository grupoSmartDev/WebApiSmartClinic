using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Dto.Sala;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Services.Sala;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebApiSmartClinic.Services.Sala;

public class SalaService : ISalaInterface
{
    private readonly AppDbContext _context;
    public SalaService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseModel<SalaModel>> BuscarPorId(int idSala)
    {
        ResponseModel<SalaModel> resposta = new ResponseModel<SalaModel>();
        try
        {
            var sala = await _context.Sala
                .Include(s => s.HorariosFuncionamento)
                .FirstOrDefaultAsync(x => x.Id == idSala);

            if (sala == null)
            {
                resposta.Mensagem = "Nenhuma Sala encontrada";
                return resposta;
            }

            resposta.Dados = sala;
            resposta.Mensagem = "Sala Encontrada";
            return resposta;
        }
        catch (Exception ex)
        {

            resposta.Mensagem = "Erro ao buscar sala";
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<SalaModel>>> Criar(SalaCreateDto dto, int pageNumber = 1, int pageSize = 10)
    {
        ResponseModel<List<SalaModel>> resposta = new();
        try
        {
            var novaSala = new SalaModel
            {
                Nome = dto.Nome,
                Capacidade = dto.Capacidade,
                Tipo = dto.Tipo,
                local = dto.Local,
                Status = dto.Status,
                Observacao = dto.Observacao,
            };

            _context.Sala.Add(novaSala);
            await _context.SaveChangesAsync();

            if (dto.HorariosFuncionamento?.Any() == true)
            {
                foreach (var horario in dto.HorariosFuncionamento)
                {
                    _context.SalaHorario.Add(new SalaHorarioModel
                    {
                        SalaId = novaSala.Id,
                        DiaSemana = horario.DiaSemana,
                        HoraInicio = TimeSpan.Parse(horario.HoraInicio),
                        HoraFim = TimeSpan.Parse(horario.HoraFim),
                        Ativo = true
                    });
                }
                await _context.SaveChangesAsync();
            }

            resposta.Mensagem = "Sala cadastrada com sucesso";
            resposta.Status = true;
            resposta.Dados = await _context.Sala.ToListAsync();
        }
        catch (Exception ex)
        {
            resposta.Mensagem = $"Erro ao criar sala: {ex.Message}";
            resposta.Status = false;
        }
        return resposta;
    }

    public async Task<ResponseModel<List<SalaModel>>> Delete(int idSala, int pageNumber = 1, int pageSize = 10)
    {
        ResponseModel<List<SalaModel>> resposta = new ResponseModel<List<SalaModel>>();
        try
        {
            var sala = await _context.Sala
                .Include(s => s.Agendamentos) // Assumindo relacionamento com agendamentos
                .FirstOrDefaultAsync(x => x.Id == idSala);

            if (sala == null)
            {
                resposta.Mensagem = "Nenhuma Sala encontrada";
                resposta.Status = false;
                return resposta;
            }

            // Verifica se é registro padrão
            if (sala.IsSystemDefault)
            {
                resposta.Mensagem = "Não é possível inativar a sala pois é padrão do sistema.";
                resposta.Status = false;
                return resposta;
            }

            // Verificar se há agendamentos ativos/futuros vinculados à sala
            if (sala.Agendamentos.Count > 0)
            {
                resposta.Mensagem = "Não é possível inativar a sala pois existem agendamentos ativos ou futuros vinculados a ela";
                resposta.Status = false;
                return resposta;
            }

            // Soft delete - marcar como inativo
            sala.Status = false;
            sala.DataAlteracao = DateTime.UtcNow;
            // sala.UsuarioAlteracao = "usuarioLogado"; // Se tiver controle de usuário

            _context.Update(sala);
            await _context.SaveChangesAsync();

            // Buscar apenas salas ativas para a paginação
            var query = _context.Sala.Where(s => s.Status == true).AsQueryable();
            resposta = await PaginationHelper.PaginateAsync(query, pageNumber, pageSize);
            resposta.Mensagem = "Esse processo não irá excluir o registro e sim inativá-lo. Sala inativada com sucesso";

            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<SalaModel>>> Editar(SalaEdicaoDto dto, int pageNumber = 1, int pageSize = 10)
    {
        ResponseModel<List<SalaModel>> resposta = new();
        try
        {
            var sala = await _context.Sala.FindAsync(dto.Id);
            if (sala == null)
            {
                resposta.Mensagem = "Sala não encontrada";
                return resposta;
            }

            sala.Nome = dto.Nome;
            sala.Capacidade = dto.Capacidade;
            sala.Tipo = dto.Tipo;
            sala.local = dto.local;
            sala.Status = dto.Status;
            sala.HorarioFincionamento = dto.HorarioFincionamento;
            sala.Observacao = dto.Observacao;

            _context.Sala.Update(sala);
            await _context.SaveChangesAsync();

            // Atualizar horários
            var antigos = _context.SalaHorario.Where(h => h.SalaId == sala.Id);
            _context.SalaHorario.RemoveRange(antigos);

            if (dto.HorariosFuncionamento?.Any() == true)
            {
                foreach (var horario in dto.HorariosFuncionamento)
                {
                    _context.SalaHorario.Add(new SalaHorarioModel
                    {
                        SalaId = sala.Id,
                        DiaSemana = horario.DiaSemana,
                        HoraInicio = horario.HoraInicio,
                        HoraFim = horario.HoraFim,
                        Ativo = horario.Ativo
                    });
                }
            }

            await _context.SaveChangesAsync();

            var query = _context.Sala.AsQueryable();

            resposta.Status = true;
            resposta.Mensagem = "Sala atualizada com sucesso";
            resposta = await PaginationHelper.PaginateAsync(query, pageNumber, pageSize);
        }
        catch (Exception ex)
        {
            resposta.Mensagem = $"Erro ao editar sala: {ex.Message}";
            resposta.Status = false;
        }
        return resposta;
    }


    public async Task<ResponseModel<List<SalaModel>>> Listar(int pageNumber = 1, int pageSize = 10, int? idFiltro = null, string? nomeFiltro = null, string? localFiltro = null, int? capacidadeFiltro = null, bool paginar = true)
    {
        ResponseModel<List<SalaModel>> resposta = new ResponseModel<List<SalaModel>>();

        try
        {
            var query = _context.Sala.Include(s => s.HorariosFuncionamento).AsQueryable();
            var resultu = await query.ToListAsync();
            if(idFiltro.HasValue)
                query = query.Where(x => x.Id == idFiltro.Value);

            if (!string.IsNullOrEmpty(nomeFiltro))
                query = query.Where(x => x.Nome.ToLower().Contains(nomeFiltro.ToLower()));

            if (!string.IsNullOrEmpty(localFiltro))
                query = query.Where(x => x.local.ToLower().Contains(localFiltro.ToLower()));

            if (capacidadeFiltro.HasValue)
                query = query.Where(x => x.Capacidade == capacidadeFiltro);


            query = query.OrderBy(x => x.Id);

            resposta = paginar ? await PaginationHelper.PaginateAsync(query, pageNumber, pageSize) : new ResponseModel<List<SalaModel>> { Dados = await query.ToListAsync() };
            resposta.Mensagem = "Todos os Sala foram encontrados";
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
