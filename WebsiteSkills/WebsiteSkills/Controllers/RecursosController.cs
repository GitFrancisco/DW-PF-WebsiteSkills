﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebsiteSkills.Data;
using WebsiteSkills.Models;

namespace WebsiteSkills.Controllers
{
    public class RecursosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RecursosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Recursos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Recurso.Include(r => r.Skill);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Recursos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recurso = await _context.Recurso
                .Include(r => r.Skill)
                .FirstOrDefaultAsync(m => m.IdRecurso == id);
            if (recurso == null)
            {
                return NotFound();
            }

            return View(recurso);
        }

        // GET: Recursos/Create
        public IActionResult Create()
        {
            ViewData["SkillsFK"] = new SelectList(_context.Skills, "SkillsId", "Nome");
            // Cria uma lista de opções para o atributo "TipoRecurso"
            // Os utilizadores podem escolher entre "PDF", "Imagem" e "Texto"
            // Em que podem ser guardados (no disco rígido) os PDFs e imagens e o texto é diretamente inserido na BD.
            ViewBag.TipoRecursoOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "PDF", Text = "PDF" },
                new SelectListItem { Value = "Imagem", Text = "Imagem" },
                new SelectListItem { Value = "Texto", Text = "Texto" }
            };

            return View();
        }

        // POST: Recursos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdRecurso,NomeRecurso,ConteudoRecurso,TipoRecurso,SkillsFK")] Recurso recurso)
        {
            // Remove o atributo "Skill" do ModelState
            ModelState.Remove("Skill");
            // Remove o atributo "ConteudoRecurso" do ModelState
            ModelState.Remove("ConteudoRecurso");

            if (ModelState.IsValid)
            {
                recurso.Skill = _context.Skills.Find(recurso.SkillsFK);
                _context.Add(recurso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SkillsFK"] = new SelectList(_context.Skills, "SkillsId", "Nome", recurso.SkillsFK);
            ViewBag.TipoRecursoOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "PDF", Text = "PDF" },
                new SelectListItem { Value = "Imagem", Text = "Imagem" },
                new SelectListItem { Value = "Texto", Text = "Texto" }
            };
            return View(recurso);
        }

        // GET: Recursos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recurso = await _context.Recurso.FindAsync(id);
            if (recurso == null)
            {
                return NotFound();
            }

            ViewBag.TipoRecursoAtual = recurso.TipoRecurso;
            return View(recurso);
        }

        // POST: Recursos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdRecurso,NomeRecurso,ConteudoRecurso")] Recurso recurso)
        {
            if (id != recurso.IdRecurso)
            {
                return NotFound();
            }
            // Remove o atributo "TipoRecurso" do ModelState
            ModelState.Remove("TipoRecurso");
            // Remove o atributo "Skill" do ModelState
            ModelState.Remove("Skill");
            // Remove o atributo "SkillsFK" do ModelState
            ModelState.Remove("SkillsFK");

            if (ModelState.IsValid)
            {
                try
                {
                    // Procura o recurso na BD
                    var existingRecurso = await _context.Recurso.Include(r => r.Skill).FirstOrDefaultAsync(r => r.IdRecurso == id);
                    if (existingRecurso == null)
                    {
                        return NotFound();
                    }

                    // Mantém os valores não editáveis
                    recurso.TipoRecurso = existingRecurso.TipoRecurso;
                    recurso.SkillsFK = existingRecurso.SkillsFK;
                    recurso.Skill = existingRecurso.Skill;

                    // Atualiza o recurso
                    _context.Entry(existingRecurso).State = EntityState.Detached;
                    _context.Update(recurso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecursoExists(recurso.IdRecurso))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(recurso);
        }


        // GET: Recursos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recurso = await _context.Recurso
                .Include(r => r.Skill)
                .FirstOrDefaultAsync(m => m.IdRecurso == id);
            if (recurso == null)
            {
                return NotFound();
            }

            return View(recurso);
        }

        // POST: Recursos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recurso = await _context.Recurso.FindAsync(id);
            if (recurso != null)
            {
                _context.Recurso.Remove(recurso);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecursoExists(int id)
        {
            return _context.Recurso.Any(e => e.IdRecurso == id);
        }
    }
}
