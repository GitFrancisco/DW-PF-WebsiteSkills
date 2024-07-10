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
        private readonly ApplicationDbContext _context;

        public ApiAlunosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Alunos
        [HttpGet]
        public ActionResult<IEnumerable<Aluno>> GetAlunos()
        {
            return _context.Aluno.ToList();
        }

        // GET: api/Alunos/5
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

        // POST: api/Alunos
        [HttpPost]
        public ActionResult<Aluno> PostAluno(Aluno aluno)
        {
            _context.Aluno.Add(aluno);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetAluno), new { id = aluno.Id }, aluno);
        }

        // PUT: api/Alunos/5
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

        // DELETE: api/Alunos/5
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
