using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Data.Abstract
{
    public interface IUnitOfWork:IAsyncDisposable
    {
        public ICategoryRepository CategoryRepository { get;}
        public IArticleRepository ArticleRepository { get;}
        public ICommentRepository CommentRepository { get;}
        public Task<int> SaveAsync();
    }
}
