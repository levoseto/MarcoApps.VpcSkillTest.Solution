namespace MarcoApps.VpcSkillTest.Services.Mobile.Models
{
    using SQLite;

    public class Solicitud
    {
        [PrimaryKey, AutoIncrement]
        public int SolicitudId { get; set; }

        [NotNull]
        public int TallerSolicitanteId { get; set; }

        [NotNull]
        public int MecanicoSolicitanteId { get; set; }

        [NotNull]
        public int TallerProveedorId { get; set; }

        [NotNull]
        public int RefaccionId { get; set; }

        [NotNull]
        public int VehiculoId { get; set; }

        [NotNull]
        public DateTime FechaSolicitud { get; set; }

        [MaxLength(50), NotNull]
        public string Estado { get; set; }

        [NotNull]
        public double LatitudSolicitante { get; set; }

        [NotNull]
        public double LongitudSolicitante { get; set; }
    }
}