using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebsiteSkills.Models;
using WebsiteSkills.Data;

namespace WebsiteSkills.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiSkillsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApiSkillsController(ApplicationDbContext context)
        {
            _context = context;
        }


        // ****** SKILLS *******

        // GET: api/Skills
        [HttpGet]
        public ActionResult<IEnumerable<Skills>> GetSkills()
        {
            return _context.Skills.ToList();
        }

        // GET: api/Skills/5
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

        // POST: api/Skills
        [HttpPost]
        public ActionResult<Skills> PostSkill(Skills skill)
        {
            _context.Skills.Add(skill);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetSkill), new { id = skill.SkillsId }, skill);
        }

        // PUT: api/Skills/5
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

        // DELETE: api/Skills/5
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
