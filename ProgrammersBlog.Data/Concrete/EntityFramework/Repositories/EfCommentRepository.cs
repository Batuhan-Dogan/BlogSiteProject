using Microsoft.EntityFrameworkCore;
using ProgrammersBlog.Data.Abstract;
using ProgrammersBlog.Data.Concrete.EntityFramework.Exstensions;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.RequestParameters;
using ProgrammersBlog.Shared.Data.Concrete.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Data.Concrete.EntityFramework.Repositories
{
    public class EfCommentRepository:EfEntityRepositoryBase<Comment>, ICommentRepository
    {
        public EfCommentRepository(DbContext context):base(context)
        {
            
        }

        public async Task<IList<Comment>> GetAllByRequestParametersAsync(CommentRequestParameters requestParameters, Expression<Func<Comment, bool>> expression)
        {
            var query = _context.Set<Comment>();
            var result=  query.FilteredByArticleId(requestParameters.ArticleId).ToPaginate(requestParameters.PageNumber,requestParameters.PageSize);
            if (expression != null )
            {
                return await result.Where(expression).ToListAsync();
            }
            else
            {
                return await result.ToListAsync();
            }
        }
    }
}
