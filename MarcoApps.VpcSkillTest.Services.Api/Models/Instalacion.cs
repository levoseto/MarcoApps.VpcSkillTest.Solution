namespace MarcoApps.VpcSkillTest.Services.Api.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text.Json.Serialization;

    public class Instalacion
    {
        [Key]
        public int InstalacionId { get; set; }

        [Required]
        [ForeignKey("Solicitud")]
        public int SolicitudId { get; set; }

        [Required]
        [ForeignKey("MecanicoInstala")]
        public int MecanicoInstalaId { get; set; }

        [Required]
        public DateTime FechaInstalacion { get; set; }

        [Required]
        public double LatitudInstalacion { get; set; }

        [Required]
        public double LongitudInstalacion { get; set; }

        // Relaciones de Navegación
        [JsonIgnore]
        public Solicitud Solicitud { get; set; }

        [JsonIgnore]
        public Mecanico MecanicoInstala { get; set; }
    }
}