using System.ComponentModel.DataAnnotations;

namespace WebsiteSkills.Models
{
    /// <summary>
    /// Classe que representa os utilizadores
    /// </summary>
    public class Utilizadores
    {
        /// <summary>
        /// Chave Primária
        /// </summary> 
        [Key] // Chave primária - Utilizadores
        public int Id { get; set; }
        /// <summary>
        /// Nome
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Data de Nascimento
        /// </summary>
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true,
                       DataFormatString = "{0:dd-MM-yyyy}")]
        [Required(ErrorMessage = "A {0} é de preenchimento obrigatório")]
        public DateOnly DataNascimento { get; set; }

        /// <summary>
        /// Número de Telemóvel do Utilizador
        /// </summary>
        [Display(Name = "Telemóvel")]
        [StringLength(9)]
        [RegularExpression("9[1236][0-9]{7}",
             ErrorMessage = "O Número de {0} só aceita 9 digitos")]
        public string Telemovel { get; set; }

        /// <summary>
        /// Atributo para fazer a ligação entre a base de dados do 'negócio' e a base de dados da 'autenticação'
        /// </summary>
        [StringLength(40)]
        public string UserId { get; set; }
    }
}
