using System.ComponentModel.DataAnnotations;

namespace MarcoApps.VpcSkillTest.Services.Api.Models
{
    public class Taller
    {
        [Key]
        public int TallerId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(200)]
        public string Direccion { get; set; }

        [Required]
        public double Latitud { get; set; }

        [Required]
        public double Longitud { get; set; }
    }
}