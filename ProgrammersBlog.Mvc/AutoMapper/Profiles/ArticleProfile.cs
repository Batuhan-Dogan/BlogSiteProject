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
    public class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
            CreateMap<ArticleAddDto, Article>().ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(X => DateTime.Now.ToUniversalTime()));
            CreateMap<ArticleUpdateDto, Article>().ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(X => DateTime.Now.ToUniversalTime()))
                .ForMember(dest => dest.ModifiedTime, opt => opt.MapFrom(X => DateTime.Now.ToUniversalTime())); ;

        }
    }
}
