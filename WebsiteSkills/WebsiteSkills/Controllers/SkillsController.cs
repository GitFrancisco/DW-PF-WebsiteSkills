using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebsiteSkills.Data;
using WebsiteSkills.Models;

namespace WebsiteSkills.Controllers
{
    public class SkillsController : Controller
    {
        private readonly ApplicationDbContext _context;
        // Relativo a wwwroot
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SkillsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            // Adição ao acesso à wwwroot (onde estão as imagens) no construtor
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Skills
        public async Task<IActionResult> Index()
        {
            return View(await _context.Skills.ToListAsync());
        }

        // GET: Skills/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skills = await _context.Skills
                .FirstOrDefaultAsync(m => m.SkillsId == id);
            if (skills == null)
            {
                return NotFound();
            }

            return View(skills);
        }

        // GET: Skills/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Skills/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Dificuldade,Tempo,Descricao,CustoAux,Custo")] Skills skills, IFormFile ImagemSkill)
        {
            // Variáveis Auxiliares
            // utilizada para guardar o nome da imagem
            string nomeImagem = "";
            // utilizada para controlar se há um imagem
            bool existeImagem = false;
            // transferir o valor de CustoAux para Custo
            skills.Custo = Convert.ToDecimal(skills.CustoAux.Replace('.', ','));

            // Se não guarda a imagem, devolve à View
            if (ImagemSkill == null)
            {
                ModelState.AddModelError("",
                   "É obrigatório o uso de uma Imagem para a representação da Skill.");
                return View(skills);
            }
            // Neste caso, existe ficheiro mas temos de confirmar se é uma imagem
            else
            {
                // Verifica se o ficheiro é dos tipos aceites (PNG, JPG e JPEG)
                if (!(ImagemSkill.ContentType == "image/png" || ImagemSkill.ContentType == "image/jpg" ||
                       ImagemSkill.ContentType == "image/jpeg"))
                {
                    // Devolve o controlo à View
                    ModelState.AddModelError("",
                   "A imagem tem de ser do tipo: PNG, JPG ou JPEG.");
                    return View(skills);
                }
                // Aqui, significa que há ficheiro e é uma imagem
                else
                {
                    // A variável de controlo existeImagem passa a ser verdadeira
                    existeImagem = true;
                    // Obter o nome aleatório e único para a imagem
                    Guid uid = Guid.NewGuid();
                    nomeImagem = uid.ToString();
                    // Obtém a extensão da imagem enviada
                    string extensao = Path.GetExtension(ImagemSkill.FileName);
                    // Adicionar a extensão ao nome da imagem
                    nomeImagem += extensao;
                    // Adicionar o nome do ficheiro ao objeto enviado
                    skills.Imagem = nomeImagem;
                }
            }

            if (ModelState.IsValid)
            {
                // Adiciona o objeto enviado à BD
                _context.Add(skills);
                // Faz o envio dos dados para a BD
                await _context.SaveChangesAsync();

                // Se existe imagem, guardar a imagem no disco rígido do servidor
                if (existeImagem)
                {
                    // Define a localização da pasta que irá conter as Imagens
                    string PastaImagens = Path.Combine(_webHostEnvironment.WebRootPath, "Imagens");
                    
                    // Se já existir o diretório, não é necessário criar
                    if (!Directory.Exists(PastaImagens))
                    {
                        // Caso contrário, cria o diretório
                        Directory.CreateDirectory(PastaImagens);
                    }

                    // Junta o nome do ficheiro ao caminho da pasta
                    string nomeFinalImagem =
                       Path.Combine(PastaImagens, nomeImagem);

                    // Guarda a imagem no disco rígido
                    using var stream = new FileStream(
                       nomeFinalImagem, FileMode.Create
                       );
                    await ImagemSkill.CopyToAsync(stream);
                }

                // redireciona o utilizador para a página Index
                return RedirectToAction(nameof(Index));
            }

            // Se o ModelState não for válido, retornar a View com os dados atuais
            return View(skills);
        }


