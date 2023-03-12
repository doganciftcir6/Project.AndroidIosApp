using AutoMapper;
using Project.AndoridIosApp.UI.Areas.Admin.Models;
using Project.AndroidIosApp.Dtos.CommentDtos;

namespace Project.AndoridIosApp.UI.Mapping.AutoMapper
{
    public class UpdateDeviceCommentModelProfile : Profile
    {
        public UpdateDeviceCommentModelProfile()
        {
            CreateMap<UpdateDeviceCommentModel, UpdateCommentDto>().ReverseMap();
        }
    }
}
