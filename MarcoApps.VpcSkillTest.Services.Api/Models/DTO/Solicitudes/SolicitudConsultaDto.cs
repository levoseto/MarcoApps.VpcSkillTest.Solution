namespace MarcoApps.VpcSkillTest.Services.Api.Models.DTO.Solicitudes
{
    public class SolicitudConsultaDto
    {
        public int SolicitudId { get; set; }
        public int TallerSolicitanteId { get; set; } // ID del taller que solicita
        public int TallerProveedorId { get; set; } // ID del taller que provee
        public int RefaccionId { get; set; } // ID de la refacción solicitada
        public int MecanicoSolicitanteId { get; set; } // ID del mecánico que solicita
        public int VehiculoId { get; set; } // ID del vehículo
        public int? MecanicoEnvioId { get; set; } // ID del mecánico que envía

        public string? Pieza { get; set; } // Nombre de la pieza solicitada
        public string? MecanicoSolicitante { get; set; } // Nombre del mecánico solicitante
        public string? MecanicoEnvio { get; set; }
        public string? VIN { get; set; } // VIN del vehículo
        public string? Estado { get; set; } // Estado de la solicitud

        public DateTime FechaSolicitud { get; set; } // Fecha en la que se creó la solicitud
    }
}