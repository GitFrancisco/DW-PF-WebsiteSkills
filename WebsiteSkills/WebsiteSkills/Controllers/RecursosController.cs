using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
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
        // Relativo a wwwroot
        private readonly IWebHostEnvironment _webHostEnvironment;

        public RecursosController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            // Adição ao acesso à wwwroot (onde estão as imagens) no construtor
            _webHostEnvironment = webHostEnvironment;
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
        public async Task<IActionResult> Edit(int id, [Bind("IdRecurso,NomeRecurso")] Recurso recurso, IFormFile FicheiroRecurso, string TextoRecurso)
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
            // Remove o atributo "ConteudoRecurso" do ModelState
            ModelState.Remove("ConteudoRecurso");
            // Remove o atributo "TextoRecurso" do ModelState
            ModelState.Remove("TextoRecurso");
            // Remove o atributo "FicheiroRecurso" do ModelState
            ModelState.Remove("FicheiroRecurso");

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

                    // Variável de controlo para verificar se existe ficheiro
                    bool existeFicheiro = false;
                    string nomeFicheiro = "";

                    // Verifica se o recurso é do tipo "PDF" ou "Imagem"
                    if (recurso.TipoRecurso == "PDF" || recurso.TipoRecurso == "Imagem"){
                        if (FicheiroRecurso == null)
                        {
                            // Mantém o mesmo ficheiro.
                            recurso.ConteudoRecurso = existingRecurso.ConteudoRecurso;
                            // Atualiza o recurso
                            _context.Entry(existingRecurso).State = EntityState.Detached;
                            _context.Update(recurso);
                            await _context.SaveChangesAsync();
                            // Redireciona para a página de Index
                            return RedirectToAction(nameof(Index));
                        }
                        // Neste caso, existe ficheiro mas temos de confirmar se é uma imagem/um pdf
                        else
                        {
                            // Se não guarda o ficheiro, devolve à View
                            // Verifica se o ficheiro é dos tipos aceites (PNG, JPG, JPEG e PDF)
                            if (!(FicheiroRecurso.ContentType == "image/png" || FicheiroRecurso.ContentType == "image/jpg" 
                                ||FicheiroRecurso.ContentType == "image/jpeg"
                                ||FicheiroRecurso.ContentType == "application/pdf"))
                            {
                                // Devolve o controlo à View
                                ModelState.AddModelError("",
                               "A imagem tem de ser do tipo: PNG, JPG ou JPEG.");
                                return View(recurso);
                            }
                            // Aqui, significa que há ficheiro e é uma imagem/um pdf
                            else
                            {
                                // A variável de controlo existeFicheiro passa a ser verdadeira
                                existeFicheiro = true;
                                // Obter o nome aleatório e único para o ficheiro
                                Guid uid = Guid.NewGuid();
                                nomeFicheiro = uid.ToString();
                                // Obtém a extensão do ficheiro enviado
                                string extensao = Path.GetExtension(FicheiroRecurso.FileName);
                                // Adicionar a extensão ao nome do ficheiro
                                nomeFicheiro += extensao;
                                // Adicionar o nome do ficheiro ao objeto enviado
                                recurso.ConteudoRecurso = nomeFicheiro;

                                // Apagar o ficheiro antigo
                                var FicheiroApagar = Path.Combine(_webHostEnvironment.WebRootPath, "FicheirosRecursos", existingRecurso.ConteudoRecurso);
                                // Se o ficheiro existir, apaga-o
                                if (System.IO.File.Exists(FicheiroApagar) && existingRecurso.ConteudoRecurso != null)
                                {
                                    System.IO.File.Delete(FicheiroApagar);
                                }
                            }
                        }

                    } else
                        recurso.ConteudoRecurso = TextoRecurso;

                    // Atualiza o recurso
                    _context.Entry(existingRecurso).State = EntityState.Detached;
                    _context.Update(recurso);
                    await _context.SaveChangesAsync();


                    // Se existe ficheiro, guardar o ficheiro no disco rígido do servidor
                    if (existeFicheiro)
                    {
                        // Define a localização da pasta que irá conter os ficheiros
                        string PastaFicheiros = Path.Combine(_webHostEnvironment.WebRootPath, "FicheirosRecursos");

                        // Se já existir o diretório, não é necessário criar
                        if (!Directory.Exists(PastaFicheiros))
                        {
                            // Caso contrário, cria o diretório
                            Directory.CreateDirectory(PastaFicheiros);
                        }

                        // Junta o nome do ficheiro ao caminho da pasta
                        string nomeFinalFicheiro =
                           Path.Combine(PastaFicheiros, nomeFicheiro);

                        // Guarda o ficheiro no disco rígido
                        using var stream = new FileStream(
                           nomeFinalFicheiro, FileMode.Create
                           );
                        await FicheiroRecurso.CopyToAsync(stream);
                    }


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
