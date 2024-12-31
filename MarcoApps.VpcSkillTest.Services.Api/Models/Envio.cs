namespace MarcoApps.VpcSkillTest.Services.Api.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text.Json.Serialization;

    public class Envio
    {
        [Key]
        public int EnvioId { get; set; }

        [Required]
        [ForeignKey("Solicitud")]
        public int SolicitudId { get; set; }

        [Required]
        public DateTime FechaEnvio { get; set; }

        [Required]
        [ForeignKey("MecanicoEnvia")]
        public int MecanicoEnviaId { get; set; }

        [Required]
        public int TallerProveedorId { get; set; }

        [Required]
        public int TallerSolicitanteId { get; set; }

        public int RefaccionId { get; set; }

        public int Cantidad { get; set; }

        // Relaciones de Navegación

        [JsonIgnore]
        public Solicitud Solicitud { get; set; }

        [JsonIgnore]
        public Mecanico MecanicoEnvia { get; set; }

        [JsonIgnore]
        public Taller TallerProveedor { get; set; }

        [JsonIgnore]
        public Taller TallerSolicitante { get; set; }

        [JsonIgnore]
        public Refaccion Refaccion { get; set; }
    }
}