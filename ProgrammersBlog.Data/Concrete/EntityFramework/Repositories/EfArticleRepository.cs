using Microsoft.EntityFrameworkCore;
using ProgrammersBlog.Data.Abstract;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.RequestParameters;
using ProgrammersBlog.Shared.Data.Concrete.EntityFrameworkCore;
using ProgrammersBlog.Data.Concrete.EntityFramework.Exstensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Data.Concrete.EntityFramework.Repositories
{
    public class EfArticleRepository : EfEntityRepositoryBase<Article>, IArticleRepository
    {
        public EfArticleRepository(DbContext context) : base(context)
        {

        }

        public async Task<IList<Article>> GetAllSelectedMembersByRequestParameters(ArticleRequestParameters requestParameters,Expression<Func<Article, Article>> selector,bool isActive)
        {
            IQueryable<Article> query = _context.Set<Article>();

            var result = query
                .FilteredByIsActive(isActive)
                .Order(requestParameters.OrderBy)
                .Include(a => a.Category)
                .FilteredByCategoryId(requestParameters.CategoryId)
                .FilteredBySearchTerm(requestParameters.SearchTerm)
                .Select(selector)
                .ToPaginate(requestParameters.PageNumber, requestParameters.PageSize);

            return await  result.ToListAsync();
        }

        public Task<int> GetAllSelectedMembersCount(ArticleRequestParameters requestParameters)
        {
            IQueryable<Article> query = _context.Set<Article>();
            var result = query
                .Include(a => a.Category)
                .FilteredByCategoryId(requestParameters.CategoryId)
                .FilteredBySearchTerm(requestParameters.SearchTerm).CountAsync();
            return result;
        }
    }
}
