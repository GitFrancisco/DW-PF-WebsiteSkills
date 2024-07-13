using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebsiteSkills.Data;
using WebsiteSkills.Models;
using WebsiteSkills.Models.DTO;

namespace WebsiteSkills.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiAnunciosController : ControllerBase
    {
        /// <summary>
        /// Vai permitir a interação com a base de dados
        /// </summary>
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="context">Contexto da Base de Dados</param>
        public ApiAnunciosController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Busca todos os Anuncios
        /// </summary>
        [HttpGet]
        [Route("GetAllAnuncios")]
        public ActionResult<IEnumerable<Anuncio>> GetAllAnuncios()
        {
            return _context.Anuncio.ToList();
        }

        /// <summary>
        /// Busca um anúncio específico
        /// </summary>
        /// <param name="id">ID do anúncio a procurar na BD</param>
        [HttpGet]
        [Route("GetAnuncio")]
        public ActionResult<Anuncio> GetAnuncio(int id)
        {
            var anuncio = _context.Anuncio.Find(id);

            if (anuncio == null)
            {
                return NotFound();
            }

            return anuncio;
        }

        [HttpPost]
        [Route("AddAnuncio")]
        public ActionResult<Skills> PostAnuncio([FromBody] AnuncioDTO dto, int skillId)
        {
            bool skillExiste = false;
            skillExiste = _context.Skills.Any(s => s.SkillsId == skillId);
            if (!skillExiste)
            {
                return BadRequest("Skill não existe!");
            }
            // Criar novo objeto Skill
            Anuncio anuncio = new Anuncio();
            // Adicionar valores do DTO ao objeto Skill
            anuncio.Texto = dto.Texto;
            anuncio.SkillsFK = skillId;
            anuncio.DataCriacao = DateTime.Now;

            // Adicionar Skill à BD
            _context.Anuncio.Add(anuncio);
            // Dar commit à BD
            _context.SaveChanges();
            return Ok();
        }
    }
}
