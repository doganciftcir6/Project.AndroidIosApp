using AutoMapper;
using Project.AndoridIosApp.UI.Models;
using Project.AndroidIosApp.Dtos.ProjectUser;

namespace Project.AndoridIosApp.UI.Mapping.AutoMapper
{
    public class UserCreateModelProfile : Profile
    {
        public UserCreateModelProfile()
        {
            CreateMap<UserCreateModel, CreateProjectUserDto>().ReverseMap();
        }
    }
}
