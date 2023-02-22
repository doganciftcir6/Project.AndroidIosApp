using AutoMapper;
using Project.AndroidIosApp.Dtos.ProjectRole;
using Project.AndroidIosApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Business.Mapping.AutoMapper
{
    public class ProjectRoleProfile : Profile
    {
        public ProjectRoleProfile()
        {
            CreateMap<ProjectRole,CreateProjectRoleDto>().ReverseMap();
            CreateMap<ProjectRole,UpdateProjectRoleDto>().ReverseMap();
            CreateMap<ProjectRole,GetProjectRoleDto>().ReverseMap();
        }
    }
}
