
using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Dto.Paciente;
using WebApiSmartClinic.Dto.Usuario;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Usuario;

public class UsuarioService : IUsuarioInterface
{
    private readonly AppDbContext _context;
    public UsuarioService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseModel<UsuarioModel>> BuscarPorId(int idUsuario)
    {
        ResponseModel<UsuarioModel> resposta = new ResponseModel<UsuarioModel>();
        try
        {
            var usuario = await _context.Usuario.FirstOrDefaultAsync(x => x.Id == idUsuario);
            if (usuario == null)
            {
                resposta.Mensagem = "Nenhum Usuario encontrado";
                return resposta;
            }

            resposta.Dados = usuario;
            resposta.Mensagem = "Usuario Encontrado";
            return resposta;
        }
        catch (Exception ex)
        {

            resposta.Mensagem = "Erro ao buscar Usuario";
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<UsuarioModel>>> Criar(UsuarioCreateDto usuarioCreateDto)
    {
        ResponseModel<List<UsuarioModel>> resposta = new ResponseModel<List<UsuarioModel>>();

        try
        {
            var usuario = new UsuarioModel();

            usuario.Nome = usuarioCreateDto.Nome;
            usuario.Email = usuarioCreateDto.Email;
            usuario.Senha = usuarioCreateDto.Senha;
            usuario.Permissao = usuarioCreateDto.Permissao;
            usuario.CPF = usuarioCreateDto.CPF;
            usuario.DataCriacao = usuarioCreateDto.DataCriacao;
            usuario.Ativo = usuarioCreateDto.Ativo;

            var profissionalExiste = await _context.Profissional.AnyAsync(c => c.Id == usuarioCreateDto.ProfissionalId);
            if (profissionalExiste)
            {
                usuario.ProfissionalId = usuarioCreateDto.ProfissionalId;
            }

            _context.Add(usuario);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Usuario.ToListAsync();
            resposta.Mensagem = "Usuario criado com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }

    }

    public async Task<ResponseModel<List<UsuarioModel>>> Delete(int idUsuario)
    {
        ResponseModel<List<UsuarioModel>> resposta = new ResponseModel<List<UsuarioModel>>();

        try
        {
            var usuario = await _context.Usuario.FirstOrDefaultAsync(x => x.Id == idUsuario);
            if (usuario == null)
            {
                resposta.Mensagem = "Nenhum Usuario encontrado";
                return resposta;
            }

            _context.Remove(usuario);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Usuario.ToListAsync();
            resposta.Mensagem = "Usuario Excluido com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<UsuarioModel>>> Editar(UsuarioEdicaoDto usuarioEdicaoDto)
    {
        ResponseModel<List<UsuarioModel>> resposta = new ResponseModel<List<UsuarioModel>>();

        try
        {
            var usuario = _context.Usuario.FirstOrDefault(x => x.Id == usuarioEdicaoDto.Id);
            if (usuario == null)
            {
                resposta.Mensagem = "Usuario não encontrado";
                return resposta;
            }

            usuario.Id = usuarioEdicaoDto.Id;
            usuario.Nome = usuarioEdicaoDto.Nome;
            usuario.Email = usuarioEdicaoDto.Email;
            usuario.Senha = usuarioEdicaoDto.Senha;
            usuario.Permissao = usuarioEdicaoDto.Permissao;
            usuario.CPF = usuarioEdicaoDto.CPF;
            usuario.DataCriacao = usuarioEdicaoDto.DataCriacao;
            usuario.Ativo = usuarioEdicaoDto.Ativo;

            var profissionalExiste = await _context.Profissional.AnyAsync(c => c.Id == usuarioEdicaoDto.ProfissionalId);
            if (profissionalExiste)
            {
                usuario.ProfissionalId = usuarioEdicaoDto.ProfissionalId;
            }

            _context.Update(usuario);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Usuario.ToListAsync();
            resposta.Mensagem = "Usuario Atualizado com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {

            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<UsuarioModel>>> Listar()
    {
        ResponseModel<List<UsuarioModel>> resposta = new ResponseModel<List<UsuarioModel>>();

        try
        {
            var usuario = await _context.Usuario.ToListAsync();

            resposta.Dados = usuario;
            resposta.Mensagem = "Todos os Usuario foram encontrados";
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