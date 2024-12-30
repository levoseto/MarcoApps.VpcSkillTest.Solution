namespace MarcoApps.VpcSkillTest.Services.Mobile.Models
{
    using SQLite;

    public class Vehiculo
    {
        [PrimaryKey, AutoIncrement]
        public int VehiculoId { get; set; }

        [MaxLength(17), NotNull] // VIN estándar
        public string VIN { get; set; }

        [MaxLength(50), NotNull]
        public string Marca { get; set; }

        [MaxLength(50), NotNull]
        public string Modelo { get; set; }

        [NotNull]
        public int Anio { get; set; }
    }
}