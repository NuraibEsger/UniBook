using AutoMapper;
using UniBook.DTOs.Student;
using UniBook.DTOs.Teacher;
using UniBook.Entities;

namespace UniBook.AutoMapper
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<AppUser, UserGetDto>().ReverseMap();
        }
    }
}
