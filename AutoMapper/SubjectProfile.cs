using AutoMapper;
using UniBook.DTOs.Subject;
using UniBook.Entities;

namespace UniBook.AutoMapper
{
    public class SubjectProfile : Profile
    {
        public SubjectProfile()
        {
            CreateMap<Subject, SubjectGetDto>().ReverseMap();
            CreateMap<SubjectPostDto, Subject>().ReverseMap();
            CreateMap<SubjectPutDto, Subject>().ReverseMap();
        }
    }
}
