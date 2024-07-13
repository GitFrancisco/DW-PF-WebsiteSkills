using WebsiteSkills.Models;
using Microsoft.AspNetCore.Identity;

namespace WebsiteSkills.Data
{
    internal class DbInitializer
    {
        internal static async void Initialize(ApplicationDbContext dbContext)
        {
            ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));
            dbContext.Database.EnsureCreated();

            // var auxiliar
            bool haAdicao = false;


            // Se não existir Skills, cria-as
            var skills = Array.Empty<Skills>();
            if (!dbContext.Skills.Any())
            {
                skills = [
                   new Skills{ Nome="Origamis", Dificuldade="Fácil", Tempo=10, Descricao="Esta skill fará de ti um professional em Origamis!", Custo=3, Imagem="semImagem"},
                   new Skills{ Nome="Cozinhar Bolos", Dificuldade="Fácil", Tempo=5, Descricao="Aprenda a cozinhar bolos!", Custo=3, Imagem="semImagem"},
                   new Skills{ Nome="Escrita", Dificuldade="Médio", Tempo=160, Descricao="Escreva como um profissional.", Custo=10, Imagem="semImagem"},
                   new Skills{ Nome="Comunicação", Dificuldade="Difícil", Tempo=100, Descricao="Expressa-te melhor!", Custo=10, Imagem="semImagem"},
                   new Skills{ Nome="Iniciação em Python", Dificuldade="Fácil", Tempo=15, Descricao="Começa a tua jornada em Python!", Custo=5, Imagem="semImagem"},
                ];
                await dbContext.Skills.AddRangeAsync(skills);
                haAdicao = true;
            }

            // Se não houver Recursos, cria-os
            var recursos = Array.Empty<Recurso>();
            if (!dbContext.Recurso.Any())
            {
                recursos = [
                   new Recurso{ NomeRecurso="Manual", TipoRecurso="PDF", ConteudoRecurso="empty", Skill = skills[0]},
                   new Recurso{ NomeRecurso="Receita Bolo de Canela", TipoRecurso="Texto", ConteudoRecurso="100g de açúcar...", Skill = skills[1]},
                   new Recurso{ NomeRecurso="Exemplo de carta", TipoRecurso="Imagem", ConteudoRecurso="empty", Skill = skills[2]},
                   new Recurso{ NomeRecurso="Guia para comunicação", TipoRecurso="Texto", ConteudoRecurso="Primeiramente...", Skill = skills[3]},
                   new Recurso{ NomeRecurso="Documentação em Python", TipoRecurso="Texto", ConteudoRecurso="https://docs.python.org/3/", Skill = skills[4]}
                ];
                await dbContext.Recurso.AddRangeAsync(recursos);
                haAdicao = true;
            }   

            // Se não houver Anúncios, cria-os
            var anuncios = Array.Empty<Anuncio>();
            if (!dbContext.Recurso.Any()){

                anuncios = [
                    new Anuncio{Texto = "Aula hoje às 19h, na plataforma Teams!", DataCriacao = DateTime.Now, Skill = skills[0]},
                    new Anuncio{Texto = "Aula hoje às 16h, na plataforma Teams!", DataCriacao = DateTime.Now, Skill = skills[1]},
                    new Anuncio{Texto = "Aula hoje às 13h, na plataforma Teams!", DataCriacao = DateTime.Now, Skill = skills[2]},
                    new Anuncio{Texto = "Aula hoje às 11h, na plataforma Teams!", DataCriacao = DateTime.Now, Skill = skills[3]},
                    new Anuncio{Texto = "Aula hoje às 9h, na plataforma Teams!", DataCriacao = DateTime.Now, Skill = skills[4]},
                ];

                await dbContext.Anuncio.AddRangeAsync(anuncios);
                haAdicao = true;
            }

            // Se não houver Utilizadores Identity, cria-os
            var users = Array.Empty<IdentityUser>();
            //a hasher to hash the password before seeding the user to the db
            var hasher = new PasswordHasher<IdentityUser>();

