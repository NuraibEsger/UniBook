using AutoMapper;
using UniBook.DTOs.Teacher;
using UniBook.Entities;

namespace UniBook.AutoMapper
{
    public class TeacherProfile : Profile
    {
        public TeacherProfile()
        {
            CreateMap<AppUser, TeacherGetDto>().ReverseMap();
            CreateMap<TeacherPutDto, AppUser>().ReverseMap();
            CreateMap<AppUser, TeacherSubjectPostDto>().ReverseMap();
        }
    }
}
