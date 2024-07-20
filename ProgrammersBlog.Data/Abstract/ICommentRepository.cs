using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.RequestParameters;
using ProgrammersBlog.Shared.Data.Abstract;
using ProgrammersBlog.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Data.Abstract
{
    public interface ICommentRepository:IEntityRepository<Comment>
    {
        Task<IList<Comment>>GetAllByRequestParametersAsync(CommentRequestParameters requestParameters, Expression<Func<Comment, bool>> expression);
    }
}
