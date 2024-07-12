using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebsiteSkills.Data;
using WebsiteSkills.Models;

namespace WebsiteSkills.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiMentoresController : ControllerBase
    {
        /// <summary>
        /// Vai permitir a interação com a base de dados
        /// </summary>
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="context">Contexto da Base de Dados</param>
        public ApiMentoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Busca a lista de Mentores
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<Mentor>> GetMentores()
        {
            return _context.Mentor.ToList();
        }

        /// <summary>
        /// Busca um Mentor específico
        /// </summary>
        /// <param name="id">ID do Mentor a procurar na BD</param>
        [HttpGet("{id}")]
        public ActionResult<Mentor> GetMentor(int id)
        {
            var mentor = _context.Mentor.Find(id);

            if (mentor == null)
            {
                return NotFound();
            }

            return mentor;
        }

        /// <summary>
        /// Adiciona um Mentor novo à BD
        /// </summary>
        /// <param name="mentor">Objeto Mentor</param>
        [HttpPost]
        public ActionResult<Mentor> PostMentor(Mentor mentor)
        {
            _context.Mentor.Add(mentor);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetMentor), new { id = mentor.Id }, mentor);
        }

        /// <summary>
        /// Editar um Aluno
        /// </summary>
        /// <param name="id">ID do Mentor a editar</param>
        /// <param name="mentor">Objeto Mentor</param>
        [HttpPut("{id}")]
        public IActionResult PutMentor(int id, Mentor mentor)
        {
            if (id != mentor.Id)
            {
                return BadRequest();
            }

            _context.Entry(mentor).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        /// <summary>
        /// Apagar um Mentor específico
        /// </summary>
        /// <param name="id">ID do Mentor a apagar</param>
        [HttpDelete("{id}")]
        public IActionResult DeleteMentor(int id)
        {
            var mentor = _context.Mentor.Find(id);
            if (mentor == null)
            {
                return NotFound();
            }

            _context.Mentor.Remove(mentor);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
