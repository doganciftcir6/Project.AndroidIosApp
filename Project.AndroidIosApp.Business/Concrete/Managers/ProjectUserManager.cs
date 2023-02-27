using AutoMapper;
using FluentValidation;
using Project.AndroidIosApp.Business.Abstract.Services;
using Project.AndroidIosApp.Business.Concrete.Managers.Constans;
using Project.AndroidIosApp.Business.Extensions;
using Project.AndroidIosApp.Business.Helpers;
using Project.AndroidIosApp.Core.Enums;
using Project.AndroidIosApp.Core.Utilities.Results.Concrete;
using Project.AndroidIosApp.Core.Utilities.Results.Interface;
using Project.AndroidIosApp.DataAccess.UnitOfWork;
using Project.AndroidIosApp.Dtos.BlogDtos;
using Project.AndroidIosApp.Dtos.ProjectRole;
using Project.AndroidIosApp.Dtos.ProjectUser;
using Project.AndroidIosApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Business.Concrete.Managers
{
    public class ProjectUserManager : IProjectUserService
    {
        private readonly IUow _uow;
        private readonly IValidator<CreateProjectUserDto> _createProjectUserValidator;
        private readonly IValidator<UpdateProjectUserDto> _updateProjectUserValidator;
        private readonly IValidator<LoginProjectUserDto> _loginProjectUserValidator;
        private readonly IMapper _mapper;
        public ProjectUserManager(IUow uow, IValidator<CreateProjectUserDto> createProjectUserValidator, IValidator<UpdateProjectUserDto> updateProjectUserValidator, IMapper mapper, IValidator<LoginProjectUserDto> loginProjectUserValidator)
        {
            _uow = uow;
            _createProjectUserValidator = createProjectUserValidator;
            _updateProjectUserValidator = updateProjectUserValidator;
            _mapper = mapper;
            _loginProjectUserValidator = loginProjectUserValidator;
        }

        public async Task<IResponse> DeleteAsync(int id)
        {
            var data = await _uow.GetRepository<ProjectUser>().GetByIdAsync(id);
            if(data != null)
            {
                _uow.GetRepository<ProjectUser>().Delete(data);
                await _uow.SaveChangesAsync();
                return new Response(ResponseType.Success, ProjectUserMessages.DeletedProjectUser);
            }
            else
            {
                return new Response(ResponseType.NotFound, ProjectUserMessages.NotDeletedProjectUser);
            }
        }
        public async Task<IDataResponse<List<GetProjectUserDto>>> GetAllAsync()
        {
            var data = await _uow.GetRepository<ProjectUser>().GetAllAsync();
            var dto = new List<GetProjectUserDto>();
            foreach (var item in data)
            {
                dto.Add(new GetProjectUserDto()
                {
                    Id = item.Id,
                    Username = item.Username,
                    Firstname = item.Firstname,
                    Lastname = item.Lastname,
                    Password = item.Password,
                    PasswordVerify = item.PasswordVerify,
                    PhoneNumber = item.PhoneNumber,
                    Email = item.Email,
                    ImageUrl = item.ImageUrl,
                    GenderId = item.GenderId,
                });
            }
            return new DataResponse<List<GetProjectUserDto>>(ResponseType.Success, dto);
        }

        public async Task<IDataResponse<IDto>> GetByIdAsync<IDto>(int id)
        {
            //var data = await _uow.GetRepository<ProjectUser>().GetByFilterAsync(x => x.Id == id);
            //var dto = new GetProjectUserDto()
            //{
            //    Id = data.Id,
            //    Username = data.Username,
            //    Firstname = data.Firstname,
            //    Lastname = data.Lastname,
            //    Password = data.Password,
            //    PasswordVerify = data.PasswordVerify,
            //    PhoneNumber = data.PhoneNumber,
            //    Email = data.Email,
            //    ImageUrl = data.ImageUrl,
            //    GenderId = data.GenderId,
            //};
            //return dto;
            var data = _mapper.Map<IDto>(await _uow.GetRepository<ProjectUser>().GetByFilterAsync(x => x.Id == id));
            if(data != null)
            {
                return new DataResponse<IDto>(ResponseType.Success, data);
            }
            else
            {
                return new DataResponse<IDto>(ResponseType.NotFound, $"{id} {ProjectUserMessages.NotFoundIdProjectUser}");
            }
        }
        public async Task<IDataResponse<GetProjectUserDto>> FindByUserNameAsync(string userName)
        {
            var data = _mapper.Map<GetProjectUserDto>(await _uow.GetRepository<ProjectUser>().GetByFilterAsync(x => x.Username == userName));
            if(data != null)
            {
                return new DataResponse<GetProjectUserDto>(ResponseType.Success, data);
            }
            return new DataResponse<GetProjectUserDto>(ResponseType.NotFound, $"{userName} {ProjectUserMessages.NotFoundUserNameProjectUser}");
        }
        public async Task<IDataResponse<GetProjectUserDto>> FindByEmailAsync(string email)
        {
            var data = _mapper.Map<GetProjectUserDto>(await _uow.GetRepository<ProjectUser>().GetByFilterAsync(x => x.Email == email));
            if(data != null)
            {
                return new DataResponse<GetProjectUserDto>(ResponseType.Success, data);
            }
            return new DataResponse<GetProjectUserDto>(ResponseType.NotFound, $"{email} {ProjectUserMessages.NotFoundEmailProjectUser}");
        }

        public async Task<IDataResponse<CreateProjectUserDto>> InsertAsync(CreateProjectUserDto createProjectUserDto)
        {
            var validationRule = _createProjectUserValidator.Validate(createProjectUserDto);
            if (validationRule.IsValid)
            {
                var entity = new ProjectUser()
                {
                    Username = createProjectUserDto.Username,
                    Firstname = createProjectUserDto.Firstname,
                    Lastname = createProjectUserDto.Lastname,
                    Password = createProjectUserDto.Password,
                    PasswordVerify = createProjectUserDto.PasswordVerify,
                    PhoneNumber = createProjectUserDto.PhoneNumber,
                    Email = createProjectUserDto.Email,
                    ImageUrl = createProjectUserDto.ImageUrl,
                    GenderId = createProjectUserDto.GenderId,
                };
                await _uow.GetRepository<ProjectUser>().InsertAsync(entity);
                await _uow.SaveChangesAsync();
                return new DataResponse<CreateProjectUserDto>(ResponseType.Success,createProjectUserDto);
            }
            else
            {
                return new DataResponse<CreateProjectUserDto>(ResponseType.ValidationError, createProjectUserDto, validationRule.ConverToCustomValidationError());
            } 
        }
        public async Task<IResponse> InsertWithRoleAsync(CreateProjectUserDto createProjectUserDto, int roleId)
        {
            var validationRule = _createProjectUserValidator.Validate(createProjectUserDto);
            if (validationRule.IsValid)
            {
                IResponse userInfChecks = RegisterRuleHelper.Run
                (
                    CheckUserNameExists(createProjectUserDto.Username),
                    CheckEmailExists(createProjectUserDto.Email)
                );
                if(userInfChecks.ResponseType != ResponseType.Success)
                {
                    return userInfChecks;
                }
                else
                {
                    var user = _mapper.Map<ProjectUser>(createProjectUserDto);
                    await _uow.GetRepository<ProjectUser>().InsertAsync(user);
                    await _uow.GetRepository<ProjectUserRole>().InsertAsync(new ProjectUserRole()
                    {
                        ProjectUser = user,
                        ProjectRoleId = roleId
                    });
                    await _uow.SaveChangesAsync();
                    return new Response(ResponseType.Success, $"{ProjectUserMessages.SuccessRegister}");
                }
            }
            return new Response(ResponseType.ValidationError, validationRule.ConverToCustomValidationError());
        }

        public async Task<IDataResponse<UpdateProjectUserDto>> UpdateAsync(UpdateProjectUserDto updateProjectUserDto)
        {
            var validationResult = _updateProjectUserValidator.Validate(updateProjectUserDto);
            if (validationResult.IsValid)
            {
                var unChangedData = await _uow.GetRepository<ProjectUser>().GetByIdAsync(updateProjectUserDto.Id);
                if(unChangedData != null)
                {
                    _uow.GetRepository<ProjectUser>().Update(new ProjectUser()
                    {
                        Id = updateProjectUserDto.Id,
                        Username = updateProjectUserDto.Username,
                        Firstname = updateProjectUserDto.Firstname,
                        Lastname = updateProjectUserDto.Lastname,
                        Password = updateProjectUserDto.Password,
                        PasswordVerify = updateProjectUserDto.PasswordVerify,
                        PhoneNumber = updateProjectUserDto.PhoneNumber,
                        Email = updateProjectUserDto.Email,
                        ImageUrl = updateProjectUserDto.ImageUrl,
                        GenderId = updateProjectUserDto.GenderId,
                    }, unChangedData);
                    await _uow.SaveChangesAsync();
                    return new DataResponse<UpdateProjectUserDto>(ResponseType.Success, updateProjectUserDto);
                }
                else
                {
                    return new DataResponse<UpdateProjectUserDto>(ResponseType.NotFound, ProjectUserMessages.NotFoundProjectUser);
                }              
            }
            else
            {
                return new DataResponse<UpdateProjectUserDto>(ResponseType.ValidationError, updateProjectUserDto, validationResult.ConverToCustomValidationError());
            }
        }
        
        public async Task<IDataResponse<GetProjectUserDto>> CheckUserAsync(LoginProjectUserDto loginProjectUserDto)
        {
            var validationResult = _loginProjectUserValidator.Validate(loginProjectUserDto);
            if (validationResult.IsValid)
            {
                var user = await _uow.GetRepository<ProjectUser>().GetByFilterAsync(x => x.Username == loginProjectUserDto.Username && x.Password == loginProjectUserDto.Password);
                if (user != null)
                {
                    var projectUserDto = _mapper.Map<GetProjectUserDto>(user);
                    return new DataResponse<GetProjectUserDto>(ResponseType.Success, projectUserDto);
                }
                return new DataResponse<GetProjectUserDto>(ResponseType.NotFound, $"{ProjectUserMessages.WrongUsernameOrPassword}");
            }
            return new DataResponse<GetProjectUserDto>(ResponseType.ValidationError, $"{ProjectUserMessages.NotNullUsernameOrPassword}");
        }

        public async Task<IDataResponse<List<GetProjectRoleDto>>> GetRolesByUserIdAsync(int userId)
        {
            var roles = await _uow.GetRepository<ProjectRole>().GetAllAsyncFilter(x => x.ProjectUserRoles.Any(x=> x.ProjectUserId == userId));
            if(roles == null)
            {
                return new DataResponse<List<GetProjectRoleDto>>(ResponseType.NotFound, $"{ProjectUserMessages.NotFoundRole}");
            }
            var dto = _mapper.Map<List<GetProjectRoleDto>>(roles);
            return new DataResponse<List<GetProjectRoleDto>>(ResponseType.Success, dto);
        }



        private IResponse CheckUserNameExists(string userName)
        {
            var query = _uow.GetRepository<ProjectUser>().GetQuery();
            var list = query.Where(x => x.Username == userName).ToList();
            if (list.Count > 0)
            {
                return new Response(ResponseType.Error,$"{ProjectUserMessages.RepeatUsername}");
            }
            return new Response(ResponseType.Success);
        }
        private IResponse CheckEmailExists(string email)
        {
            var query = _uow.GetRepository<ProjectUser>().GetQuery();
            var list = query.Where(x => x.Email == email).ToList();
            if (list.Count > 0)
            {
                return new Response(ResponseType.Error, $"{ProjectUserMessages.RepeatEmail}");
            }
            return new Response(ResponseType.Success);
        }

    }
}
