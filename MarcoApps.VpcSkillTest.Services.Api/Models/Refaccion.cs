using System.ComponentModel.DataAnnotations;

namespace MarcoApps.VpcSkillTest.Services.Api.Models
{
    public class Refaccion
    {
        [Key]
        public int RefaccionId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [MaxLength(300)]
        public string Descripcion { get; set; }
    }
}