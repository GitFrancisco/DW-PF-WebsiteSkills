using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteSkills.Models
{
    /// <summary>
    /// Classe que representa os Recursos de cada Skill
    /// </summary>
    public class Recurso 
    {
        /// <summary>
        /// Chave Primária
        /// </summary>
        [Key] // Primary Key - Recurso
        public int IdRecurso { get; set; }
        /// <summary>
        /// Nome do Recurso
        /// </summary>
        public string NomeRecurso { get; set; }
        /// <summary>
        /// Conteúdo do Recurso
        /// </summary>
        public string ConteudoRecurso { get; set; }
        /// <summary>
        /// Tipo de dados do Recurso
        /// </summary>
        public string TipoRecurso { get; set; }

        // *******************************************
        // Relações com outras tabelas - Foreign Keys

        /// <summary>
        /// Chave forasteira "Skills"
        /// </summary>
        [Required]
        [ForeignKey(nameof(Skills))]
        public int SkillsFK { get; set; }
        public Skills Skills { get; set; }
    }
}
