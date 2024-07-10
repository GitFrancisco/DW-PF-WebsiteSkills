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
        private readonly ApplicationDbContext _context;

        public ApiMentoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Mentores
        [HttpGet]
        public ActionResult<IEnumerable<Mentor>> GetMentores()
        {
            return _context.Mentor.ToList();
        }

        // GET: api/Mentores/5
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

        // POST: api/Mentores
        [HttpPost]
        public ActionResult<Mentor> PostMentor(Mentor mentor)
        {
            _context.Mentor.Add(mentor);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetMentor), new { id = mentor.Id }, mentor);
        }

        // PUT: api/Mentores/5
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

        // DELETE: api/Mentores/5
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
