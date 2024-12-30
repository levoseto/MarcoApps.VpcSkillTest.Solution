using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MarcoApps.VpcSkillTest.Services.Api.Models
{
    public class Inventario
    {
        [Key]
        public int InventarioId { get; set; }

        [Required]
        [ForeignKey("Taller")]
        public int TallerId { get; set; }

        [Required]
        [ForeignKey("Refaccion")]
        public int RefaccionId { get; set; }

        [Required]
        public int Cantidad { get; set; }

        // Relaciones de Navegación
        [JsonIgnore]
        public Taller Taller { get; set; }

        [JsonIgnore]
        public Refaccion Refaccion { get; set; }
    }
}