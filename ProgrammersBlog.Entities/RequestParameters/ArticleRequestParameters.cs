using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Entities.RequestParameters
{
    public class ArticleRequestParameters:RequestParameters
    {
        public int? CategoryId { get; set; }

        public ArticleRequestParameters(int pageNumber , int pageSize)
        {
            PageNumber = pageNumber;
            PageSize=pageSize;
        }
        public ArticleRequestParameters():this(1,10)
        {
                
        }
    }
}
