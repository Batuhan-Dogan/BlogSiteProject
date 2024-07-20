using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Entities.RequestParameters
{
    public abstract class RequestParameters
    {
        public String? SearchTerm { get; set; }
        public string? OrderBy{get;set;}
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
