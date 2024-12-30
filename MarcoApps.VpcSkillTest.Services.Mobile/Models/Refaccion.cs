namespace MarcoApps.VpcSkillTest.Services.Mobile.Models
{
    using SQLite;

    public class Refaccion
    {
        [PrimaryKey]
        public int RefaccionId { get; set; }

        [MaxLength(100), NotNull]
        public string Nombre { get; set; }

        [MaxLength(300)]
        public string Descripcion { get; set; }
    }
}