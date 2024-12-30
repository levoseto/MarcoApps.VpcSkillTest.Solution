namespace MarcoApps.VpcSkillTest.Services.Mobile.Models
{
    using SQLite;

    public class Instalacion
    {
        [PrimaryKey, AutoIncrement]
        public int InstalacionId { get; set; }

        [NotNull]
        public int SolicitudId { get; set; }

        [NotNull]
        public int MecanicoInstalaId { get; set; }

        [NotNull]
        public DateTime FechaInstalacion { get; set; }

        [NotNull]
        public double LatitudInstalacion { get; set; }

        [NotNull]
        public double LongitudInstalacion { get; set; }
    }
}