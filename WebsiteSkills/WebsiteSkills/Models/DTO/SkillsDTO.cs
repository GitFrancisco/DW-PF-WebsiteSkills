using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebsiteSkills.Models.DTO
{
    public class SkillsDTO
    {
        public string Nome { get; set; }
        public string Dificuldade { get; set; }
        public int Tempo { get; set; }
        public string Descricao { get; set; }
        public decimal Custo { get; set; }
        public string? Imagem { get; set; }
    }
}
