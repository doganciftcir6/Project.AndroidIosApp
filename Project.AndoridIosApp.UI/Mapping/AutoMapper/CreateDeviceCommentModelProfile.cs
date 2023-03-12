using AutoMapper;
using Project.AndoridIosApp.UI.Areas.Admin.Models;
using Project.AndoridIosApp.UI.Models;
using Project.AndroidIosApp.Dtos.CommentDtos;
using Project.AndroidIosApp.Dtos.ProjectUser;

namespace Project.AndoridIosApp.UI.Mapping.AutoMapper
{
    public class CreateDeviceCommentModelProfile : Profile
    {
        public CreateDeviceCommentModelProfile()
        {
            CreateMap<CreateDeviceCommentModel, CreateCommentDto>().ReverseMap();
        }
    }
}
