﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebsiteSkills.Data;
using WebsiteSkills.Models;
using WebsiteSkills.Models.DTO;

namespace WebsiteSkills.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiRecursosController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public ApiRecursosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Recurso
        [HttpGet]
        public ActionResult<IEnumerable<Recurso>> GetRecursos()
        {
            return _context.Recurso.ToList();
        }

        // GET: api/Recurso/5
        [HttpGet("{id}")]
        public ActionResult<Recurso> GetRecurso(int id)
        {
            var recurso = _context.Recurso.Find(id);

            if (recurso == null)
            {
                return NotFound();
            }

            return recurso;
        }

        // POST: api/Recurso
        [HttpPost]
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

        // PUT: api/Recurso/5
        [HttpPut("{id}")]
        public IActionResult PutRecurso(int id, Recurso recurso)
        {
            if (id != recurso.IdRecurso)
            {
                return BadRequest();
            }

            _context.Entry(recurso).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/Recurso/5
        [HttpDelete("{id}")]
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

    }
}