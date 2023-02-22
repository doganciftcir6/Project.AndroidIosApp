using AutoMapper;
using FluentValidation;
using Project.AndroidIosApp.Business.Abstract.Services;
using Project.AndroidIosApp.Business.Concrete.Managers.Constans;
using Project.AndroidIosApp.Business.Extensions;
using Project.AndroidIosApp.Core.Enums;
using Project.AndroidIosApp.Core.Utilities.Results.Concrete;
using Project.AndroidIosApp.Core.Utilities.Results.Interface;
using Project.AndroidIosApp.DataAccess.UnitOfWork;
using Project.AndroidIosApp.Dtos.BlogDtos;
using Project.AndroidIosApp.Dtos.ProjectRole;
using Project.AndroidIosApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Business.Concrete.Managers
{
    public class ProjectRoleManager : IProjectRoleService
    {
        private readonly IUow _uow;
        private readonly IValidator<CreateProjectRoleDto> _createProjectRoleValidator;
        private readonly IValidator<UpdateProjectRoleDto> _updateProjectRoleValidator;
        private readonly IMapper _mapper;

        public ProjectRoleManager(IUow uow, IValidator<CreateProjectRoleDto> createProjectRoleValidator, IValidator<UpdateProjectRoleDto> updateProjectRoleValidator, IMapper mapper)
        {
            _uow = uow;
            _createProjectRoleValidator = createProjectRoleValidator;
            _updateProjectRoleValidator = updateProjectRoleValidator;
            _mapper = mapper;
        }

        public async Task<IResponse> DeleteAsync(int id)
        {
            var data = await _uow.GetRepository<ProjectRole>().GetByIdAsync(id);
            if(data != null)
            {
                _uow.GetRepository<ProjectRole>().Delete(data);
                await _uow.SaveChangesAsync();
                return new Response(ResponseType.Success, ProjectRoleMessages.DeletedProjectRole);
            }
            else
            {
                return new Response(ResponseType.NotFound, ProjectRoleMessages.NotDeletedProjectRole);
            }
        }

        public async Task<IDataResponse<List<GetProjectRoleDto>>> GetAllAsync()
        {
            var data = await _uow.GetRepository<ProjectRole>().GetAllAsync();
            var dto = new List<GetProjectRoleDto>();
            foreach (var item in data)
            {
                dto.Add(new GetProjectRoleDto()
                {
                    Id = item.Id,
                    Definition = item.Definition,
                });
            }
            return new DataResponse<List<GetProjectRoleDto>>(ResponseType.Success, dto);
        }

        public async Task<IDataResponse<IDto>> GetByIdAsync<IDto>(int id)
        {
            //var data = await _uow.GetRepository<ProjectRole>().GetByFilterAsync(x => x.Id == id);
            //var dto = new GetProjectRoleDto()
            //{
            //    Id = data.Id,
            //    Definition = data.Definition,
            //};
            //return dto;
            var data = _mapper.Map<IDto>(await _uow.GetRepository<ProjectRole>().GetByFilterAsync(x => x.Id == id));
            if(data != null)
            {
                return new DataResponse<IDto>(ResponseType.Success,data);
            }
            else
            {
                return new DataResponse<IDto>(ResponseType.NotFound, $"{id} {ProjectRoleMessages.NotFoundIdProjectRole}");
            }
        }

        public async Task<IDataResponse<CreateProjectRoleDto>> InsertAsync(CreateProjectRoleDto createProjectRoleDto)
        {
            var validationResult = _createProjectRoleValidator.Validate(createProjectRoleDto);
            if (validationResult.IsValid)
            {
                var data = new ProjectRole()
                {
                    Definition = createProjectRoleDto.Definition,
                };
                await _uow.GetRepository<ProjectRole>().InsertAsync(data);
                await _uow.SaveChangesAsync();
                return new DataResponse<CreateProjectRoleDto>(ResponseType.Success, createProjectRoleDto);
            }
            else
            {
                return new DataResponse<CreateProjectRoleDto>(ResponseType.ValidationError, createProjectRoleDto, validationResult.ConverToCustomValidationError());
            }
        }

        public async Task<IDataResponse<UpdateProjectRoleDto>> UpdateAsync(UpdateProjectRoleDto updateProjectRoleDto)
        {
            var validationResult = _updateProjectRoleValidator.Validate(updateProjectRoleDto);
            if (validationResult.IsValid)
            {
                var unChangedData = await _uow.GetRepository<ProjectRole>().GetByIdAsync(updateProjectRoleDto.Id);
                if(unChangedData != null)
                {
                    _uow.GetRepository<ProjectRole>().Update(new ProjectRole()
                    {
                        Id = updateProjectRoleDto.Id,
                        Definition = updateProjectRoleDto.Definition,
                    }, unChangedData);
                    await _uow.SaveChangesAsync();
                    return new DataResponse<UpdateProjectRoleDto>(ResponseType.Success,updateProjectRoleDto);
                }
                else
                {
                    return new DataResponse<UpdateProjectRoleDto>(ResponseType.NotFound, ProjectRoleMessages.NotFoundProjectRole);
                }
            }
            else
            {
                return new DataResponse<UpdateProjectRoleDto>(ResponseType.ValidationError, updateProjectRoleDto, validationResult.ConverToCustomValidationError());
            }
        }
    }
}
