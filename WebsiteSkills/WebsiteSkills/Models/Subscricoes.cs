using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteSkills.Models
{
    // Chave Primária Composta
    [PrimaryKey(nameof(SkillsFK), nameof(AlunoFK))]
    public class Subscricoes
    {
        /// <summary>
        /// Data em que foi efetuada a subscrição
        /// </summary>
        public DateTime dataSubscricao { get; set; }

        // Foreign Key - Tabela "Skills"
        /// <summary>
        /// Chave forasteira da tabela "Skills"
        /// </summary>
        [ForeignKey(nameof(Skills))]
        public int SkillsFK { get; set; }
        public Skills Skills { get; set; }

        // Foreign Key - Tabela "Aluno"
        /// <summary>
        /// Chave forasteira da tabela "Aluno"
        /// </summary>
        [ForeignKey(nameof(Aluno))]
        public int AlunoFK { get; set; }
        public Aluno Aluno { get; set; }
    }
}
