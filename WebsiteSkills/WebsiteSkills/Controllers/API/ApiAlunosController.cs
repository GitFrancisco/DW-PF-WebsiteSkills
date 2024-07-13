using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        [Route("GetAllAlunos")]
        public ActionResult<IEnumerable<Aluno>> GetAlunos()
        {
            return _context.Aluno.ToList();
        }

        /// <summary>
        /// Busca um Aluno específico
        /// </summary>
        /// <param name="id">ID do Aluno a procurar na BD</param>
        [HttpGet]
        [Route("GetAluno")]
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
        /// Apagar um Aluno específico
        /// </summary>
        /// <param name="id">ID do Aluno a apagar</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteAluno")]
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

        /// <summary>
        /// Subscreve um aluno numa Skill específica
        /// </summary>
        /// <param name="alunoId">ID do Aluno</param>
        /// <param name="skillId">ID da Skill</param>
        [HttpPost]
        [Route("AdicionarSkillAluno")]
        public IActionResult AdicionarSkillMentor(int alunoId, int skillId)
        {
            var aluno = _context.Aluno.Include(a => a.ListaSubscricoes).FirstOrDefault(a => a.Id == alunoId);
            if (aluno == null)
            {
                return NotFound(new { Message = "Aluno não encontrado." });
            }

            var skill = _context.Skills.Find(skillId);
            if (skill == null)
            {
                return NotFound(new { Message = "Skill não encontrada." });
            }

            // Verifica se a subscrição já existe
            var subscricaoExistente = _context.Subscricoes
                .FirstOrDefault(s => s.SkillsFK == skill.SkillsId && s.AlunoFK == aluno.Id);

            if (subscricaoExistente != null)
            {
                return BadRequest(new { Message = "Aluno já subscrito nesta Skill." });
            }

            // Criação do objeto Subscricao
            var subscricao = new Subscricoes
            {
                SkillsFK = skill.SkillsId,
                AlunoFK = aluno.Id,
                dataSubscricao = DateTime.Now
            };
            // Adiciona a subscrição à BD
            _context.Subscricoes.Add(subscricao);
            _context.SaveChanges();

            return Ok(new { Message = "Aluno subscrito na Skill." });
        }
    }
}
