using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos.Classes;
using ProgrammersBlog.Mvc.Models;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class ArticleListViewModel
    {
        public ArticleListDto Articles { get; set; } = null;
        public Pagination Pagination { get; set; } = new();
        public int TotalCount => Articles.Articles.Count();
    }
}
