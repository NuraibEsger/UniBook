using AutoMapper;
using UniBook.DTOs.Student;
using UniBook.Entities;

namespace UniBook.AutoMapper
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<Student, StudentGetDto>().ReverseMap();
            CreateMap<StudentPostDto, Student>().ReverseMap();
            CreateMap<StudentPutDto, Student>().ReverseMap();
        }
    }
}
