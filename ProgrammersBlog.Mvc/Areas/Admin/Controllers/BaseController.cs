using Microsoft.AspNetCore.Mvc;
using ProgrammersBlog.Mvc.Helpers.Abstract;
using ProgrammersBlog.Services.Abstract;

namespace ProgrammersBlog.Mvc.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected IServiceManager ServiceManager { get; }
        protected IImageHelper ImageHelper { get; }
        public BaseController(IServiceManager serviceManager)
        {
            ServiceManager=serviceManager;
        }
        public BaseController(IServiceManager serviceManager, IImageHelper imageHelper)
        {
            ServiceManager = serviceManager;
            ImageHelper = imageHelper;
        }
    }
}
