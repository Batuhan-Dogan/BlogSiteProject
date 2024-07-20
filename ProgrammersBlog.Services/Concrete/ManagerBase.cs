using AutoMapper;
using ProgrammersBlog.Data.Abstract;
using ProgrammersBlog.Data.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Services.Concrete
{
    public class ManagerBase
    {
        protected IUnitOfWork UnitOfWork { get;}
        protected IMapper Mapper { get;}

        public ManagerBase(IMapper mapper,IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }
    }
}
