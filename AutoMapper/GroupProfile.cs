using AutoMapper;
using UniBook.DTOs.Group;
using UniBook.Entities;

namespace UniBook.AutoMapper
{
    public class GroupProfile : Profile
    {
        public GroupProfile()
        {
            CreateMap<Group, GroupGetDto>().ReverseMap();
            CreateMap<GroupPostDto, Group>().ReverseMap();
            CreateMap<GroupPutDto, Group>().ReverseMap();
        }
    }
}
