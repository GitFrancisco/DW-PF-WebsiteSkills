using System.ComponentModel.DataAnnotations;

namespace WebsiteSkills.Models
{
    public class Utilizadores
    {
        /// <summary>
        /// Chave Primária
        /// </summary>
        [Key] // Chave primária - Utilizadores
        public int Id { get; set; }
        /// <summary>
        /// Nome
        /// </summary>
        public string Nome { get; set; }
    }
}
