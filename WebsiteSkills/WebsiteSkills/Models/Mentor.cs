using System.Collections.Generic;

namespace WebsiteSkills.Models
{
    /// <summary>
    /// Classe que representa os mentores
    /// </summary>
    public class Mentor : Utilizadores
    {
        public Mentor()
        {
            ListaSkills = new HashSet<Skills>();
        }
        
        //*******************************************
        // Relações com outras tabelas - Foreign Keys

        /// <summary>
        /// Lista de Skills que o mentor ensina
        /// </summary>
        public ICollection<Skills> ListaSkills { get; set; }
    }
}