        // GET: Skills/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skills = await _context.Skills.FindAsync(id);
            if (skills == null)
            {
                return NotFound();

            }
            //Passar o dado do custo para a View
            ViewBag.CustoAtual = skills.Custo;
            return View(skills);
        }

        // POST: Skills/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SkillsId,Nome,Dificuldade,Tempo,Descricao,Custo,CustoAux")] Skills skills, IFormFile ImagemSkill)
        {
            if (id != skills.SkillsId)
            {
                return NotFound();
            }

            // Variáveis Auxiliares
            // utilizada para guardar o nome da imagem
            string nomeImagem = "";
            // utilizada para controlar se há um imagem
            bool existeImagem = false;
            // transferir o valor de CustoAux para Custo
            skills.Custo = Convert.ToDecimal(skills.CustoAux.Replace('.', ','));

            // Se não guarda a imagem, devolve à View
            if (ImagemSkill == null)
            {
                //A skill que esta a ser editada recebe os valores que tinha anteriormente
                var existingSkill = await _context.Skills.FindAsync(id);
                if (existingSkill != null)
                {
                    //substitui a imagem que esta a ser editada pela que já existe
                    skills.Imagem = existingSkill.Imagem;
                    ModelState.Remove("ImagemSkill");
                    _context.Entry(existingSkill).State = EntityState.Detached;
                }
            }

            // Neste caso, existe ficheiro mas temos de confirmar se é uma imagem
            else
            {
                // Verifica se o ficheiro é dos tipos aceites (PNG, JPG e JPEG)
                if (!(ImagemSkill.ContentType == "image/png" || ImagemSkill.ContentType == "image/jpg" ||
                       ImagemSkill.ContentType == "image/jpeg"))
                {
                    // Devolve o controlo à View
                    ModelState.AddModelError("",
                   "A imagem tem de ser do tipo: PNG, JPG ou JPEG.");
                    return View(skills);
                }
                // Aqui, significa que há ficheiro e é uma imagem
                else
                {
                    // A variável de controlo existeImagem passa a ser verdadeira
                    existeImagem = true;
                    // Obter o nome aleatório e único para a imagem
                    Guid uid = Guid.NewGuid();
                    nomeImagem = uid.ToString();
                    // Obtém a extensão da imagem enviada
                    string extensao = Path.GetExtension(ImagemSkill.FileName);
                    // Adicionar a extensão ao nome da imagem
                    nomeImagem += extensao;
                    // Adicionar o nome do ficheiro ao objeto enviado
                    skills.Imagem = nomeImagem;
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                     //manda os dados para a BD
                    _context.Update(skills);
                    await _context.SaveChangesAsync();

                    // Se existe imagem, guardar a imagem no disco rígido do servidor
                    if (existeImagem)
                    {
                        string PastaImagens = Path.Combine(_webHostEnvironment.WebRootPath, "Imagens");

                        if (!Directory.Exists(PastaImagens))
                        {
                            Directory.CreateDirectory(PastaImagens);
                        }

                        string nomeFinalImagem = Path.Combine(PastaImagens, nomeImagem);

                        using var stream = new FileStream(nomeFinalImagem, FileMode.Create);
                        await ImagemSkill.CopyToAsync(stream);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SkillsExists(skills.SkillsId))
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
            return View(skills);
        }

        // GET: Skills/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skills = await _context.Skills
                .FirstOrDefaultAsync(m => m.SkillsId == id);
            if (skills == null)
            {
                return NotFound();
            }

            return View(skills);
        }

        // POST: Skills/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var skills = await _context.Skills.FindAsync(id);
            if (skills != null)
            {
                _context.Skills.Remove(skills);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SkillsExists(int id)
        {
            return _context.Skills.Any(e => e.SkillsId == id);
        }

        [Authorize(Roles = "Mentor")]
        public IActionResult AdicionarSkillsMentores(int id)
        {
            // Obtém o ID do mentor autenticado
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            // Procura o mentor na BD
            var mentor = _context.Mentor.FirstOrDefault(m => m.UserId == userId);

            // Se o mentor não existir, devolve erro
            if (mentor == null)
            {
                return NotFound("Mentor não encontrado.");
            }

            // Procura a skill na BD
            var skill = _context.Skills.Find(id);
            if (skill == null)
            {
                return NotFound("Skill não encontrada.");
            }

            // Se a skill não existir na lista de skills do mentor, adiciona
            if (!mentor.ListaSkills.Contains(skill))
            {
                mentor.ListaSkills.Add(skill);
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }


        [Authorize(Roles = "Aluno")]
        public IActionResult AdicionarSubscricaoAluno(int id)
        {
            // Obtém o ID do aluno autenticado
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Procura o aluno na BD
            var aluno = _context.Aluno.FirstOrDefault(a => a.UserId == userId);

            // Se o aluno não existir, devolve erro
            if (aluno == null)
            {
                return NotFound("Aluno não encontrado.");
            }

            // Verifica se a skill existe
            var skill = _context.Skills.FirstOrDefault(s => s.SkillsId == id);
            if (skill == null)
            {
                return NotFound("Skill não encontrada.");
            }

            // Verifica se a subscrição já existe
            var subscricaoExistente = _context.Subscricoes
                .FirstOrDefault(s => s.SkillsFK == skill.SkillsId && s.AlunoFK == aluno.Id);

            if (subscricaoExistente == null)
            {
                var subscricao = new Subscricoes
                {
                    SkillsFK = skill.SkillsId,
                    AlunoFK = aluno.Id,
                    dataSubscricao = DateTime.Now
                };

                _context.Subscricoes.Add(subscricao);
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }

    }
}
