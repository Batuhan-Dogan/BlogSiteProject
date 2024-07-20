using Microsoft.AspNetCore.Mvc;
using ProgrammersBlog.Services.Abstract;

namespace ProgrammersBlog.Mvc.Components
{
    public class UsersCommentCountViewComponent:ViewComponent
    {
        private readonly IServiceManager _serviceManager;
        public UsersCommentCountViewComponent(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        public async Task<string> InvokeAsync(string userName)
        {
            int result=await _serviceManager.CommentService.GetUserCommentCountAsync(userName);
            return result.ToString();

        }
    }
}
