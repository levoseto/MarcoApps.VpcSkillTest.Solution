using MarcoApps.VpcSkillTest.Services.Api.Data;
using MarcoApps.VpcSkillTest.Services.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace MarcoApps.VpcSkillTest.Services.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventarioController : BaseController<Inventario>
    {
        public InventarioController(ApplicationDbContext context) : base(context)
        {
        }
    }
}