using AutoMapper;
using Project.AndroidIosApp.Dtos.SupportUserSupportDtos;
using Project.AndroidIosApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Business.Mapping.AutoMapper
{
    public class SupportUserSupportProfile : Profile
    {
        public SupportUserSupportProfile()
        {
            CreateMap<SupportUserSupport,CreateSupportUserSupportDto>().ReverseMap();
            CreateMap<SupportUserSupport,UpdateSupportUserSupportDto>().ReverseMap();
            CreateMap<SupportUserSupport,GetSupportUserSupportDto>().ReverseMap();
        }
    }
}
