using AutoMapper;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos.Records;

namespace ProgrammersBlog.Mvc.AutoMapper.Profiles
{
    public class CommentProfile:Profile
    {
        public CommentProfile()
        {
            CreateMap<CommentAddDto, Comment>().ForMember(dest => dest.ModifiedByName, opt => opt.MapFrom(x => x.CreatedByName))
                .ForMember(dest=>dest.IsActive,opt=>opt.MapFrom(x=> true));
            CreateMap<CommentUpdateDto, Comment>().ReverseMap();
        }
    }
}
