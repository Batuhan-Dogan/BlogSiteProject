using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using ProgrammersBlog.Entities.RequestParameters;
using ProgrammersBlog.Mvc.Models;
using ProgrammersBlog.Services.Abstract;
using System.Data.SqlTypes;

namespace ProgrammersBlog.Mvc.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IServiceManager _serviceManager;
        public ArticleController(IServiceManager serviceManager)
        {
            _serviceManager=serviceManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index(ArticleRequestParameters requestParameters)
        {
            var articleDataResult=await _serviceManager.ArticleService.GetArticleSummariesByRequestParameters(requestParameters,true);
            Pagination pagination = new Pagination()
            {
                CurrentPage = requestParameters.PageNumber,
                ItemsPerPage = requestParameters.PageSize,
                TotalItems = await _serviceManager.ArticleService.GetAllSelectedMembersCountsync(requestParameters),
                SearchTerm = requestParameters.SearchTerm,
                CategoryId = requestParameters.CategoryId,
                OrderBy=requestParameters.OrderBy
            };
            return View(new ArticleListViewModel() { Articles=articleDataResult.Data,Pagination=pagination});
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var article=await _serviceManager.ArticleService.GetArticleWithUserAndCategoryAsync(id,true);
            return View(article.Data);
        }

    }
}
