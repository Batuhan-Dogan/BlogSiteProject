using AutoMapper;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Mvc.AutoMapper.Profiles
{
    public class CategoryProfile:Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryAddDto, Category>().ForMember(dest=>dest.CreatedTime,opt=>opt.MapFrom(x=>DateTime.Now.ToUniversalTime()));
            CreateMap<CategoryUpdateDto, Category>().ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(x => DateTime.Now.ToUniversalTime())); 
        }
    }
}
