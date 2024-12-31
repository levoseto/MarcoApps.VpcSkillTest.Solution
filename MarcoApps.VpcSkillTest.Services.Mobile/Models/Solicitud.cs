namespace MarcoApps.VpcSkillTest.Services.Mobile.Models
{
    using SQLite;

    public class Solicitud
    {
        [PrimaryKey, AutoIncrement]
        public int SolicitudLocalId { get; set; }

        public int SolicitudId { get; set; }

        public int TallerSolicitanteId { get; set; }
        public int TallerProveedorId { get; set; }
        public int RefaccionId { get; set; }
        public int MecanicoSolicitanteId { get; set; }
        public int VehiculoId { get; set; }
        public string Pieza { get; set; }
        public string MecanicoSolicitante { get; set; }
        public string MecanicoEnvio { get; set; }
        public int? MecanicoEnvioId { get; set; } // Nuevo campo
        public string VIN { get; set; }
        public DateTime FechaSolicitud { get; set; }

        [MaxLength(50), NotNull]
        public string Estado { get; set; }

        [NotNull]
        public double LatitudSolicitante { get; set; }

        [NotNull]
        public double LongitudSolicitante { get; set; }
    }
}