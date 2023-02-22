using AutoMapper;
using Project.AndroidIosApp.Dtos.BlogDtos;
using Project.AndroidIosApp.Dtos.OSDtos;
using Project.AndroidIosApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Business.Mapping.AutoMapper
{
    public class OSProfile : Profile
    {
        public OSProfile()
        {
            CreateMap<OS, CreateOSDto>().ReverseMap();
            CreateMap<OS, GetOSDto>().ReverseMap();
            CreateMap<OS, UpdateOSDto>().ReverseMap();
        }
    }
}
