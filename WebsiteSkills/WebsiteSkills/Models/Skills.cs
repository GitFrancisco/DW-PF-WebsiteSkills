using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebsiteSkills.Models
{
    /// <summary>
    /// Classe que representa as "Skills"
    /// </summary>
    public class Skills
    {
        public Skills()
        {
            ListaEnsina = new HashSet<Ensina>();
            ListaRecursos = new HashSet<Recurso>();
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
        public string Descricao { get; set; }

        // *******************************************
        // Relações com outras tabelas - Foreign Keys

        // Relacionamento 1-N com a tabela "Ofere"
        // Relação de Chave Estrangeira com a tabela "Ofere"

        /// <summary>
        /// Chave (composta) Forasteira para a tabela "Ofere"
        /// </summary>
        [ForeignKey(nameof(Ofere))] // Especifica a relação com a classe Ofere
        [Column(Order = 1)] // Define a ordem da coluna na chave composta
        public int SkillsFK { get; set; }
        /// <summary>
        /// Chave (composta) Forasteira para a tabela "Ofere"
        /// </summary>
        [ForeignKey(nameof(Ofere))] // Especifica a relação com a classe Ofere
        [Column(Order = 2)] // Define a ordem da coluna na chave composta
        public int SubscricaoFK { get; set; }

        // Relacionamento 1-N com a tabela "Recurso"
        /// <summary>
        /// Lista de Recursos associados à Skill
        /// </summary>
        public ICollection<Recurso> ListaRecursos { get; set; }

        // Relacionamento 1-N com a tabela "Ensina"
        /// <summary>
        /// Lista de "Ensina" associados à Skill
        /// </summary>
        public ICollection<Ensina> ListaEnsina { get; set; }
        // *******************************************
    }
}
    
