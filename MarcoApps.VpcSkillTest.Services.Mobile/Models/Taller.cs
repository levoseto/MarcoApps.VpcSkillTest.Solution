namespace MarcoApps.VpcSkillTest.Services.Mobile.Models
{
    using SQLite;

    public class Taller
    {
        [PrimaryKey]
        public int TallerId { get; set; }

        [NotNull]
        public string Nombre { get; set; }

        [NotNull]
        public string Direccion { get; set; }

        [NotNull]
        public double Latitud { get; set; }

        [NotNull]
        public double Longitud { get; set; }
    }
}