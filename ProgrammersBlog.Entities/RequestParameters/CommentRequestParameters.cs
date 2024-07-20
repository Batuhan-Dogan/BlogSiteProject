using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Entities.RequestParameters
{
    public class CommentRequestParameters:RequestParameters
    {
        public int? ArticleId { get; set; }
        public CommentRequestParameters(int pageNumber,int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
        public CommentRequestParameters():this(1,12)
        {
                
        }
    }
}
