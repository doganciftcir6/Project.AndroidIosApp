using AutoMapper;
using Project.AndroidIosApp.Dtos.ProjectUserRoleDto;
using Project.AndroidIosApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Business.Mapping.AutoMapper
{
    public class ProjectUserRoleProfile : Profile
    {
        public ProjectUserRoleProfile()
        {
            CreateMap<ProjectUserRole,CreateProjectUserRoleDto>().ReverseMap();
            CreateMap<ProjectUserRole,UpdateProjectUserRoleDto>().ReverseMap();
            CreateMap<ProjectUserRole,GetProjectUserRoleDto>().ReverseMap();
        }
    }
}
