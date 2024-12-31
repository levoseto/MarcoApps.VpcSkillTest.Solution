namespace MarcoApps.VpcSkillTest.Services.Mobile.Models
{
    using SQLite;

    public class Envio
    {
        [PrimaryKey, AutoIncrement]
        public int EnvioId { get; set; }

        [NotNull]
        public int SolicitudId { get; set; }

        [NotNull]
        public int MecanicoEnviaId { get; set; }

        [NotNull]
        public DateTime FechaEnvio { get; set; }

        public int TallerProveedorId { get; set; }

        public int TallerSolicitanteId { get; set; }

        public int RefaccionId { get; set; }

        public int Cantidad { get; set; }
    }
}