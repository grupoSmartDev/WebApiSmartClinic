using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Dto.Autor;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Autor
{
    public class AutorService : IAutorInterface
    {
        //AQUI VAI FICAR O CONTEXT
        private readonly AppDbContext _context;
        public AutorService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseModel<AutorModel>> BuscarAutorPorId(int idAutor)
        {
            ResponseModel<AutorModel> resposta = new ResponseModel<AutorModel>();
            try
            {
                var autor = await _context.Autores.FirstOrDefaultAsync(x => x.Id == idAutor);
                if (autor != null)
                {
                    resposta.Dados = autor;
                    resposta.Mensagem = "Autor encontrado";
                    return resposta;

                }
                else
                {
                    resposta.Mensagem = "Autor não encontrado";
                    return resposta;
                }
            }
            catch (Exception e)
            {

                resposta.Mensagem = e.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<AutorModel>> BuscarAutorPorIdLivro(int idLivro)
        {
            ResponseModel<AutorModel> resposta = new ResponseModel<AutorModel>();

            try
            {
                var livro = await _context.Livros.Include(a => a.Autor)
                    .FirstOrDefaultAsync(livro => livro.Id == idLivro);

                if (livro != null)
                {
                    resposta.Dados = livro.Autor;
                    resposta.Mensagem = "Autor encontrado";
                    return resposta;
                }
                else
                {
                    resposta.Mensagem = "Autor não encontrado";
                    resposta.Status = false;
                    return resposta;
                }
            }
            catch (Exception e)
            {

                resposta.Mensagem = e.Message;
                resposta.Status = false;
                return resposta;
            }


        }

        public async Task<ResponseModel<List<AutorModel>>> CriarAutor(AutorCreateDto autorCreateDto)
        {
            ResponseModel<List<AutorModel>> resposta = new ResponseModel<List<AutorModel>>();
            try
            {
                var autor = new AutorModel()
                {
                    Nome = autorCreateDto.Nome,
                    Sobrenome = autorCreateDto.Sobrenome
                };

                _context.Add(autor);
                await _context.SaveChangesAsync();
                resposta.Dados = await _context.Autores.ToListAsync();
                resposta.Mensagem = "Autor criado com sucesso!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
                
            }
        }

        public async Task<ResponseModel<List<AutorModel>>> EditarAutor(AutorEdicaoDto autorEdicaoDto)
        {
            ResponseModel<List<AutorModel>> resposta = new ResponseModel<List<AutorModel>>();
            try
            {
                var autor = await _context.Autores
                    .FirstOrDefaultAsync(x => x.Id == autorEdicaoDto.Id);
                if (autor == null)
                {
                    resposta.Mensagem = "Autor não encontrato";
                    return resposta;
                }

                autor.Nome = autorEdicaoDto.Nome;
                autor.Sobrenome = autorEdicaoDto.Sobrenome;

                _context.Update(autor);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Autores.ToListAsync();
                resposta.Mensagem = "Autor atualizado com sucesso";
                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
                
            }
        }

        public async Task<ResponseModel<List<AutorModel>>> ExcluirAutor(int idAutor)
        {
            //primeiro, criar variavel de resposta
            ResponseModel<List<AutorModel>> resposta = new ResponseModel<List<AutorModel>>();
            try
            {
                var autor = await _context.Autores
                    .FirstOrDefaultAsync(x => x.Id ==  idAutor);
                if(autor == null)
                {
                    resposta.Mensagem = "Nenhum altor localizado";
                    return resposta;
                }

                _context.Remove(autor);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Autores.ToListAsync();
                resposta.Mensagem = "Autor Excluido com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {

                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }

        }

        public async Task<ResponseModel<List<AutorModel>>> ListarAutores()
        {
            ResponseModel<List<AutorModel>> resposta = new ResponseModel<List<AutorModel>>();
            try
            {
                var autores = await _context.Autores.ToListAsync();
                resposta.Dados = autores;
                resposta.Mensagem = "Todos os Autores foram coletados";
                resposta.PageSize = 10;
                resposta.TotalCount = autores.Count();
                return resposta;
            }
            catch (Exception ex)
            {

                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }
    }
}
