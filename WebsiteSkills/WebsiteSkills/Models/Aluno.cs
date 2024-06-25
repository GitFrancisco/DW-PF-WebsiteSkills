using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteSkills.Models
{
    /// <summary>
    ///  Classe que representa os alunos
    /// </summary>
    public class Aluno : Utilizadores
    {
        public Aluno()
        {
            ListaSubscricoes = new HashSet<Subscricao>();
        }

        /// <summary>
        /// Chave primária
        /// </summary>
        public int IdAluno { get; set; }
        /// <summary>
        /// Data de Inscrição
        /// </summary>
        public DateTime DataInscricao { get; set; }
        //*******************************************
        // Relações com outras tabelas - Foreign Key

        // Relacionamento 1-N com a tabela "Subscricao"
        /// <summary>
        /// Lista de subscrições do aluno
        /// </summary>
        public ICollection<Subscricao> ListaSubscricoes { get; set; }
        //*******************************************
    }
}
