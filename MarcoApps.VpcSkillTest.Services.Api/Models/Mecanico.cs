using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MarcoApps.VpcSkillTest.Services.Api.Models
{
    public class Mecanico
    {
        [Key]
        public int MecanicoId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Required]
        [ForeignKey("Taller")]
        public int TallerId { get; set; }

        // Relación de Navegación
        [JsonIgnore]
        public Taller Taller { get; set; }
    }
}