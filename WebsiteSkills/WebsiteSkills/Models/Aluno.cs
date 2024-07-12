using System.Collections.Generic;

namespace WebsiteSkills.Models
{
    public class Aluno : Utilizadores
    {
        public Aluno() 
        {
            ListaSubscricoes = new HashSet<Subscricoes>();
        }

        // Relações com outras tabelas - Foreign Key

        /// <summary>
        /// Lista de subscrições do aluno
        /// </summary>
        public ICollection<Subscricoes> ListaSubscricoes { get; set; }
    }
}
