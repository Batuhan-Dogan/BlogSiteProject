using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProgrammersBlog.Entities.Dtos.Records;
using ProgrammersBlog.Entities.RequestParameters;
using ProgrammersBlog.Mvc.Models;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Shared.Utilities.Extensions;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes.Concrete;
using System.Text.Json;

namespace ProgrammersBlog.Mvc.Controllers
{
    public class CommentController : Controller
    {
        private readonly IServiceManager _serviceManager;
        public CommentController(IServiceManager serviceManager)
        {
             _serviceManager=serviceManager;
        }
        [HttpPost]
        [Authorize]
        public async Task<JsonResult> Add([FromForm]CommentAddDto commentAddDto)
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
            commentAddDto.CreatedByName  = await _serviceManager.AuthService.GetUserNameAsync(HttpContext.User);
            var result= await _serviceManager.CommentService.AddAsync(commentAddDto);
            return Json(JsonSerializer.Serialize(result));
        }
        [HttpGet]
        public async Task<JsonResult> GetCommentsByResquestParameters(CommentRequestParameters commentRequestParameters)
        {
            var commentsDataResult=await _serviceManager.CommentService.GetCommentByRequestParametersAsync(commentRequestParameters,true);
            return Json(JsonSerializer.Serialize(commentsDataResult.Data));
        }
        [HttpGet]
        [Authorize]
        public async Task<JsonResult> GetUsersComments(CommentRequestParameters commentRequestParameters)
        {
            string userName = await _serviceManager.AuthService.GetUserNameAsync(HttpContext.User);
            var commentsDataResult = await _serviceManager.CommentService.GetCommentByUserNameAsync(userName, commentRequestParameters,true);
            return Json(JsonSerializer.Serialize(commentsDataResult.Data));

        }
    }
}
