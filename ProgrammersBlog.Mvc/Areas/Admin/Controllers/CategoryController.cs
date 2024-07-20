using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;
using ProgrammersBlog.Entities.Dtos.Records;
using Microsoft.AspNetCore.Authorization;
using ProgrammersBlog.Mvc.Helpers.Concrete;
using Microsoft.AspNetCore.Identity;
using ProgrammersBlog.Entities.Concrete;

namespace ProgrammersBlog.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : BaseController
    {
        #region Constractor
        public CategoryController(IServiceManager serviceManager):base(serviceManager)
        {
        }
        #endregion

        #region Methods
        [Authorize(Roles = "admin,editor")]
        public async Task<IActionResult> Index()
        {
            var result =await ServiceManager.CategoryService.GetAllAsync();
            
            return View(result.Data);
        }
        [Authorize(Roles = "admin,editor")]
        public IActionResult AddCategory()
        {
            return View();
        }
        [Authorize(Roles = "admin,editor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCategory([FromForm]CategoryAddDto categoryAddDto)
        {
            if (ModelState.IsValid)
            {
                var user = await ServiceManager.AuthService.UserManager.GetUserAsync(HttpContext.User);
                var result = await ServiceManager.CategoryService.AddAsync(categoryAddDto, user.UserName);
                if (result.ResultStatus.Equals(ResultStatus.Success))
                {
                    TempData["success"] = result.Message;
                    return Redirect("Index");
                }
                else if (result.ResultStatus.Equals(ResultStatus.Error))
                {
                    TempData["danger"] = result.Message;

                }
            }
            return View(categoryAddDto);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<JsonResult> Delete(int categoryId)
        {
            var result = await ServiceManager.CategoryService.DeleteAsync(categoryId, "Batuhan Doğan");
            var ajaxResult=JsonSerializer.Serialize(result);
            return Json(ajaxResult);
        }
        [Authorize(Roles = "admin,editor")]
        [HttpGet]
        public async Task<IActionResult> UpdateCategory([FromRoute] int id)
        {
            var category = await ServiceManager.CategoryService.GetAsync(id);
            if (category.ResultStatus.Equals(ResultStatus.Success))
            {
                return View(category.Data);
            }
            return View(category.Data);
        }
        [Authorize(Roles = "admin,editor")]
        [HttpPost]
        public async Task<JsonResult> UpdateCategory([FromForm] CategoryUpdateDto categoryUpdateDto)
        {
            if (ModelState.IsValid)
            {
                var user =await ServiceManager.AuthService.UserManager.GetUserAsync(HttpContext.User);
                var result = await ServiceManager.CategoryService.UpdateAsync(categoryUpdateDto, user.UserName);
                var ajaxModelResult = JsonSerializer.Serialize(result.Data);
                return Json(ajaxModelResult);
            }
            Dictionary<string, string[]> errors = ModelState.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
            );
            var ajaxErrorsResult = JsonSerializer.Serialize(errors);
            return Json(ajaxErrorsResult);
        }
        #endregion
    }
}
