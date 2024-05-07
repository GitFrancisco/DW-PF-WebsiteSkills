using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteSkills.Models
{
    // Chave Primária Composta
    [PrimaryKey(nameof(SkillsFK), nameof(MentorFK))]

    /// <summary>
    /// Tabela do "meio" entre Mentor e Skills, representa a relação de ensino entre um Mentor e uma Skill
    /// </summary>
    public class Ensina
    {
            
        // Foreign Key - Tabela "Skills"
        /// <summary>
        /// Chave Forasteira "Skills"
        /// </summary>
        [ForeignKey(nameof(Skills))]
        public int SkillsFK { get; set; }
        public Skills Skills { get; set; }

        // Foreign Key - Tabela "Mentor"
        /// <summary>
        /// Chave Forasteira "Mentor"
        /// </summary>
        [ForeignKey(nameof(Mentor))]
        public int MentorFK { get; set; }
        public Mentor Mentor { get; set; }
    }
}
