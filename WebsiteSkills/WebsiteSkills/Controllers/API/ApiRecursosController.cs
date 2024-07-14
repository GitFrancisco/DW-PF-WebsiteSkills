using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebsiteSkills.Data;
using WebsiteSkills.Models;
using WebsiteSkills.Models.DTO;
using Microsoft.AspNetCore.Authorization;

namespace WebsiteSkills.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiRecursosController : ControllerBase
    {
        /// <summary>
        /// Vai permitir a interação com a base de dados
        /// </summary>
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="context">BD</param>
        public ApiRecursosController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Busca todos os recursos da BD
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllRecursos")]
        [Authorize(Roles= "Administrador")]
        public ActionResult<IEnumerable<Recurso>> GetRecursos()
        {
            return _context.Recurso.ToList();
        }

        /// <summary>
        /// Busca todos os Recursos relativos a uma Skill
        /// </summary>
        [HttpGet]
        [Route("SkillRecursos")]
        [Authorize(Roles = "Mentor, Aluno")]
        public ActionResult<IEnumerable<Anuncio>> GetAllSkillRecursos(int id)
        {
            var recursos = _context.Recurso.Where(r => r.SkillsFK == id).ToList();
            if (recursos == null || recursos.Count == 0)
            {
                return NotFound();
            }
            return Ok(recursos);
        }

        /// <summary>
        /// Busca um recurso específico
        /// </summary>
        /// <param name="id">ID de um recurso específico</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetRecurso")]
        [Authorize(Roles = "Mentor, Administrador")]
        public ActionResult<Recurso> GetRecurso(int id)
        {
            var recurso = _context.Recurso.Find(id);

            if (recurso == null)
            {
                return NotFound();
            }

            return recurso;
        }

        /// <summary>
        /// Adiciona um recurso
        /// </summary>
        /// <param name="dto">Recurso DTO</param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddRecurso")]
        [Authorize(Roles = "Mentor, Administrador")]
        public ActionResult<Recurso> PostRecurso([FromBody] RecursoDTO dto)
        {
            Recurso recurso = new Recurso();
            recurso.NomeRecurso = dto.NomeRecurso;
            recurso.TipoRecurso = dto.TipoRecurso;
            recurso.SkillsFK = dto.SkillsFK;
            recurso.ConteudoRecurso = null;

            _context.Recurso.Add(recurso);
            _context.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// Editar um recurso
        /// </summary>
        /// <param name="dto">Recurso DTO</param>
        /// <param name="id">ID do Recurso a editar</param>
        /// <returns></returns>
        [HttpPost]
        [Route("EditRecurso")]
        [Authorize(Roles = "Mentor, Administrador")]
        public IActionResult EditRecurso([FromBody] RecursoDTO dto, [FromQuery] int id)
        {
            // Procurar Skill (existente) na BD
            Recurso recurso = _context.Recurso.Where(r => r.IdRecurso == id).FirstOrDefault();

            recurso.NomeRecurso = dto.NomeRecurso;
            recurso.ConteudoRecurso = dto.ConteudoRecurso;

            _context.Recurso.Update(recurso);
            _context.SaveChanges();

            return NoContent();
        }

        /// <summary>
        /// Apagar um recurso
        /// </summary>
        /// <param name="id">ID do recurso a apagar</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteRecurso")]
        [Authorize(Roles = "Mentor, Administrador")]
        public IActionResult DeleteRecurso(int id)
        {
            var recurso = _context.Recurso.Find(id);
            if (recurso == null)
            {
                return NotFound();
            }

            _context.Recurso.Remove(recurso);
            _context.SaveChanges();

            return NoContent();
        }

        /// <summary>
        /// Upload de um ficheiro
        /// </summary>
        /// <param name="file">Ficheiro a ser carregado</param>
        [HttpPost]
        [Route("UploadFile")]
        [Authorize]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado.");
            }

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/FicheirosRecursos", file.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok(new { filePath = path });
        }

    }
}
