using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Mvc.Areas.Admin.Models;

namespace ProgrammersBlog.Mvc.Areas.Admin.Components
{
    public class SideBarNavViewComponent:ViewComponent
    {
        private readonly UserManager<User> _userManager;
        public SideBarNavViewComponent(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            User user=await _userManager.GetUserAsync(HttpContext.User);
            IList<string> roles =(IList<string>)await _userManager.GetRolesAsync(user);
            return View(new UserWithRolesViewModel { Roles=roles,User=user});
        }
    }
}
