using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebsiteSkills.Models
{
    public class Skills
    {
        /// <summary>
        /// Classe que representa as "Skills"
        /// </summary>
        public Skills()
        {
            ListaMentores = new HashSet<Mentor>();
            ListaRecursos = new HashSet<Recurso>();
            ListaSubscricoes = new HashSet<Subscricoes>();
        }

        /// <summary>
        /// Chave Primária
        /// </summary>
        [Key] // Primary Key - Skills
        public int SkillsId { get; set; }
        /// <summary>
        /// Nome da Skill
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// Nível de Dificuldade
        /// </summary>
        public string Dificuldade { get; set; }
        /// <summary>
        /// Tempo estimado para a conclusão da Skill
        /// </summary>
        public int Tempo { get; set; }
        /// <summary>
        /// Descrição da Skill
        /// </summary>
        [Display(Name ="Descrição")]
        public string Descricao { get; set; }

        /// <summary>
        /// Lista de Subscrições associados à Skill
        /// </summary>
        public ICollection<Subscricoes> ListaSubscricoes { get; set; }
        /// <summary>
        /// Lista de Recursos associados à Skill
        /// </summary>
        public ICollection<Recurso> ListaRecursos { get; set; }
        /// <summary>
        /// Lista de Mentores associados à Skill
        /// </summary>
        public ICollection<Mentor> ListaMentores { get; set; }
    }
}
