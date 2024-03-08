using AutoMapper;
using UniBook.DTOs.Exam;
using UniBook.Entities;

namespace UniBook.AutoMapper
{
    public class ExamProfile : Profile
    {
        public ExamProfile()
        {
            CreateMap<Exam, ExamGetDto>().ReverseMap();
            CreateMap<ExamPostDto, Exam>().ReverseMap();
            CreateMap<ExamPutDto, Exam>().ReverseMap();
        }
    }
}
