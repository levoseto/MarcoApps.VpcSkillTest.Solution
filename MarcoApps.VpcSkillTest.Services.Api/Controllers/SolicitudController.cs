using MarcoApps.VpcSkillTest.Services.Api.Data;
using MarcoApps.VpcSkillTest.Services.Api.Models;
using MarcoApps.VpcSkillTest.Services.Api.Models.DTO.Solicitudes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarcoApps.VpcSkillTest.Services.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SolicitudController : BaseController<Solicitud>
    {
        private readonly ApplicationDbContext _context;

        public SolicitudController(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        // Registrar una nueva solicitud usando un DTO
        [HttpPost("registrar")]
        public async Task<IActionResult> RegisterAsync([FromBody] SolicitudDto solicitudDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Mapear el DTO al modelo de EF
                var solicitud = new Solicitud
                {
                    TallerSolicitanteId = solicitudDto.TallerSolicitanteId,
                    MecanicoSolicitanteId = solicitudDto.MecanicoSolicitanteId,
                    TallerProveedorId = solicitudDto.TallerProveedorId,
                    RefaccionId = solicitudDto.RefaccionId,
                    VehiculoId = solicitudDto.VehiculoId,
                    FechaSolicitud = solicitudDto.FechaSolicitud,
                    Estado = solicitudDto.Estado,
                    LatitudSolicitante = solicitudDto.LatitudSolicitante,
                    LongitudSolicitante = solicitudDto.LongitudSolicitante
                };

                _context.Solicitudes.Add(solicitud);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetById), new { id = solicitud.SolicitudId }, solicitud);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // Obtener solicitudes por taller solicitante
        [HttpGet("taller/{tallerId}")]
        public async Task<ActionResult<IEnumerable<SolicitudConsultaDto>>> GetSolicitudesPorTaller(int tallerId)
        {
            try
            {
                var solicitudes = await _context.Solicitudes
            .Where(s => s.TallerSolicitanteId == tallerId)
            .Include(s => s.Refaccion)
            .Include(s => s.MecanicoSolicitante)
            .Include(s => s.Vehiculo)
            .Select(s => new SolicitudConsultaDto
            {
                SolicitudId = s.SolicitudId,
                TallerSolicitanteId = s.TallerSolicitanteId,
                TallerProveedorId = s.TallerProveedorId,
                RefaccionId = s.RefaccionId,
                MecanicoSolicitanteId = s.MecanicoSolicitanteId,
                VehiculoId = s.VehiculoId,
                Pieza = s.Refaccion.Nombre,
                MecanicoSolicitante = s.MecanicoSolicitante.Nombre,
                VIN = s.Vehiculo.VIN,
                Estado = s.Estado,
                FechaSolicitud = s.FechaSolicitud
            })
            .ToListAsync();

                return Ok(solicitudes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPut("editar/{id}")]
        public async Task<IActionResult> UpdateSolicitud(int id, [FromBody] SolicitudDto solicitudDto)
        {
            if (id != solicitudDto.SolicitudId)
            {
                return BadRequest("El ID de la solicitud no coincide.");
            }

            var solicitud = await _context.Solicitudes.FindAsync(id);
            if (solicitud == null)
            {
                return NotFound("La solicitud no existe.");
            }

            // Actualizar los campos necesarios
            solicitud.TallerProveedorId = solicitudDto.TallerProveedorId;
            solicitud.RefaccionId = solicitudDto.RefaccionId;
            solicitud.MecanicoSolicitanteId = solicitudDto.MecanicoSolicitanteId;
            solicitud.VehiculoId = solicitudDto.VehiculoId;
            solicitud.FechaSolicitud = solicitudDto.FechaSolicitud;
            solicitud.Estado = solicitudDto.Estado;

            try
            {
                _context.Entry(solicitud).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "Error al actualizar la solicitud.");
            }
        }

    }
}