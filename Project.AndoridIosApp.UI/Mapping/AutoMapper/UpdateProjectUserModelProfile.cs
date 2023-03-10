using AutoMapper;
using Project.AndoridIosApp.UI.Areas.Admin.Models;
using Project.AndoridIosApp.UI.Models;
using Project.AndroidIosApp.Dtos.ProjectUser;

namespace Project.AndoridIosApp.UI.Mapping.AutoMapper
{
    public class UpdateProjectUserModelProfile : Profile
    {
        public UpdateProjectUserModelProfile()
        {
            CreateMap<UpdateProjectUserModel, UpdateProjectUserDto>().ReverseMap();
        }
    }
}
