using AutoMapper;
using Project.AndroidIosApp.Dtos.ContactDtos;
using Project.AndroidIosApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Business.Mapping.AutoMapper
{
    public class ContactBlogProfile : Profile
    {
        public ContactBlogProfile()
        {
            CreateMap<Contact,GetContactDto>().ReverseMap();
            CreateMap<Contact,CreateContactDto>().ReverseMap();
            CreateMap<Contact,UpdateContactDto>().ReverseMap();
        }
    }
}
