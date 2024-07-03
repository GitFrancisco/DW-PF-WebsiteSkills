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
        [Display(Name = "Nome do Recurso")]
        public string NomeRecurso { get; set; }
        /// <summary>
        /// Conteúdo do Recurso
        /// </summary>
        [Display(Name = "Conteúdo do Recurso")]
        // "?" torna o atributo facultativo
        public string? ConteudoRecurso { get; set; }
        /// <summary>
        /// Tipo de dados do Recurso
        /// </summary>
        [Display(Name = "Tipo de Recurso")]
        public string TipoRecurso { get; set; }

        // *******************************************
        // Relações com outras tabelas - Foreign Keys

        /// <summary>
        /// Chave forasteira "Skills"
        /// </summary>
        [Required]
        [ForeignKey(nameof(Skill))]
        [Display(Name = "Skill associada ao Recurso")]
        public int SkillsFK { get; set; }
        public Skills Skill { get; set; }
    }
}
