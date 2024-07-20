using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Mvc.Areas.Admin.Models;

namespace ProgrammersBlog.Mvc.Areas.Admin.Components
{
    public class TopBarNavViewComponent:ViewComponent
    {
        UserManager<User> _userManager;
        public TopBarNavViewComponent(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            UserViewModel userViewModel = new UserViewModel();
            userViewModel.User = await _userManager.GetUserAsync(HttpContext.User);
            return View(userViewModel);
        }
    }
}
