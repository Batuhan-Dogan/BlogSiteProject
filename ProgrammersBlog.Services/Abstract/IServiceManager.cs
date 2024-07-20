using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Services.Abstract
{
    public interface IServiceManager
    {
        public IArticleService ArticleService { get; }
        public IAuthService AuthService { get; }
        public ICategoryService CategoryService { get; }
        public ICommentService CommentService { get; }
    }
}
