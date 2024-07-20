using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgrammersBlog.Entities.ComplexTypes;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos.Classes;
using ProgrammersBlog.Entities.Dtos.Records;
using ProgrammersBlog.Mvc.Helpers.Abstract;
using ProgrammersBlog.Mvc.Helpers.Concrete;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Shared.Utilities.Extensions;
using ProgrammersBlog.Shared.Utilities.Results.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes.Concrete;
using System.Data;
using System.Text.Json;

namespace ProgrammersBlog.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class UserController : BaseController
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        #region Constractors
        public UserController( IImageHelper imageHelper, IServiceManager serviceManager):base(serviceManager, imageHelper)
        {
        }
        #endregion
        #region Methods
        public async Task<IActionResult> Index()
        {
            var users = await ServiceManager.AuthService.GetAllUsersInDataResultObjectAsync();
            return View(users.Data);
        }
        public async Task<IActionResult> AddUser()
        {
            HashSet<string> roles=new HashSet<string>  (await ServiceManager.AuthService.RoleManager.Roles.Select(r=>r.Name).ToListAsync());
            ViewBag.Roles = roles;
            return View(new UserAddDto() );
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUser([FromForm] UserAddDto userAddDto)
        {
            HashSet<string> roles = new HashSet<string>(await ServiceManager.AuthService.RoleManager.Roles.Select(r => r.Name).ToListAsync());
            ViewBag.Roles = roles;
            if (ModelState.IsValid)
            {
                IDataResult<ImageUploadedDto> uploadedImageDataResult = await ImageHelper.UploadImage(userAddDto.UserName, userAddDto.PictureFile,PictureTypes.User);
                userAddDto.Picture = uploadedImageDataResult.ResultStatus == ResultStatus.Success ? uploadedImageDataResult.Data.FullName : "userImages/defaultUser.png";
                
                var result = await ServiceManager.AuthService.AddUserAsync(userAddDto);
                if (result.ResultStatus.Equals(ResultStatus.Success))
                { 
                    TempData["success"] = result.Message;
                    return RedirectToAction("Index");
                }
                else
                {
                    ImageHelper.Delete(userAddDto.Picture);
                    TempData["danger"] = result.Message;
                }
            }
            return View(userAddDto);
        }
        [HttpGet]
        public async Task<IActionResult> UpdateUser([FromRoute] int id)
        {
            DataResult<UserDtoForGetOne> result =(DataResult < UserDtoForGetOne >)await ServiceManager.AuthService.GetUserInDataResultObjectAsync(id);
            return View(result.Data);
        }
        [HttpPost]
        public async Task<JsonResult> UpdateUser([FromForm]UserUpdateDto userUpdateDto)
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
            if (userUpdateDto.PictureFile is not null)
            {
                var deletedImageDtoResult = ImageHelper.Delete(userUpdateDto.Picture);
                if (deletedImageDtoResult.ResultStatus.Equals(ResultStatus.Success))
                {
                    var uploadedImageDataResult = await ImageHelper.UploadImage(userUpdateDto.UserName, userUpdateDto.PictureFile,PictureTypes.User);
                    userUpdateDto.Picture = uploadedImageDataResult.Data.FullName;
                }
                else
                {
                    return new JsonResult(new { success = false, message = deletedImageDtoResult.Message });
                }
            }
            var result = await ServiceManager.AuthService.UpdateUserAsync(userUpdateDto);
            var ajaxModelResult = JsonSerializer.Serialize(result);
            return Json(ajaxModelResult);
        }
        [HttpPost]
        public async Task<JsonResult> Delete(int userId)
        {
            User logInUser = await ServiceManager.AuthService.UserManager.GetUserAsync(HttpContext.User);
            var result = await ServiceManager.AuthService.HardDeleteUserAsync(userId,logInUser.Id);
            if (result.ResultStatus.Equals(ResultStatus.Success))
            {
                ImageHelper.Delete(result.Data);
            }
            var jsonResult= JsonSerializer.Serialize(result);
            return Json(jsonResult);
        }

        #endregion
    }
}
