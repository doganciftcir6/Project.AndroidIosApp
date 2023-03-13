using AutoMapper;
using Project.AndoridIosApp.UI.Areas.Admin.Models;
using Project.AndroidIosApp.Dtos.CommentDtos;
using Project.AndroidIosApp.Dtos.ProjectUserRoleDto;

namespace Project.AndoridIosApp.UI.Mapping.AutoMapper
{
    public class UpdateProjectUserRoleModelProfile : Profile
    {
        public UpdateProjectUserRoleModelProfile()
        {
            CreateMap<UpdateProjectUserRoleModel, UpdateProjectUserRoleDto>().ReverseMap();
        }
    }
}
