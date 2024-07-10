using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteSkills.Models
{
    public class Anuncio
    {
        /// <summary>
        /// Chave Primária
        /// </summary>
        [Key]
        public int AnuncioId { get; set; }

        /// <summary>
        /// Texto do Anúncio
        /// </summary>
        [Display(Name = "Anúncio")]
        [StringLength(200)] // Define o tamanho máximo como 200 caracteres
        public string Texto { get; set; }

        /// <summary>
        /// Data de criação do Anúncio
        /// </summary>
        public DateTime DataCriacao { get; set; }

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
