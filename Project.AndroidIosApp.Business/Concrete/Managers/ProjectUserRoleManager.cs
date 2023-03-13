using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.AndroidIosApp.Business.Abstract.Services;
using Project.AndroidIosApp.Business.Concrete.Managers.Constans;
using Project.AndroidIosApp.Business.ValidationRules.FluentValidation;
using Project.AndroidIosApp.Core.Enums;
using Project.AndroidIosApp.Core.Utilities.Results.Concrete;
using Project.AndroidIosApp.Core.Utilities.Results.Interface;
using Project.AndroidIosApp.DataAccess.UnitOfWork;
using Project.AndroidIosApp.Dtos.BlogDtos;
using Project.AndroidIosApp.Dtos.OSDtos;
using Project.AndroidIosApp.Dtos.ProjectUserRoleDto;
using Project.AndroidIosApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Business.Concrete.Managers
{
    public class ProjectUserRoleManager : IProjectUserRoleService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        public ProjectUserRoleManager(IUow uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IResponse> DeleteAsync(int id)
        {
            var data = await _uow.GetRepository<ProjectUserRole>().GetByIdAsync(id);
            if (data != null)
            {
                _uow.GetRepository<ProjectUserRole>().Delete(data);
                await _uow.SaveChangesAsync();
                return new Response(Core.Enums.ResponseType.Success, ProjectUserRoleMessages.DeletedProjectUserRole);
            }
            else
            {
                return new Response(ResponseType.NotFound, ProjectUserRoleMessages.NotDeletedProjectUserRole);
            }
        }

        public async Task<IDataResponse<List<GetProjectUserRoleDto>>> GetAllAsync()
        {
            var entity = await _uow.GetRepository<ProjectUserRole>().GetAllAsync();
            var dto = new List<GetProjectUserRoleDto>();
            foreach (var item in entity)
            {
                dto.Add(new GetProjectUserRoleDto()
                {
                    Id = item.Id,
                    ProjectUserId = item.ProjectUserId,
                    ProjectRoleId = item.ProjectRoleId,
                });
            }
            return new DataResponse<List<GetProjectUserRoleDto>>(ResponseType.Success, dto);
        }

        public async Task<IDataResponse<List<GetProjectUserRoleDto>>> GetAllWithProjectUserAndRoleAsync()
        {
            var query = _uow.GetRepository<ProjectUserRole>().GetQuery();
            var data = await query.Include(x => x.ProjectUser).Include(x => x.ProjectRole).ToListAsync();
            var mappingData = _mapper.Map<List<GetProjectUserRoleDto>>(data);
            return new DataResponse<List<GetProjectUserRoleDto>>(ResponseType.Success, mappingData);
        }

        public async Task<IDataResponse<IDto>> GetByIdAsync<IDto>(int id)
        {
            //var data = await _uow.GetRepository<ProjectUserRole>().GetByFilterAsync(x => x.Id == id);
            //var dto = new GetProjectUserRoleDto()
            //{
            //    Id = data.Id,
            //    ProjectUserId = data.ProjectUserId,
            //    ProjectRoleId = data.ProjectRoleId,
            //};
            //return dto;
            var data = _mapper.Map<IDto>(await _uow.GetRepository<ProjectUserRole>().GetByFilterAsync(x => x.Id == id));
            if (data != null)
            {
                return new DataResponse<IDto>(ResponseType.Success, data);
            }
            else
            {
                return new DataResponse<IDto>(ResponseType.NotFound, $"{id} {ProjectUserRoleMessages.NotFoundIdProjectUserRole}");
            }
        }

        public async Task<IDataResponse<CreateProjectUserRoleDto>> InsertAsync(CreateProjectUserRoleDto createProjectUserRoleDto)
        {
            var entity = new ProjectUserRole()
            {
                ProjectUserId = createProjectUserRoleDto.ProjectUserId,
                ProjectRoleId = createProjectUserRoleDto.ProjectRoleId,
            };
            await _uow.GetRepository<ProjectUserRole>().InsertAsync(entity);
            await _uow.SaveChangesAsync();
            return new DataResponse<CreateProjectUserRoleDto>(ResponseType.Success, createProjectUserRoleDto);
        }

        public async Task<IDataResponse<UpdateProjectUserRoleDto>> UpdateAsync(UpdateProjectUserRoleDto updateProjectUserRoleDto)
        {
            var updatedEntity = await _uow.GetRepository<ProjectUserRole>().GetByIdAsync(updateProjectUserRoleDto.Id);
            if (updatedEntity != null)
            {
                _uow.GetRepository<ProjectUserRole>().Update(new()
                {
                    Id = updatedEntity.Id,
                    ProjectUserId = updateProjectUserRoleDto.ProjectUserId,
                    ProjectRoleId = updateProjectUserRoleDto.ProjectRoleId,
                }, updatedEntity);
                await _uow.SaveChangesAsync();
                return new DataResponse<UpdateProjectUserRoleDto>(ResponseType.Success, updateProjectUserRoleDto);
            }
            return new DataResponse<UpdateProjectUserRoleDto>(ResponseType.NotFound, BlogMessages.NotFoundBlog);
        }
    }
}
