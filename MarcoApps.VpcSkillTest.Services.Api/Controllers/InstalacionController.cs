namespace MarcoApps.VpcSkillTest.Services.Api.Controllers
{
    using MarcoApps.VpcSkillTest.Services.Api.Data;
    using MarcoApps.VpcSkillTest.Services.Api.Models;
    using MarcoApps.VpcSkillTest.Services.Api.Models.DTO;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Route("api/[controller]")]
    public class InstalacionController : BaseController<Instalacion>
    {
        private readonly ApplicationDbContext _context;

        public InstalacionController(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        [HttpPost("registra")]
        public async Task<IActionResult> RegistrarInstalacion([FromBody] InstalacionDto instalacionDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Verificar existencia en inventario
                var inventario = await _context.Inventarios
                    .FirstOrDefaultAsync(i => i.TallerId == instalacionDto.TallerId && i.RefaccionId == instalacionDto.RefaccionId);

                if (inventario != null && inventario.Cantidad > 0)
                {
                    // Restar del inventario
                    inventario.Cantidad -= 1;
                    _context.Entry(inventario).State = EntityState.Modified;
                }
                else
                {
                    // Verificar si hay una solicitud relacionada con la pieza
                    var solicitud = await _context.Solicitudes
                        .Where(s => s.TallerSolicitanteId == instalacionDto.TallerId && s.RefaccionId == instalacionDto.RefaccionId && s.Estado == "Completado")
                        .FirstOrDefaultAsync();

                    if (solicitud == null)
                    {
                        return BadRequest("La pieza no está en inventario ni hay una solicitud completada relacionada.");
                    }

                    // Marcar la solicitud como "Instalada"
                    solicitud.Estado = "Instalada";
                    _context.Entry(solicitud).State = EntityState.Modified;
                }

                // Registrar la instalación
                var instalacion = new Instalacion
                {
                    VehiculoId = instalacionDto.VehiculoId,
                    RefaccionId = instalacionDto.RefaccionId,
                    TallerId = instalacionDto.TallerId,
                    MecanicoId = instalacionDto.MecanicoId,
                    FechaInstalacion = DateTime.Now,
                    LatitudInstalacion = instalacionDto.Latitud,
                    LongitudInstalacion = instalacionDto.Longitud
                };

                _context.Instalaciones.Add(instalacion);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok("Instalación registrada correctamente.");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}