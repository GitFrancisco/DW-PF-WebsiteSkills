using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebsiteSkills.Data;
using WebsiteSkills.Models;

namespace WebsiteSkills.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiAlunosController : ControllerBase
    {
        /// <summary>
        /// Vai permitir a interação com a base de dados
        /// </summary>
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="context">Contexto da Base de Dados</param>
        public ApiAlunosController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Busca a lista de Alunos
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<Aluno>> GetAlunos()
        {
            return _context.Aluno.ToList();
        }

        /// <summary>
        /// Busca um Aluno específico
        /// </summary>
        /// <param name="id">ID do Aluno a procurar na BD</param>
        [HttpGet("{id}")]
        public ActionResult<Aluno> GetAluno(int id)
        {
            var aluno = _context.Aluno.Find(id);

            if (aluno == null)
            {
                return NotFound();
            }

            return aluno;
        }

        /// <summary>
        /// Adiciona um Aluno novo à BD
        /// </summary>
        /// <param name="aluno">Objeto Aluno</param>
        [HttpPost]
        public ActionResult<Aluno> PostAluno(Aluno aluno)
        {
            _context.Aluno.Add(aluno);
            _context.SaveChanges();

            return Ok("Novo aluno criado.");
        }

        /// <summary>
        /// Editar um Aluno
        /// </summary>
        /// <param name="id">ID do Aluno a editar</param>
        /// <param name="aluno">Objeto Aluno</param>
        [HttpPut("{id}")]
        public IActionResult PutAluno(int id, Aluno aluno)
        {
            if (id != aluno.Id)
            {
                return BadRequest();
            }

            _context.Entry(aluno).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        /// <summary>
        /// Apagar um Aluno específico
        /// </summary>
        /// <param name="id">ID do Aluno a apagar</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteAluno(int id)
        {
            var aluno = _context.Aluno.Find(id);
            if (aluno == null)
            {
                return NotFound();
            }

            _context.Aluno.Remove(aluno);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
