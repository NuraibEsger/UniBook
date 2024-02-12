using AutoMapper;
using System.Text.RegularExpressions;
using UniBook.DTOs.Group;

namespace UniBook.AutoMapper
{
    public class GroupProfile : Profile
    {
        public GroupProfile()
        {
            CreateMap<Group, GroupGetDto>().ReverseMap();
        }
    }
}
