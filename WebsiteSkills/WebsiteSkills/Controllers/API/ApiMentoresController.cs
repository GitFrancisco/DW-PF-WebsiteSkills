using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        [Route("GetAllMentores")]
        public ActionResult<IEnumerable<Mentor>> GetMentores()
        {
            return _context.Mentor.ToList();
        }

        /// <summary>
        /// Busca um Mentor específico
        /// </summary>
        /// <param name="id">ID do Mentor a procurar na BD</param>
        [HttpGet]
        [Route("GetMentor")]
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
        /// Adiciona uma Skill à lista de Skills de um Mentor específico
        /// </summary>
        /// <param name="mentorId">ID do Mentor</param>
        /// <param name="skillId">ID da Skill</param>
        [HttpPost]
        [Route("AdicionarSkillMentor")]
        public IActionResult AdicionarSkillMentor(int mentorId, int skillId)
        {
            var mentor = _context.Mentor.Include(m => m.ListaSkills).FirstOrDefault(m => m.Id == mentorId);
            if (mentor == null)
            {
                return NotFound(new { Message = "Mentor não encontrado." });
            }

            var skill = _context.Skills.Find(skillId);
            if (skill == null)
            {
                return NotFound(new { Message = "Skill não encontrado." });
            }

            mentor.ListaSkills.Add(skill);
            _context.SaveChanges();

            return Ok(new { Message = "Skill adicionada ao mentor." });
        }
    }
}




