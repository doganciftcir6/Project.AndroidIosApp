using AutoMapper;
using Project.AndroidIosApp.Dtos.DeviceDtos;
using Project.AndroidIosApp.Dtos.DeviceTypeDtos;
using Project.AndroidIosApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Business.Mapping.AutoMapper
{
    public class DeviceTypeProfile : Profile
    {
        public DeviceTypeProfile()
        {
            CreateMap<DeviceType, GetDeviceTypeDto>().ReverseMap();
            CreateMap<DeviceType, CreateDeviceTypeDto>().ReverseMap();
            CreateMap<DeviceType, UpdateDeviceTypeDto>().ReverseMap();
        }
    }
}
