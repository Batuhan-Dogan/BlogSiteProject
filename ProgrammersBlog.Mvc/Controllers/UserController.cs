using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Shared.Entities.Concrete;
using System.Text.Json;
using ProgrammersBlog.Shared.Utilities.Results.Abstract;

namespace ProgrammersBlog.Mvc.Controllers
{
    public class UserController:Controller
    {
        private readonly IServiceManager _serviceManager;
        public UserController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index([FromRoute]int id)
        {
            var user = await _serviceManager.AuthService.UserManager.GetUserAsync(HttpContext.User);
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<JsonResult> EditSocialBlades([FromForm] SocialBlades socialBlades)
        {
            if (!ModelState.IsValid)
            {
                Dictionary<string, string[]> errors = ModelState.ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                );
                var ajaxErrorsResult = JsonSerializer.Serialize(errors);
                return Json(ajaxErrorsResult);
            }
            else
            {
                Shared.Utilities.Results.Abstract.IResult result =await _serviceManager.AuthService.ChangeSocialBladesAsync(HttpContext.User, socialBlades);

                return Json(JsonSerializer.Serialize(result));
            }
        }
    }
}
