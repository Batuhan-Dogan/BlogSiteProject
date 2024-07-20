using ProgrammersBlog.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Services.Concrete
{
    public class ServiceManager : IServiceManager
    {
        #region fields
        private readonly IArticleService _articleService;
        private readonly IAuthService _authService;
        private readonly ICategoryService _categoryService;
        private readonly ICommentService _commentService;
        #endregion

        public ServiceManager(IArticleService articleService,IAuthService authService,ICategoryService categoryService,ICommentService commentService) 
        {
            _articleService = articleService;
            _authService = authService;
            _categoryService = categoryService;
            _commentService = commentService;
        }


        #region Properties
        public IArticleService ArticleService => _articleService;

        public IAuthService AuthService => _authService;

        public ICategoryService CategoryService => _categoryService;

        public ICommentService CommentService => _commentService;
        #endregion
    }
}
