namespace MarcoApps.VpcSkillTest.Services.Api.Controllers
{
    using MarcoApps.VpcSkillTest.Services.Api.Data;
    using MarcoApps.VpcSkillTest.Services.Api.Models;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    public class TallerController : BaseController<Taller>
    {
        public TallerController(ApplicationDbContext context) : base(context)
        {
        }
    }
}