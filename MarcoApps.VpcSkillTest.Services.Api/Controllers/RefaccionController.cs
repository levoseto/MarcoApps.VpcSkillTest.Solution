namespace MarcoApps.VpcSkillTest.Services.Api.Controllers
{
    using MarcoApps.VpcSkillTest.Services.Api.Data;
    using MarcoApps.VpcSkillTest.Services.Api.Models;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    public class RefaccionController : BaseController<Refaccion>
    {
        public RefaccionController(ApplicationDbContext context) : base(context)
        {
        }
    }
}