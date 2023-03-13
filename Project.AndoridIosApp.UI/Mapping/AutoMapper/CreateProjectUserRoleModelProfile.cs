using AutoMapper;
using Project.AndoridIosApp.UI.Areas.Admin.Models;
using Project.AndroidIosApp.Dtos.CommentDtos;
using Project.AndroidIosApp.Dtos.ProjectUserRoleDto;

namespace Project.AndoridIosApp.UI.Mapping.AutoMapper
{
    public class CreateProjectUserRoleModelProfile : Profile
    {
        public CreateProjectUserRoleModelProfile()
        {
            CreateMap<CreateProjectUserRoleModel, CreateProjectUserRoleDto>().ReverseMap();
        }
    }
}
