using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Services.Abstract;

namespace ProgrammersBlog.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="admin")]
    public class RoleController : BaseController
    {
        public RoleController(IServiceManager serviceManager) : base(serviceManager)
        {
                
        }
        public async Task< IActionResult> Index()
        {
            IEnumerable<Role> roles = ServiceManager.AuthService.RoleManager.Roles.ToList();
            return View(roles);
        }
    }
}
