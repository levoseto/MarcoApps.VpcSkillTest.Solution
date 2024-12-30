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
        [ForeignKey("MecanicoEnvia")]
        public int MecanicoEnviaId { get; set; }

        [Required]
        public DateTime FechaEnvio { get; set; }

        // Relaciones de Navegación

        [JsonIgnore]
        public Solicitud Solicitud { get; set; }

        [JsonIgnore]
        public Mecanico MecanicoEnvia { get; set; }
    }
}