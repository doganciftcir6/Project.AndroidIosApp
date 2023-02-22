using AutoMapper;
using Project.AndroidIosApp.Dtos.GenderDto;
using Project.AndroidIosApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Business.Mapping.AutoMapper
{
    public class GenderProfile : Profile
    {
        public GenderProfile()
        {
            CreateMap<Gender,CreateGenderDto>().ReverseMap();
            CreateMap<Gender,UpdateGenderDto>().ReverseMap();
            CreateMap<Gender,GetGenderDto>().ReverseMap();
        }
    }
}
