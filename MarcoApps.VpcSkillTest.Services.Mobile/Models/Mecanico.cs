namespace MarcoApps.VpcSkillTest.Services.Mobile.Models
{
    using SQLite;

    public class Mecanico
    {
        [PrimaryKey, AutoIncrement]
        public int MecanicoId { get; set; }

        [MaxLength(100), NotNull]
        public string Nombre { get; set; }

        [NotNull]
        public int TallerId { get; set; }
    }
}