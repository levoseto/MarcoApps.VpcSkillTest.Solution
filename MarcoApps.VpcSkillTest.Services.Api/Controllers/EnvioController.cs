namespace MarcoApps.VpcSkillTest.Services.Api.Controllers
{
    using MarcoApps.VpcSkillTest.Services.Api.Data;
    using MarcoApps.VpcSkillTest.Services.Api.Models;
    using MarcoApps.VpcSkillTest.Services.Api.Models.DTO;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Route("api/[controller]")]
    public class EnvioController : BaseController<Envio>
    {
        private readonly ApplicationDbContext _context;

        public EnvioController(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        [HttpPost("registra")]
        public async Task<IActionResult> RegistrarEnvio([FromBody] EnvioDto envioDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Registrar el envío
                var envio = new Envio
                {
                    SolicitudId = envioDto.SolicitudId,
                    TallerProveedorId = envioDto.TallerProveedorId,
                    TallerSolicitanteId = envioDto.TallerSolicitanteId,
                    RefaccionId = envioDto.RefaccionId,
                    Cantidad = envioDto.Cantidad,
                    MecanicoEnviaId = envioDto.MecanicoEnviaId,
                    FechaEnvio = DateTime.Now
                };

                _context.Envios.Add(envio);
                await _context.SaveChangesAsync();

                // Actualizar el inventario del taller proveedor y solicitante
                var inventarioProveedor = await _context.Inventarios
                    .FirstOrDefaultAsync(i => i.TallerId == envio.TallerProveedorId && i.RefaccionId == envio.RefaccionId);

                if (inventarioProveedor == null || inventarioProveedor.Cantidad < envio.Cantidad)
                {
                    return BadRequest("Cantidad insuficiente en el taller proveedor.");
                }

                inventarioProveedor.Cantidad -= envio.Cantidad;

                var inventarioSolicitante = await _context.Inventarios
                    .FirstOrDefaultAsync(i => i.TallerId == envio.TallerSolicitanteId && i.RefaccionId == envio.RefaccionId);

                if (inventarioSolicitante == null)
                {
                    inventarioSolicitante = new Inventario
                    {
                        TallerId = envio.TallerSolicitanteId,
                        RefaccionId = envio.RefaccionId,
                        Cantidad = envio.Cantidad
                    };
                    _context.Inventarios.Add(inventarioSolicitante);
                }
                else
                {
                    inventarioSolicitante.Cantidad += envio.Cantidad;
                }

                // Actualizar el estado de la solicitud
                var solicitud = await _context.Solicitudes.FindAsync(envioDto.SolicitudId);
                if (solicitud == null)
                {
                    return NotFound("La solicitud no existe.");
                }

                solicitud.Estado = "Completado";
                _context.Entry(solicitud).State = EntityState.Modified;

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok("Envío registrado y actualizado en inventarios.");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }
    }
}