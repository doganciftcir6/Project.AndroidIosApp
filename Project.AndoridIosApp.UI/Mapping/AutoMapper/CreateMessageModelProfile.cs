using AutoMapper;
using Project.AndoridIosApp.UI.Areas.Admin.Models;
using Project.AndroidIosApp.Dtos.CommentDtos;
using Project.AndroidIosApp.Dtos.SupportDtos;

namespace Project.AndoridIosApp.UI.Mapping.AutoMapper
{
    public class CreateMessageModelProfile : Profile
    {
        public CreateMessageModelProfile()
        {
            CreateMap<CreateMessageModel, CreateSupportDto>().ReverseMap();
        }
    }
}
