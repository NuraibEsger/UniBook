using AutoMapper;
using UniBook.DTOs.Student;
using UniBook.DTOs.Teacher;
using UniBook.DTOs.UserGroup;
using UniBook.Entities;

namespace UniBook.AutoMapper
{
    public class UserGroupProfile : Profile
    {
        public UserGroupProfile()
        {
            CreateMap<UserGroup, UserGroupGetDto>().ReverseMap();
            CreateMap<UserGroupPostDto, UserGroup>().ReverseMap();
            CreateMap<UserGroupPutDto, UserGroup>().ReverseMap();
        }
    }
}
