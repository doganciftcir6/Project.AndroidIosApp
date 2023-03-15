using AutoMapper;
using Project.AndoridIosApp.UI.Areas.Admin.Models;
using Project.AndroidIosApp.Dtos.BlogCommentDtos;
using Project.AndroidIosApp.Dtos.DeviceDtos;

namespace Project.AndoridIosApp.UI.Mapping.AutoMapper
{
    public class CreateDeviceModelProfile : Profile
    {
        public CreateDeviceModelProfile()
        {
            CreateMap<CreateDeviceModel, CreateDeviceDto>().ReverseMap();
        }
    }
}
