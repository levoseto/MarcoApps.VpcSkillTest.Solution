using MarcoApps.VpcSkillTest.Services.Api.Data;
using MarcoApps.VpcSkillTest.Services.Api.Models.DTO.Login;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarcoApps.VpcSkillTest.Services.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginRequestDto loginRequest)
        {
            try
            {
                // Buscar al mecánico por email
                var mecanico = await _context.Mecanicos
                    .Include(m => m.Taller)
                    .FirstOrDefaultAsync(m => m.Email == loginRequest.Email);

                if (mecanico == null)
                {
                    return Unauthorized("Credenciales incorrectas.");
                }

                // Validar la contraseña
                if (mecanico.Password != loginRequest.Password) // Usa hashing en producción
                {
                    return Unauthorized("Credenciales incorrectas.");
                }

                // Crear la respuesta
                var response = new LoginResponseDto
                {
                    MecanicoId = mecanico.MecanicoId,
                    TallerId = mecanico.TallerId,
                    Nombre = mecanico.Nombre,
                    Email = mecanico.Email,
                    Telefono = mecanico.Telefono,
                    TallerNombre = mecanico.Taller.Nombre
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}