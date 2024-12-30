using System.ComponentModel.DataAnnotations;

namespace MarcoApps.VpcSkillTest.Services.Api.Models
{
    public class Vehiculo
    {
        [Key]
        public int VehiculoId { get; set; }

        [Required]
        [MaxLength(17)] // Estándar VIN
        public string VIN { get; set; }

        [Required]
        [MaxLength(50)]
        public string Marca { get; set; }

        [Required]
        [MaxLength(50)]
        public string Modelo { get; set; }

        [Required]
        public int Anio { get; set; }
    }
}