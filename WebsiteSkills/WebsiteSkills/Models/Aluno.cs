using System.Collections.Generic;

namespace WebsiteSkills.Models
{
    public class Aluno : Utilizadores
    {
        public Aluno()
        {
            ListaSubscricoes = new HashSet<Subscricoes>();
        }

        /// <summary>
        /// Número de aluno
        /// </summary>
        public int NumAluno { get; set; }

        // Relações com outras tabelas - Foreign Key

        /// <summary>
        /// Lista de subscrições do aluno
        /// </summary>
        public ICollection<Subscricoes> ListaSubscricoes { get; set; }
    }
}
