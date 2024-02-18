using AutoMapper;
using System.Text.RegularExpressions;
using UniBook.DTOs.Course;
using UniBook.DTOs.Group;
using UniBook.Entities;

namespace UniBook.AutoMapper
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<Course, CourseGetDto>().ReverseMap();
            CreateMap<CoursePostDto, Course>().ReverseMap();
            CreateMap<CoursePutDto, Course>().ReverseMap();
        }
    }
}
