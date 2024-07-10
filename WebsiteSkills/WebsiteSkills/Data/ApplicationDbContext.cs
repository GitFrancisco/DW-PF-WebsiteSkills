using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebsiteSkills.Models;

namespace WebsiteSkills.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Skills> Skills { get; set; }
        public DbSet<Aluno> Aluno { get; set; }
        public DbSet<Mentor> Mentor { get; set; }
        public DbSet<Subscricoes> Subscricoes { get; set; }
        public DbSet<Recurso> Recurso { get; set; }
        public DbSet<Utilizadores> Utilizadores { get; set; }

        public DbSet<Anuncio> Anuncio { get; set; }
    }
}
