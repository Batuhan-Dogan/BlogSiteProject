using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProgrammersBlog.Data.Abstract;
using ProgrammersBlog.Data.Concrete;
using ProgrammersBlog.Mvc.Areas.Admin.Models;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Services.Concrete;

namespace ProgrammersBlog.Mvc.Areas.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        IDataCountService _categoryCountManager;
        IDataCountService _articleCountManager;
        IDataCountService _commentCountManager;


        public DashboardController(ICategoryService categoryManager,IArticleService articleManager, ICommentService commentCountManager)
        {
            _categoryCountManager = (IDataCountService)categoryManager;
            _articleCountManager = (IDataCountService)articleManager;
            _commentCountManager = (IDataCountService)commentCountManager;
        }
        [Authorize(Roles = "admin,editor")]
        public async Task<IActionResult> Index()
        {
            int activeCategoryCount = await (_categoryCountManager.GetActiveDataCountAsync());
            int deletedCategoryCount=await _categoryCountManager.GetDeletedDataCountAsync();
            int categoryCount=await _categoryCountManager.CountAsync();

            int deletedArticleCount = await _articleCountManager.GetDeletedDataCountAsync();
            int activeArticleCount = await _articleCountManager.GetActiveDataCountAsync();
            int articleCount=await _articleCountManager.CountAsync();

            int deletedCommentCount = await _commentCountManager.GetDeletedDataCountAsync();
            int activeCommentCount = await _commentCountManager.GetActiveDataCountAsync();
            int commentCount = await _commentCountManager.CountAsync();

            var dashboardViewModel = new DashboardViewModel()
            {
                ActiveCategoryCount = activeCategoryCount,
                CategoryCount = categoryCount,
                DeletedCategoryCount = deletedCategoryCount,

                ActiveArticleCount= activeArticleCount,
                DeletedArticleCount = deletedArticleCount,
                ArticleCount=articleCount,

                ActiveCommentCount=activeCommentCount,
                DeletedCommentCount=deletedCommentCount,
                CommentCount=commentCount
            };
            return View(dashboardViewModel);
        }
    }
}
