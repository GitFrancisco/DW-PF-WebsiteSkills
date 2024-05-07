using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteSkills.Models
{
    /// <summary>
    /// Classe que representa os mentores
    /// </summary>
    public class Mentor : Utilizadores
    {
        public Mentor()
        {
            ListaEnsina = new HashSet<Ensina>();
        }

        /// <summary>
        /// Chave primária
        /// </summary>
        public int NumMentor { get; set; }

        //*******************************************
        // Relações com outras tabelas - Foreign Keys

        /// <summary>
        /// Chave forasteira "Skills"
        /// </summary>
        [ForeignKey(nameof(Skills))]
        public int SkillsFK { get; set; }
        public Skills Skills { get; set; }

        /// <summary>
        /// Lista de Skills que o mentor ensina
        /// </summary>
        public ICollection<Ensina> ListaEnsina { get; set; }
        //*******************************************   
    }
}

