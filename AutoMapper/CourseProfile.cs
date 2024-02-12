using AutoMapper;
using System.Text.RegularExpressions;
using UniBook.DTOs.Group;

namespace UniBook.AutoMapper
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<Group, GroupGetDto>().ReverseMap();
            CreateMap<GroupPostDto, Group>().ReverseMap();
            CreateMap<GroupPutDto, Group>().ReverseMap();
        }
    }
}
