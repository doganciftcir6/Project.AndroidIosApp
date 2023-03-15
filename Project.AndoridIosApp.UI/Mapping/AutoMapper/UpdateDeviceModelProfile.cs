using AutoMapper;
using FluentValidation;
using Project.AndoridIosApp.UI.Areas.Admin.Models;
using Project.AndroidIosApp.Dtos.CommentDtos;
using Project.AndroidIosApp.Dtos.DeviceDtos;

namespace Project.AndoridIosApp.UI.Mapping.AutoMapper
{
    public class UpdateDeviceModelProfile : Profile
    {
        public UpdateDeviceModelProfile()
        {
            CreateMap<UpdateDeviceModel, UpdateDeviceDto>().ReverseMap();
        }
    }
}
