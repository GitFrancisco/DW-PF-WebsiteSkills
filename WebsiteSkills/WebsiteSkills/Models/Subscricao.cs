using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteSkills.Models
{
    /// <summary>
    /// Tabela que representa a Subscrição
    /// </summary>
    public class Subscricao
    {
        /// <summary>
        /// Chave primária
        /// </summary>
        [Key] // Primary Key - Subscricao
        public int Id { get; set; }
        /// <summary>
        /// Preço
        /// </summary>
        public decimal Preco { get; set; }

        // *******************************************
        // Relações com outras tabelas - Foreign Keys

        /// <summary>
        /// Chave Forasteira "Aluno"
        /// </summary>
        [ForeignKey(nameof(Aluno))]
        public int AlunoFK { get; set; }
        public Aluno Aluno { get; set; }
        // *******************************************
    }
}
