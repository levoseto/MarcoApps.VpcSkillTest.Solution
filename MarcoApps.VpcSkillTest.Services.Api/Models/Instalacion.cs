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
        public int VehiculoId { get; set; }

        [Required]
        [ForeignKey("Solicitud")]
        public int SolicitudId { get; set; }

        [Required]
        [ForeignKey("Solicitud")]
        public int RefaccionId { get; set; }

        [Required]
        [ForeignKey("Solicitud")]
        public int TallerId { get; set; }

        [Required]
        [ForeignKey("Mecanico")]
        public int MecanicoId { get; set; }

        [Required]
        public DateTime FechaInstalacion { get; set; }

        [Required]
        public double LatitudInstalacion { get; set; }

        [Required]
        public double LongitudInstalacion { get; set; }

        // Propiedades de navegación (opcional)
        [JsonIgnore]
        public Vehiculo Vehiculo { get; set; }

        [JsonIgnore]
        public Refaccion Refaccion { get; set; }

        [JsonIgnore]
        public Taller Taller { get; set; }

        [JsonIgnore]
        public Mecanico Mecanico { get; set; }
    }
}