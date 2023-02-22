using AutoMapper;
using Project.AndroidIosApp.Dtos.CommentDtos;
using Project.AndroidIosApp.Dtos.DeviceDtos;
using Project.AndroidIosApp.Dtos.ProjectUser;
using Project.AndroidIosApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Business.Mapping.AutoMapper
{
    public class DeviceProfile : Profile
    {
        public DeviceProfile()
        {
            CreateMap<Device, GetDeviceDto>().ReverseMap();
            CreateMap<Device, CreateDeviceDto>().ReverseMap();
            CreateMap<Device, UpdateDeviceDto>().ReverseMap();
        }
    }
}
