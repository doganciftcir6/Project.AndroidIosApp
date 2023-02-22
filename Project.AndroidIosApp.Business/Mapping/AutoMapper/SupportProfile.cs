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
    public class SupportProfile : Profile
    {
        public SupportProfile()
        {
            CreateMap<Support,CreateSupportDto>().ReverseMap();
            CreateMap<Support,UpdateSupportDto>().ReverseMap();
            CreateMap<Support,GetSupportDto>().ReverseMap();
        }
    }
}
