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
using Project.AndroidIosApp.Dtos.SupportUserDtos;
using Project.AndroidIosApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Business.Concrete.Managers
{
    public class SupportUserManager : ISupportUserService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateSupportUserDto> _createSupportUserValidator;
        private readonly IValidator<UpdateSupportUserDto> _updateSupportUserValidator;

        public SupportUserManager(IUow uow, IMapper mapper, IValidator<CreateSupportUserDto> createSupportUserValidator, IValidator<UpdateSupportUserDto> updateSupportUserValidator)
        {
            _uow = uow;
            _mapper = mapper;
            _createSupportUserValidator = createSupportUserValidator;
            _updateSupportUserValidator = updateSupportUserValidator;
        }

        public async Task<IResponse> DeleteAsync(int id)
        {
            var data = await _uow.GetRepository<SupportUser>().GetByIdAsync(id);
            if(data != null)
            {
                _uow.GetRepository<SupportUser>().Delete(data);
                await _uow.SaveChangesAsync();
                return new Response(ResponseType.Success, SupportUserMessages.DeletedSupportUser);
            }
            else
            {
                return new Response(ResponseType.NotFound, SupportUserMessages.NotDeletedSupportUser);
            }
        }

        public async Task<IDataResponse<List<GetSupportUserDto>>> GetAllAsync()
        {
            var data = _mapper.Map<List<GetSupportUserDto>>(await _uow.GetRepository<SupportUser>().GetAllAsync());
            return new DataResponse<List<GetSupportUserDto>>(ResponseType.Success, data);
        }

        public async Task<IDataResponse<IDto>> GetByIdAsync<IDto>(int id)
        {
            var data = _mapper.Map<IDto>(await _uow.GetRepository<SupportUser>().GetByFilterAsync(x => x.Id == id));
            if(data != null)
            {
                return new DataResponse<IDto>(ResponseType.Success, data);
            }
            else
            {
                return new DataResponse<IDto>(ResponseType.NotFound, $"{id} {SupportUserMessages.NotFoundIdSupportUser}");
            }
        }

        public async Task<IDataResponse<CreateSupportUserDto>> InsertAsync(CreateSupportUserDto createSupportUserDto)
        {
            var validationResult = _createSupportUserValidator.Validate(createSupportUserDto);
            if (validationResult.IsValid)
            {
                await _uow.GetRepository<SupportUser>().InsertAsync(_mapper.Map<SupportUser>(createSupportUserDto));
                await _uow.SaveChangesAsync();
                return new DataResponse<CreateSupportUserDto>(ResponseType.Success, createSupportUserDto);
            }
            else
            {
                return new DataResponse<CreateSupportUserDto>(ResponseType.ValidationError, createSupportUserDto, validationResult.ConverToCustomValidationError());
            }
        }

        public async Task<IDataResponse<UpdateSupportUserDto>> UpdateAsync(UpdateSupportUserDto updateSupportUserDto)
        {
            var validationResult = _updateSupportUserValidator.Validate(updateSupportUserDto);
            if (validationResult.IsValid)
            {
                var unChangedData = await _uow.GetRepository<SupportUser>().GetByIdAsync(updateSupportUserDto.Id);
                if(unChangedData != null)
                {
                    _uow.GetRepository<SupportUser>().Update(_mapper.Map<SupportUser>(updateSupportUserDto), unChangedData);
                    await _uow.SaveChangesAsync();
                    return new DataResponse<UpdateSupportUserDto>(ResponseType.Success, updateSupportUserDto);
                }
                else
                {
                    return new DataResponse<UpdateSupportUserDto>(ResponseType.NotFound, SupportUserMessages.NotFoundSupportUser);
                }
            }
            else
            {
                return new DataResponse<UpdateSupportUserDto>(ResponseType.ValidationError, updateSupportUserDto, validationResult.ConverToCustomValidationError());
            }
        }
    }
}
