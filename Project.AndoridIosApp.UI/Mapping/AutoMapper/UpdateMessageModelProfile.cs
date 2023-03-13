using AutoMapper;
using Project.AndoridIosApp.UI.Areas.Admin.Models;
using Project.AndroidIosApp.Dtos.SupportDtos;

namespace Project.AndoridIosApp.UI.Mapping.AutoMapper
{
    public class UpdateMessageModelProfile : Profile
    {
        public UpdateMessageModelProfile()
        {
            CreateMap<UpdateMessageModel, UpdateSupportDto>().ReverseMap();
        }
    }
}