            if (!dbContext.Users.Any())
            {
                users = [
                   new IdentityUser{UserName="aluno_mario@mail.pt", NormalizedUserName="ALUNO_MARIO@MAIL.PT",
                   Email="aluno_mario@mail.pt",NormalizedEmail="ALUNO.MARIO@MAIL.PT", EmailConfirmed=true,
                   SecurityStamp="5ZPZEF6SBW7IU4M344XNLT4NN5RO4GRU", ConcurrencyStamp="c86d8254-dd50-44be-8561-d2d44d4bbb2f",
                   PasswordHash=hasher.HashPassword(null,"Mario123!") },

                   new IdentityUser{UserName="aluno_maria@mail.pt", NormalizedUserName="ALUNO_MARIA@MAIL.PT",
                   Email="aluno_maria@mail.pt",NormalizedEmail="ALUNO_MARIA@MAIL.PT", EmailConfirmed=true,
                   SecurityStamp="5ZPZEF6SBW7222M344XNLT4NN5RO4GRU", ConcurrencyStamp="c8644454-dd50-44be-8561-d2d44d4bbb2f",
                   PasswordHash=hasher.HashPassword(null,"Maria123!") },

                   new IdentityUser{UserName="mentor_jose@mail.pt", NormalizedUserName="MENTOR_JOSE@MAIL.PT",
                   Email="mentor_jose@mail.pt",NormalizedEmail="MENTOR_JOSE@MAIL.PT", EmailConfirmed=true,
                   SecurityStamp="5111EF6SBW7222M344XNLT4NN5RO4GRU", ConcurrencyStamp="c8644454-dd50-44be-8561-d1114d4bbb2f",
                   PasswordHash=hasher.HashPassword(null,"Jose123!") },

                   new IdentityUser{UserName="mentor_lucia@mail.pt", NormalizedUserName="MENTOR_LUCIA@MAIL.PT",
                   Email="mentor_lucia@mail.pt",NormalizedEmail="MENTOR_LUCIA@MAIL.PT", EmailConfirmed=true,
                   SecurityStamp="5111EF6SBW7222M344XNLT4N11RO4GRU", ConcurrencyStamp="c8644454-dddd-44be-8561-d1114d4bbb2f",
                   PasswordHash=hasher.HashPassword(null,"Lucia123!") },

                   new IdentityUser{UserName="mentor_luis@mail.pt", NormalizedUserName="MENTOR_LUIS@MAIL.PT",
                   Email="mentor_luis@mail.pt",NormalizedEmail="MENTOR_LUIS@MAIL.PT", EmailConfirmed=true,
                   SecurityStamp="5111EF6SBW7222M344XNLT4111RO4GRU", ConcurrencyStamp="c8644454-dd50-43be-8561-d1114d4bbb2f",
                   PasswordHash=hasher.HashPassword(null,"Luis123!") },

                   new IdentityUser{UserName="admin@mail.pt", NormalizedUserName="ADMIN@MAIL.PT",
                   Email="admin@mail.pt",NormalizedEmail="ADMIN@MAIL.PT", EmailConfirmed=true,
                   SecurityStamp="511186SBW7222M344XNLT4111RO4GRU", ConcurrencyStamp="c8644454-dd50-488e-8561-d1114d4bbb2f",
                   PasswordHash=hasher.HashPassword(null,"Admin123!") }
                   ];

                await dbContext.Users.AddRangeAsync(users);

                // Adicionar Roles
                var userRoles = new List<IdentityUserRole<string>>
                {
                    new IdentityUserRole<string> { UserId = users[0].Id, RoleId = "2" },
                    new IdentityUserRole<string> { UserId = users[1].Id, RoleId = "2" },
                    new IdentityUserRole<string> { UserId = users[2].Id, RoleId = "1" },
                    new IdentityUserRole<string> { UserId = users[3].Id, RoleId = "1" },
                    new IdentityUserRole<string> { UserId = users[4].Id, RoleId = "1" },
                    new IdentityUserRole<string> { UserId = users[5].Id, RoleId = "3" }
                };

                await dbContext.UserRoles.AddRangeAsync(userRoles);

                haAdicao = true;
            }

            // Se não houver Alunos, cria-os
            var alunos = Array.Empty<Aluno>();
            if (!dbContext.Aluno.Any())
            {
                alunos = [
                   new Aluno{Nome="Mário", DataNascimento=DateOnly.Parse("2000-12-15"),Telemovel="966666666", UserId = users[0].Id},
                   new Aluno{Nome="Maria", DataNascimento=DateOnly.Parse("2000-12-16"),Telemovel="966666665", UserId = users[1].Id}
                ];

                // Subscrever Alunos a Skills
                alunos[0].ListaSubscricoes.Add(new Subscricoes { dataSubscricao = DateTime.Now, Skills = skills[0] });
                alunos[1].ListaSubscricoes.Add(new Subscricoes { dataSubscricao = DateTime.Now, Skills = skills[1] });

                await dbContext.Aluno.AddRangeAsync(alunos);
                haAdicao = true;
            }

            // Se não houver Professores, cria-os
            var mentores = Array.Empty<Mentor>();
            if (!dbContext.Mentor.Any())
            {
                mentores = [
                   new Mentor { Nome="José Mendes", DataNascimento=DateOnly.Parse("1970-04-10"), Telemovel="919876543" , UserId=users[2].Id },
                   new Mentor { Nome="Lúcia José", DataNascimento=DateOnly.Parse("1988-09-12"), Telemovel="918076543" , UserId=users[3].Id  },
                   new Mentor { Nome="Luís Silva", DataNascimento=DateOnly.Parse("1988-01-12"), Telemovel="918376543" , UserId=users[4].Id  }
                  ];

                // Adicionar Skills aos Mentores
                mentores[0].ListaSkills.Add(skills[0]);
                mentores[1].ListaSkills.Add(skills[1]);

                await dbContext.Mentor.AddRangeAsync(mentores);
                haAdicao = true;
            }

            try
            {
                if (haAdicao)
                {
                    // tornar persistentes os dados
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
