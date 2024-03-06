using AutoMapper;
using UniBook.DTOs.Semester;
using UniBook.Entities;

namespace UniBook.AutoMapper
{
    public class SemesterProfile : Profile
    {
        public SemesterProfile()
        {
            CreateMap<Semester, SemesterGetDto>().ReverseMap();
        }
    }
}
