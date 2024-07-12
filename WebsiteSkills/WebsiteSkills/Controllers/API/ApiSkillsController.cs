using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebsiteSkills.Models;
using WebsiteSkills.Data;
using WebsiteSkills.Models.DTO;

namespace WebsiteSkills.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiSkillsController : ControllerBase
    {
        /// <summary>
        /// Vai permitir a interação com a base de dados
        /// </summary>
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="context">Contexto da Base de Dados</param>
        public ApiSkillsController(ApplicationDbContext context)
        {
            _context = context;
        }


        // ****** SKILLS *******

        /// <summary>
        /// Busca todas as skills
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<Skills>> GetSkills()
        {
            return _context.Skills.ToList();
        }

        /// <summary>
        /// Busca uma skill específica
        /// </summary>
        /// <param name="id">ID da Skill a procurar na BD</param>
        [HttpGet("{id}")]
        public ActionResult<Skills> GetSkill(int id)
        {
            var skill = _context.Skills.Find(id);

            if (skill == null)
            {
                return NotFound();
            }

            return skill;
        }

        /// <summary>
        /// Adiciona uma skill
        /// </summary>
        /// <param name="dto">Data Transfer Object para Skills</param>
        [HttpPost]
        public ActionResult<Skills> PostSkill([FromBody] SkillsDTO dto)
        {
            // Criar novo objeto Skill
            Skills skill = new Skills();
            // Adicionar valores do DTO ao objeto Skill
            skill.Nome = dto.Nome;
            skill.Dificuldade = dto.Dificuldade;
            skill.Tempo = dto.Tempo;
            skill.Descricao = dto.Descricao;
            skill.Custo = dto.Custo;
            skill.Imagem = dto.Imagem;
            
            // Adicionar Skill à BD
            _context.Skills.Add(skill);
            // Dar commit à BD
            _context.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// Editar uma skill
        /// </summary>
        /// <param name="id">ID da Skill a procurar na BD</param>
        /// <param name="skill">Skill</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult PutSkill(int id, Skills skill)
        {
            if (id != skill.SkillsId)
            {
                return BadRequest();
            }

            _context.Entry(skill).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        /// <summary>
        /// Apagar uma skill
        /// </summary>
        /// <param name="id">ID da Skill a apagar</param>
        [HttpDelete("{id}")]
        public IActionResult DeleteSkill(int id)
        {
            var skill = _context.Skills.Find(id);
            if (skill == null)
            {
                return NotFound();
            }

            _context.Skills.Remove(skill);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
