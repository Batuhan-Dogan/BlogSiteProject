using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.RequestParameters;
using ProgrammersBlog.Shared.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Data.Abstract
{
    public interface IArticleRepository:IEntityRepository<Article> 
    {
        Task<IList<Article>> GetAllSelectedMembersByRequestParameters(ArticleRequestParameters requestParameters, Expression<Func<Article, Article>> selector,
            bool isActive);

        Task<int> GetAllSelectedMembersCount(ArticleRequestParameters requestParameters);
    }
}
