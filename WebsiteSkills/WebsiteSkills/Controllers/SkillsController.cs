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
            // Obter o ID do utilizador autenticado
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            // Procurar o aluno na BD
            var aluno = userId != null ? _context.Aluno.FirstOrDefault(a => a.UserId == userId) : null;

            // Obter todas as skills
            var skills = await _context.Skills.ToListAsync();

            // Mapear as skills para a ViewModel
            var skillsViewModel = skills.Select(skill => new SkillViewModel
            {
                Skill = skill,
                IsSubscribed = aluno != null && _context.Subscricoes.Any(s => s.SkillsFK == skill.SkillsId && s.AlunoFK == aluno.Id)
            }).ToList();

            // Passar a lista de skills para a View
            return View(skillsViewModel);
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
        [Authorize(Roles="Administrador")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Skills/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrador")]
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
        [Authorize(Roles = "Administrador")]
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
        [Authorize(Roles = "Administrador")]
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
        [Authorize(Roles = "Administrador")]
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
        [Authorize(Roles = "Administrador")]
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

        // Método GET para exibir o formulário de Checkout
        [Authorize(Roles = "Aluno,Administrador")]
        [HttpGet]
        public async Task<IActionResult> Checkout(int id)
        {
            // Obter a skill
            var skill = await _context.Skills.FindAsync(id);

            // Se a skill não existir, devolve erro
            if (skill == null)
            {
                return NotFound();
            }

            return View(skill);
        }

        // Método POST para processar a submissão do formulário de Checkout
        [Authorize(Roles = "Aluno,Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckoutConfirmed(int id, string CCnum, string CCnome, string CCvalidade, string CCcvv)
        {
            // Verifica se os campos do cartão de crédito estão preenchidos
            if (string.IsNullOrEmpty(CCnum) || string.IsNullOrEmpty(CCnome) || string.IsNullOrEmpty(CCvalidade) || string.IsNullOrEmpty(CCcvv))
            {
                var skill = await _context.Skills.FindAsync(id);
                ViewBag.Message = "Todos os campos do cartão de crédito são obrigatórios.";
                return View("Checkout", skill);
            }

            try
            {
                // Obter a skill
                var skill = await _context.Skills.FindAsync(id);

                // Obtém o ID do aluno autenticado
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                // Procura o aluno na BD
                var aluno = _context.Aluno.FirstOrDefault(a => a.UserId == userId);

                // Se o aluno não existir, devolve erro
                if (aluno == null)
                {
                    return NotFound("Aluno não encontrado.");
                }

                // Se a skill não existir, devolve erro
                if (skill == null)
                {
                    return NotFound();
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
                    // Adiciona a subscrição à BD
                    _context.Subscricoes.Add(subscricao);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    ViewBag.Message = "Você já está subscrito a esta Skill.";
                    return View("Checkout", skill);
                }

                ViewBag.Message = "Acabou de subscrever esta Skill!";
                return View("Checkout", skill);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro interno.");
            }
        }

        // GET: Skills/Recursos
        [Authorize]
        public async Task<IActionResult> Recursos(int id)
        {
            // Obter o ID do utilizador autenticado
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var skill = await _context.Skills.FindAsync(id);

            // Verificação se o Mentor ensina a Skill
            if (User.IsInRole("Mentor"))
            {
                // Obter o mentor autenticado
                var mentor = userId != null ? _context.Mentor.Include(m => m.ListaSkills).FirstOrDefault(m => m.UserId == userId) : null;

                // Verificar se o mentor ensina a skill
                if (!mentor.ListaSkills.Contains(skill))
                {
                    return NotFound();
                }

            }

            // Verificação se o Aluno está subscrito a Skill
            if (User.IsInRole("Aluno"))
            {
                // Obter o aluno autenticado
                var aluno = userId != null ? _context.Aluno.FirstOrDefault(m => m.UserId == userId) : null;

                // Verificar se o aluno está subscrito à skill
                var subscricao = aluno != null ? _context.Subscricoes.FirstOrDefault(s => s.SkillsFK == id && s.AlunoFK == aluno.Id) : null;

                // Se o aluno não estiver subscrito, devolve erro
                if (subscricao == null)
                {
                    return NotFound();
                }
            }

            // Se a skill não existir, devolve erro
            if (skill == null)
            {
                return NotFound();
            }

            // Obter a lista de recursos associados à skill
            var recursos = await _context.Recurso.Where(r => r.SkillsFK == id).ToListAsync();

            // Passar a lista de recursos para a View
            ViewBag.Recursos = recursos;

            return View();

        }

        // Método GET para exibir o formulário de adicionar anuncios
        [Authorize(Roles = "Aluno,Mentor,Administrador")]
        [HttpGet]
        public async Task<IActionResult> Anuncios(int id)
        {
            // Obter a skill
            var skill = await _context.Skills.FindAsync(id);

            // Se a skill não existir, devolve erro
            if (skill == null)
            {
                return NotFound();
            }

            ViewBag.Anuncios = await _context.Anuncio.Where(anuncio => anuncio.SkillsFK == id).OrderByDescending(anuncio => anuncio.DataCriacao).ToListAsync();
            return View(skill);
        }

        // Método GET para exibir o formulário de Criar Anuncios
        [Authorize(Roles = "Mentor,Administrador")]
        [HttpGet]
        public async Task<IActionResult> CriarAnuncios(int id)
        {
            // Obter a skill
            var skill = await _context.Skills.FindAsync(id);

            // Se a skill não existir, devolve erro
            if (skill == null)
            {
                return NotFound();
            }

            return View(skill);
        }

        // Método POST para processar a submissão do formulário de adicionar anuncios
        [Authorize(Roles = "Mentor,Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CriarAnuncios(int id, string texto)
        {
            if (ModelState.IsValid)
            {
                // Criar um novo anúncio com os dados recebidos do formulário
                var anuncio = new Anuncio
                {
                    SkillsFK = id,
                    Texto = texto,
                    DataCriacao = DateTime.Now // Define a data atual
                };

                // Adicionar o novo anúncio ao banco de dados
                _context.Anuncio.Add(anuncio);
                await _context.SaveChangesAsync();

                // Redirecionar para a página de anúncios da mesma skill
                return RedirectToAction("Anuncios", new { id = id });
            }

            return RedirectToAction("Anuncios", new { id = id });
        }

    }
}
