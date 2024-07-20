using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProgrammersBlog.Data.Concrete;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos.Records;
using ProgrammersBlog.Mvc.Areas.Admin.Models;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace ProgrammersBlog.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CommentController : BaseController
    {
        #region Constractors
        public CommentController(IServiceManager serviceManager):base(serviceManager)
        {
        }
        #endregion

        #region Methods
        [HttpGet]
        [Authorize(Roles = "admin,editor")]
        public async Task<IActionResult> Index()
        {
            var result= await ServiceManager.CommentService.GetAllAsync();
            return View(result.Data);
        }
        [HttpPost]
        [Authorize(Roles = "admin,editor")]
        public async Task<JsonResult>DeleteComment(int id)
        {
            var result= await ServiceManager.CommentService.DeleteAsync(id,"batuhan Doğan");
            var jsonResult= JsonSerializer.Serialize(result);
            return Json(jsonResult);
        }
        [Authorize(Roles = "admin,editor")]

        public async Task<IActionResult> Add()
        {
            ViewBag.Articles =await GetArticlesAsync();
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "admin,editor")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Add([FromForm] CommentAddDto comment)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Articles=await GetArticlesAsync();
                return View(comment);
            }
            var user = await ServiceManager.AuthService.UserManager.GetUserAsync(HttpContext.User);
            comment.CreatedByName = user.UserName;
            var result = await ServiceManager.CommentService.AddAsync(comment);
            if (result.ResultStatus.Equals(ResultStatus.Success))
            {
                TempData["success"] = result.Message;
            }
            else
            {
                TempData["danger"] = result.Message;
            }
            ViewBag.Articles =await GetArticlesAsync();
            return View(comment);
        }
        [HttpGet]
        [Authorize(Roles = "admin,editor")]
        public async Task<IActionResult>Update([FromRoute]int id)
        {
            var result=await ServiceManager.CommentService.GetAsync(id);
            if (result.ResultStatus == 0)
            {
                ViewBag.Articles = await GetArticlesAsync();
            }
            return View(result.Data);
        }
        [HttpPost]
        [Authorize(Roles = "admin,editor")]
        public async Task<JsonResult> Update([FromForm] CommentUpdateDto comment)
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
            var user= await ServiceManager.AuthService.UserManager.GetUserAsync(HttpContext.User);
            var result= await ServiceManager.CommentService.UpdateAsync(comment,user.UserName);
            var jsonResult=JsonSerializer.Serialize(result);
            return Json(jsonResult);
        }
        [HttpPost]
        [Authorize(Roles = "admin,editor")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var user = await ServiceManager.AuthService.UserManager.GetUserAsync(HttpContext.User);
            var result= await ServiceManager.CommentService.DeleteAsync(id, user.UserName);
            var jsonResult=JsonSerializer.Serialize(result);
            return Json(jsonResult);
        }
        private async Task<IList<Article>> GetArticlesAsync()
        {
            var result = await ServiceManager.ArticleService.GetAllByIsDeletedAsync(true);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return  (IList<Article>)result.Data.Articles;
            }
            else
            {
                return  new List<Article>();
            }
        }
        #endregion
    }
}
