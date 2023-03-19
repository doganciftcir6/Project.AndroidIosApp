using AutoMapper;
using Project.AndoridIosApp.UI.Models;
using Project.AndroidIosApp.Dtos.ProjectUser;

namespace Project.AndoridIosApp.UI.Mapping.AutoMapper
{
    public class UserUpdateModelProfile : Profile
    {
        public UserUpdateModelProfile()
        {
            CreateMap<UserUpdateModel, UpdateProjectUserDto>().ReverseMap();
        }
    }
}
