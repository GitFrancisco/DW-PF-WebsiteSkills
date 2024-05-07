using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace WebsiteSkills.Models
{

    // Chave Primária Composta
    [PrimaryKey(nameof(SkillsFK), nameof(SubscricaoFK))]

    /// <summary>
    /// Tabela do "meio" entre Subscricao e Skills, que permite a relação entre as duas tabelas
    /// </summary>
    public class Ofere
    {

        // Foreign Key - Tabela "Skills"
        /// <summary>
        /// Chave forasteira da tabela "Skills"
        /// </summary>
        [ForeignKey(nameof(Skills))]
        public int SkillsFK { get; set; }
        public Skills Skills { get; set; }

        // Foreign Key - Tabela "Subscricao"
        /// <summary>
        /// Chave forasteira da tabela "Subscricao"
        /// </summary>
        [ForeignKey(nameof(Subscricao))]
        public int SubscricaoFK { get; set; }
        public Subscricao Subscricao { get; set; }
    }
}
