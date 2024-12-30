namespace MarcoApps.VpcSkillTest.Services.Api.Models.DTO.Solicitudes
{
    public class SolicitudDto
    {
        public int TallerSolicitanteId { get; set; }
        public int MecanicoSolicitanteId { get; set; }
        public int TallerProveedorId { get; set; }
        public int RefaccionId { get; set; }
        public int VehiculoId { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string Estado { get; set; }
        public double LatitudSolicitante { get; set; }
        public double LongitudSolicitante { get; set; }
    }
}