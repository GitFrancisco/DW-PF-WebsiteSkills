using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebsiteSkills.Models;
using WebsiteSkills.Data;
using WebsiteSkills.Models.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

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

        /// <summary>
        /// Busca todas as skills
        /// </summary>
        [HttpGet]
        [Route("GetAllSkills")]
        public ActionResult<IEnumerable<Skills>> GetSkills()
        {
            return _context.Skills.ToList();
        }

        /// <summary>
        /// Busca uma skill específica
        /// </summary>
        /// <param name="id">ID da Skill a procurar na BD</param>
        [HttpGet]
        [Route("GetSkill")]
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
        [Route("AddSkill")]
        [Authorize(Roles = "Administrador")]
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
        /// <param name="dto">Skill DTO</param>
        /// <param name="id">ID da Skill a editar</param>
        /// <returns></returns>
        [HttpPost]
        [Route("EditSkill")]
        [Authorize(Roles = "Administrador")]
        public IActionResult EditSkill([FromBody] SkillsDTO dto, [FromQuery] int id)
        {
            // Procurar Skill (existente) na BD
            Skills skill = _context.Skills.Where(s => s.SkillsId == id).FirstOrDefault();

            skill.Nome = dto.Nome;
            skill.Dificuldade = dto.Dificuldade;
            skill.Tempo = dto.Tempo;
            skill.Descricao = dto.Descricao;
            skill.Custo = dto.Custo;
            skill.Imagem = dto.Imagem;

            _context.Skills.Update(skill);
            _context.SaveChanges();

            return NoContent();
        }

        /// <summary>
        /// Apagar uma skill
        /// </summary>
        /// <param name="id">ID da Skill a apagar</param>
        [HttpDelete]
        [Route("DeleteSkill")]
        [Authorize(Roles = "Administrador")]
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

        /// <summary>
        /// Upload de uma imagem
        /// </summary>
        /// <param name="file">Imagem a ser carregada</param>
        [HttpPost]
        [Route("UploadImage")]
        [Authorize]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado.");
            }

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Imagens", file.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok(new { filePath = path });
        }


    }
}
