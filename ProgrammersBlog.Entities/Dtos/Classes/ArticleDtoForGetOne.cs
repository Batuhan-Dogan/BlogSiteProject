using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos.Records;
using ProgrammersBlog.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Entities.Dtos.Classes
{
    public class ArticleDtoForGetOne : DtoGetBase
    {
        public Article Article { get; set; }
    }
}
