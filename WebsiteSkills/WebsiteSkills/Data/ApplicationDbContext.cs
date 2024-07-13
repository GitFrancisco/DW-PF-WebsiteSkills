using Microsoft.AspNetCore.Identity;
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Seed Roles
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "1", Name = "Mentor", NormalizedName = "MENTOR" },
                new IdentityRole { Id = "2", Name = "Aluno", NormalizedName = "ALUNO" },
                new IdentityRole { Id = "3", Name = "Administrador", NormalizedName = "ADMINISTRADOR" }
                );
        }

        // Definição das Tabelas

        /// <summary>
        /// Tabela Skills
        /// </summary>
        public DbSet<Skills> Skills { get; set; }
        /// <summary>
        /// Tabela Ãluno
        /// </summary>
        public DbSet<Aluno> Aluno { get; set; }
        /// <summary>
        /// Tabela Mentor
        /// </summary>
        public DbSet<Mentor> Mentor { get; set; }
        /// <summary>
        /// Tabela Subscricoes
        /// </summary>
        public DbSet<Subscricoes> Subscricoes { get; set; }
        /// <summary>
        /// Tabela Recurso
        /// </summary>
        public DbSet<Recurso> Recurso { get; set; }
        /// <summary>
        /// Tabela Utilizadores
        /// </summary>
        public DbSet<Utilizadores> Utilizadores { get; set; }
        /// <summary>
        /// Tabela Anuncio
        /// </summary>
        public DbSet<Anuncio> Anuncio { get; set; }
    }
}
