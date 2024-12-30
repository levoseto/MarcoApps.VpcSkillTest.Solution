namespace MarcoApps.VpcSkillTest.Services.Api.Controllers
{
    using MarcoApps.VpcSkillTest.Services.Api.Data;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController<T> : ControllerBase where T : class
    {
        private readonly ApplicationDbContext _context;

        protected BaseController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var entities = await _context.Set<T>().ToListAsync();
            return Ok(entities);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity == null) return NotFound();
            return Ok(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] T entity)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = entity.GetType().GetProperty("Id")?.GetValue(entity) }, entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] T entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Obtener la propiedad ID del objeto recibido
            var entityIdProperty = typeof(T).GetProperty("Id");
            if (entityIdProperty == null)
                return BadRequest("La entidad no tiene una propiedad 'Id' definida.");

            var entityIdValue = entityIdProperty.GetValue(entity);
            if (!id.Equals(entityIdValue))
                return BadRequest("El ID proporcionado no coincide con el ID del cuerpo de la solicitud.");

            // Marcar la entidad como modificada
            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Verificar si la entidad existe
                var exists = await _context.Set<T>().FindAsync(id);
                if (exists == null)
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity == null) return NotFound();

            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}