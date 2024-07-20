using Microsoft.AspNetCore.Mvc;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Authorization;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos.Records;
using ProgrammersBlog.Mvc.Helpers.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.Abstract;
using ProgrammersBlog.Entities.Dtos.Classes;
using ProgrammersBlog.Entities.ComplexTypes;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace ProgrammersBlog.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin,editor")]
    public class ArticleController : BaseController
    {
        #region Constractors
        public ArticleController(IServiceManager serviceManager,IImageHelper imageHelper ):base(serviceManager, imageHelper)
        {
        }
        #endregion
        #region Methods
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await ServiceManager.ArticleService.GetAllAsync();
            return View(result.Data);
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            ViewBag.Categories = await GetCategoriesAsync();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([FromForm]ArticleAddDto addDto)
        {
            var user= await ServiceManager.AuthService.UserManager.GetUserAsync(HttpContext.User);
            if (ModelState.IsValid)
            {
                IDataResult<ImageUploadedDto> uploadedImageDataResult= await ImageHelper.UploadImage(addDto.SeoAuthor, addDto.ThumbnailFile, PictureTypes.Article);
                addDto.Thumbnail = uploadedImageDataResult.ResultStatus == ResultStatus.Success ? uploadedImageDataResult.Data.FullName : $"/defaultThumbnail.png";
                addDto.UserId = user.Id;
                var result= await ServiceManager.ArticleService.AddAsync(addDto,user.UserName);
                if(result.ResultStatus==ResultStatus.Success)
                {
                    TempData["success"] = result.Message;
                }
                else
                {
                    TempData["danger"] = result.Message; 
                }
            }
            ViewBag.Categories = await GetCategoriesAsync();
            return View(addDto);
        }
        [HttpPost]
        public async Task<JsonResult> Delete([FromRoute]int id)
        {
            var user =await ServiceManager.AuthService.UserManager.GetUserAsync(HttpContext.User);
            var result= await ServiceManager.ArticleService.DeleteAsync(id, user.UserName);
            var ajaxResult = JsonSerializer.Serialize(result);
            return Json(ajaxResult);
        }
        [HttpGet]
        public async Task<IActionResult> Update([FromRoute]int id)
        {
            
            var result= await ServiceManager.ArticleService.GetAsync(id);
            if (result.ResultStatus.Equals(ResultStatus.Success))
            {
                ViewBag.Categories = await GetCategoriesAsync();
            }
            return View(result.Data);
        }
        [HttpPost]
        public async Task<JsonResult> Update([FromForm]ArticleUpdateDto articleUpdate)
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
            if (articleUpdate.ThumbnailFile != null)
            {
                var deletedImageDtoResult = ImageHelper.Delete(articleUpdate.Thumbnail);
                if (deletedImageDtoResult.ResultStatus.Equals(ResultStatus.Success))
                {
                    var uploadedImageDataResult = await ImageHelper.UploadImage(Regex.Replace(articleUpdate.Title, @"[^\w\s]", ""), articleUpdate.ThumbnailFile, PictureTypes.Article);
                    articleUpdate.Thumbnail = uploadedImageDataResult.Data.FullName;
                }
                else
                {
                    return new JsonResult(new { success = false, message = deletedImageDtoResult.Message });
                }
            }
            var user = await ServiceManager.AuthService.UserManager.GetUserAsync(HttpContext.User);
            var result=await ServiceManager.ArticleService.UpdateAsync(articleUpdate,user.UserName);
            var ajaxModelResult = JsonSerializer.Serialize(result);
            return Json(ajaxModelResult);
        }

        private async Task<IList<Category>> GetCategoriesAsync()
        {
            var categories = await ServiceManager.CategoryService.GetAllByIsDeletedAsync(false);
            if (categories.Data.ResultStatus == 0)
            {
                return categories.Data.Categories;
            }
            return new List<Category>();
        }
        #endregion
    }
}
