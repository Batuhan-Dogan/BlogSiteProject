using Microsoft.AspNetCore.Mvc;

using ProgrammersBlog.Services.Abstract;

namespace ProgrammersBlog.Mvc.Areas
{
    public class TopNavBarViewComponent:ViewComponent
    {
        IServiceManager _serviceManager;
        public TopNavBarViewComponent(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories=await _serviceManager.CategoryService.GetAllByIsDeletedAsync(false);
            return View(categories.Data);
        }
    }
}
