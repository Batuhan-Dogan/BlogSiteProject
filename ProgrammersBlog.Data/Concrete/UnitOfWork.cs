using ProgrammersBlog.Data.Abstract;
using ProgrammersBlog.Data.Concrete.EntityFramework.Contexts;
using ProgrammersBlog.Data.Concrete.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Data.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Constractors
        public UnitOfWork(ProgrammersBlogContext context)
        {
            _context = context;
        }
        #endregion

        #region Fields
        private readonly ProgrammersBlogContext _context;
        private EfCategoryRepository _categoryRepository;
        private EfCommentRepository _commentRepository;
        private EfArticleRepository _articleRepository;
        #endregion

        #region Properties
        public ICategoryRepository CategoryRepository=> _categoryRepository ?? new EfCategoryRepository(_context);

        public IArticleRepository ArticleRepository => _articleRepository?? new EfArticleRepository(_context);

        public ICommentRepository CommentRepository => _commentRepository ?? new EfCommentRepository(_context);
        #endregion

        #region Methods
        public async Task<int> SaveAsync()
        {
           return await _context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }
        #endregion
    }
}
