using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebsiteSkills.Data;
using WebsiteSkills.Models;

namespace WebsiteSkills.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiUserController : ControllerBase {
        /// <summary>
        /// Vai permitir a interação com a base de dados
        /// </summary>
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Autenticação
        /// Faz a pesquisa de utilizadores, adição, remoção.. 
        /// </summary>
        private readonly UserManager<IdentityUser> _userManager;

        /// <summary>
        /// Autenticação
        /// SignInManager é uma classe que faz parte do Identity, que é um sistema   de autenticação.
        /// </summary>
        public SignInManager<IdentityUser> _signInManager;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="context">Contexto da Base De Dados</param>
        /// <param name="userManager">User Manager</param>
        /// <param name="signInManager">Sign In</param>
        public ApiUserController(ApplicationDbContext context,
               UserManager<IdentityUser> userManager,
               SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        /// <summary>
        /// Criar utilizador
        /// </summary>
        /// <param name="tipoRegisto"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="nome"></param>
        /// <param name="dataNascimento"></param>
        /// <param name="telemovel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("createUser")]
        public async Task<ActionResult> CreateUser([FromQuery] string tipoRegisto, [FromQuery] string email, [FromQuery] string password, [FromQuery] string nome, [FromQuery] DateOnly dataNascimento, [FromQuery] string telemovel)
        {
            // Criação de um utilizador no .AspNetUsers
            IdentityUser identityUser = new IdentityUser();
            identityUser.UserName = email;
            identityUser.Email = email;
            identityUser.NormalizedUserName = identityUser.UserName.ToUpper();
            identityUser.NormalizedEmail = identityUser.Email.ToUpper();
            identityUser.PasswordHash = null;
            identityUser.Id = Guid.NewGuid().ToString();
            identityUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, password);

            // Adicionar utilizador à base de dados
            var result = await _userManager.CreateAsync(identityUser);
            _context.SaveChanges();

            if (result.Succeeded)
            {
                // Se a conta foi criada com sucesso, vamos então a adição do mentor/aluno à base de dados

                // Caso seja mentor
                if (tipoRegisto == "mentor")
                {
                    // Adicionar role ao utilizador
                    var roleResult = await _userManager.AddToRoleAsync(identityUser, "Mentor");
                    if (!roleResult.Succeeded)
                    {
                        return BadRequest("Erro ao adicionar role: " + string.Join(", ", roleResult.Errors.Select(e => e.Description)));
                    }

                    // Criar novo mentor
                    Mentor mentor = new Mentor();
                    mentor.UserId = identityUser.Id;
                    mentor.Nome = nome;
                    mentor.DataNascimento = dataNascimento;
                    mentor.Telemovel = telemovel;

                    _context.AddAsync(mentor);
                    _context.SaveChanges();

                    return Ok("Mentor criado com sucesso");
                } else
                {
                    // Adicionar role ao utilizador
                    var roleResult = await _userManager.AddToRoleAsync(identityUser, "Aluno");
                    if (!roleResult.Succeeded)
                    {
                        return BadRequest("Erro ao adicionar role: " + string.Join(", ", roleResult.Errors.Select(e => e.Description)));
                    }

                    // Criar novo aluno
                    Aluno aluno = new Aluno();
                    aluno.UserId = identityUser.Id;
                    aluno.Nome = nome;
                    aluno.DataNascimento = dataNascimento;
                    aluno.Telemovel = telemovel;

                    _context.AddAsync(aluno);
                    _context.SaveChanges();

                    return Ok("Aluno criado com sucesso");
                }
            }
            else
            {
                return BadRequest("Erro ao criar utilizador: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }


        /// <summary>
        /// Autenticar utilizador
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("loginUser")]
        // Para fazer esta autenticação, é necessário passar o email e a password
        public async Task<ActionResult> SignInUserAsync([FromQuery] string email, [FromQuery] string password)
        {
            // Procura o utilizador na BD
            IdentityUser user = _userManager.FindByEmailAsync(email).Result;

            // se o user existir
            if (user != null)
            {
                // Verifica se a password é válida
                // Se for válida, faz login
                // Esta verificação que é feita com base na hash da password
                PasswordVerificationResult passWorks = new PasswordHasher<IdentityUser>().VerifyHashedPassword(null, user.PasswordHash, password);
                if (passWorks.Equals(PasswordVerificationResult.Success))
                {
                    await _signInManager.SignInAsync(user, false);
                    return Ok("O utilizador deu login com sucesso!");
                }
            }
            // Se não for válido, retorna erro
            return BadRequest("Erro ao fazer login.");
        }

        /// <summary>
        /// Logout do utilizador
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("logoutUser")]
        public async Task<ActionResult> LogoutUser()
        {
            // Faz logout do utilizador
            await _signInManager.SignOutAsync();
            return Ok("O utilizador fez logout com sucesso!");
        }

        /// <summary>
        /// Apaga um utilizador junto com a sua introdução como mentor/aluno na BD
        /// </summary>
        /// <param name="userId">ID do utilizador a apagar</param>
        /// <returns></returns>
        [HttpPost]
        [Route("deleteUser")]
        public async Task<ActionResult> DeleteUser([FromQuery] string userId)
        {
            IdentityUser user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound("Utilizador não encontrado.");
            }

            // Verificar se o utilizador é mentor ou aluno
            // Remove o mentor/aluno da base de dados
            if (_userManager.IsInRoleAsync(user, "Mentor").Result)
            {
                Mentor mentor = _context.Mentor.FirstOrDefault(m => m.UserId == user.Id);
                _context.Mentor.Remove(mentor);
            }
            else
            {
                Aluno aluno = _context.Aluno.FirstOrDefault(a => a.UserId == user.Id);
                _context.Aluno.Remove(aluno);
            }

            // Remove o utilizador da base de dados
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return Ok("Utilizador apagado com sucesso.");
            }
            else
            {
                return BadRequest("Erro ao apagar utilizador: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }

    }

}
