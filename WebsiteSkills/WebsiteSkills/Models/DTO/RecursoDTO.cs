using System.ComponentModel.DataAnnotations;

namespace WebsiteSkills.Models.DTO
{
    public class RecursoDTO
    {
        public string NomeRecurso { get; set; }
        public string TipoRecurso { get; set; }
        public int SkillsFK { get; set; }
    }
}
