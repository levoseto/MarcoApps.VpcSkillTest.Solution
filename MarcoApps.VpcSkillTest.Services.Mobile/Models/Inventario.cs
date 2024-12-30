namespace MarcoApps.VpcSkillTest.Services.Mobile.Models
{
    using SQLite;

    public class Inventario
    {
        [PrimaryKey, AutoIncrement]
        public int InventarioId { get; set; }

        [NotNull]
        public int TallerId { get; set; }

        [NotNull]
        public int RefaccionId { get; set; }

        [NotNull]
        public int Cantidad { get; set; }
    }
}