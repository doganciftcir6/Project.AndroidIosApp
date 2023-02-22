using AutoMapper;
using Project.AndroidIosApp.Dtos.SupportDtos;
using Project.AndroidIosApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Business.Mapping.AutoMapper
{
    public class SupportUserProfile : Profile
    {
        public SupportUserProfile()
        {
            CreateMap<SupportUser, CreateSupportDto>().ReverseMap();
            CreateMap<SupportUser, UpdateSupportDto>().ReverseMap();
            CreateMap<SupportUser, GetSupportDto>().ReverseMap();
        }
    }
}
