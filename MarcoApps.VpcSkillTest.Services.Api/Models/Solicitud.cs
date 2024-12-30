namespace MarcoApps.VpcSkillTest.Services.Api.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text.Json.Serialization;

    public class Solicitud
    {
        [Key]
        public int SolicitudId { get; set; }

        [Required]
        [ForeignKey("TallerSolicitante")]
        public int TallerSolicitanteId { get; set; }

        [Required]
        [ForeignKey("MecanicoSolicitante")]
        public int MecanicoSolicitanteId { get; set; }

        [Required]
        [ForeignKey("TallerProveedor")]
        public int TallerProveedorId { get; set; }

        [Required]
        [ForeignKey("Refaccion")]
        public int RefaccionId { get; set; }

        [Required]
        [ForeignKey("Vehiculo")]
        public int VehiculoId { get; set; }

        [Required]
        public DateTime FechaSolicitud { get; set; }

        [Required]
        [MaxLength(50)]
        public string Estado { get; set; }

        [Required]
        public double LatitudSolicitante { get; set; }

        [Required]
        public double LongitudSolicitante { get; set; }

        // Relaciones de Navegación
        [JsonIgnore]
        public Taller TallerSolicitante { get; set; }

        [JsonIgnore]
        public Taller TallerProveedor { get; set; }

        [JsonIgnore]
        public Mecanico MecanicoSolicitante { get; set; }

        [JsonIgnore]
        public Refaccion Refaccion { get; set; }

        [JsonIgnore]
        public Vehiculo Vehiculo { get; set; }
    }
}