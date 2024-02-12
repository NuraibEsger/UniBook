using AutoMapper;
using UniBook.DTOs.Department.cs;
using UniBook.Entities;

namespace UniBook.AutoMapper
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department, DepartmentGetDto>().ReverseMap();
            CreateMap<DepartmentPostDto, Department>().ReverseMap();
            CreateMap<DepartmentPutDto, Department>().ReverseMap();
        }
    }
}
