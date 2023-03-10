using AutoMapper;
using AutoMapper.Configuration.Conventions;
using Project.AndroidIosApp.Dtos.ProjectUser;
using Project.AndroidIosApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Business.Mapping.AutoMapper
{
    public class ProjectUserProfile : Profile
    {
        public ProjectUserProfile()
        {
            CreateMap<ProjectUser,CreateProjectUserDto>().ReverseMap();
            CreateMap<ProjectUser,UpdateProjectUserDto>().ReverseMap();
            CreateMap<ProjectUser,GetProjectUserDto>().ReverseMap();
            CreateMap<ProjectUser,LoginProjectUserDto>().ReverseMap();
        }
    }
}
