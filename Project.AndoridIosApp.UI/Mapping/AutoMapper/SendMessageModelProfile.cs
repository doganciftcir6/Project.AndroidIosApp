using AutoMapper;
using Project.AndoridIosApp.UI.Models;
using Project.AndroidIosApp.Dtos.ProjectUser;
using Project.AndroidIosApp.Dtos.SupportDtos;

namespace Project.AndoridIosApp.UI.Mapping.AutoMapper
{
    public class SendMessageModelProfile : Profile
    {
        public SendMessageModelProfile()
        {
            CreateMap<SendMessageModel, CreateSupportDto>().ReverseMap();
        }
    }
}
